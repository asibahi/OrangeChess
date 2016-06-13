using System;
using System.Collections.Generic;

namespace OrangeChess
{
    public interface IPiece : IEquatable<IPiece>
    {
        Color Color { get; }
        char FENChar { get; }
        HashSet<Directions> LegalMoves { get; }

        bool Equals(object obj);
        int GetHashCode();
    }
}