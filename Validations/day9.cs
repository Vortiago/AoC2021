namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day9Validations {
    private string testInput = @"2199943210
3987894921
9856789892
8767896789
9899965678";

    [Test]
    public void ValidatePart1() {
        var day = new Day9(testInput);
        day.Part1().Should().Be(15);
    }

    [Test]
    public void ValidatePart2() {
        var day = new Day9(testInput);
        day.Part2().Should().Be(1134);
    }
}