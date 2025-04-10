using System;

[Flags]
public enum ExampleFlags : uint
{
    None = 0,
    Flag1 = 1 << 0,
    Flag2 = 1 << 1,
    Flag3 = 1 << 2
}
