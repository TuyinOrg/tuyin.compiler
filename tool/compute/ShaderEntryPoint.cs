namespace compute
{
    public class ShaderEntryPoint                                       
    {
        public ShaderEntryPoint(ShaderExecutionMode mode, ShaderEntryPointSize size, string entryPoint, Interface[] interfaces)
        {
            Mode = mode;
            EntryPointName = entryPoint;
            Interfaces = interfaces;
            Size = size;
        }

        public ShaderExecutionMode Mode { get; }

        public ShaderEntryPointSize Size { get; }

        public string EntryPointName { get; }

        public Interface[] Interfaces { get; }
    }
}
