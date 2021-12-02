using NUnit.Framework;
using FluentAssertions;
using Days;
using System;

namespace Validations;

public class Day2Validations {
    private string testInput = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

    [Test]
    public void Part1() {
        var day = new Day2(testInput);
        day.Part1().Should().Be(150);
    }

    [Test]
    public void Part2() {
        var day = new Day2(testInput);
        day.Part2().Should().Be(900);
    }

    [Test]
    public void TestParseMoveCommand() {
        var day = new Day2(String.Empty);
        var moveCommand = day.ParseMoveCommand("down 5");
        moveCommand.Direction.Should().Be("down");
        moveCommand.Distance.Should().Be(5);
    }
}