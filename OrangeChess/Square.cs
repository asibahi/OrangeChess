using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeChess
{
    /// <summary>
    /// Indicates a position on the board. However, it is used to define Moves, valid
    /// or invalid, not to actually indicate Squares on the Board, which uses a [,].
    /// Ideally in future versions of C# this would be a record type, or a data class.
    /// </summary>
    public struct Square : IEquatable<Square>
    {
        const string _fileValues = "abcdefghjklmnopqrstuvwxyz"; // no i
        byte _file; // starts from 0 at the Queenside to whatever on the Kingside
        byte _rank; // starts from 0 at White's side  to whatever on Black's side

        /// <summary>
        /// Square constructor based on file and rank, in algebraic notation.
        /// </summary>
        /// <param name="file">File, or Column on the board.</param>
        /// <param name="rank">Rank, or Row on the board.</param>
        public Square(char file, byte rank) : this()
        {
            File = file;
            Rank = rank;
        }

        /// <summary>
        /// Square constructor based on file and rank in string format, in algebraic notation.
        /// </summary>
        /// <param name="position">Algebraic location of the square.</param>
        public Square(string position) : this()
        {
            byte tmp;
            if(string.IsNullOrWhiteSpace(position) || position.Length < 2 || !byte.TryParse(position.Substring(1), out tmp))
                throw new ArgumentException("Algebraic notation format not recognized.");

            File = position[0];
            Rank = tmp;
        }

        /// <summary>
        /// File, or Column. For example in Chess the King starts on the e file.
        /// </summary>
        public char File
        {
            get
            {
                return _fileValues[_file];
            }

            private set
            {
                if(char.IsLetter(value) && _fileValues.ToCharArray().Contains(char.ToLower(value)))
                    _file = (byte)_fileValues.IndexOf(char.ToLower(value));
                else
                    throw new ArgumentException("File must be an English letter other than 'i'");
            }
        }

        /// <summary>
        /// Rank, or Row. For example in Chess the pawns start on the 2nd rank.
        /// </summary>
        public byte Rank
        {
            get
            {
                // to correct for 0 being the default value
                return (byte)(_rank + 1);
            }

            private set
            {
                if(value <= 0 || value > 26)
                    throw new ArgumentException("Rank must be between 1 and 26 inclusive");

                _rank = (byte)(value - 1);
            }
        }

        /// <summary>
        /// Overrides Object.ToString()
        /// </summary>
        /// <returns>The File and Rank in Algebraic notation.</returns>
        public override string ToString() => (File + Rank.ToString());

        /// <summary> Value Equality based on Rank and File. </summary>
        /// <param name="other">The other Square</param>
        /// <returns>True if both Squares share the rank and file. (Wouldn't they be technically the same square?)</returns>
        public bool Equals(Square other) => _rank == other._rank && _file == other._file;

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj) => (obj is Square) && Equals((Square)obj);

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() => (Rank.GetHashCode() * 31) ^ File.GetHashCode();

        public static bool operator ==(Square sq1, Square sq2) => sq1.Equals(sq2);
        public static bool operator !=(Square sq1, Square sq2) => !sq1.Equals(sq2);
    }
}
