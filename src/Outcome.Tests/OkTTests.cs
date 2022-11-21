
using Newtonsoft.Json.Linq;

namespace Outcome.Tests;

public class Given_an_OkT_Constructor
{
    public class When_Invoked : Given_an_OkT_Constructor
    {
        private const int _value1 = 1;
        private const string _value2 = "2";

        private Result<int> _ok1 = Result<int>.Ok(_value1);
        private Result<string> _ok2 = Result<string>.Ok(_value2);

        [Fact]
        public void It_succeeds()
        {
            (_ok1 is Ok<int>).Should().BeTrue();
            (_ok2 is Ok<string>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_Ok()
        {
            Func<Result<int>> func1 = () => _ok1.OkOrThrow<int>();
            func1.Should().NotThrow();

            Func<Result<string>> func2 = () => _ok2.OkOrThrow<string>();
            func2.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_Value()
        {
            Func<int> func1 = () => _ok1.ValueOrThrow<int>();
            func1.Should().NotThrow().Which.Should().Be(_value1);

            Func<string> func2 = () => _ok2.ValueOrThrow<string>();
            func2.Should().NotThrow().Which.Should().Be(_value2);
        }

        [Fact]
        public void Result_cannot_resolve_to_Undefined()
        {
            Func<Result<int>> func1 = () => _ok1.UndefinedOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Undefined<Int32>.");

            Func<Result<string>> func2 = () => _ok2.UndefinedOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Undefined<String>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_ErrorT()
        {
            Func<Result<int>> func1 = () => _ok1.ErrorOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<Int32>.");

            Func<Result<string>> func2 = () => _ok2.ErrorOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<String>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_NilT()
        {
            Func<Result<int>> func1 = () => _ok1.NilOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Nil<Int32>.");

            Func<Result<string>> func2 = () => _ok2.NilOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Nil<String>.");
        }
    }
}
