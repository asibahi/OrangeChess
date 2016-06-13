using System;
using System.Collections.Generic;

namespace OrangeChess
{
    // This is a sample implememntation NOT MEANT TO BE INHERITED
    // You may look at it to understand how to define pieces.
    sealed class PieceSample : IPiece
    {
        public Color Color { get; }

        char _fenchar;
        public char FENChar => Color == Color.White ? char.ToUpper(_fenchar) : char.ToLower(_fenchar);

        HashSet<Directions> _legalMoves;
        public HashSet<Directions> LegalMoves { get; }

        PieceSample(Color color, char fenchar, HashSet<Directions> legalMoves)
        {
            Color = color;
            _fenchar = fenchar;
            _legalMoves = legalMoves;
        }

        #region Equality
        public bool Equals(IPiece other)
            => other != null && Color == other.Color && FENChar == other.FENChar && GetType() == other.GetType();
        public override bool Equals(object obj)
            => Equals(obj as IPiece);
        public override int GetHashCode()
            => (Color.GetHashCode() * 11) ^ (FENChar.GetHashCode() * 7);
        #endregion
    }
}