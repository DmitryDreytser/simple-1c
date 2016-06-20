using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple1C.Impl.Com;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.���������;
using Simple1C.Tests.Metadata1C.������������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.TestEntities;

namespace Simple1C.Tests
{
    public class COMDataContextTest : IntegrationTestBase
    {
        private IDataContext dataContext;
        private TestObjectsManager testObjectsManager;
        private EnumConverter enumConverter;

        protected override void SetUp()
        {
            base.SetUp();
            dataContext = DataContextFactory.CreateCOM(globalContext.ComObject, typeof (�����������).Assembly);
            enumConverter = new EnumConverter(globalContext);
            testObjectsManager = new TestObjectsManager(globalContext, enumConverter, organizationInn);
        }

        [Test]
        public void Simple()
        {
            testObjectsManager.CreateCounterparty(new Counterpart
            {
                Name = "test-name",
                Inn = "1234567890",
                Kpp = "123456789"
            });
            var instance = dataContext
                .Select<�����������>()
                .Single(x => x.��� == "1234567890");
            Assert.That(instance.������������, Is.EqualTo("test-name"));
            Assert.That(instance.���, Is.EqualTo("1234567890"));
            Assert.That(instance.���, Is.EqualTo("123456789"));
        }

        [Test]
        public void SelectWithRef()
        {
            var counterpart = new Counterpart
            {
                Name = "test-counterpart-name",
                Inn = "0987654321",
                Kpp = "987654321"
            };
            dynamic counterpartAccessObject = testObjectsManager.CreateCounterparty(counterpart);
            var bankAccountAccessObject = testObjectsManager.CreateBankAccount(counterpartAccessObject.������,
                new BankAccount
                {
                    Bank = new Bank
                    {
                        Bik = Banks.AlfaBankBik
                    },
                    Number = "40702810001111122222",
                    CurrencyCode = "643"
                });

            counterpartAccessObject.���������������������� = bankAccountAccessObject.������;
            counterpartAccessObject.Write();

            var counterpartyContractAccessObject = testObjectsManager.CreateCounterpartContract(counterpartAccessObject.������, new CounterpartyContract
            {
                CurrencyCode = "643",
                Name = "������",
                Kind = CounterpartContractKind.OutgoingWithAgency
            });
            string counterpartContractCode = counterpartyContractAccessObject.���;


            var contractFromStore = dataContext
                .Select<��������������������>()
                .Single(x => x.��� == counterpartContractCode);

            Assert.That(contractFromStore.��������.���, Is.EqualTo("0987654321"));
            Assert.That(contractFromStore.��������.���, Is.EqualTo("987654321"));
            Assert.That(contractFromStore.��������.������������, Is.EqualTo("test-counterpart-name"));
            Assert.That(contractFromStore.��������.����������������������.����������,
                        Is.EqualTo("40702810001111122222"));
            Assert.That(contractFromStore.��������.����������������������.��������,
                        Is.TypeOf<�����������>());
            Assert.That(((�����������)contractFromStore.��������.����������������������.��������)
                    .���,
                Is.EqualTo("0987654321"));
            Assert.That(contractFromStore.�����������, Is.EqualTo(�������������������������.�����������������������));
        }

        [Test]
        public void QueryWithRefAccess()
        {
            var counterpart = new Counterpart
            {
                Name = "test-counterpart-name",
                Inn = "0987654321",
                Kpp = "987654321"
            };
            dynamic counterpartAccessObject = testObjectsManager.CreateCounterparty(counterpart);
            dynamic bankAccountAccessObject = testObjectsManager.CreateBankAccount(counterpartAccessObject.������,
                new BankAccount
                {
                    Bank = new Bank
                    {
                        Bik = Banks.AlfaBankBik
                    },
                    Number = "40702810001111122222",
                    CurrencyCode = "643"
                });

            counterpartAccessObject.���������������������� = bankAccountAccessObject.������;
            counterpartAccessObject.Write();

            testObjectsManager.CreateCounterpartContract(counterpartAccessObject.������, new CounterpartyContract
            {
                CurrencyCode = "643",
                Name = "������",
                Kind = CounterpartContractKind.OutgoingWithAgency
            });

            var contractFromStore = dataContext.Select<��������������������>()
                .Single(x => x.��������.����������������������.���������� == "40702810001111122222");
            Assert.That(contractFromStore.������������, Is.EqualTo("������"));
        }

