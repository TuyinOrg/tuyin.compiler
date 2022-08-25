using System;

namespace libfsm
{
    [Flags]
    public enum ConflictsDetectionFlags
    {
        None        = 0,
        Symbol      = 1,
        Metadata    = 2,
        Next        = 4
    }
}
