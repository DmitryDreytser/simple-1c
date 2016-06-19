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
        
        public object Convert(NdsRate ndsRate)
        {
            switch (ndsRate)
            {
                case NdsRate.NoNds:
                    return EnumValue("���������", "������");
                case NdsRate.Nds10:
                    return EnumValue("���������", "���10");
                case NdsRate.Nds18:
                    return EnumValue("���������", "���18");
                case NdsRate.Nds20:
                    return EnumValue("���������", "���20");
                case NdsRate.Nds0:
                    return EnumValue("���������", "���0");
                case NdsRate.Nds10110:
                    return EnumValue("���������", "���10_110");
                case NdsRate.Nds18118:
                    return EnumValue("���������", "���18_118");
                case NdsRate.Nds20120:
                    return EnumValue("���������", "���20_120");
                default:
                    throw new ArgumentOutOfRangeException("ndsRate", ndsRate, null);
            }
        }

        public object Convert(IncomingOperationKind incomingOperationKind)
        {
            switch (incomingOperationKind)
            {
                case IncomingOperationKind.Goods:
                    return EnumValue("�����������������������������������", "������");
                case IncomingOperationKind.Services:
                    return EnumValue("�����������������������������������", "������");
                case IncomingOperationKind.BuyingCommission:
                    return EnumValue("�����������������������������������", "���������������");
                default:
                    throw new ArgumentOutOfRangeException("incomingOperationKind", incomingOperationKind, null);
            }
        }

        public object Convert(AdvanceWay advanceWay)
        {
            switch (advanceWay)
            {
                case AdvanceWay.Automatically:
                    return EnumValue("��������������������", "�������������");
                case AdvanceWay.ByDocument:
                    return EnumValue("��������������������", "�����������");
                case AdvanceWay.DontTakeIntoAccount:
                    return EnumValue("��������������������", "������������");
                default:
                    throw new ArgumentOutOfRangeException("advanceWay", advanceWay, null);
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