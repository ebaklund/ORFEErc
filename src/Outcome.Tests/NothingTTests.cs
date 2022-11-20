
namespace Outcome.Tests;

public class Given_an_NothingT_Constructor
{
    public class When_Invoked : Given_an_NothingT_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Result<int> _nilInstance1 = Result<int>.Nothing(_msg1);
        private Result<int> _nilInstance2 = Result<int>.Nothing(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_nilInstance1 is Nothing<int>).Should().BeTrue();
            (_nilInstance2 is Nothing<int>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_ErrorT()
        {
            var func = new Func<Result<int>>(() => _nilInstance1.NothingOrThrow<int>());
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_to_UndefinedT()
        {
            var func = new Func<Result<int>>(() => _nilInstance1.UndefinedOrThrow<int>());
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_OkT()
        {
            var func = new Func<Result<int>>(() => _nilInstance1.OkOrThrow<int>());
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_Value()
        {
            Func<int> func = () => _nilInstance1.ValueOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_ErrorT()
        {
            var func = new Func<Result<int>>(() => _nilInstance1.ErrorOrThrow<int>());
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<Int32>.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _nilInstance1.NothingOrThrow<int>().Reason.Should().BeOfType<NothingResultException>();
            _nilInstance2.NothingOrThrow<int>().Reason.Should().BeOfType<NothingResultException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _nilInstance1.NothingOrThrow<int>().Reason.Message.Should().Be(_msg1);
            _nilInstance2.NothingOrThrow<int>().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _nilInstance1.NothingOrThrow<int>().Reason.InnerException.Should().Be(null);
            _nilInstance2.NothingOrThrow<int>().Reason.InnerException.Should().Be(_innerEx);
        }
    }
}
