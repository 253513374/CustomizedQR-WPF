namespace QrCodeEncoding.Versions
{
    internal struct VersionControlStruct
    {
        internal VersionDetail VersionDetail { get; set; }
        internal bool isContainECI { get; set; }
        internal BitList ECIHeader { get; set; }
    }
}