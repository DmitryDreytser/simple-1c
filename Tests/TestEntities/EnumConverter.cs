using System;
using System.ComponentModel;
using Simple1C.Impl;
using Simple1C.Impl.Com;

namespace Simple1C.Tests.TestEntities
{
    internal class EnumConverter
    {
        private readonly GlobalContext globalContext;

        public EnumConverter(GlobalContext globalContext)
        {
            this.globalContext = globalContext;
        }

        public object ConvertTo1C<T>(T enumValue, string enumName)
            where T: struct
        {
            dynamic self = this;
            return self.Convert(enumValue);
        }

        public object Convert(LegalForm legalForm)
        {
            switch (legalForm)
            {
                case LegalForm.Ip:
                    return EnumValue("�������������������������", "��������������");
                case LegalForm.Organization:
                    return EnumValue("�������������������������", "���������������");
                default:
                    throw new InvalidEnumArgumentException(string.Format("unexpected value [{0}]", legalForm));
            }
        }

        public object Convert(CounterpartContractKind value)
        {
            switch (value)
            {
                case CounterpartContractKind.Outgoing:
                    return EnumValue("�������������������������", "������������");
                case CounterpartContractKind.Incoming:
                    return EnumValue("�������������������������", "������������");
                case CounterpartContractKind.Others:
                    return EnumValue("�������������������������", "������");
                case CounterpartContractKind.OutgoingWithComitent:
                    return EnumValue("�������������������������", "��������������������");
                case CounterpartContractKind.OutgoingWithAgency:
                    return EnumValue("�������������������������", "�����������������������");
                case CounterpartContractKind.IncomingWithComitent:
                    return EnumValue("�������������������������", "�����������");
                case CounterpartContractKind.IncomingWithAgency:
                    return EnumValue("�������������������������", "��������������");
                default:
                    throw new ArgumentOutOfRangeException("value", value, null);
            }
        }

        private dynamic ComObject()
        {
            return globalContext.ComObject;
        }

        private dynamic EnumValue(string enumName, string enumValue)
        {
            var enumsObject = ComObject().������������;
            var enumObject = ComHelpers.GetProperty(enumsObject, enumName);
            return ComHelpers.GetProperty(enumObject, enumValue);
        }
    }
}