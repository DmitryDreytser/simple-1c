using System;
using System.Collections.Generic;
using System.Linq;
using Simple1C.Impl;
using Simple1C.Impl.Com;

namespace Simple1C.Tests.TestEntities
{
    internal class TestObjectsManager
    {
        private readonly GlobalContext globalContext;
        private readonly string organizationInn;

        public TestObjectsManager(GlobalContext globalContext, string organizationInn)
        {
            this.globalContext = globalContext;
            this.organizationInn = organizationInn;
        }

        public dynamic ComObject
        {
            get { return globalContext.ComObject; }
        }

        public object CreateCounterparty(Counterpart counterpart)
        {
            var item = ComObject.�����������.�����������.CreateItem();
            var legalFormEnum = ComObject.������������.�������������������������;
            item.������������������������� = counterpart.LegalForm == LegalForm.Ip
                ? legalFormEnum.��������������
                : legalFormEnum.���������������;
            item.��� = counterpart.Inn;
            if (counterpart.LegalForm == LegalForm.Organization)
                item.��� = counterpart.Kpp;
            item.������������ = counterpart.Name;
            item.������������������ = counterpart.Name;
            item.�������������������� = false;
            item.Write();
            return item;
        }

        public object CreateBankAccount(object owner, BankAccount bankAccount)
        {
            var item = ComObject.�����������.���������������.CreateItem();
            item.���������� = bankAccount.Number;
            item.���� = GetBankByBic(bankAccount.Bank.Bik);
            item.������������ = bankAccount.Name ?? GenerateBankAccountName(item.����, bankAccount.Number);
            if (bankAccount.CurrencyCode != null)
                item.��������������������� = GetCurrencyByCode(bankAccount.CurrencyCode);
            item.�������� = owner;
            item.Write();
            return item;
        }

        public object CreateCounterpartContract(object counterpartReference, CounterpartyContract contract)
        {
            var item = ComObject.�����������.��������������������.CreateItem();
            var contractKindsEnum = ComObject.������������.�������������������������;
            item.����������� = ComHelpers.GetProperty(contractKindsEnum, ConvertCounterpartyContract(contract.Kind));
            item.����������� = GetOrganization();
            item.�������� = counterpartReference;
            item.������������ = contract.Name;
            item.����������� = string.Format("test {0}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
            if (!string.IsNullOrEmpty(contract.CurrencyCode))
                item.�������������������� = GetCurrencyByCode(contract.CurrencyCode);
            item.Write();
            return item;
        }

        private static string ConvertCounterpartyContract(CounterpartContractKind value)
        {
            switch (value)
            {
                case CounterpartContractKind.Outgoing:
                    return "������������";
                case CounterpartContractKind.Incoming:
                    return "������������";
                case CounterpartContractKind.Others:
                    return "������";
                case CounterpartContractKind.OutgoingWithComitent:
                    return "��������������������";
                case CounterpartContractKind.OutgoingWithAgency:
                    return "�����������������������";
                case CounterpartContractKind.IncomingWithComitent:
                    return "�����������";
                case CounterpartContractKind.IncomingWithAgency:
                    return "��������������";
                default:
                    throw new ArgumentOutOfRangeException("value", value, null);
            }
        }

        private static string GenerateBankAccountName(dynamic bank, string number)
        {
            return string.Format("{0}, {1}", number, bank.������������);
        }

        private object GetCurrencyByCode(string currencyCode)
        {
            return GetCatalogItemByCode("������", currencyCode);
        }

        private object GetBankByBic(string bik)
        {
            return GetCatalogItemByCode("�����", bik);
        }

        private object GetCatalogItemByKeyOrNull(string catalogName, string keyName, string keyValue)
        {
            const string queryFormat = @"
                �������
	                catalog.������ ��� ������
                ��
	                ����������.{0} ��� catalog
                ���
	                catalog.{1} = &key";
            return globalContext.Execute(string.Format(queryFormat, catalogName, keyName), new Dictionary<string, object> {{"key", keyValue}}).Select(x => x["������"]).FirstOrDefault();
        }

        private object GetOrganization()
        {
            return GetCatalogItemByKey("�����������", "���", organizationInn);
        }

        private object GetCatalogItemByCode(string catalogName, string code)
        {
            return GetCatalogItemByKey(catalogName, "���", code);
        }

        private object GetCatalogItemByKey(string catalogName, string keyName, string keyValue)
        {
            var result = GetCatalogItemByKeyOrNull(catalogName, keyName, keyValue);
            if (result == null)
            {
                const string messageFormat = "catalog [{0}] item with {1} [{2}] not found";
                throw new InvalidOperationException(string.Format(messageFormat, catalogName, keyName, keyValue));
            }
            return result;
        }
    }
}