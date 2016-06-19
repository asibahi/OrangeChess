using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrangeChess;
using static OrangeChess.PredefinedMoves;

namespace OrangeClassicalChess
{
    public class King : Piece
    {
        public King(Color clr) : base(clr, 'K', KingMoves) { }
    }

    public class Queen : Piece
    {
        public Queen(Color clr) : base(clr, 'Q', QueenMoves) { }
    }

    public class Knight : Piece
    {
        public Knight(Color clr) : base(clr, 'N', KnightMoves) { }
    }

    public class Bishop : Piece
    {
        public Bishop(Color clr) : base(clr, 'B', BishopMoves) { }
    }

    public class Rook : Piece
    {
        public Rook(Color clr) : base(clr, 'R', RookMoves) { }
    }

    public class Pawn : Piece
    {
        public Pawn(Color clr) : base(clr, 'P', PawnMoves) { }
    }
}
