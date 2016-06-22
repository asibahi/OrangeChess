using System;
using System.Linq;

namespace OrangeChess
{
    /// <summary>
    /// A Struct to quickly indicate the position of a Piece on the Board in a human readable fashion.
    /// </summary>
    public struct Square : IEquatable<Square>
    {
        const string _fileValues = "abcdefghjklmnopqrstuvwxyz"; // no i
        byte _file; // starts from 0 at the Queenside to whatever on the Kingside
        byte _rank; // starts from 0 at White's side  to whatever on Black's side

        // internal ctor to quickly translate Array positions to Squares. Might not be needed.
        internal Square(byte fileValue, byte rankValue)
        {
            if(fileValue < 0 || fileValue > 24 || rankValue < 0 || rankValue > 24)
                throw new ArgumentException("File Value and Rank Value must be between 0 and 24 inclusive");

            _file = fileValue;
            _rank = rankValue;
        }

        /// <summary>Internal File Value.</summary>
        public byte FileValue => _file;

        /// <summary>Internal Rank Value.</summary>
        public byte RankValue => _rank;

        /// <summary>Creates a Square with the provided value..</summary>
        /// <param name="file">File letter: any English letter except 'i'.</param>
        /// <param name="rank">Rank number: any number between 1 and 25 inclusive.</param>
        public Square(char file, byte rank) : this()
        {
            File = file;
            Rank = rank;
        }

        /// <summary>Creates a Square with the provided value.</summary>
        /// <param name="position">Square position in algebraic notation. Must be within a 25x25 chess board.</param>
        public Square(string position) : this()
        {
            byte tmp;
            if(position == null || position.Length < 2 || !byte.TryParse(position.Substring(1), out tmp))
                throw new ArgumentException("Algebraic notation format not recognized.");

            File = position[0];
            Rank = tmp;
        }

        /// <summary>File letter</summary>
        public char File
        {
            get { return _fileValues[_file]; }
            private set
            {
                if(!char.IsLetter(value) || !_fileValues.ToCharArray().Contains(char.ToLower(value)))
                    throw new ArgumentException("File must be an English letter other than 'i'");

                _file = (byte)_fileValues.IndexOf(char.ToLower(value));
            }
        }

        /// <summary>Rank number</summary>
        public byte Rank
        {
            // to correct for 0 being the default value
            get { return (byte)(_rank + 1); }
            private set
            {
                if(value <= 0 || value > 25)
                    throw new ArgumentException("Rank must be between 1 and 25 inclusive");

                _rank = (byte)(value - 1);
            }
        }

        public override string ToString() => (File + Rank.ToString());
        public bool Equals(Square other) => _rank == other._rank && _file == other._file;
        public override bool Equals(object obj) => (obj is Square) && Equals((Square)obj);
        public override int GetHashCode() => (Rank * 7) ^ File;
        public static bool operator ==(Square sq1, Square sq2) => sq1.Equals(sq2);
        public static bool operator !=(Square sq1, Square sq2) => !sq1.Equals(sq2);
    }
}