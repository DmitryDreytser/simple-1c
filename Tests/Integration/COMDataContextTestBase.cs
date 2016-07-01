using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.���������;
using Simple1C.Tests.Metadata1C.������������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.TestEntities;

namespace Simple1C.Tests.Integration
{
    internal abstract class COMDataContextTestBase : IntegrationTestBase
    {
        protected IDataContext dataContext;
        protected internal TestObjectsManager testObjectsManager;
        protected EnumConverter enumConverter;

        protected override void SetUp()
        {
            base.SetUp();
            dataContext = DataContextFactory.CreateCOM(globalContext.ComObject(), typeof(�����������).Assembly);
            enumConverter = new EnumConverter(globalContext);
            testObjectsManager = new TestObjectsManager(globalContext, enumConverter, organizationInn);
        }

        protected ����������� ��������������������������()
        {
            return dataContext.Single<�����������>(
                x => !x.���������������,
                x => x.��� == organizationInn);
        }

        protected ����������������������� CreateFullFilledDocument()
        {
            var ���������� = new �����������
            {
                ��� = "7711223344",
                ������������ = "��� �������� ����������"
            };
            var ����������� = dataContext.Single<�����������>(x => x.��� == organizationInn);
            var �������������������� = dataContext.Single<������>(x => x.��� == "643");
            var �������������������� = new ��������������������
            {
                �������� = ����������,
                ����������� = �����������,
                ����������� = �������������������������.������������,
                ������������ = "test contract",
                ����������� = "test contract comment",
                �������������������� = ��������������������
            };
            var ����26 = dataContext.Single<������������>(x => x.��� == "26");
            var ����1904 = dataContext.Single<������������>(x => x.��� == "19.04");
            var ����6001 = dataContext.Single<������������>(x => x.��� == "60.01");
            var ����6002 = dataContext.Single<������������>(x => x.��� == "60.02");
            var ������������������� = new ������������
            {
                ������������ = "������������ �������",
                ������������� = ��������������.�������������������
            };
            return new �����������������������
            {
                ���������������������� = new DateTime(2016, 6, 1),
                ���� = new DateTime(2016, 6, 1),
                ����������������������� = "12345",
                ����������� = �����������������������������������.������,
                ���������� = ����������,
                ������������������ = ��������������������,
                ����������� = �����������,
                ������������������� = ��������������������.�������������,
                ��������������� = ��������������������,
                ������������������������������ = ����6001,
                �������������������������� = ����6002,
                ������ = new List<�����������������������.��������������������>
                {
                    new �����������������������.��������������������
                    {
                        ������������ = new ������������
                        {
                            ������������ = "�������"
                        },
                        ���������� = 10,
                        ���������� = "������� � ����������",
                        ����� = 120,
                        ���� = 12,
                        ��������� = ���������.���18,
                        �������� = 21.6m,
                        ���������� = ����26,
                        ������������ = ����26,
                        ������������ = ����1904,
                        ������������� = �������������.�����������,
                        ��������1 = �������������������,
                        ������������������� = �����������������������������()
                    },
                    new �����������������������.��������������������
                    {
                        ������������ = new ������������
                        {
                            ������������ = "����� ������"
                        },
                        ���������� = 10,
                        ���������� = "����� ������ ������������� �����",
                        ����� = 120,
                        ���� = 12,
                        ��������� = ���������.���18,
                        �������� = 21.6m,
                        ���������� = ����26,
                        ������������ = ����26,
                        ������������ = ����1904,
                        ������������� = �������������.�����������,
                        ��������1 = �������������������,
                        ������������������� = �����������������������������()
                    }
                }
            };
        }

        protected ������������������������ �����������������������������()
        {
            var ����������� = ��������������������������();
            return dataContext.Single<������������������������>(
                x => x.��������.��� == �����������.���,
                x => !x.���������������,
                x => x.��� == "00-000001"
                     || x.��� == "000000001"
                     || x.��� == "99-000001"
                     || x.������������ == "�������� �������������");
        }

        protected dynamic GetDocumentByNumber(string number)
        {
            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", number}
                }).Unload();
            Assert.That(valueTable.Count, Is.EqualTo(1));
            return valueTable[0]["������"];
        }

        protected object CreateTestCounterpart()
        {
            var counterpart = new Counterpart
            {
                Name = "test-counterpart-name",
                Inn = "0987654321",
                Kpp = "987654321"
            };
            dynamic counterpartAccessObject = testObjectsManager.CreateCounterparty(counterpart);
            testObjectsManager.CreateBankAccount(counterpartAccessObject.������,
                new BankAccount
                {
                    Bank = new Bank
                    {
                        Bik = Banks.AlfaBankBik
                    },
                    Number = "40702810001111122222",
                    CurrencyCode = "643"
                });
            return counterpartAccessObject;
        }
    }
}