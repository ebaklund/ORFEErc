
using Newtonsoft.Json.Linq;

namespace Outcome.Tests;

public class Given_an_OkT_Constructor
{
    public class When_Invoked : Given_an_OkT_Constructor
    {
        private const int _value1 = 1;
        private const string _value2 = "2";

        private Outcome<int> _ok1 = Outcome<int>.Ok(_value1);
        private Outcome<string> _ok2 = Outcome<string>.Ok(_value2);

        [Fact]
        public void It_succeeds()
        {
            (_ok1 is Ok<int>).Should().BeTrue();
            (_ok2 is Ok<string>).Should().BeTrue();
        }

        [Fact]
        public void Outcome_can_resolve_to_Ok()
        {
            Func<Outcome<int>> func1 = () => _ok1.OkOrThrow<int>();
            func1.Should().NotThrow();

            Func<Outcome<string>> func2 = () => _ok2.OkOrThrow<string>();
            func2.Should().NotThrow();
        }

        [Fact]
        public void Outcome_can_resolve_Value()
        {
            Func<int> func1 = () => _ok1.ValueOrThrow<int>();
            func1.Should().NotThrow().Which.Should().Be(_value1);

            Func<string> func2 = () => _ok2.ValueOrThrow<string>();
            func2.Should().NotThrow().Which.Should().Be(_value2);
        }

        [Fact]
        public void Outcome_cannot_resolve_to_Undefined()
        {
            Func<Outcome<int>> func1 = () => _ok1.UndefinedOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Undefined<Int32>.");

            Func<Outcome<string>> func2 = () => _ok2.UndefinedOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Undefined<String>.");
        }

        [Fact]
        public void Outcome_cannot_resolve_to_ErrorT()
        {
            Func<Outcome<int>> func1 = () => _ok1.ErrorOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Error<Int32>.");

            Func<Outcome<string>> func2 = () => _ok2.ErrorOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Error<String>.");
        }

        [Fact]
        public void Outcome_cannot_resolve_to_NilT()
        {
            Func<Outcome<int>> func1 = () => _ok1.NilOrThrow<int>();
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Nil<Int32>.");

            Func<Outcome<string>> func2 = () => _ok2.NilOrThrow<string>();
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Nil<String>.");
        }
    }
}
