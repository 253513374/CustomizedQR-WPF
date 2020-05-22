using System.ComponentModel;

namespace QrCodeEncoding
{
    public enum ErrorCorrectionLevel
    {
        [Description("L-7%")]
        L,
        [Description("M-15%")]
        M,
        [Description("Q-25%")]
        Q,
        [Description("H-30%")]
        H
    }
}