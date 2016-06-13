using System;
using System.Collections.Generic;

namespace OrangeChess
{
    // This is a sample implememntation NOT MEANT TO BE INHERITED
    // You may look at it to understand how to define pieces.
    sealed class PieceSample : IEquatable<PieceSample>, IPiece
    {
        public Color Color { get; }

        char _fenchar;
        public char FENChar => Color == Color.White ? char.ToUpper(_fenchar) : char.ToLower(_fenchar);

        public HashSet<Directions> LegalMoves { get; }

        PieceSample(Color color, char fenchar, HashSet<Directions)
        {
            Color = color;
            _fenchar = fenchar;
        }

        #region Equality
        public bool Equals(PieceSample other) => other != null && Color == other.Color && FENChar == other.FENChar && GetType() == other.GetType();
        public bool Equals(IPiece other) => Equals(other as PieceSample);
        public override bool Equals(object obj) => Equals(obj as PieceSample);
        public override int GetHashCode() => (Color.GetHashCode() * 11) ^ (FENChar.GetHashCode() * 7);
        public static bool operator ==(PieceSample piece1, PieceSample piece2) => piece1.Equals(piece2);
        public static bool operator !=(PieceSample piece1, PieceSample piece2) => !piece1.Equals(piece2);
        #endregion
    }
}
