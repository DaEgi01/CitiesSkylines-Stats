using AutoFixture;
using FluentAssertions;
using Moq;
using Stats;
using Stats.Config;
using Stats.Model;
using System;
using Xunit;

namespace StatsTest
{
    public class ItemsInIndexOrderTests
    {
        private readonly Func<bool> alwaysReturnFalse = () => false;

        [Fact]
        public void ItemsInIndexOrder_Constructor_Should_Not_ThrowIndexesMessedUpException_When_IndexesAlign()
        {
            var items = new[] {
                new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, true, 10, 10),
                new Item(ItemData.Cemetery, alwaysReturnFalse, alwaysReturnFalse, true, 10, 10),
            };

            Action sut = () => new ItemsInIndexOrder(items);

            sut.Should()
                .NotThrow<IndexesMessedUpException>();
        }

        [Fact]
        public void ItemsInIndexOrder_Constructor_Should_Not_ThrowIndexesMessedUpException_When_IndexesAlignButAreOutOfOrder()
        {
            var fix = new Fixture();

            var moqConfigService = new Mock<ConfigurationService<ConfigurationDto>>(fix.Create<string>());
            var moqConfigDto = new Mock<ConfigurationDto>();
            var moqConfig = new Mock<Configuration>(moqConfigService.Object, moqConfigDto.Object);
            var items = new[] {
                new Item(ItemData.Cemetery, alwaysReturnFalse, alwaysReturnFalse,  true, 10, 10),
                new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, true, 10, 10),
            };

            Action sut = () => new ItemsInIndexOrder(items);

            sut.Should()
                .NotThrow<IndexesMessedUpException>();
        }

        [Fact]
        public void ItemsInIndexOrder_Constructor_Should_ThrowIndexesMessedUpException_When_ItemIndexesHaveGaps()
        {
            var fix = new Fixture();

            var moqConfigService = new Mock<ConfigurationService<ConfigurationDto>>(fix.Create<string>());
            var moqConfigDto = new Mock<ConfigurationDto>();
            var moqConfig = new Mock<Configuration>(moqConfigService.Object, moqConfigDto.Object);
            var items = new[] {
                new Item(ItemData.AverageIllnessRate, alwaysReturnFalse, alwaysReturnFalse, true, 10, 10),
                new Item(ItemData.CityUnattractiveness, alwaysReturnFalse, alwaysReturnFalse, true, 10, 10),
            };

            Action sut = () => new ItemsInIndexOrder(items);

            sut.Should()
                .Throw<IndexesMessedUpException>();
        }
    }
}
