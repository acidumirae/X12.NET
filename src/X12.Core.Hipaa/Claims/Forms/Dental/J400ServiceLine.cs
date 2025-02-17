﻿namespace X12.Core.Hipaa.Claims.Forms.Dental
{
    using System;

    public class J400ServiceLine
    {
        public DateTime? Field24_ProcedureDate { get; set; }
        public string Field25_AreaOfOralCavity { get; set; }
        public string Field26_ToothSystem { get; set; }
        public string Field27_ToothNumberOrLetter { get; set; }
        public string Field28_ToothSurface { get; set; }
        public string Field29_ProcedureCode { get; set; }
        public string Field30_Description { get; set; }
        public decimal? Field31_Fee { get; set; }
    }
}
