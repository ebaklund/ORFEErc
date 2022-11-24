
using FluentAssertions;

namespace OutcomeCs.Tests;

public class Given_an_Error_Constructor
{
    public class When_Invoked : Given_an_Error_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Outcome _err1 = Outcome.Error(_msg1);
        private Outcome _err2 = Outcome.Error(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_err1 is Error).Should().BeTrue();
            (_err2 is Error).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_Error()
        {
            Func<Error> func = () => _err1.ErrorOrThrow();
            func.Should().NotThrow();
        }

        [Fact]
        public async Task Result_can_async_resolve_to_Error()
        {
            Func<Task<Error>> func = () => Task<Outcome>.Run(() => Outcome.Error(_msg1)).ErrorOrThrowAsync();
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public void Result_cannot_resolve_to_Ok()
        {
            Func<Outcome> func = () => _err1.OkOrThrow();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _err1.ErrorOrThrow().Reason.Should().BeOfType<ErrorOutcomeException>();
            _err2.ErrorOrThrow().Reason.Should().BeOfType<ErrorOutcomeException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _err1.ErrorOrThrow().Reason.Message.Should().Be(_msg1);
            _err2.ErrorOrThrow().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _err1.ErrorOrThrow().Reason.InnerException.Should().Be(null);
            _err2.ErrorOrThrow().Reason.InnerException.Should().Be(_innerEx);
        }

        [Fact]
        public void Stack_trace_is_retrievable()
        {
           _err1.ErrorOrThrow().Reason.OutcomeStackTrace.Should().Contain("OutcomeCs.Tests");
           _err2.ErrorOrThrow().Reason.OutcomeStackTrace.Should().Contain("OutcomeCs.Tests");
        }

        [Fact]
        public void Message_trace_is_retrievable()
        {
            _err1.ErrorOrThrow().Reason.OutcomeMessageTrace.Should().Be("1");
            _err2.ErrorOrThrow().Reason.OutcomeMessageTrace.Should().Be($"2{Environment.NewLine}3");
        }
    }
}
