namespace compute
{
    public class Shader                                                 
    {
        internal byte[] ShaderBytes { get; }

        public ShaderInterfaceType[] Structs { get; }

        public ShaderEntryPoint[] EntryPoints { get; }

        public Shader(byte[] shaderBytes, ShaderInterfaceType[] structs, ShaderEntryPoint[] entryPoints)                               
        {
            ShaderBytes = shaderBytes;
            Structs = structs;
            EntryPoints = entryPoints;
        }
    }
}
