using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple1C.Impl.Com;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.������������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.TestEntities;

namespace Simple1C.Tests.Integration
{
    internal class SaveTest : COMDataContextTestBase
    {
        [Test]
        public void CanAddRecursive()
        {
            var organization = dataContext.Single<�����������>(x => x.��� == organizationInn);
            var counterpartyContract = new ��������������������
            {
                ����������� = �������������������������.������������,
                ������������ = "test name",
                �������� = new �����������
                {
                    ��� = "1234567890",
                    ������������ = "test-counterparty",
                    ������������������������� = �������������������������.���������������
                },
                ����������� = organization
            };
            dataContext.Save(counterpartyContract);
            Assert.That(string.IsNullOrEmpty(counterpartyContract.���), Is.False);
            Assert.That(string.IsNullOrEmpty(counterpartyContract.��������.���), Is.False);

            var valueTable = globalContext.Execute("������� * �� ����������.�������������������� ��� ���=&Code", new Dictionary<string, object>
            {
                {"Code", counterpartyContract.���}
            }).Unload();
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test name"));
        }

        [Test]
        public void CanSaveNullDateTimes()
        {
            var ���������� = new �����������
            {
                ������������ = "test",
                ��� = "123456789"
            };
            dataContext.Save(����������);

            var ����������2 = dataContext.Single<�����������>(x => x.��� == ����������.���);
            Assert.That(����������2.�����������������������, Is.Null);

            ����������2.����������������������� = new DateTime(2010, 7, 18);
            dataContext.Save(����������2);
            ����������2 = dataContext.Single<�����������>(x => x.��� == ����������.���);
            Assert.That(����������2.�����������������������, Is.EqualTo(new DateTime(2010, 7, 18)));

            ����������2.����������������������� = null;
            dataContext.Save(����������2);
            ����������2 = dataContext.Single<�����������>(x => x.��� == ����������.���);
            Assert.That(����������2.�����������������������, Is.Null);
        }

        [Test]
        public void RecursiveSave()
        {
            var �������������� = new �����������
            {
                ������������ = "�������"
            };
            ��������������.������������������ = ��������������;
            var exception = Assert.Throws<InvalidOperationException>(() => dataContext.Save(��������������));
            Assert.That(exception.Message,
                Does.Contain("cycle detected for entity type [�����������]: [�����������->������������������]"));

            //��. ������� ��� ������������������
            //            var valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Code", new[]
            //            {
            //                new KeyValuePair<string, object>("Code", ��������������.���)
            //            });
            //
            //            Assert.That(valueTable.Count, Is.EqualTo(1));
            //            Assert.That(valueTable[0]["������������"], Is.EqualTo("�������"));
            //            Assert.That(ComHelpers.GetProperty(valueTable[0]["������������������"],
            //                "������������"), Is.EqualTo("�������"));
        }

        [Test]
        public void CanSaveNewEntityWithoutAnyPropertiesSet()
        {
            var ���������� = new �����������();
            dataContext.Save(����������);

            Assert.That(string.IsNullOrEmpty(����������.���), Is.False);
            var counterpartyTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Code",
                new Dictionary<string, object>
                {
                    {"Code", ����������.���}
                }).Unload();
            Assert.That(counterpartyTable.Count, Is.EqualTo(1));
        }

        [Test]
        public void ChangeMustBeStrongerThanTracking()
        {
            var counterpart = new Counterpart
            {
                Inn = "7711223344",
                Kpp = "771101001",
                FullName = "Test counterparty",
                Name = "Test counterparty"
            };
            dynamic counterpartyAccessObject = testObjectsManager.CreateCounterparty(counterpart);
            var counterpartContractAccessObject = testObjectsManager.CreateCounterpartContract(counterpartyAccessObject.������,
                new CounterpartyContract
                {
                    CurrencyCode = "643",
                    Name = "test-contract",
                    Kind = CounterpartContractKind.Incoming
                });
            string counterpartyContractCode = counterpartContractAccessObject.Code;

            var contract = dataContext.Select<��������������������>()
                .Single(x => x.��� == counterpartyContractCode);
            if (contract.��������.��� == "7711223344")
            {
                contract.��������.��� = "7711223345";
                contract.�������� = new �����������
                {
                    ��� = "7711223355",
                    ������������ = "Test counterparty 2",
                    ������������������ = "Test counterparty 2"
                };
            }
            dataContext.Save(contract);

            var valueTable = globalContext.Execute("������� * �� ����������.�������������������� ��� ���=&Code",
                new Dictionary<string, object>
                {
                    {"Code", contract.���}
                }).Unload();
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(ComHelpers.GetProperty(valueTable[0]["��������"], "������������"), Is.EqualTo("Test counterparty 2"));
        }

        [Test]
        public void CanUnpostDocument()
        {
            var ����������������������� = CreateFullFilledDocument();
            �����������������������.�������� = true;
            dataContext.Save(�����������������������);

            �����������������������.������[0].���������� = "����";
            dataContext.Save(�����������������������);

            var document = GetDocumentByNumber(�����������������������.�����);
            Assert.That(document.��������, Is.True);
            Assert.That(document.������.��������(0).����������, Is.EqualTo("����"));
        }

        [Test]
        public void CanPostDocuments()
        {
            var ����������������������� = CreateFullFilledDocument();
            dataContext.Save(�����������������������);
            Assert.That(GetDocumentByNumber(�����������������������.�����).��������, Is.False);
            �����������������������.�������� = true;
            dataContext.Save(�����������������������);
            Assert.That(GetDocumentByNumber(�����������������������.�����).��������);
        }

        [Test]
        public void ModifyReference()
        {
            var counterpart = new Counterpart
            {
                Inn = "7711223344",
                Kpp = "771101001",
                FullName = "Test counterparty",
                Name = "Test counterparty"
            };
            dynamic counterpartyAccessObject = testObjectsManager.CreateCounterparty(counterpart);
            var counterpartContractAccessObject = testObjectsManager.CreateCounterpartContract(counterpartyAccessObject.������,
                new CounterpartyContract
                {
                    Name = "test-counterparty-contract",
                    Kind = CounterpartContractKind.Incoming,
                    CurrencyCode = "643"
                });
            string initialCounterpartyContractVersion = counterpartContractAccessObject.DataVersion;
            string counterpartyContractCode = counterpartContractAccessObject.Code;
            var contract = dataContext.Select<��������������������>()
                .Single(x => x.��� == counterpartyContractCode);
            contract.��������.��� = "7711223344";
            contract.��������.������������ = "Test counterparty 2";
            contract.��������.������������������ = "Test counterparty 2";
            dataContext.Save(contract);

            var counterpartyTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Inn",
                new Dictionary<string, object>
                {
                    {"Inn", "7711223344"}
                }).Unload();
            Assert.That(counterpartyTable.Count, Is.EqualTo(1));
            Assert.That(counterpartyTable[0].GetString("������������"), Is.EqualTo("Test counterparty 2"));

            var counterpartyContractTable = globalContext.Execute("������� ������ �� ����������.�������������������� ��� ���=&Code",
                new Dictionary<string, object>
                {
                    {"Code", counterpartyContractCode}
                }).Unload();
            Assert.That(counterpartyContractTable.Count, Is.EqualTo(1));

            var comObj = counterpartyContractTable[0]["������"];
            var dataVersion = ((dynamic)comObj).DataVersion;
            Assert.That(dataVersion, Is.EqualTo(initialCounterpartyContractVersion));
        }
    }
}