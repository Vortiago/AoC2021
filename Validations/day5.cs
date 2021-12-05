namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;
using System.Collections.Generic;
using System;
using System.Linq;

public class Day5Validations {
    private string testInput = string.Empty;
    private Day5 day = new Day5(string.Empty);
    private Day5.Map map = new Day5.Map();

    [SetUp]
    public void resetData() {
        testInput = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

        this.day = new Day5(testInput);

        this.map = new Day5.Map{
            Lines = new List<Day5.Map.Line>{
                new Day5.Map.Line{Start = (0,9), Stop = (5,9)},
                new Day5.Map.Line{Start = (8,0), Stop = (0,8)},
                new Day5.Map.Line{Start = (9,4), Stop = (3,4)},
                new Day5.Map.Line{Start = (2,2), Stop = (2,1)},
                new Day5.Map.Line{Start = (7,0), Stop = (7,4)},
                new Day5.Map.Line{Start = (6,4), Stop = (2,0)},
                new Day5.Map.Line{Start = (0,9), Stop = (2,9)},
                new Day5.Map.Line{Start = (3,4), Stop = (1,4)},
                new Day5.Map.Line{Start = (0,0), Stop = (8,8)},
                new Day5.Map.Line{Start = (5,5), Stop = (8,2)},
            }
        };
    }

    [Test]
    public void ValidatePart1() {
        day.Part1().Should().Be(5);
    }

    [Test]
    public void ValidatePart2() {
        day.Part2().Should().Be(12);
    }

    [Test]
    public void ValidateDrawMap() {
        this.map.ToString(true).Should().Be(@".......1..
..1....1..
..1....1..
.......1..
.112111211
..........
..........
..........
..........
222111....");
    }

    [Test]
    public void ValidateDrawAllMap() {
        this.map.ToString(false).Should().Be(@"1.1....11.
.111...2..
..2.1.111.
...1.2.2..
.112313211
...1.2....
..1...1...
.1.....1..
1.......1.
222111....");
    }

    [Test]
    public void ValidateParser() {
        this.day.ParseInput(testInput).Should().BeEquivalentTo(this.map);
    }

    [Test]
    public void ValidateMapSizeCalculation() {
        this.map.Size().Should().Be((9,9));
    }

    [TestCase(0, 0, 2, 0, 1, 0, true)]
    [TestCase(5, 5, 2, 5, 4, 5, true)]
    [TestCase(0, 0, 0, 3, 1, 1, false)]
    [TestCase(3, 2, 3, 7, 1, 1, false)]
    [TestCase(0, 9, 5, 9, 0, 0, false)]
    [TestCase(9, 7, 7, 9, 8, 8, true)]
    [TestCase(8, 0, 0, 8, 0, 0, false)]
    [TestCase(0, 0, 8, 8, 3, 3, true)]
    public void ValidateLineIntersect(Int64 StartX, Int64 StartY, Int64 StopX, Int64 StopY, Int64 A, Int64 B, bool result) {
        var line = new Day5.Map.Line() {
            Start = (StartX, StartY),
            Stop  = (StopX, StopY)
        };
        line.Intersects(A, B).Should().Be(result);
    }

    [Test]
    public void NumberOfStraightLines() {
        map.Lines.Where(x => x.StraightLine).Count().Should().Be(6);
    }
}