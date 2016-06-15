using System;

namespace OrangeChess
{
    public struct Move : IEquatable<Move>
    {
        /*   
         *   Four Major Directions:
         *   
         *   Rank Differemce:
         *      1. Forward   (+)  // relative to the player.
         *      2. Backward  (-)  // relative to the player.
         *   File Difference:       
         *      3. Kingside  (+)  // towards White's right, Black's left, aka the Kingside.    
         *      4. Queenside (-)  // towards White's left, Black's right, aka the Queenside.
         *
         *   This allows ONE definition for Pawn movement and, say, Shogi pieces. But say, more 
         *   specific directions for Castling.
         *
         *   Signs indicate how things should change from WHITE's POV.
         *   For Black, signs for Forward and Backward are reversed.
         *  
         */

        [Flags]
        public enum Purpose
        {
            toMoveOnly = 1,
            toCaptureOnly = 2,
            toMoveandCapture = toMoveOnly | toCaptureOnly
        }

        Move(sbyte rd, sbyte fd, bool gl, Purpose mt)
        {
            RankDifference = rd;
            FileDifference = fd;
            Glide = gl;
            _purpose = mt;
        }

        /// <summary>Generic Move to mark gliders ala Bishops and Rooks </summary>
        /// <param name="rd">Rank Difference. +1 is one rank forward.</param>
        /// <param name="fd">File Difference. +1 is one file to the Kingside.</param>
        /// <param name="gl">Does it Glide? true if it moves across open lines.</param>
        public Move(sbyte rd, sbyte fd, bool gl)
            : this(rd, fd, gl, Purpose.toMoveandCapture)
        {
        }

        /// <summary>Default Step Move, ala for Kings or Knights.</summary>
        /// <param name="rd">Rank Difference. +1 is one rank forward.</param>
        /// <param name="fd">File Difference. +1 is one file to the Kingside.</param>
        public Move(sbyte rd, sbyte fd)
            : this(rd, fd, false, Purpose.toMoveandCapture)
        {
        }


        /// <summary>To define moves where a piece can only move or capture but not both, ala Pawns.</summary>
        /// <param name="rd">Rank Difference. +1 is one rank forward.</param>
        /// <param name="fd">File Difference. +1 is one file to the Kingside.</param>
        /// <param name="mp">Move Type, enum options should be self-explanatory.</param>
        public Move(sbyte rd, sbyte fd, Purpose mp)
            : this(rd, fd, false, mp)
        {
        }

        /// <summary>Change in Rank (row) after the move. Positive is Forward, which changes per player.</summary>
        public sbyte RankDifference { get; }

        /// <summary>Change in File (column) after the move. Positive is Kingside.</summary>
        public sbyte FileDifference { get; }

        /// <summary> Whether the piece can "glide" across the board.</summary>
        public bool Glide { get; }

        Purpose _purpose;
        /// <summary>To distinguish between moves that can capture and moves that can't. Useful to define Pawn moves. </summary>
        public Purpose MovePurpose => _purpose == 0 ? Purpose.toMoveandCapture : _purpose;

        public override string ToString()
            => $"({RankDifference}, {FileDifference}, {(Glide ? "glide" : "step")}, {MovePurpose})";

        public bool Equals(Move other)
            => RankDifference == other.RankDifference
            && FileDifference == other.FileDifference
            && Glide == other.Glide
            && MovePurpose == other.MovePurpose;

        public override bool Equals(object obj)
            => (obj is Move) && Equals((Move)obj);

        public override int GetHashCode()
            => (RankDifference.GetHashCode() * 31)
             ^ (FileDifference.GetHashCode() * 11)
             ^ (Glide.GetHashCode() * 7)
             ^ MovePurpose.GetHashCode();

        public static bool operator ==(Move m1, Move m2) => m1.Equals(m2);
        public static bool operator !=(Move m1, Move m2) => !m1.Equals(m2);
    }
}
