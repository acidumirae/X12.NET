﻿namespace X12.Core.Transformations
{
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Represents a <see cref="Stream"/> factory for transformation resources
    /// </summary>
    public static class TransformationStreamFactory
    {
        /// <summary>
        /// Creates a <see cref="Stream"/> for X12 to HTML transformation definition
        /// </summary>
        /// <returns><see cref="Stream"/> for the manifest resource</returns>
        public static Stream GetHtmlTransformationStream()
        {
            return Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream("X12.Core.Transformations.Resources.X12-XML-to-HTML.xslt");
        }

        /// <summary>
        /// Creates a <see cref="Stream"/> for X12 to X12 transformation definition
        /// </summary>
        /// <returns><see cref="Stream"/> for the manifest resource</returns>
        public static Stream GetX12TransformationStream()
        {
            return Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream("X12.Core.Transformations.Resources.X12-XML-to-X12.xslt");
        }
    }
}
