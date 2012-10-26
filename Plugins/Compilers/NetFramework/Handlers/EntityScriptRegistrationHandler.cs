using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CryEngine.Extensions;
using CryEngine.Initialization;

namespace CryEngine.Compilers.NET.Handlers
{
    internal class EntityScriptRegistrationHandler : IScriptRegistrationParamsHandler
    {
        bool TryGetProperty(MemberInfo memberInfo, out EntityProperty property)
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

        public IScriptRegistrationParams GetScriptRegistrationParams(Type type)
        {
            var entityRegistrationParams = new EntityRegistrationParams();

            //LoadFlowNode(ref script, true);

            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var folders = type.GetNestedTypes(flags).Where(x => x.ContainsAttribute<EditorPropertyFolderAttribute>());
            var members = type.GetMembers(flags);
            var entityProperties = new List<EntityProperty>();

            EntityProperty property;
            members.ForEach(member =>
            {
                if (TryGetProperty(member, out property))
                    entityProperties.Add(property);
            });

            folders.ForEach(folder => folder.GetMembers().ForEach(member =>
                                                                      {
                                                                          if (TryGetProperty(member, out property))
                                                                          {
                                                                              property.Folder = folder.Name;
                                                                              entityProperties.Add(property);
                                                                          }
                                                                      }));

            entityRegistrationParams.Properties = entityProperties.ToArray();

            EntityAttribute entAttribute;
            if (type.TryGetAttribute(out entAttribute))
            {
                entityRegistrationParams.Name = entAttribute.Name;
                entityRegistrationParams.Category = entAttribute.Category;
                entityRegistrationParams.EditorHelper = entAttribute.EditorHelper;
                entityRegistrationParams.EditorIcon = entAttribute.Icon;
                entityRegistrationParams.Flags = entAttribute.Flags;
            }

            return entityRegistrationParams;

        }
    }
}
