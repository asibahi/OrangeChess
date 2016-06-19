using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NUnit.Framework;
using OrangeChess;

namespace OrangeChessTests
{
    public class TestPiece : Piece
    {
        public static IEnumerable<Move> TestMoves
         => ImmutableHashSet.Create<Move>()
                            .Add(new Move(1, 1)).AsEnumerable();

        public TestPiece(Color clr, char ch) : base(clr, ch, TestMoves) { }
    }

    [TestFixture]
    public class PieceTests
    {
        [TestCase('ب')]
        [TestCase('4')] // Digit
        [TestCase('*')]
        [TestCase(' ')] // Whitespace
        [TestCase('ط')] // Arabic
        [TestCase('ה')] // Hebrew
        [TestCase('ש')] // Hebrew
        [TestCase('枠')] // Japanese
        [TestCase('へ')] // Japanese
        [TestCase('.')]
        public void CharInPieceCtorTest_InvalidInput_Throws(char ch)
        {
            Assert.Throws<ArgumentException>(() => new TestPiece(Color.White, ch));
        }

        [TestCase('s')]
        [TestCase('S')]
        [TestCase('D')]
        [TestCase('Λ')]
        [TestCase('λ')] // Greek
        [TestCase('з')]
        [TestCase('Ё')] // Russian
        [TestCase('խ')] // Armenian
        [TestCase('Ş')] // Turkish
        [TestCase('ь')]
        public void CharInPieceCtorTest_ValidInput_Works(char ch)
        {
            Assert.DoesNotThrow(() => new TestPiece(Color.White, ch));
        }

        [TestCase(Color.White)]
        [TestCase(Color.Black)]
        public void ColorInPieceCtorTest_ValidInput_Works(Color clr)
        {
            Assert.DoesNotThrow(() => new TestPiece(clr, 'K'));
        }

        [TestCase(Color.None)]
        [TestCase((Color)4)]
        [TestCase((Color)3)]
        [TestCase(Color.White | Color.Black)]
        public void ColorInPieceCtorTest_InvalidInput_Throws(Color clr)
        {
            Assert.Throws<ArgumentException>(() => new TestPiece(clr, 'K'));
        }

        [TestCase(Color.White, 'k', 'K')]
        [TestCase(Color.Black, 'K', 'k')]
        public void FENCharProperty_CharCapitalizedCorrectly_Verifies(Color clr, char ctor, char expected)
        {
            var king = new TestPiece(clr, ctor);

            Assert.AreEqual(expected, king.FENChar);
        }
    }
}
