
namespace OutcomeCs.Tests;

public class Given_an_Outcome_wrapper
{
    public class When_Invoked : Given_an_Outcome_wrapper
    {   private static string _errMsg = "my err";
        private Outcome _ok = Outcome.FromRunning(() => { } );
        private Outcome _err = Outcome.FromRunning(() => { throw new Exception(_errMsg);  });

        [Fact]
        public void It_Wraps_Success()
        {
            _ok.Should().BeOfType<Ok>();
        }

        [Fact]
        public void It_Wraps_Failure()
        {
            _err.Should().BeOfType<Error>().Which.Reason.Message.Should().Be("Outcome wrapper received an exception.");

        }
    }
}

public class Given_an_OutcomeT_wrapper
{
    public class When_Invoked : Given_an_OutcomeT_wrapper
    {
        private static string _okMsg = "my okT";
        private static string _errMsg = "my errT";
        private Outcome<string> _ok = Outcome<string>.FromRunning(() => _okMsg);
        private Outcome<string> _nil = Outcome<string>.FromRunning(() => null);
        private Outcome<string> _err = Outcome<string>.FromRunning(() => { throw new Exception(_errMsg); });

        [Fact]
        public void It_Wraps_Success()
        {
            _ok.Should().BeOfType<Ok<string>>().Which.Value.Should().Be(_okMsg);
        }

        [Fact]
        public void It_Wraps_Null()
        {
            _nil.Should().BeOfType<Nil<string>>().Which.Reason.Message.Should().Be("Outcome<String> wrapper received a null value.");

        }

        [Fact]
        public void It_Wraps_Failure()
        {
            _err.Should().BeOfType<Error<string>>().Which.Reason.Message.Should().Be("Outcome<String> wrapper received an exception.");

        }
    }
}
