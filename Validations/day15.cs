namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day15Validations
{
    private string map { get; } = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";


    [Test]
    public void ValidatePart1()
    {
        var day = new Day15(map);
        day.Part1().Should().Be(40);
    }

    [Test]
    public void ValidatePart2()
    {
        var day = new Day15(map);
        day.Part2().Should().Be(315);
    }
}