﻿namespace X12.Core.Hipaa.Common
{
    public class Paperwork
    {
        public Lookup ReportType { get; set; }

        public Lookup ReportTransmission { get; set; }

        public Identification Identification { get; set; }
    }
}
