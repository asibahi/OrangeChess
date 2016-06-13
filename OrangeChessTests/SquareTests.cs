using System;
using NUnit.Framework;
using OrangeChess;

namespace OrangeChessTests
{
    [TestFixture]
    public class SquareTests
    {
        [Test]
        public void DefaultSquareCotr_EvaluatesToA1()
        {
            var sq = new Square();

            Assert.AreEqual("a1", sq.ToString());
        }

        [TestCase("a2")]
        [TestCase("h2")]
        [TestCase("r5")]
        [TestCase("s26")]
        [TestCase("l25")]
        [TestCase("j1")]
        public void SquareStringCtor_ValidArguments_Works(string str)
        {
            var sq = new Square(str);

            Assert.AreEqual(str.ToLower(), sq.ToString());
        }

        [TestCase("a")]
        [TestCase("")]
        [TestCase("5r")]
        [TestCase("s100")]
        [TestCase("435")]
        [TestCase("fd")]
        [TestCase(null)]
        [TestCase("$#5")]
        [TestCase("ب4")]
        public void SquareStringCtor_InvalidArguments_Throws(string str)
        {
            Assert.Throws<ArgumentException>(() => new Square(str));
        }

        [TestCase('a', 3, "a3")]
        [TestCase('h', 25, "h25")]
        [TestCase('D', 20, "d20")]
        [TestCase('z', 3, "z3")]
        [TestCase('j', 13, "j13")]
        [TestCase('a', 1, "a1")]
        public void SquareFileRankCtor_ValidArguments_Works(char f, byte r, string expected)
        {
            var sq = new Square(f, r);

            Assert.AreEqual(expected, sq.ToString());
        }

        [TestCase('a', 27)]
        [TestCase('i', 2)]
        [TestCase('f', 0)]
        [TestCase('ب', 3)]
        [TestCase(' ', 1)]
        public void SquareFileRankCtor_InvalidArguments_Throws(char f, byte r)
        {
            Assert.Throws<ArgumentException>(() => new Square(f, r));
        }

        [TestCase('a', 1, "a1")]
        [TestCase('b', 2, "b2")]
        [TestCase('h', 8, "h8")]
        [TestCase('j', 10, "j10")]
        public void SquareEquals_EqualArguments_ReturnsTrue(char f, byte r, string str)
        {
            var sq1 = new Square(f, r);
            var sq2 = new Square(str);

            Assert.True(sq1 == sq2);
            Assert.True(sq1.Equals(sq2));
            Assert.True(sq1.GetHashCode() == sq2.GetHashCode());

            Assert.False(sq1 != sq2); // I am not sure about multiple Asserts like that
        }
    }
}
