
using FluentAssertions;
using OutcomeCs;

namespace Readme.Tests;

public class Given_FirstInList_Examples
{
    public static Outcome<T> FirstInList_1<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Nil("Array is empty"),
            _ => list[0] switch
            {
                null => Outcome<T>.Nil("First item is null"),
                _ => Outcome<T>.Ok(list[0])
            }
        };
    }

    public static Outcome<T> FirstInList_2<T>(List<T> list)
    {
        return list?.Count switch
        {
            null => Outcome<T>.Error("Array is undefined"),
            0 => Outcome<T>.Error("Array is empty"),
            _ => list[0] switch
            {
                null => Outcome<T>.Error("First item is null"),
                _ => Outcome<T>.Ok(list[0])
            }
        };
    }

    public class When_Invoked : Given_FirstInList_Examples
    {
        [Fact]
        public void Example_1_works()
        {
            FirstInList_1<string>(null)
                .Should().BeOfType<Error<string>>()
                .Which.Reason.Message
                .Should().Be("Array is undefined");

            FirstInList_1<string>(new List<string>())
                .Should().BeOfType<Nil<string>>()
                .Which.Reason.Message
                .Should().Be("Array is empty");

            FirstInList_1<string>(new List<string>() { null })
                .Should().BeOfType<Nil<string>>()
                .Which.Reason.Message
                .Should().Be("First item is null");

            FirstInList_1<string>(new List<string>() { "1" })
                .Should().BeOfType<Ok<string>>()
                .Which.Value
                .Should().Be("1");
        }

        [Fact]
        public void Example_2_works()
        {
            FirstInList_2<string>(null)
                .Should().BeOfType<Error<string>>()
                .Which.Reason.Message
                .Should().Be("Array is undefined");

            FirstInList_2<string>(new List<string>())
                .Should().BeOfType<Error<string>>()
                .Which.Reason.Message
                .Should().Be("Array is empty");

            FirstInList_2<string>(new List<string>() { null })
                .Should().BeOfType<Error<string>>()
                .Which.Reason.Message
                .Should().Be("First item is null");

            FirstInList_2<string>(new List<string>() { "2" })
                .Should().BeOfType<Ok<string>>()
                .Which.Value
                .Should().Be("2");
        }
    }
}
