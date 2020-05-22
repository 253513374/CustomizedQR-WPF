using  QrCodeEncoding.Versions;

namespace QrCodeEncoding.DataEncodation
{
    internal struct EncodationStruct
    {
        internal VersionDetail VersionDetail { get; set; }
        internal Mode Mode { get; set; }
        internal BitList DataCodewords { get; set; }

        internal EncodationStruct(VersionControlStruct vcStruct)
            : this()
        {
            this.VersionDetail = vcStruct.VersionDetail;
        }
    }
}