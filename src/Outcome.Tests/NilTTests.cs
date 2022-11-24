
namespace OutcomeCs.Tests;

public class Given_an_NilT_Constructor
{
    public class When_Invoked : Given_an_NilT_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Outcome<int> _nil1 = Outcome<int>.Nil(_msg1);
        private Outcome<int> _nil2 = Outcome<int>.Nil(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_nil1 is Nil<int>).Should().BeTrue();
            (_nil2 is Nil<int>).Should().BeTrue();
        }

        [Fact]
        public void Outcome_can_resolve_to_ErrorT()
        {
            Func<Outcome<int>> func = () => _nil1.NilOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public void Outcome_can_resolve_to_UndefinedT()
        {
            Func<Outcome<int>> func = () => _nil1.UndefinedOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public async Task Result_can_async_resolve_to_NilT()
        {
            Func<Task<Nil<int>>> func = () => Task<Outcome<int>>.Run(() => Outcome<int>.Nil(_msg1)).NilOrThrowAsync();
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public void Outcome_cannot_resolve_to_Value()
        {
            Func<int> func = () => _nil1.ValueOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Outcome_cannot_resolve_to_ErrorT()
        {
            Func<Outcome<int>> func = () => _nil1.ErrorOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Outcome is not of type: Error<Int32>.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _nil1.NilOrThrow<int>().Reason.Should().BeOfType<NilOutcomeException>();
            _nil2.NilOrThrow<int>().Reason.Should().BeOfType<NilOutcomeException>();
        }

        [Fact]
        public void Message_is_retrievable()
        {
            _nil1.NilOrThrow<int>().Reason.Message.Should().Be(_msg1);
            _nil2.NilOrThrow<int>().Reason.Message.Should().Be(_msg2);
        }

        [Fact]
        public void Inner_exception_is_retrievable()
        {
            _nil1.NilOrThrow<int>().Reason.InnerException.Should().Be(null);
            _nil2.NilOrThrow<int>().Reason.InnerException.Should().Be(_innerEx);
        }

        [Fact]
        public void Stack_trace_is_retrievable()
        {
            _nil1.NilOrThrow().Reason.OutcomeStackTrace.Should().Contain("OutcomeCs.Tests");
            _nil2.NilOrThrow().Reason.OutcomeStackTrace.Should().Contain("OutcomeCs.Tests");
        }

        [Fact]
        public void Message_trace_is_retrievable()
        {
            _nil1.NilOrThrow().Reason.OutcomeMessageTrace.Should().Be("1");
            _nil2.NilOrThrow().Reason.OutcomeMessageTrace.Should().Be($"2{Environment.NewLine}3");
        }
    }
}
