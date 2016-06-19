using System;
using System.Collections.Generic;
using System.Linq;

namespace OrangeChess
{
    public abstract class Piece : IEquatable<Piece>
    {
        protected Piece(Color clr, char ch, IEnumerable<Move> lm)
        {
            if(clr != Color.White && clr != Color.Black)
                throw new ArgumentException("Piece color has to be either White or Black.");

            if(!char.IsLower(ch) && !char.IsUpper(ch))
                throw new ArgumentException("Character has to have an upper and lower case.");

            Color = clr;
            _fenchar = ch;
            LegalMoves = lm.AsEnumerable();
        }

        public Color Color { get; }

        char _fenchar;
        public char FENChar => Color.HasFlag(Color.White) ? char.ToUpperInvariant(_fenchar) : char.ToLowerInvariant(_fenchar);
        public IEnumerable<Move> LegalMoves { get; }

        public bool Equals(Piece other)
            => GetType() == other.GetType()
            && Color == other.Color
            && new HashSet<Move>(LegalMoves).SetEquals(new HashSet<Move>(other.LegalMoves));
    }
}