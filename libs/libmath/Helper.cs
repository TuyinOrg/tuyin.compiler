using System.ComponentModel;
using System.Reflection;

namespace libmath
{
    static class Helper
    {
        /// <summary> 
        /// 获取枚举变量值的 Description 属性 
        /// </summary> 
        /// <param name="obj">枚举变量</param> 
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns> 
        public static string GetHelperDescrption(this object obj)
        {
            return obj.GetHelperDescrption(!obj.GetType().IsEnum);
        }

        /// <summary> 
        /// 获取枚举变量值的 Description 属性 
        /// </summary> 
        /// <param name="obj">枚举变量</param> 
        /// <param name="isTop">是否改变为返回该类、枚举类型的头 Description 属性，而不是当前的属性或枚举变量值的 Description 属性</param> 
        /// <returns>如果包含 Description 属性，则返回 Description 属性的值，否则返回枚举变量值的名称</returns> 
        public static string GetHelperDescrption(this object obj, bool isTop)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            try
            {
                var _enumType = obj.GetType();
                DescriptionAttribute dna = null;
                if (isTop)
                {
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(_enumType, typeof(DescriptionAttribute));
                }
                else
                {
                    FieldInfo fi = _enumType.GetField(Enum.GetName(_enumType, obj));
                    dna = (DescriptionAttribute)Attribute.GetCustomAttribute(
                       fi, typeof(DescriptionAttribute));
                }
                if (dna != null && string.IsNullOrEmpty(dna.Description) == false)
                    return dna.Description;
            }
            catch
            {
            }
            return obj.ToString();
        }
    }
}
