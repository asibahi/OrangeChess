using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace OrangeChess
{
    public static class PredefinedMoves
    {
        public static IEnumerable<Move> KnightMoves
            => ImmutableHashSet.Create<Move>()
                               .Add(new Move(1, 2))
                               .Add(new Move(2, 1))
                               .Add(new Move(-1, 2))
                               .Add(new Move(2, -1))
                               .Add(new Move(-2, 1))
                               .Add(new Move(1, -2))
                               .Add(new Move(-1, -2))
                               .Add(new Move(-2, -1)).AsEnumerable(); // I really like the syntax.

        public static IEnumerable<Move> RookMoves
            => ImmutableHashSet.Create<Move>()
                               .Add(new Move(1, 0, true))
                               .Add(new Move(-1, 0, true))
                               .Add(new Move(0, 1, true))
                               .Add(new Move(0, -1, true)).AsEnumerable();

        public static IEnumerable<Move> BishopMoves
            => ImmutableHashSet.Create<Move>()
                               .Add(new Move(1, 1, true))
                               .Add(new Move(-1, -1, true))
                               .Add(new Move(-1, 1, true))
                               .Add(new Move(1, -1, true)).AsEnumerable();

        // no need to check for duplicates because I am not an idiot
        public static IEnumerable<Move> QueenMoves => BishopMoves.Concat(RookMoves);

        // Castling is defined per game.
        public static IEnumerable<Move> KingMoves
            => ImmutableHashSet.Create<Move>()
                               .Add(new Move(1, 1))
                               .Add(new Move(-1, -1))
                               .Add(new Move(-1, 1))
                               .Add(new Move(1, -1))
                               .Add(new Move(1, 0))
                               .Add(new Move(-1, 0))
                               .Add(new Move(0, 1))
                               .Add(new Move(0, -1)).AsEnumerable();

        // Initial Double Step and Promotion are defined by game.
        public static IEnumerable<Move> PawnMoves
            => ImmutableHashSet.Create<Move>()
                               .Add(new Move(1, 0, Move.Purpose.toMoveOnly))
                               .Add(new Move(1, 1, Move.Purpose.toCaptureOnly))
                               .Add(new Move(1, -1, Move.Purpose.toCaptureOnly)).AsEnumerable();
    }
}
