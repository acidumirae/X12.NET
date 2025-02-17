﻿namespace X12.Core.Shared.Enumerations
{
    using X12.Core.Shared.Attributes;

    public enum PricingMethodology
    {
        [EdiFieldValue("00")]
        ZeroPricing_NotCoveredUnderContract,

        [EdiFieldValue("01")]
        PricedAsBilledAt100Percent,

        [EdiFieldValue("02")]
        PricedAtTheStandardFeeSchedule,

        [EdiFieldValue("03")]
        PricedAtAContractualPercentage,

        [EdiFieldValue("04")]
        BundledPricing,

        [EdiFieldValue("05")]
        PeerReviewPricing,

        [EdiFieldValue("06")]
        PerDiemPricing,

        [EdiFieldValue("07")]
        FlatRatePricing,

        [EdiFieldValue("08")]
        CombinationPricing,

        [EdiFieldValue("09")]
        MaternityPricing,

        [EdiFieldValue("10")]
        OtherPricing,

        [EdiFieldValue("11")]
        LowerOfCost,

        [EdiFieldValue("12")]
        RatioOfCost,

        [EdiFieldValue("13")]
        CostReimbursed,

        [EdiFieldValue("14")]
        AdjustmentPricing
    }
}
