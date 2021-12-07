namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day6Validations {

    [Test]
    public void ValidatePart1() {
        var day = new Day6("3,4,3,1,2");
        day.Part1().Should().Be(5934);
    }

    [Test]
    public void ValidatePart2() {
        var day = new Day6("3,4,3,1,2");
        day.Part2().Should().Be(26984457539);
    }
}