
namespace OutcomeCs.Tests;

public class Given_CheckCountedOutcomes
{
   public class When_leaving_scope_unchecked : Given_CheckCountedOutcomes
   {
      [Fact]
      public void Ok_throws()
      {
         Action act = () => { using var ok = Outcome.Ok(); };
         act.Should().Throw<UncheckedOutcomeException>().Which.UncheckedOutcome.Should().BeOfType<Ok>();
      }

      [Fact]
      public void Error_throws()
      {
         Action act = () => { using var err = Outcome.Error(""); };
         act.Should().Throw<UncheckedOutcomeException>().Which.UncheckedOutcome.Should().BeOfType<Error>();
      }

      [Fact]
      public void OkT_throws()
      {
         Action act = () => { using var ok = Outcome<int>.Ok(1); };
         act.Should().Throw<UncheckedOutcomeException>().Which.UncheckedOutcome.Should().BeOfType<Ok<int>>();
      }

      [Fact]
      public void NilT_throws()
      {
         Action act = () => { using var nil = Outcome<int>.Nil(""); };
         act.Should().Throw<UncheckedOutcomeException>().Which.UncheckedOutcome.Should().BeOfType<Nil<int>>();
      }

      [Fact]
      public void ErrorT_throws()
      {
         Action act = () => { using var err = Outcome<int>.Error(""); };
         act.Should().Throw<UncheckedOutcomeException>().Which.UncheckedOutcome.Should().BeOfType<Error<int>>();
      }
   }

   public class When_leaving_scope_checked : Given_CheckCountedOutcomes
   {
      [Fact]
      public void Ok_throws_not()
      {
         Action act = () => 
         { 
            using var ok = Outcome.Ok();
            ok.OkOrThrow();
         };

         act.Should().NotThrow();
      }

      [Fact]
      public void Error_throws_not()
      {
         Action act = () => 
         {
            using var err = Outcome.Error("");
            err.ErrorOrThrow();
         };

         act.Should().NotThrow();
      }

      [Fact]
      public void OkT_throws_not()
      {
         Action act = () => 
         { 
            using var ok = Outcome<int>.Ok(1);
            ok.ValueOrThrow();
         };

         act.Should().NotThrow();
      }

      [Fact]
      public void NilT_throws_not()
      {
         Action act = () => 
         { 
            using var nil = Outcome<int>.Nil("");
            nil.NilOrThrow();
         };

         act.Should().NotThrow();
      }

      [Fact]
      public void ErrorT_throws_not()
      {
         Action act = () => 
         { 
            using var err = Outcome<int>.Error("");
            err.ErrorOrThrow();
         };

         act.Should().NotThrow();
      }
   }
}
