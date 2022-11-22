
using FluentAssertions;
using Outcome;

namespace Readme.Tests;

public class Given_FirstInList_Examples
{
    public static Outcome<T> FirstInList_1<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Nil("Array is empty"), // <== Nil
            _ => Outcome<T>.Ok(list[0])
        };
    }

    public static Outcome<T> FirstInList_2<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Error("Array is empty"), // <== Error
            _ => Outcome<T>.Ok(list[0])
        };
    }

    public class When_Invoked : Given_FirstInList_Examples
    {
        [Fact]
        public void Example_1_works()
        {
            FirstInList_1<int>(null)
                .Should().BeOfType<Error<int>>()
                .Which.Reason.Message
                .Should().Be("Array is undefined");

            FirstInList_1<int>(new List<int>())
                .Should().BeOfType<Nil<int>>()
                .Which.Reason.Message
                .Should().Be("Array is empty");

            FirstInList_1<int>(new List<int>() { 1 })
                .Should().BeOfType<Ok<int>>()
                .Which.Value
                .Should().Be(1);
        }

        [Fact]
        public void Example_2_works()
        {
            FirstInList_2<int>(null)
                .Should().BeOfType<Error<int>>()
                .Which.Reason.Message
                .Should().Be("Array is undefined");

            FirstInList_2<int>(new List<int>())
                .Should().BeOfType<Error<int>>()
                .Which.Reason.Message
                .Should().Be("Array is empty");

            FirstInList_2<int>(new List<int>() { 2 })
                .Should().BeOfType<Ok<int>>()
                .Which.Value
                .Should().Be(2);
        }
    }
}
