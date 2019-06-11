namespace Df.Stochastic.Fare
{
    using System;

    [Flags]
    internal enum RegExpSyntaxOptions
    {
        Intersection = 0x0001,

        Complement = 0x0002,

        Empty = 0x0004,

        Anystring = 0x0008,

        Automaton = 0x0010,

        Interval = 0x0020,

        All = 0xffff,
    }
}