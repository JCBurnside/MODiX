using System;
using System.Collections.Generic;
using System.Text;

namespace Modix.Services.Coliru
{
    public enum SupportedLangs
    {
        cpp,
        c,
        py,
        haskell,
        // aliases
        python = py,
        cc = cpp,
        h = cpp,
        hpp = cpp
    }
}
