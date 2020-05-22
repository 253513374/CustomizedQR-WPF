using QrCodeEncoding.Masking;
using System.Collections.Generic;

namespace QrCodeEncoding
{
    public class QrEncoder
    {
        public ErrorCorrectionLevel ErrorCorrectionLevel { get; set; }
        public MaskPatternType PatternType { set; get; } 

        /// <summary>
        /// Default QrEncoder will set ErrorCorrectionLevel as M
        /// </summary>
        public QrEncoder()
            : this(ErrorCorrectionLevel.M)
        {
        }

        /// <summary>
        /// QrEncoder with parameter ErrorCorrectionLevel.
        /// </summary>
        /// <param name="errorCorrectionLevel"></param>
        public QrEncoder(ErrorCorrectionLevel errorCorrectionLevel)
        {
            ErrorCorrectionLevel = errorCorrectionLevel;
        }

        /// <summary>
        /// Encode string content to QrCode matrix
        /// </summary>
        /// <exception cref="InputOutOfBoundaryException">
        /// This exception for string content is null, empty or too large</exception>
        public QrCode Encode(string content, MaskPatternType PatternType = MaskPatternType.Type5)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new InputOutOfBoundaryException("Input should not be empty or null");
            }
            else
                return new QrCode(QRCodeEncode.Encode(content, ErrorCorrectionLevel, PatternType));
        }

        /// <summary>
        /// Try to encode content
        /// </summary>
        /// <returns>False if input content is empty, null or too large.</returns>
        public bool TryEncode(string content, out QrCode qrCode)
        {
            try
            {
                qrCode = this.Encode(content);
                return true;
            }
            catch (InputOutOfBoundaryException)
            {
                qrCode = new QrCode();
                return false;
            }
        }

        /// <summary>
        /// Encode byte array content to QrCode matrix
        /// </summary>
        /// <exception cref="InputOutOfBoundaryException">
        /// This exception for string content is null, empty or too large</exception>
        public QrCode Encode(IEnumerable<byte> content)
        {
            if (content == null)
            {
                throw new InputOutOfBoundaryException("Input should not be empty or null");
            }
            else
                return new QrCode(QRCodeEncode.Encode(content, ErrorCorrectionLevel));
        }

        /// <summary>
        /// Try to encode content
        /// </summary>
        /// <returns>False if input content is empty, null or too large.</returns>
        public bool TryEncode(IEnumerable<byte> content, out QrCode qrCode)
        {
            try
            {
                qrCode = this.Encode(content);
                return true;
            }
            catch (InputOutOfBoundaryException)
            {
                qrCode = new QrCode();
                return false;
            }
        }
    }
}