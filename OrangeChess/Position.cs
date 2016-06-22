using System;

namespace OrangeChess
{
    public abstract class Position : IEquatable<Position>
    {
        protected Position(sbyte f, sbyte r)
        {
            FileCount = f;
            RankCount = r;
        }

        public sbyte FileCount { get; }
        public sbyte RankCount { get; }

        public abstract Piece[,] PieceArray { get; }
        public abstract Color Turn { get; }

        public abstract int PlyCount { get; }
        public abstract int ReversiblePlyCount { get; }

        public abstract bool Equals(Position other);
    }
}