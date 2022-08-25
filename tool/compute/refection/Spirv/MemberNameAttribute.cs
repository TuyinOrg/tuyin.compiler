using System;
using System.Reflection;

namespace Toe.SPIRV
{
    internal class MemberNameAttribute: Attribute
    {
        private readonly string _name;

        public MemberNameAttribute(string name)
        {
            _name = name;
        }

        public string Name => _name;

        public static string GetName(MemberInfo memberInfo)
        {
            var attribute = memberInfo.GetCustomAttribute<MemberNameAttribute>();
            return attribute == null ? memberInfo.Name : attribute.Name;
        }

        public override string ToString()
        {
            return _name ?? base.ToString();
        }
    }
}