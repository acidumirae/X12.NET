﻿namespace X12.Core.Sql.IdentityProviders
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using X12.Core.Sql.Interfaces;

    /// <summary>
    /// Represents an Identity provider for long-based identifiers
    /// </summary>
    public class LongHiLowIdentityProvider : IIdentityProvider
    {
        private readonly IDictionary<string, Identity<long>> ids = new Dictionary<string, Identity<long>>();
        private readonly string dsn;
        private readonly string schema;
        private readonly int batchSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="LongHiLowIdentityProvider"/> class
        /// </summary>
        /// <param name="dsn">Data source name</param>
        /// <param name="schema">Database schema</param>
        /// <param name="batchSize">Size of batch for chunking</param>
        public LongHiLowIdentityProvider(string dsn, string schema, int batchSize)
        {
            this.dsn = dsn;
            this.schema = schema;
            this.batchSize = batchSize;
        }

        /// <summary>
        /// Validates the provider's schema and ensures a table exists
        /// </summary>
        public void EnsureSchema()
        {
            using (var conn = new SqlConnection(this.dsn))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql = string.Format(
                        @"if not EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[{0}].[HiLo]') AND type in (N'U'))
						begin
						  CREATE TABLE [{0}].HiLo (
							NextId bigint not null,
							[Table] varchar(100) not null
						  )
						end",
                        this.schema);

                    cmd.CommandText = sql;
                }
            }
        }

        /// <summary>
        /// Obtains the next identifier from the database and returns it
        /// </summary>
        /// <param name="schema">Schema of database to retrieve next id from</param>
        /// <param name="table">Table to get next id from</param>
        /// <returns>Next id obtained from database</returns>
        public object NextId(string schema, string table)
        {
            if (!this.ids.ContainsKey(table))
            {
                this.ids.Add(table, new Identity<long> { NextId = 0, MaxId = 0 });
            }

            Identity<long> id = this.ids[table];

            if (id != null && id.NextId < id.MaxId)
            {
                return id.NextId++;
            }

            var sql = 
                @"declare @table varchar(100)
				set @table = '[{1}].[{2}]'
				select @nextId = NextId from [{0}].HiLo with (updlock, rowlock) where lower([table]) = lower(@table)
				if isnull(@nextId, 0) = 0
				begin
					declare @maxId bigint
					select @maxId = isnull(max(Id), 0) + 1 from [{1}].[{2}]
					insert into [{0}].HiLo (NextId, [Table]) values (@maxId, @table)
					select @nextId = 1
				end
				update [{0}].HiLo set NextId = NextId + @batchSize where lower([table]) = lower(@table)";

            using (var conn = new SqlConnection(this.dsn))
            {
                conn.Open();

                using (var tx = conn.BeginTransaction(IsolationLevel.Serializable))
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.Parameters.Add("@nextId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@batchSize", SqlDbType.Int).Value = this.batchSize;
                    cmd.Parameters.Add("@table", SqlDbType.VarChar, 100).Value = table;
                    cmd.CommandText = string.Format(sql, this.schema);
                    cmd.ExecuteNonQuery();
                    id.NextId = Convert.ToInt64(cmd.Parameters["@nextId"].Value);
                    id.MaxId = id.NextId + this.batchSize;
                    tx.Commit();
                    return id.NextId++;
                }
            }
        }
    }
}
