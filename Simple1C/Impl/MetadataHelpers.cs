using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Simple1C.Impl.Com;
using Simple1C.Impl.Generation;
using Simple1C.Interface;

namespace Simple1C.Impl
{
    internal static class MetadataHelpers
    {
        private static readonly string[] standardPropertiesToExclude =
        {
            "�������������������������",
            "������"
        };

        public static ConfigurationItemDescriptor GetDescriptor(ConfigurationScope scope)
        {
            return descriptors[scope];
        }

        public static IEnumerable<object> GetAttributes(object comObject, ConfigurationItemDescriptor descriptor)
        {
            var standardAttributes = ComHelpers.GetProperty(comObject, "��������������������");
            var isChartOfAccounts = Call.���(comObject) == "������������";
            foreach (var attr in (IEnumerable)standardAttributes)
            {
                var name = Call.���(attr);
                if (isChartOfAccounts && name != "���" && name != "������������")
                    continue;
                if (standardPropertiesToExclude.Contains(name))
                    continue;
                yield return attr;
            }
            foreach (var propertyName in descriptor.AttributePropertyNames)
            {
                var attributes = ComHelpers.GetProperty(comObject, propertyName);
                var attributesCount = Call.����������(attributes);
                for (var i = 0; i < attributesCount; ++i)
                    yield return Call.��������(attributes, i);
            }
        }

        private static readonly Dictionary<ConfigurationScope, ConfigurationItemDescriptor> descriptors =
            new Dictionary<ConfigurationScope, ConfigurationItemDescriptor>
            {
                {
                    ConfigurationScope.�����������, new ConfigurationItemDescriptor
                    {
                        AttributePropertyNames = new[] {"���������"},
                        HasTableSections = true
                    }
                },
                {
                    ConfigurationScope.���������,
                    new ConfigurationItemDescriptor
                    {
                        AttributePropertyNames = new[] {"���������"},
                        HasTableSections = true
                    }
                },
                {
                    ConfigurationScope.����������������,
                    new ConfigurationItemDescriptor
                    {
                        AttributePropertyNames = new[] {"���������", "���������", "�������"}
                    }
                },
                {
                    ConfigurationScope.�����������,
                    new ConfigurationItemDescriptor
                    {
                        AttributePropertyNames = new[] {"���������"},
                        HasTableSections = true
                    }
                },
                {
                    ConfigurationScope.�����������������������,
                    new ConfigurationItemDescriptor
                    {
                        AttributePropertyNames = new[] {"���������"},
                        HasTableSections = true
                    }
                }
            };
    }
}