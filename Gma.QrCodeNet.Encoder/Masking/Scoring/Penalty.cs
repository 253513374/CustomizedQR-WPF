namespace QrCodeEncoding.Masking.Scoring
{
    public abstract class Penalty
    {
        internal abstract int PenaltyCalculate(BitMatrix matrix);
    }
}