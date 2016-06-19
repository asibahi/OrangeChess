using System;
using System.Text;
using OrangeChess;

namespace OrangeClassicalChess
{
    public class ChessPosition : Position
    {
        public ChessPosition() : base(8, 8)
        {
        }

        public override Piece[][] PieceArray
        {
            get;  // what if this was just an implementation detail.
        }

        // what if my "array" was a FEN-like system.
        [Flags]
        public enum CastlingRights
        {
            None = 0,
            WhiteKingSide = 1 << 0,
            WhiteQueenSide = 1 << 1,
            BlackKingSide = 1 << 2,
            BlackQueenSide = 1 << 3
        }

        public Square EnPassantSquare { get; }

        public override Color Turn { get; }
        public CastlingRights CastlingLeft { get; }
        public override int PlyCount { get; }
        public override int ReversiblePlyCount { get; } // 100 plys == 50 move

        string _fenmemoized;
        string MakeFEN()
        {
            if(!string.IsNullOrEmpty(_fenmemoized))
                return _fenmemoized;

            var sb = new StringBuilder("Chess: ");
            var emptySquareCount = 0;

            // This whole sequence here would be soooo much easier with F#'s printfn.
            foreach(var rank in PieceArray)
            {
                foreach(var piece in rank)
                    if(piece == null)
                        emptySquareCount++;
                    else
                    {
                        if(emptySquareCount > 0)
                        {
                            sb.Append(emptySquareCount);
                            emptySquareCount = 0;
                        }
                        sb.Append(piece.FENChar);
                    }
                if(emptySquareCount > 0)
                {
                    sb.Append(emptySquareCount);
                    emptySquareCount = 0;
                }
                sb.Append('/');
            }

            var formattedPosition = sb.Remove(sb.Length - 1, 1).ToString(); // remove the last '/'

            #region CastlingRights
            var sbCastling = new StringBuilder();

            if(CastlingLeft.HasFlag(CastlingRights.WhiteKingSide))
                sbCastling.Append('K');

            if(CastlingLeft.HasFlag(CastlingRights.WhiteQueenSide))
                sbCastling.Append('Q');

            if(CastlingLeft.HasFlag(CastlingRights.BlackKingSide))
                sbCastling.Append('k');

            if(CastlingLeft.HasFlag(CastlingRights.BlackQueenSide))
                sbCastling.Append('q');

            if(sbCastling.Length == 0)
                sbCastling.Append('-');

            var castling = sbCastling.ToString();
            #endregion

            _fenmemoized =
                string.Format
                (
                    "{0} {1} {2} {3} {4} {5}",
                    formattedPosition,
                    Turn == Color.White ? 'w' : 'b',
                    castling,
                    EnPassantSquare,
                    ReversiblePlyCount,
                    PlyCount / 2
                );

            return _fenmemoized;
        }

        public override bool Equals(Position other) => GetType().Equals(other.GetType()) && ToString() == other.ToString();
        public override bool Equals(object obj) => obj is Position && Equals(obj as Position);
        public override int GetHashCode() => MakeFEN().GetHashCode();
        public override string ToString() => MakeFEN();
    }
}