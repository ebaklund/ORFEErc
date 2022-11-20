
using Newtonsoft.Json.Linq;

namespace Outcome.Tests;

public class Given_an_OkT_Constructor
{
    public class When_Invoked : Given_an_OkT_Constructor
    {
        private const int _value1 = 1;
        private const string _value2 = "2";

        private Result<int> _okInstance1 = Result<int>.Ok(_value1);
        private Result<string> _okInstance2 = Result<string>.Ok(_value2);

        [Fact]
        public void It_succeeds()
        {
            (_okInstance1 is Ok<int>).Should().BeTrue();
            (_okInstance2 is Ok<string>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_Ok()
        {
            var func1 = new Func<Result<int>>(() => _okInstance1.OkOrThrow<int>());
            func1.Should().NotThrow();

            var func2 = new Func<Result<string>>(() => _okInstance2.OkOrThrow<string>());
            func2.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_Value()
        {
            Func<int> func1 = () => _okInstance1.ValueOrThrow<int>();
            func1.Should().NotThrow().Which.Should().Be(_value1);

            Func<string> func2 = () => _okInstance2.ValueOrThrow<string>();
            func2.Should().NotThrow().Which.Should().Be(_value2);
        }

        [Fact]
        public void Result_cannot_resolve_to_Undefined()
        {
            var func1 = new Func<Result<int>>(() => _okInstance1.UndefinedOrThrow<int>());
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Undefined<Int32>.");

            var func2 = new Func<Result<string>>(() => _okInstance2.UndefinedOrThrow<string>());
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Undefined<String>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_ErrorT()
        {
            var func1 = new Func<Result<int>>(() => _okInstance1.ErrorOrThrow<int>());
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<Int32>.");

            var func2 = new Func<Result<string>>(() => _okInstance2.ErrorOrThrow<string>());
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<String>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_NothingT()
        {
            var func1 = new Func<Result<int>>(() => _okInstance1.NothingOrThrow<int>());
            func1.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Nothing<Int32>.");

            var func2 = new Func<Result<string>>(() => _okInstance2.NothingOrThrow<string>());
            func2.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Nothing<String>.");
        }
    }
}
