﻿using System;

namespace Stats;

public sealed class IsNullException : Exception
{
    public IsNullException(string name)
        : base(name + " is null.")
    {
    }

    public IsNullException(string name, string message)
        : base(name + " is null. " + message)
    {
    }
}
