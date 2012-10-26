using System;
using System.Reflection;
using CryEngine.Extensions;

namespace CryEngine.Compilers.NET.Extensions
{
    internal static class MemberInfoExtensions
    {
        internal static bool TryGetProperty(this MemberInfo memberInfo, out EntityProperty property)
        {
            EditorPropertyAttribute propertyAttribute;
            if (memberInfo.TryGetAttribute(out propertyAttribute))
            {
                Type memberType = null;
                switch (memberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        memberType = ((FieldInfo)memberInfo).FieldType;
                        break;
                    case MemberTypes.Property:
                        memberType = ((PropertyInfo)memberInfo).PropertyType;
                        break;
                }

                var limits = new EntityPropertyLimits(propertyAttribute.Min, propertyAttribute.Max);

                property = new EntityProperty(memberInfo.Name, propertyAttribute.Description, Entity.GetEditorType(memberType, propertyAttribute.Type), limits, propertyAttribute.Flags);
                return true;
            }

            property = new EntityProperty();
            return false;
        }
    }
}
