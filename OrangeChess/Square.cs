using System;
using System.Linq;

namespace OrangeChess
{
    // I don't even know if I want this !!
    public struct Square : IEquatable<Square>
    {
        const string _fileValues = "abcdefghjklmnopqrstuvwxyz"; // no i
        byte _file; // starts from 0 at the Queenside to whatever on the Kingside
        byte _rank; // starts from 0 at White's side  to whatever on Black's side

        public Square(char file, byte rank) : this()
        {
            File = file;
            Rank = rank;
        }

        public Square(string position) : this()
        {
            byte tmp;
            if(string.IsNullOrWhiteSpace(position) || position.Length < 2 || !byte.TryParse(position.Substring(1), out tmp))
                throw new ArgumentException("Algebraic notation format not recognized.");

            File = position[0];
            Rank = tmp;
        }

        public char File
        {
            get { return _fileValues[_file]; }
            private set
            {
                if(char.IsLetter(value) && _fileValues.ToCharArray().Contains(char.ToLower(value)))
                    _file = (byte)_fileValues.IndexOf(char.ToLower(value));
                else
                    throw new ArgumentException("File must be an English letter other than 'i'");
            }
        }

        public byte Rank
        {
            // to correct for 0 being the default value
            get { return (byte)(_rank + 1); }
            private set
            {
                if(value <= 0 || value > 26)
                    throw new ArgumentException("Rank must be between 1 and 26 inclusive");

                _rank = (byte)(value - 1);
            }
        }

        public override string ToString() => (File + Rank.ToString());
        public bool Equals(Square other) => _rank == other._rank && _file == other._file;
        public override bool Equals(object obj) => (obj is Square) && Equals((Square)obj);
        public override int GetHashCode() => (Rank.GetHashCode() * 31) ^ File.GetHashCode();
        public static bool operator ==(Square sq1, Square sq2) => sq1.Equals(sq2);
        public static bool operator !=(Square sq1, Square sq2) => !sq1.Equals(sq2);
    }
}