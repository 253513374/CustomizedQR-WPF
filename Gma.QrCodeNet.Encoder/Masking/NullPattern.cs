using System;

namespace QrCodeEncoding.Masking
{
    internal class NullPattern : Pattern
    {
        public override bool this[int i, int j]
        {
            get { return false; }
            set { throw new NotSupportedException(); }
        }

        public override MaskPatternType MaskPatternType
        {
            get { throw new NotSupportedException("Null pattern is not supported in QR code standard."); }
        }
    }
}