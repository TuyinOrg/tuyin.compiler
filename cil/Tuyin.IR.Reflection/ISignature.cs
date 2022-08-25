using System;
using System.Linq;

namespace Tuyin.IR.Reflection
{
    public interface ISignature
    {
        string Content { get; }

        bool Is(ISignature other);
    }

    abstract class MetadataSignature : ISignature, IEquatable<ISignature>
    {
        public abstract string Content { get; }

        public abstract bool Is(ISignature other);

        public override bool Equals(object obj)
        {
            if (obj is ISignature sign)
                return Equals(sign);

            return false;
        }

        public bool Equals(ISignature other)
        {
            return Is(other);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return Content;
        }
    }

    class FileSignature : MetadataSignature
    {
        public string FileName { get; }

        public override string Content => FileName;

        public FileSignature(string fileName)
        {
            FileName = fileName;
        }

        public override bool Is(ISignature other)
        {
            if (other is FileSignature otherFS)
                return FileName == otherFS.FileName;

            return false;
        }

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }
    }

    class LibrarySignature : MetadataSignature
    {
        public string FileName { get; }

        public string LibraryName { get; }

        public override string Content => $"{FileName}.{LibraryName}";

        public LibrarySignature(string name, string fileName)
        {
            LibraryName = name;
            FileName = fileName;
        }

        public override bool Is(ISignature other)
        {
            if (other is LibrarySignature otherFS)
                return FileName.Equals(otherFS.FileName);

            return false;
        }
    }

    class ModuleSignature : MetadataSignature
    {
        public override string Content => FileName;

        public string FileName { get; }

        public ModuleSignature(string fileName) 
        {
            FileName = fileName;
        }

        public override bool Is(ISignature other)
        {
            if (other is ModuleSignature otherFS)
                return FileName.Equals(otherFS.FileName);

            return false;
        }

        public override string ToString()
        {
            return FileName;
        }
    }

    class MemberSignature : MetadataSignature
    {
        private readonly ModuleSignature mModule;

        public override string Content { get; }

        public MemberSignature(ModuleSignature library, params string[] fullPath)
        {
            mModule = library;
            Content = string.Join(".", fullPath);
        }

        public override bool Is(ISignature other)
        {
            if (other is MemberSignature otherFS)
            {
                return mModule.Is(otherFS.mModule) &&
                    Content.SequenceEqual(otherFS.Content);
            }

            return false;
        }
    }
}
