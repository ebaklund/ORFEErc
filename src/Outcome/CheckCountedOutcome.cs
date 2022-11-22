using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutcomeCs;

public record CheckCountedOutcome : IDisposable
{
    private int _checkCount;

    internal CheckCountedOutcome()
    {
        _checkCount = 0;
    }

    public void IncrementCheckCounter()
    {
      // Outcomes must always be checked. If not, throw exception when outcome is disposed.
      _checkCount++;
    }

    public void Dispose()
    {
        if (_checkCount == 0)
        {
            // Intentionally violate Microsoft design rule CA1065 by throwing exception.
            throw new UncheckedOutcomeException(this);
        }
    }
}
