
namespace Outcome.Tests;

public class Given_an_ErrorT_Constructor
{
    public class When_Invoked : Given_an_ErrorT_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Result<int> _err1 = Result<int>.Error(_msg1);
        private Result<int> _err2 = Result<int>.Error(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_err1 is Error<int>).Should().BeTrue();
            (_err2 is Error<int>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_ErrorT()
        {
            Func<Result<int>> func = () => _err1.ErrorOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_to_UndefinedT()
        {
            Func<Result<int>> func = () => _err1.UndefinedOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_OkT()
        {
            Func<Result<int>> func = () => _err1.OkOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_Value()
        {
            Func<int> func = () => _err1.ValueOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_NilT()
        {
            Func<Result<int>> func = () => _err1.NilOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Nil<Int32>.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _err1.ErrorOrThrow<int>().Reason.Should().BeOfType<ErrorResultException>();
            _err2.ErrorOrThrow<int>().Reason.Should().BeOfType<ErrorResultException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _err1.ErrorOrThrow<int>().Reason.Message.Should().Be(_msg1);
            _err2.ErrorOrThrow<int>().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _err1.ErrorOrThrow<int>().Reason.InnerException.Should().Be(null);
            _err2.ErrorOrThrow<int>().Reason.InnerException.Should().Be(_innerEx);
        }
    }
}
