using System;

namespace OrangeChess
{
    [Flags] // I am not sure how to define this
    public enum Directions
    {
        None = 0,
        Forwaward = 1 << 0,
        Backward = 1 << 1,
        Kingside = 1 << 2, // right from White's persepctive, left from Black's
        Queenside = 1 << 3  // left from White's prspective, right from Black's
    }
}
