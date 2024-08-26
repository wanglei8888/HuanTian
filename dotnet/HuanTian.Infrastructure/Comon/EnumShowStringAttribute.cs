namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 字符串列备注显示枚举值
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnumShowStringAttribute : Attribute
    {
        /// <summary>
        /// 枚举显示字符串
        /// </summary>
        /// <param name="enumType"></param>
        public EnumShowStringAttribute(Type enumType)
        {
            EnumType = enumType;
        }
        /// <summary>
        /// 枚举类型
        /// </summary>
        public Type EnumType { get; }
    }
}
