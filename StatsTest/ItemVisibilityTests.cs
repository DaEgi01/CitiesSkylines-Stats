using AutoFixture;
using FluentAssertions;
using Moq;
using Stats;
using Stats.Model;
using System;
using Xunit;

namespace StatsTest
{
    public class ItemVisibilityTests
    {
        [Theory]
        [InlineData(false, )]
        public void GetItemVisibility_Should_ReturnFalse_When_Disabled(bool enabled, bool hideItemsNotAvailable, bool hideItemsBelowThreshold, int threshold, int percent)
        {
            var result = Item.GetItemVisibility(enabled, hideItemsNotAvailable, hideItemsBelowThreshold, threshold, percent);
            result.Should().BeFalse();

            var fixture = new Fixture();

            var item = new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, true, fixture.Create<int>(), fixture.Create<int>());
            item.Percent = 100;

            item.IsVisible.Should().BeTrue();
        }

        [Fact]
        public void Item_Should_AlwaysBeInvisible_When_Disabled()
        {
            var fixture = new Fixture();

            var item = new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, false, fixture.Create<int>(), fixture.Create<int>());

            item.IsVisible.Should().BeFalse();
        }

        [Fact]
        public void Item_Should_BeVisible_When_Enabled()
        {
            var fixture = new Fixture();

            var item = new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, false, fixture.Create<int>(), fixture.Create<int>());

            item.IsVisible.Should().BeFalse();
        }
    }
}
