
namespace Outcome.Tests;

public class Given_an_ErrorT_Constructor
{
    public class When_Invoked : Given_an_ErrorT_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Result<int> _errInstance1 = Result.Error<int>(_msg1);
        private Result<int> _errInstance2 = Result.Error<int>(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_errInstance1 is Error<int>).Should().BeTrue();
            (_errInstance2 is Error<int>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_ErrorT()
        {
            var func = new Func<Result<int>>(() => _errInstance1.ErrorOrThrow<int>());
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_to_UndefinedT()
        {
            var func = new Func<Result<int>>(() => _errInstance1.UndefinedOrThrow<int>());
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_OkT()
        {
            var func = new Func<Result<int>>(() => _errInstance1.OkOrThrow<int>());
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of Ok<Int32> type.");
        }

        [Fact]
        public void Result_cannot_resolve_to_NothingT()
        {
            var func = new Func<Result<int>>(() => _errInstance1.NothingOrThrow<int>());
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of Nothing<Int32> type.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _errInstance1.ErrorOrThrow<int>().Reason.Should().BeOfType<ErrorResultException>();
            _errInstance2.ErrorOrThrow<int>().Reason.Should().BeOfType<ErrorResultException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _errInstance1.ErrorOrThrow<int>().Reason.Message.Should().Be(_msg1);
            _errInstance2.ErrorOrThrow<int>().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _errInstance1.ErrorOrThrow<int>().Reason.InnerException.Should().Be(null);
            _errInstance2.ErrorOrThrow<int>().Reason.InnerException.Should().Be(_innerEx);
        }
    }
}
