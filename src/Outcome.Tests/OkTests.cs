
namespace Outcome.Tests;

public class Given_an_Ok_Constructor
{
    public class When_Invoked : Given_an_Ok_Constructor
    {
        private Result _okInstance = Result.Ok();

        [Fact]
        public void It_succeeds()
        {
            (_okInstance is Ok).Should().BeTrue();
        }

        [Fact]
        public void Result_can_resolve_to_Ok()
        {
            var func = new Func<Result>(() => _okInstance.OkOrThrow());
            func.Should().NotThrow();
        }

        [Fact]
        public void Result_cannot_resolve_to_Error()
        {
            var func = new Func<Result>(() => _okInstance.ErrorOrThrow());
            func.Should().Throw<InvalidCastException>().Which.Message.Should().Match("Input Result is not of type: Error.");
        }
    }
}
