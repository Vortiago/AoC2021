namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day11Validations {

    [Test]
    public void ValidatePart1() {
        var day = new Day11(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526");
        day.Part1().Should().Be(1656);
    }

    [Test]
    public void ValidatePart2() {
        var day = new Day11(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526");
        day.Part1();
        day.Part2().Should().Be(195);
    }
}