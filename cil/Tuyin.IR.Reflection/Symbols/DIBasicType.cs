namespace Tuyin.IR.Reflection.Symbols
{
    public enum DW_TAG
    {
        DW_TAG_base_type,
        DW_TAG_unspecified_type
    }

    public class DIBasicType
    {
        public string Name { get; }

        public int Size { get; }

        public int Align { get; }

        public DIEncoding Encoding { get; }

        public DW_TAG? Tag { get; }

        public DIBasicType(string name, int size, int align, DIEncoding encoding, DW_TAG? tag = null)
        {
            Name = name;
            Size = size;
            Align = align;
            Encoding = encoding;
            Tag = tag;
        }

        public override string ToString()
        {
            string encoding = string.Empty;
            switch (Encoding)
            {
                case DIEncoding.DW_ATE_address:
                    encoding = "DW_ATE_address";
                    break;
                case DIEncoding.DW_ATE_boolean:
                    encoding = "DW_ATE_boolean";
                    break;
                case DIEncoding.DW_ATE_float:
                    encoding = "DW_ATE_float";
                    break;
                case DIEncoding.DW_ATE_signed:
                    encoding = "DW_ATE_signed";
                    break;
                case DIEncoding.DW_ATE_signed_char:
                    encoding = "DW_ATE_signed_char";
                    break;
                case DIEncoding.DW_ATE_unsigned:
                    encoding = "DW_ATE_unsigned";
                    break;
                case DIEncoding.DW_ATE_unsigned_char:
                    encoding = "DW_ATE_unsigned_char";
                    break;
            }

            string tag = string.Empty;
            if (Tag.HasValue)
            {
                switch (Tag.Value)
                {
                    case DW_TAG.DW_TAG_base_type:
                        tag = "DW_TAG_base_type";
                        break;
                    case DW_TAG.DW_TAG_unspecified_type:
                        tag = "DW_TAG_unspecified_type";
                        break;
                }
            }

            return $"!DIBasicType(name: \"{Name}\", size: {Size}, align: {Align}, encoding: {encoding}" + (Tag.HasValue ? $",tag: {tag}" : string.Empty) + ")";
        }
    }
}
