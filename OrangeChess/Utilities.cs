using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeChess
{
    public static class Utilities
    {
        // shamelessly stolen from http://stackoverflow.com/questions/21986909/convert-multidimensional-array-to-jagged-array-in-c-sharp
        static T[][] RectangularToJagged<T>(T[,] recArray)
        {
            var rowsFstI = recArray.GetLowerBound(0);
            var rowsLstI = recArray.GetUpperBound(0);
            var rowCount = rowsLstI + 1;

            var colsFstI = recArray.GetLowerBound(1);
            var colsLstI = recArray.GetUpperBound(1);
            var colCount = colsLstI + 1;

            T[][] jagArray = new T[rowCount][];

            for(int i = rowsFstI; i <= rowsLstI; i++)
            {
                jagArray[i] = new T[colCount];

                for(int j = colsFstI; j <= colsLstI; j++)
                    jagArray[i][j] = recArray[i, j];
            }

            return jagArray;
        }

        // shamelessly stolen from http://stackoverflow.com/questions/26291609/converting-jagged-array-to-2d-array-c-sharp
        static T[,] JaggedToRectangular<T>(T[][] jagArray)
        {
            try
            {
                var fstDim = jagArray.Length;
                var sndDim = jagArray.GroupBy(row => row.Length).Single().Key;
                // GroupBy reproduces an IEnumerable that is paired with a single Key value. It is all
                // the stuff in the Enumerable that share a value. This is just a quick way to know that
                // all the rows in the jagged array have the same length. By using the Single() method,
                // it makes sure that all rows share the same Length or there would be TWO IGrouping's
                // since it throws InvalidOperationException otherwise.

                var recArray = new T[fstDim, sndDim];

                for(int i = 0; i < fstDim; i++)
                    for(int j = 0; j < sndDim; j++)
                        recArray[i, j] = jagArray[i][j];

                return recArray;
            }
            catch(InvalidOperationException)
            {
                throw new ArgumentException("The given jagged array is not rectangular.");
            }
        }

        public static string ArrayToFEN(Piece[,] pieceArray)
        {
            var workingArray = RectangularToJagged(pieceArray);

            var sb = new StringBuilder();
            var emptySquareCount = 0;

            foreach(var rank in workingArray)
            {
                foreach(var piece in rank)
                {
                    if(piece == null)
                    {
                        emptySquareCount++;
                    }
                    else
                    {
                        if(emptySquareCount > 0)
                        {
                            sb.Append(emptySquareCount);
                            emptySquareCount = 0;
                        }

                        sb.Append(piece.FENChar);
                    }
                }

                if(emptySquareCount > 0)
                {
                    sb.Append(emptySquareCount);
                    emptySquareCount = 0;
                }

                sb.Append('/');
            }

            return sb.Remove(sb.Length - 1, 1).ToString(); // remove the last '/'
        }

        //public static Piece[,] FENToArray(string fenString)
        //{
        //    var strings = fenString.Split('/');

        //    throw new NotImplementedException();
        //}
    }
}
