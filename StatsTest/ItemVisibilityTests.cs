using FluentAssertions;
using Stats.Model;
using Xunit;

namespace StatsTest
{
    public class ItemVisibilityTests
    {
        [Theory]
        [InlineData(false, false, false, 50, null)]
        [InlineData(false, false, false, 50, 10)]
        [InlineData(false, false, false, 50, 100)]
        [InlineData(false, false, true, 50, null)]
        [InlineData(false, false, true, 50, 10)]
        [InlineData(false, false, true, 50, 100)]
        [InlineData(false, true, false, 50, null)]
        [InlineData(false, true, false, 50, 10)]
        [InlineData(false, true, false, 50, 100)]
        [InlineData(false, true, true, 50, null)]
        [InlineData(false, true, true, 50, 10)]
        [InlineData(false, true, true, 50, 100)]
        public void GetItemVisibility_Should_BeFalse_When_Disabled(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData(true, true, false, 50, null)]
        [InlineData(true, true, true, 50, null)]
        public void GetItemVisibility_Should_BeFalse_When_EnabledHideItemsNotAvailableAndPercentNull(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData(true, false, true, 50, 10)]
        [InlineData(true, true, true, 50, 10)]
        public void GetItemVisibility_Should_BeFalse_When_EnabledHideItemsBelowThresholdTrueAndPercentBelowThreshhold(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeFalse();
        }

        [Theory]
        [InlineData(true, false, true, 50, null)]
        [InlineData(true, false, false, 50, null)]
        public void GetItemVisibility_Should_BeTrue_When_EnabledHideItemsNotAvailableFalseAndPercentNull(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData(true, false, false, 50, 10)]
        [InlineData(true, true, false, 50, 10)]
        public void GetItemVisibility_Should_BeTrue_When_EnabledHideItemsBelowThresholdFalseAndPercentBelowThreshold(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeTrue();
        }

        [Theory]
        [InlineData(true, false, false, 50, 100)]
        [InlineData(true, true, false, 50, 100)]
        [InlineData(true, false, true, 50, 100)]
        [InlineData(true, true, true, 50, 100)]
        public void GetItemVisibility_Should_BeTrue_When_EnabledAndPercentAboveThreshold(
            bool enabled,
            bool hideItemsNotAvailable,
            bool hideItemsBelowThreshold,
            int threshold,
            int? percent)
        {
            Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent)
                .Should()
                .BeTrue();
        }
    }
}
