namespace Validations;

using NUnit.Framework;
using FluentAssertions;
using Days;

public class Day12Validations
{
    private string testInput {get;} = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

    [Test]
    public void ValidatePart1()
    {
        var day = new Day12(testInput);
        day.Part1().Should().Be(10);
    }

    [Test]
    public void ValidatePart1ExtendedDataset()
    {
        var day = new Day12(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc");
        day.Part1().Should().Be(19);
    }

    [Test]
    public void ValidatePart1MassiveDataset()
    {
        var day = new Day12(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW");
        day.Part1().Should().Be(226);
    }

    [Test]
    public void ValidatePart2()
    {
        var day = new Day12(testInput);
        day.Part2().Should().Be(36);
    }

    [Test]
    public void ValidatePart2ExtendedDataset()
    {
        var day = new Day12(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc");
        day.Part2().Should().Be(103);
    }

    [Test]
    public void ValidatePart2MassiveDataset()
    {
        var day = new Day12(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW");
        day.Part2().Should().Be(3509);
    }
}