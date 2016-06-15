using NUnit.Framework;
using OrangeChess;

namespace OrangeChessTests
{
    [TestFixture]
    public class MoveTests
    {
        [Test]
        public void MoveDefaultCtor_PurposeIsToMoveAndCapture_ReturnsTrue()
        {
            var mv = new Move();

            Assert.AreEqual(Move.Purpose.toMoveandCapture, mv.MovePurpose);
        }

        [Test]
        public void DifferentCtors_EquivelantInput_AllAreEqual()
        {
            var mv1 = new Move(1, 0);
            var mv2 = new Move(1, 0, false);
            var mv3 = new Move(1, 0, Move.Purpose.toMoveandCapture);

            Assert.True(mv1 == mv2);
            Assert.True(mv1 == mv3);
            Assert.True(mv3 == mv2);
        }

        [Test]
        public void DifferentCtors_EquivelantInput_AllHashCodesAreEqual()
        {
            var mv1 = new Move(1, 0).GetHashCode();
            var mv2 = new Move(1, 0, false).GetHashCode();
            var mv3 = new Move(1, 0, Move.Purpose.toMoveandCapture).GetHashCode();

            Assert.True(mv1 == mv2);
            Assert.True(mv1 == mv3);
            Assert.True(mv3 == mv2);
        }

        [Test]
        public void MovePurposeCtor_DifferentInPurpose_AllAreDifferent()
        {
            var mv1 = new Move(1, 0, Move.Purpose.toCaptureOnly);
            var mv2 = new Move(1, 0, Move.Purpose.toMoveandCapture);
            var mv3 = new Move(1, 0, Move.Purpose.toMoveOnly);

            Assert.False(mv1 == mv2);
            Assert.False(mv2 == mv3);
            Assert.False(mv3 == mv1);
        }

        [Test]
        public void MovePurposeCtor_DifferentInPurpose_AllHashcodesAreDifferent()
        {
            var mv1 = new Move(1, 0, Move.Purpose.toCaptureOnly).GetHashCode();
            var mv2 = new Move(1, 0, Move.Purpose.toMoveandCapture).GetHashCode();
            var mv3 = new Move(1, 0, Move.Purpose.toMoveOnly).GetHashCode();

            Assert.False(mv1 == mv2);
            Assert.False(mv2 == mv3);
            Assert.False(mv3 == mv1);
        }

        [Test]
        public void MoveGliderCtor_DifferentInGlide_AreDifferent()
        {
            var mv1 = new Move(1, 0, false);
            var mv2 = new Move(1, 0, true);

            Assert.False(mv1 == mv2);
        }

        [Test]
        public void MoveGliderCtor_DifferentInGlide_HashCodesAreDifferent()
        {
            var mv1 = new Move(1, 0, false).GetHashCode();
            var mv2 = new Move(1, 0, true).GetHashCode();

            Assert.False(mv1 == mv2);
        }
    }
}