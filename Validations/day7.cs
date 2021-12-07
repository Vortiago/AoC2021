namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day7Validations {

    [Test]
    public void ValidatePart1() {
        var day = new Day7("16,1,2,0,4,2,7,1,2,14");
        day.Part1().Should().Be(37);
    }

    [Test]
    public void ValidatePart2() {
        var day = new Day7("16,1,2,0,4,2,7,1,2,14");
        day.Part2().Should().Be(168);
    }
}