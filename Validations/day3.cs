using NUnit.Framework;
using FluentAssertions;
using Days;
using System.Collections.Generic;

namespace Validations;

public class Day3Validations {
    private string testInput = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

    [Test]
    public void ValidatePart1() {
        var day = new Day3(testInput);
        day.Part1().Should().Be(198);
    }

    [Test]
    public void ValidatePart2() {
        var day = new Day3(testInput);
        day.Part1().Should().Be(230);
    }

    [Test]
    public void ValidateFindMostCommon() {
        var day = new Day3(testInput);
        day.FindMostCommon().Should().BeEquivalentTo(new List<int>{1,0,1,1,0});
    }
}