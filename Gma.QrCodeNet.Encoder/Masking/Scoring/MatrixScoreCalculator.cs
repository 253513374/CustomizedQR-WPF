using System.Linq;
using QrCodeEncoding.DrawQrCode.ConfigJson;
namespace QrCodeEncoding.Masking.Scoring
{
    internal static class MatrixScoreCalculator
    {
        internal static BitMatrix GetLowestPenaltyMatrix(this TriStateMatrix matrix, ErrorCorrectionLevel errorlevel, Pattern mypattern)
        {
            PatternFactory patternFactory = new PatternFactory();
            int score = int.MaxValue;
            int tempScore;
            TriStateMatrix result = new TriStateMatrix(matrix.Width);
            TriStateMatrix triMatrix;
            foreach (Pattern pattern in patternFactory.AllPatterns())
            {
                triMatrix = matrix.Apply(pattern, errorlevel);
                tempScore = triMatrix.PenaltyScore();
                if (tempScore < score)
                {
                    score = tempScore;
                    result = triMatrix;
                }
            }

            //triMatrix = matrix.Apply(pattern, errorlevel);
            //result = triMatrix;
            return result;
        }

        internal static int PenaltyScore(this BitMatrix matrix)
        {
            PenaltyFactory penaltyFactory = new PenaltyFactory();
            return
                penaltyFactory
                .AllRules()
                .Sum(penalty => penalty.PenaltyCalculate(matrix));
        }

       
    }
}