        [Test]
        public void QueryWithObject()
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

            var account = dataContext.Select<���������������>()
                .Single(x => x.�������� is �����������);
            Assert.That(account.���������������������.���, Is.EqualTo("643"));
            Assert.That(account.��������, Is.TypeOf<�����������>());
            Assert.That(((�����������)account.��������).���, Is.EqualTo("0987654321"));
            Assert.That(((�����������)account.��������).���, Is.EqualTo("987654321"));
        }

        [Test]
        public void NullableEnumCanSet()
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
            testObjectsManager.CreateCounterpartContract(counterpartAccessObject.������, new CounterpartyContract
            {
                CurrencyCode = "643",
                Name = "������",
                Kind = CounterpartContractKind.OutgoingWithAgency
            });
            string counterpartyCode = counterpartAccessObject.���;

            var contracts = dataContext.Select<��������������������>()
                .Where(x => x.��������.��� == counterpartyCode)
                .ToArray();
            Assert.That(contracts.Length, Is.EqualTo(1));
            Assert.That(contracts[0].�����������, Is.EqualTo(�������������������������.�����������������������));
        }

        [Test]
        public void EnumParameterMapping()
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
            testObjectsManager.CreateCounterpartContract(counterpartAccessObject.������, new CounterpartyContract
            {
                CurrencyCode = "643",
                Name = "������",
                Kind = CounterpartContractKind.OutgoingWithAgency
            });

            var contracts = dataContext.Select<��������������������>()
                .Where(x => x.����������� == �������������������������.�����������������������)
                .ToArray();

            Assert.That(contracts.Length, Is.EqualTo(1));
            Assert.That(contracts[0].�����������, Is.EqualTo(�������������������������.�����������������������));
        }

        [Test]
        public void CanFilterForEmptyReference()
        {
            dynamic counterpartAccessObject = CreateTestCounterpart();
            var counterpartyContractAccessObject = testObjectsManager.CreateCounterpartContract(counterpartAccessObject.������,
                new CounterpartyContract
                {
                    Name = "test-description",
                    Kind = CounterpartContractKind.Others
                });
            string counterpartyContractCode = counterpartyContractAccessObject.���;

            var contracts = dataContext.Select<��������������������>()
                .Where(x => x.��� == counterpartyContractCode)
                .Where(x => x.������ == null)
                .ToArray();
            Assert.That(contracts.Length, Is.EqualTo(1));
            Assert.That(contracts[0].���, Is.EqualTo(counterpartyContractCode));
            Assert.That(contracts[0].������, Is.Null);

            contracts = dataContext.Select<��������������������>()
                .Where(x => x.��� == counterpartyContractCode)
                .Where(x => x.������ != null)
                .ToArray();
            Assert.That(contracts.Length, Is.EqualTo(0));

            contracts = dataContext.Select<��������������������>()
                .Where(x => counterpartyContractCode == x.���)
                .Where(x => null == x.������)
                .ToArray();
            Assert.That(contracts.Length, Is.EqualTo(1));
            Assert.That(contracts[0].���, Is.EqualTo(counterpartyContractCode));
            Assert.That(contracts[0].������, Is.Null);
        }

        [Test]
        public void TableSections()
        {
            dynamic accessObject = testObjectsManager.CreateAccountingDocument(new AccountingDocument
            {
                Date = DateTime.Now,
                Number = "k-12345",
                Counterpart = new Counterpart
                {
                    Inn = "7711223344",
                    Name = "��� �������",
                    LegalForm = LegalForm.Organization
                },
                CounterpartContract = new CounterpartyContract
                {
                    CurrencyCode = "643",
                    Name = "������",
                    Kind = CounterpartContractKind.OutgoingWithAgency
                },
                SumIncludesNds = true,
                IsCreatedByEmployee = false,
                Items = new[]
                {
                    new NomenclatureItem
                    {
                        Name = "�������� ����",
                        NdsRate = NdsRate.NoNds,
                        Count = 1,
                        Price = 1000m,
                        Sum = 1000m,
                        NdsSum = 0m
                    }
                },
                Comment = "test comment",
                OperationKind = IncomingOperationKind.Services
            });

            string number = accessObject.�����;
            DateTime date = accessObject.����;

            var acts = dataContext
                .Select<�����������������������>()
                .Single(x => x.����� == number && x.���� == date);
            Assert.That(acts.������.Count, Is.EqualTo(1));
            Assert.That(acts.������[0].�����, Is.EqualTo(1000m));
        }

        [Test]
        public void CanAddCounterparty()
        {
            var counterparty = new �����������
            {
                ��� = "1234567890",
                ������������ = "test-counterparty",
                ������������������������� = �������������������������.���������������
            };
            dataContext.Save(counterparty);
            Assert.That(string.IsNullOrEmpty(counterparty.���), Is.False);

            var valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
            {
                {"Code", counterparty.���}
            });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("���"), Is.EqualTo("1234567890"));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test-counterparty"));

            dynamic comObject = globalContext.ComObject;
            var enumsDispatchObject = comObject.������������.�������������������������;
            var expectedEnumValue = enumConverter.Convert(LegalForm.Organization);
            var expectedEnumValueIndex = enumsDispatchObject.IndexOf(expectedEnumValue);
            var actualEnumValueIndex = enumsDispatchObject.IndexOf(valueTable[0]["�������������������������"]);

            Assert.That(actualEnumValueIndex, Is.EqualTo(expectedEnumValueIndex));
        }

        [Test]
        public void CanAddCounterpartyWithNullableEnum()
        {
            var counterparty = new �����������
            {
                ��� = "1234567890",
                ������������ = "test-counterparty",
                ������������������������� = null
            };
            dataContext.Save(counterparty);
            Assert.That(string.IsNullOrEmpty(counterparty.���), Is.False);

            var valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
            {
                {"Code", counterparty.���}
            });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("���"), Is.EqualTo("1234567890"));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test-counterparty"));
            Assert.That(ComHelpers.Invoke(valueTable[0]["�������������������������"], "IsEmpty"), Is.True);
        }

        [Test]
        public void CanAddCounterpartyContract()
        {
            var organization = dataContext.Single<�����������>(x => x.��� == organizationInn);
            var counterparty = new �����������
            {
                ��� = "1234567890",
                ������������ = "test-counterparty",
                ������������������������� = �������������������������.���������������
            };
            dataContext.Save(counterparty);

            var counterpartyFromStore = dataContext.Select<�����������>().Single(x => x.��� == counterparty.���);
            var counterpartyContract = new ��������������������
            {
                ����������� = �������������������������.������������,
                ������������ = "test name",
                �������� = counterpartyFromStore,
                ����������� = organization
            };
            dataContext.Save(counterpartyContract);
            Assert.That(string.IsNullOrEmpty(counterpartyContract.���), Is.False);

            var valueTable = globalContext.Execute("������� * �� ����������.�������������������� ��� ���=&Code", new Dictionary<string, object>
            {
                {"Code", counterpartyContract.���}
            });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test name"));
            Assert.That(((dynamic)valueTable[0]["��������"]).���, Is.EqualTo(counterparty.���));
            valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
            {
                {"Code", counterparty.���}
            });
            Assert.That(valueTable.Count, Is.EqualTo(1));
        }

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
            });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test name"));
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
                });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(ComHelpers.GetProperty(valueTable[0]["��������"], "������������"), Is.EqualTo("Test counterparty 2"));
        }

        [Test]
        public void ModifyCounterparty()
        {
            dynamic counterpartyAccessObject = testObjectsManager.CreateCounterparty(new Counterpart
            {
                Inn = "7711223344",
                Kpp = "771101001",
                FullName = "Test counterparty",
                Name = "Test counterparty"
            });
            string counterpartyCode = counterpartyAccessObject.���;

            var counterparty = dataContext.Single<�����������>(x => x.��� == counterpartyCode);
            counterparty.��� = "7711223344";
            counterparty.������������ = "Test counterparty 2";
            counterparty.������������������ = "Test counterparty 2";
            dataContext.Save(counterparty);
            var valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Inn",
                new Dictionary<string, object>
                {
                    {"Inn", "7711223344"}
                });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("Test counterparty 2"));
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
        public void CanAddDocumentWithTableSection()
        {
            var ����������������������� = new �����������������������
            {
                ���������������������� = new DateTime(2016, 6, 1),
                ���� = new DateTime(2016, 6, 1),
                ����������������������� = "12345",
                ����������� = �����������������������������������.������,
                ���������� = new �����������
                {
                    ��� = "7711223344",
                    ������������ = "��� �������� ����������",
                },
                ����������� = ��������������������������(),
                ������������������� = ��������������������.�������������,
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
                        �������� = 21.6m
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
                        �������� = 21.6m
                    }
                }
            };

            dataContext.Save(�����������������������);

            Assert.That(�����������������������.�����, Is.Not.Null);
            Assert.That(�����������������������.����, Is.Not.EqualTo(default(DateTime)));
            Assert.That(�����������������������.������.Count, Is.EqualTo(2));
            Assert.That(�����������������������.������[0].�����������, Is.EqualTo(1));
            Assert.That(�����������������������.������[1].�����������, Is.EqualTo(2));

            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", �����������������������.�����}
                });

            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0]["�����������������������"], Is.EqualTo("12345"));
            Assert.That(valueTable[0]["����������������������"], Is.EqualTo(new DateTime(2016, 6, 1)));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(2));

            var row1 = servicesTablePart.��������(0);
            Assert.That(row1.������������.������������, Is.EqualTo("�������"));
            Assert.That(row1.����������, Is.EqualTo(10));
            Assert.That(row1.����������, Is.EqualTo("������� � ����������"));
            Assert.That(row1.�����, Is.EqualTo(120));
            Assert.That(row1.����, Is.EqualTo(12));

            var row2 = servicesTablePart.��������(1);
            Assert.That(row2.������������.������������, Is.EqualTo("����� ������"));
            Assert.That(row2.����������, Is.EqualTo("����� ������ ������������� �����"));
        }

        [Test]
        public void CanModifyTableSection()
        {
            var ����������������������� = new �����������������������
            {
                ���������������������� = new DateTime(2016, 6, 1),
                ���� = new DateTime(2016, 6, 1),
                ����������������������� = "12345",
                ����������� = �����������������������������������.������,
                ���������� = new �����������
                {
                    ��� = "7711223344",
                    ������������ = "��� �������� ����������",
                },
                ����������� = ��������������������������(),
                ������������������� = ��������������������.�������������,
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
                        �������� = 21.6m
                    }
                }
            };
            dataContext.Save(�����������������������);

            �����������������������.������[0].���������� = "������� ������";
            dataContext.Save(�����������������������);

            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", �����������������������.�����}
                });

            Assert.That(valueTable.Count, Is.EqualTo(1));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(1));

            var row1 = servicesTablePart.��������(0);
            Assert.That(row1.����������, Is.EqualTo("������� ������"));
        }

        [Test]
        public void CanDeleteItemFromTableSection()
        {
            var ����������������������� = new �����������������������
            {
                ���������������������� = new DateTime(2016, 6, 1),
                ���� = new DateTime(2016, 6, 1),
                ����������������������� = "12345",
                ����������� = �����������������������������������.������,
                ���������� = new �����������
                {
                    ��� = "7711223344",
                    ������������ = "��� �������� ����������",
                },
                ����������� =  ��������������������������(),
                ������������������� = ��������������������.�������������,
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
                        �������� = 21.6m
                    },
                    new �����������������������.��������������������
                    {
                        ������������ = new ������������
                        {
                            ������������ = "������� ����"
                        },
                        ���������� = 10,
                        ���������� = "������� ������",
                        ����� = 120,
                        ���� = 12,
                        ��������� = ���������.���18,
                        �������� = 21.6m
                    },
                }
            };
            dataContext.Save(�����������������������);

            �����������������������.������.RemoveAt(0);
            dataContext.Save(�����������������������);

            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", �����������������������.�����}
                });

            Assert.That(valueTable.Count, Is.EqualTo(1));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(1));

            var row1 = servicesTablePart.��������(0);
            Assert.That(row1.����������, Is.EqualTo("������� ������"));
        }

        [Test]
        public void CanChangeTableSectionItemsOrdering()
        {
            var ����������������������� = new �����������������������
            {
                ���������������������� = new DateTime(2016, 6, 1),
                ���� = new DateTime(2016, 6, 1),
                ����������������������� = "12345",
                ����������� = �����������������������������������.������,
                ���������� = new �����������
                {
                    ��� = "7711223344",
                    ������������ = "��� �������� ����������",
                },
                ����������� = ��������������������������(),
                ������������������� = ��������������������.�������������,
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
                        �������� = 21.6m
                    },
                    new �����������������������.��������������������
                    {
                        ������������ = new ������������
                        {
                            ������������ = "������� ����"
                        },
                        ���������� = 10,
                        ���������� = "������� ������",
                        ����� = 120,
                        ���� = 12,
                        ��������� = ���������.���18,
                        �������� = 21.6m
                    },
                }
            };
            dataContext.Save(�����������������������);

            var t = �����������������������.������[0];
            �����������������������.������[0] = �����������������������.������[1];
            �����������������������.������[1] = t;
            dataContext.Save(�����������������������);

            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", �����������������������.�����}
                });

            Assert.That(valueTable.Count, Is.EqualTo(1));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(2));

            var row0 = servicesTablePart.��������(0);
            Assert.That(row0.����������, Is.EqualTo("������� ������"));

            var row1 = servicesTablePart.��������(1);
            Assert.That(row1.����������, Is.EqualTo("������� � ����������"));
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
            string counterpartyContractCode = counterpartContractAccessObject.Code;
            var contract = dataContext.Select<��������������������>()
                .Single(x => x.��� == counterpartyContractCode);
            contract.��������.��� = "7711223344";
            contract.��������.������������ = "Test counterparty 2";
            contract.��������.������������������ = "Test counterparty 2";
            dataContext.Save(contract);

            var valueTable = globalContext.Execute("������� * �� ����������.����������� ��� ���=&Inn",
                new Dictionary<string, object>
                {
                    {"Inn", "7711223344"}
                });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("Test counterparty 2"));
        }

        public interface IGenericCatalog
        {
            string ������������ { get; set; }
        }

        [Test]
        public void CanQueryWithSourceNameViaGenericInterface()
        {
            var counterpart = new Counterpart
            {
                Inn = "7711223344",
                Kpp = "771101001",
                FullName = "Test counterparty 1",
                Name = "Test counterparty 2"
            };
            testObjectsManager.CreateCounterparty(counterpart);

            var catalogItem = dataContext.Select<IGenericCatalog>("����������.�����������")
                .Where(x => x.������������ == "Test counterparty 2")
                .Cast<object>()
                .Single();
            Assert.That(catalogItem, Is.TypeOf<�����������>());
        }

        [Test]
        public void RecursiveSave()
        {
            var �������������� = new �����������
            {
                ������������ = "�������"
            };
            ��������������.������������������ = ��������������;
            var exception = Assert.Throws<InvalidOperationException>(()=> dataContext.Save(��������������));
            Assert.That(exception.Message, Does.Contain("cycle detected for entity type [�����������]: [�����������->������������������]"));

//TODO - ����������������
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

        private ����������������������� CreateFullFilledDocument()
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

        private ������������������������ �����������������������������()
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

        private ����������� ��������������������������()
        {
            return dataContext.Single<�����������>(
                x => !x.���������������,
                x => x.��� == organizationInn);
        }

        private dynamic GetDocumentByNumber(string number)
        {
            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", number}
                });
            Assert.That(valueTable.Count, Is.EqualTo(1));
            return valueTable[0]["������"];
        }

        private object CreateTestCounterpart()
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