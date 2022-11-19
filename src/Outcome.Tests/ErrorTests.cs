
namespace Outcome.Tests;

public class Given_an_Error_Constructor
{
    public class When_Invoked : Given_an_Error_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Result _errSut1 = Result.Error(_msg1);
        private Result _errSut2 = Result.Error(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            _errSut1.Should().BeOfType<Error>();
            _errSut2.Should().BeOfType<Error>();
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _errSut1.ErrorOrThrow().Reason.Should().BeOfType<OutcomeErrorException>();
            _errSut2.ErrorOrThrow().Reason.Should().BeOfType<OutcomeErrorException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _errSut1.ErrorOrThrow().Reason.Message.Should().Be(_msg1);
            _errSut2.ErrorOrThrow().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _errSut1.ErrorOrThrow().Reason.InnerException.Should().Be(null);
            _errSut2.ErrorOrThrow().Reason.InnerException.Should().Be(_innerEx);
        }
    }
}
