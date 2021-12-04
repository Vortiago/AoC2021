using NUnit.Framework;
using FluentAssertions;
using Days;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Validations;

public class Day4Validations
{
    private string testInput = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";

    private string testBoards = @"22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";

    private IEnumerable<Int64> draws = new List<Int64> { 7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1 };

    private Day4.Board validationBoard = new Day4.Board();

    [SetUp]
    public void CreateBoard()
    {
        validationBoard = new Day4.Board
        {
            Rows = new List<List<Day4.Board.Cell>>
                {
                    new List<Day4.Board.Cell>{
                        new Day4.Board.Cell{
                            Value = 14,
                        },
                        new Day4.Board.Cell{
                            Value = 21,
                        },
                        new Day4.Board.Cell{
                            Value = 17,
                        },
                        new Day4.Board.Cell{
                            Value = 24,
                        },
                        new Day4.Board.Cell{
                            Value = 4,
                        },
                    },new List<Day4.Board.Cell>{
                        new Day4.Board.Cell{
                            Value = 10,
                        },
                        new Day4.Board.Cell{
                            Value = 16,
                        },
                        new Day4.Board.Cell{
                            Value = 15,
                        },
                        new Day4.Board.Cell{
                            Value = 9,
                        },
                        new Day4.Board.Cell{
                            Value = 19,
                        },
                    },new List<Day4.Board.Cell>{
                        new Day4.Board.Cell{
                            Value = 18,
                        },
                        new Day4.Board.Cell{
                            Value = 8,
                        },
                        new Day4.Board.Cell{
                            Value = 23,
                        },
                        new Day4.Board.Cell{
                            Value = 26,
                        },
                        new Day4.Board.Cell{
                            Value = 20,
                        },
                    },new List<Day4.Board.Cell>{
                        new Day4.Board.Cell{
                            Value = 22,
                        },
                        new Day4.Board.Cell{
                            Value = 11,
                        },
                        new Day4.Board.Cell{
                            Value = 13,
                        },
                        new Day4.Board.Cell{
                            Value = 6,
                        },
                        new Day4.Board.Cell{
                            Value = 5,
                        },
                    },new List<Day4.Board.Cell>{
                        new Day4.Board.Cell{
                            Value = 2,
                        },
                        new Day4.Board.Cell{
                            Value = 0,
                        },
                        new Day4.Board.Cell{
                            Value = 12,
                        },
                        new Day4.Board.Cell{
                            Value = 3,
                        },
                        new Day4.Board.Cell{
                            Value = 7,
                        },
                    },
                }
        };
    }

    [Test]
    public void ValidatePart1()
    {
        var day = new Day4(testInput);
        day.Part1().Should().Be(4512);
    }

    [Test]
    public void ValidatePart2()
    {
        var day = new Day4(testInput);
        day.Part2().Should().Be(1924);
    }

    [Test]
    public void ValidateDrawParsing()
    {
        var day = new Day4(testInput);
        day.ParseDraws(testInput.Split(Environment.NewLine)[0]).Should().BeEquivalentTo(draws);
    }

    [Test]
    public void ValdiateBoardsParsing()
    {
        var day = new Day4(testInput);
        day.ParseBoards(testBoards).Last().Should().BeEquivalentTo(validationBoard);
    }

    [Test]
    public void ValidateSingleBoardParsing()
    {
        var testBoard = @"14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";
        var day = new Day4(testInput);
        day.ParseSingleBoard(testBoard).Should().BeEquivalentTo(validationBoard);
    }

    [TestCase(21, 0, 1)]
    [TestCase(13, 3, 2)]
    public void ValidateMarkNumber(int a, int b, int c)
    {
        validationBoard.MarkNumber(a);
        validationBoard.Rows[b][c].Marked.Should().BeTrue();
    }

    [TestCase(new int[] { 10, 16, 15, 9, 19 }, true)]
    [TestCase(new int[] { 14, 21, 10, 15, 6 }, false)]
    [TestCase(new int[] { 10, 14, 18, 22, 2 }, true)]
    public void ValidateBingo(IEnumerable<int> marks, bool result)
    {
        foreach (var mark in marks)
        {
            validationBoard.MarkNumber(mark);
        }

        validationBoard.CheckBingo().Should().Be(result);
    }

    [Test]
    public void ValidateFindWinner()
    {
        var day = new Day4(testInput);
        draws.Take(12).ToList().ForEach(x => validationBoard.MarkNumber(x));
        day.FindWinnerBoard(day.ParseBoards(testBoards)).First().Item3.Should().BeEquivalentTo(validationBoard);
    }

    [Test]
    public void ValidateSumOfBoard()
    {
        draws.Take(12).ToList().ForEach(x => validationBoard.MarkNumber(x));
        validationBoard.Sum().Should().Be(188);
    }
}