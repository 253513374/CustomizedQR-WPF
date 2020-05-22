namespace QrCodeEncoding
{
    /// <summary>
    /// 这个类中有两个变量
    /// BitMatrix 与 QrCode
    /// isContainMatrix 指示 QRCode 中是否包含BitMatrix；
    /// 如果BitMatrix 为null，isContainMatrix 的值为 false.
    /// </summary>
    public class QrCode
    {
        internal QrCode(BitMatrix matrix)
        {
            this.Matrix = matrix;
            this.isContainMatrix = true;
        }

        public QrCode()
        {
            this.isContainMatrix = false;
            this.Matrix = null;
        }

        public bool isContainMatrix
        {
            get;
            private set;
        }

        public BitMatrix Matrix
        {
            get;
            private set;
        }
    }
}