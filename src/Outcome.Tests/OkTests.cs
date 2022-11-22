
namespace OutcomeCs.Tests;

public class Given_an_Ok_Constructor
{
    public class When_Invoked : Given_an_Ok_Constructor
    {
        private Outcome _ok = Outcome.Ok();

        [Fact]
        public void It_succeeds()
        {
            (_ok is Ok).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_Ok()
        {
            Func<Outcome> func = () => _ok.OkOrThrow();
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_Error()
        {
            Func<Outcome> func = () => _ok.ErrorOrThrow();
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error.");
        }
    }
}
