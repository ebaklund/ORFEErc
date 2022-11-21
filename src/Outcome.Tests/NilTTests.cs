
namespace Outcome.Tests;

public class Given_an_NilT_Constructor
{
    public class When_Invoked : Given_an_NilT_Constructor
    {
        private static string _msg1 = "1";
        private static string _msg2 = "2";
        private static string _msg3 = "3";
        private static Exception _innerEx = new(_msg3);

        private Result<int> _nil1 = Result<int>.Nil(_msg1);
        private Result<int> _nil2 = Result<int>.Nil(_msg2, _innerEx);

        [Fact]
        public void It_succeeds()
        {
            (_nil1 is Nil<int>).Should().BeTrue();
            (_nil2 is Nil<int>).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_ErrorT()
        {
            Func<Result<int>> func = () => _nil1.NilOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_can_resolve_to_UndefinedT()
        {
            Func<Result<int>> func = () => _nil1.UndefinedOrThrow<int>();
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_OkT()
        {
            Func<Result<int>> func = () => _nil1.OkOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_Value()
        {
            Func<int> func = () => _nil1.ValueOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Ok<Int32>.");
        }

        [Fact]
        public void Result_cannot_resolve_to_ErrorT()
        {
            Func<Result<int>> func = () => _nil1.ErrorOrThrow<int>();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error<Int32>.");
        }

        [Fact]
        public void Reason_is_retrievable()
        {
            _nil1.NilOrThrow<int>().Reason.Should().BeOfType<NilResultException>();
            _nil2.NilOrThrow<int>().Reason.Should().BeOfType<NilResultException>();
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
    }
}
