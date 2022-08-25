namespace Tuyin.IR.Reflection.Symbols
{
    public sealed class DIFile
    {
        public string Include { get; }

        public string Directory { get; }

        public DIChecksumKind ChecksumKind { get; }

        public string Checksum { get; }

        internal DIFile(string filename, string dir, DIChecksumKind kind, string checksum) 
        {
            Include = filename;
            Directory = dir;
            ChecksumKind = kind;
            Checksum = checksum;
        }

        public override string ToString()
        {
            if (ChecksumKind != DIChecksumKind.CSK_None)
            {
                var checkkind = string.Empty;
                switch (ChecksumKind)
                {
                    case DIChecksumKind.CSK_MD5:
                        checkkind = "CSK_MD5";
                        break;
                    case DIChecksumKind.CSK_SHA1:
                        checkkind = "CSK_SHA1";
                        break;
                    case DIChecksumKind.CSK_SHA256:
                        checkkind = "CSK_SHA256";
                        break;
                }

                return
                    $"!DIFile(" +
                    $"filename: \"{Include.Replace("\\", "\\\\")}\", " +
                    $"directory: \"{Directory.Replace("\\", "\\\\")}\", " +
                    $"checksumkind: {checkkind}, " +
                    $"checksum: \"{Checksum}\")";
            }
            else
            {
                return
                    $"!DIFile(" +
                    $"filename: \"{Include.Replace("\\", "\\\\")}\", " +
                    $"directory: \"{Directory.Replace("\\", "\\\\")}\")";
            }
        }
    }
}
