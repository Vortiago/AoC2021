using NUnit.Framework;
using FluentAssertions;

namespace Validations;

public class Tests
{
    private string testInput = @"199
200
208
210
200
207
240
269
260
263";

    [Test]
    public void Part1(){
        var day = new Day1(testInput);
        day.Part1().Should().Be(7);
    }

    [Test]
    public void Part2() {
        var day = new Day1(testInput);
        day.Part2().Should().Be(5);
    }
}