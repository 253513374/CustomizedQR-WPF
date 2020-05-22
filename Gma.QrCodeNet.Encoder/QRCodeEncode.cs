using  QrCodeEncoding.DataEncodation;
using  QrCodeEncoding.EncodingRegion;
using  QrCodeEncoding.ErrorCorrection;
using  QrCodeEncoding.Masking;
using  QrCodeEncoding.Masking.Scoring;
using  QrCodeEncoding.Positioning;
using System.Collections.Generic;

namespace QrCodeEncoding
{
    internal static class QRCodeEncode
    {
        internal static BitMatrix Encode(string content, ErrorCorrectionLevel errorLevel, MaskPatternType maskPatternType)
        {
            EncodationStruct encodeStruct = DataEncode.Encode(content, errorLevel);

            return ProcessEncodationResult(encodeStruct, errorLevel, maskPatternType);
        }

        internal static BitMatrix Encode(IEnumerable<byte> content, ErrorCorrectionLevel errorLevel)
        {
            EncodationStruct encodeStruct = DataEncode.Encode(content, errorLevel);

            return ProcessEncodationResult(encodeStruct, errorLevel);
        }

        private static BitMatrix ProcessEncodationResult(EncodationStruct encodeStruct, ErrorCorrectionLevel errorLevel, MaskPatternType maskPatternType= MaskPatternType.Type5)
        {
            BitList codewords = ECGenerator.FillECCodewords(encodeStruct.DataCodewords, encodeStruct.VersionDetail);

            TriStateMatrix triMatrix = new TriStateMatrix(encodeStruct.VersionDetail.MatrixWidth);
             PositioninngPatternBuilder.EmbedBasicPatterns(encodeStruct.VersionDetail.Version, triMatrix);
            triMatrix.EmbedVersionInformation(encodeStruct.VersionDetail.Version);

       

           // triMatrix.EmbedFormatInformation(errorLevel, new Pattern0());
            Pattern pattern = new PatternFactory().CreateByType(maskPatternType);


            triMatrix.EmbedFormatInformation(errorLevel, pattern);
            triMatrix.TryEmbedCodewords(codewords);
            
            return triMatrix.GetLowestPenaltyMatrix(errorLevel, pattern);
        }
    }
}