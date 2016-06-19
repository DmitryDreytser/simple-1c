using System.Linq;
using NUnit.Framework;
using Simple1C.Tests.Metadata1C.�����������;

namespace Simple1C.Tests
{
    //[Inject] public CounterpartManager counterpartManager;
    //[Inject] public CounterpartContractManager counterpartContractManager;
    //[Inject] public BankAccountManager bankAccountManager;
    //[Inject] public ICatalogRegistry catalogRegistry;
    //[Inject] public IIncomingAccountingDocumentManager incomingAccountingDocumentManager;
    //[Inject] public IQueryExecuter queryExecuter;
    //[Inject] public EnumerationManager enumerationManager;
    //[Inject] public ���������������������� ����������������������;
    public class Store1CIntegrationTest : IntegrationTestBase
    {
        private DataContext dataContext;

        protected override void SetUp()
        {
            base.SetUp();
            dataContext = new DataContext(globalContext);
        }

        [Test]
        public void Simple()
        {
            //counterpartManager.Create(new Counterpart
            //{
            //    Name = "test-name",
            //    Inn = "1234567890",
            //    Kpp = "123456789"
            //});

            var instance = dataContext
                .Select<�����������>()
                .Single(x => x.��� == "1234567890");
            Assert.That(instance.������������, Is.EqualTo("test-name"));
            Assert.That(instance.���, Is.EqualTo("1234567890"));
            Assert.That(instance.���, Is.EqualTo("123456789"));
        }

        //[Test]
        //public void SelectWithRef()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    var bankAccountAccessObject = bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });

        //    counterpartAccessObject.DefaultBankAccount = bankAccountAccessObject.Reference;
        //    counterpartAccessObject.Write();

        //    var counterpartyContractCode = counterpartContractManager.Create(counterpart, new CounterpartyContract
        //    {
        //        CounterpartyCode = counterpartAccessObject.Code,
        //        CurrencyCode = "643",
        //        Name = "������",
        //        Type = CounterpartContractKind.OutgoingWithAgency
        //    }).Code;

        //    var contractFromStore = store1C
        //        .Select<��������������������>()
        //        .Single(x => x.��� == counterpartyContractCode);

        //    Assert.That(contractFromStore.��������.���, Is.EqualTo("0987654321"));
        //    Assert.That(contractFromStore.��������.���, Is.EqualTo("987654321"));
        //    Assert.That(contractFromStore.��������.������������, Is.EqualTo("test-counterpart-name"));
        //    Assert.That(contractFromStore.��������.����������������������.����������,
        //                Is.EqualTo("40702810001111122222"));
        //    Assert.That(contractFromStore.��������.����������������������.��������,
        //                Is.TypeOf<�����������>());
        //    Assert.That(((�����������)contractFromStore.��������.����������������������.��������)
        //            .���,
        //        Is.EqualTo("0987654321"));
        //    Assert.That(contractFromStore.�����������, Is.EqualTo(�������������������������.�����������������������));
        //}

        //[Test]
        //public void QueryWithRefAccess()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    var bankAccountAccessObject = bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });

        //    counterpartAccessObject.DefaultBankAccount = bankAccountAccessObject.Reference;
        //    counterpartAccessObject.Write();

        //    counterpartContractManager.Create(counterpart, new CounterpartyContract
        //    {
        //        CounterpartyCode = counterpartAccessObject.Code,
        //        CurrencyCode = "643",
        //        Name = "������",
        //        Type = CounterpartContractKind.OutgoingWithAgency
        //    });

        //    var contractFromStore = store1C.Select<��������������������>()
        //        .Single(x => x.��������.����������������������.���������� == "40702810001111122222");
        //    Assert.That(contractFromStore.������������, Is.EqualTo("������"));
        //}

        //[Test]
        //public void QueryWithObject()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });

        //    var account = store1C.Select<���������������>()
        //        .Single(x => x.�������� is �����������);
        //    Assert.That(account.���������������������.���, Is.EqualTo("643"));
        //    Assert.That(account.��������, Is.TypeOf<�����������>());
        //    Assert.That(((�����������)account.��������).���, Is.EqualTo("0987654321"));
        //    Assert.That(((�����������)account.��������).���, Is.EqualTo("987654321"));
        //}

        //[Test]
        //public void NullableEnumCanSet()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });

        //    counterpartContractManager.Create(counterpart, new CounterpartyContract
        //    {
        //        CounterpartyCode = counterpartAccessObject.Code,
        //        CurrencyCode = "643",
        //        Name = "������",
        //        Type = CounterpartContractKind.OutgoingWithAgency
        //    });

        //    var contracts = store1C.Select<��������������������>()
        //        .Where(x => x.��������.��� == counterpart.Code)
        //        .ToArray();

        //    Assert.That(contracts.Length, Is.EqualTo(1));
        //    Assert.That(contracts[0].�����������, Is.EqualTo(�������������������������.�����������������������));
        //}

        //[Test]
        //public void EnumParameterMapping()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });

        //    counterpartContractManager.Create(counterpart, new CounterpartyContract
        //    {
        //        CounterpartyCode = counterpartAccessObject.Code,
        //        CurrencyCode = "643",
        //        Name = "������",
        //        Type = CounterpartContractKind.OutgoingWithAgency
        //    });

        //    var contracts = store1C.Select<��������������������>()
        //        .Where(x => x.����������� == �������������������������.�����������������������)
        //        .ToArray();

        //    Assert.That(contracts.Length, Is.EqualTo(1));
        //    Assert.That(contracts[0].�����������, Is.EqualTo(�������������������������.�����������������������));
        //}

        //[Test]
        //public void CanFilterForEmptyReference()
        //{
        //    var counterpartAccessObject = CreateTestCounterpart();
        //    var counterpartContractAccessObject = catalogRegistry.GetManager<CounterpertContractCatalog>().CreateItem();
        //    counterpartContractAccessObject.Owner = counterpartAccessObject.Reference;
        //    counterpartContractAccessObject.Description = "test-description";
        //    counterpartContractAccessObject.Write();

        //    var contracts = store1C.Select<��������������������>()
        //        .Where(x => x.��� == counterpartContractAccessObject.Code)
        //        .Where(x => x.������ == null)
        //        .ToArray();
        //    Assert.That(contracts.Length, Is.EqualTo(1));
        //    Assert.That(contracts[0].���, Is.EqualTo(counterpartContractAccessObject.Code));
        //    Assert.That(contracts[0].������, Is.Null);

        //    contracts = store1C.Select<��������������������>()
        //        .Where(x => x.��� == counterpartContractAccessObject.Code)
        //        .Where(x => x.������ != null)
        //        .ToArray();
        //    Assert.That(contracts.Length, Is.EqualTo(0));

        //    contracts = store1C.Select<��������������������>()
        //        .Where(x => counterpartContractAccessObject.Code == x.���)
        //        .Where(x => null == x.������)
        //        .ToArray();
        //    Assert.That(contracts.Length, Is.EqualTo(1));
        //    Assert.That(contracts[0].���, Is.EqualTo(counterpartContractAccessObject.Code));
        //    Assert.That(contracts[0].������, Is.Null);
        //}

        //[Test]
        //public void TableSections()
        //{
        //    var accessObject = incomingAccountingDocumentManager.Create(new AccountingDocument
        //    {
        //        Date = DateTime.Now,
        //        Number = "k-12345",
        //        Counterpart = new Counterpart
        //        {
        //            Inn = "7711223344",
        //            Name = "��� �������",
        //            LegalForm = LegalForm.Organization
        //        },
        //        Kind = DocumentKind.Incoming,
        //        NeedPosting = false,
        //        SumIncludesNds = true,
        //        IsCreatedByEmployee = false,
        //        NdsRate = NdsRate.NoNds,
        //        Nomenclatures = new[]
        //        {
        //            new Nomenclature
        //            {
        //                Name = "�������� ����",
        //                NdsRate = NdsRate.NoNds,
        //                Type = NomenclatureType.Service,
        //                Count = 1,
        //                Price = 1000m,
        //                Sum = 1000m,
        //                ServiceDescription = "�������� ����",
        //                Units = "��.",
        //                NdsSum = 0m,
        //            }
        //        }
        //    });

        //    var acts = store1C
        //        .Select<�����������������������>()
        //        .Single(x => x.����� == accessObject.Number && x.���� == accessObject.Date);
        //    Assert.That(acts.������.Count, Is.EqualTo(1));
        //    Assert.That(acts.������[0].�����, Is.EqualTo(1000m));
        //}

        //[Test]
        //public void CanAddCounterparty()
        //{
        //    var counterparty = new �����������
        //    {
        //        ��� = "1234567890",
        //        ������������ = "test-counterparty",
        //        ������������������������� = �������������������������.���������������
        //    };
        //    store1C.Save(counterparty);
        //    Assert.That(string.IsNullOrEmpty(counterparty.���), Is.False);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
        //    {
        //        {"Code", counterparty.���}
        //    });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("���"), Is.EqualTo("1234567890"));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test-counterparty"));
        //    Assert.That(
        //        enumerationManager.Is(
        //            valueTable[0].GetDispatchObject<Enumeration<LegalForm>>("�������������������������"),
        //            LegalForm.Organization));
        //}

        //[Test]
        //public void CanAddCounterpartyWithNullableEnum()
        //{
        //    var counterparty = new �����������
        //    {
        //        ��� = "1234567890",
        //        ������������ = "test-counterparty",
        //        ������������������������� = null
        //    };
        //    store1C.Save(counterparty);
        //    Assert.That(string.IsNullOrEmpty(counterparty.���), Is.False);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
        //    {
        //        {"Code", counterparty.���}
        //    });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("���"), Is.EqualTo("1234567890"));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test-counterparty"));
        //    Assert.That(ComHelpers.Invoke(valueTable[0]["�������������������������"], "IsEmpty"), Is.True);
        //}

        //[Test]
        //public void CanAddCounterpartyContract()
        //{
        //    var organization = store1C.Select<�����������>().Single(x => x.��� == organizationInn);
        //    var counterparty = new �����������
        //    {
        //        ��� = "1234567890",
        //        ������������ = "test-counterparty",
        //        ������������������������� = �������������������������.���������������
        //    };
        //    store1C.Save(counterparty);

        //    var counterpartyFromStore = store1C.Select<�����������>().Single(x => x.��� == counterparty.���);
        //    var counterpartyContract = new ��������������������
        //    {
        //        ����������� = �������������������������.������������,
        //        ������������ = "test name",
        //        �������� = counterpartyFromStore,
        //        ����������� = organization
        //    };
        //    store1C.Save(counterpartyContract);
        //    Assert.That(string.IsNullOrEmpty(counterpartyContract.���), Is.False);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.�������������������� ��� ���=&Code", new Dictionary<string, object>
        //    {
        //        {"Code", counterpartyContract.���}
        //    });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test name"));
        //    Assert.That(valueTable[0].GetDispatchObject<Reference<CounterpartAccessObject>>("��������").Object.Code,
        //        Is.EqualTo(counterparty.���));
        //    valueTable = queryExecuter.Execute("������� * �� ����������.����������� ��� ���=&Code", new Dictionary<string, object>
        //    {
        //        {"Code", counterparty.���}
        //    });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //}

        //[Test]
        //public void CanAddRecursive()
        //{
        //    var organization = store1C.Select<�����������>().Single(x => x.��� == organizationInn);
        //    var counterpartyContract = new ��������������������
        //    {
        //        ����������� = �������������������������.������������,
        //        ������������ = "test name",
        //        �������� = new �����������
        //        {
        //            ��� = "1234567890",
        //            ������������ = "test-counterparty",
        //            ������������������������� = �������������������������.���������������
        //        },
        //        ����������� = organization
        //    };
        //    store1C.Save(counterpartyContract);
        //    Assert.That(string.IsNullOrEmpty(counterpartyContract.���), Is.False);
        //    Assert.That(string.IsNullOrEmpty(counterpartyContract.��������.���), Is.False);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.�������������������� ��� ���=&Code", new Dictionary<string, object>
        //    {
        //        {"Code", counterpartyContract.���}
        //    });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("test name"));
        //}

        //[Test]
        //public void ChangeMustBeStrongerThanTracking()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Inn = "7711223344",
        //        Kpp = "771101001",
        //        FullName = "Test counterparty",
        //        Name = "Test counterparty"
        //    };
        //    var counterpartyAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartyAccessObject.Code;

        //    var counterpartContractAccessObject = counterpartContractManager.Create(counterpart,
        //        CounterpartContractKind.Incoming, "643");

        //    var contract = store1C.Select<��������������������>()
        //        .Single(x => x.��� == counterpartContractAccessObject.Code);
        //    if (contract.��������.��� == "7711223344")
        //    {
        //        contract.��������.��� = "7711223345";
        //        contract.�������� = new �����������
        //        {
        //            ��� = "7711223355",
        //            ������������ = "Test counterparty 2",
        //            ������������������ = "Test counterparty 2"
        //        };
        //    }
        //    store1C.Save(contract);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.�������������������� ��� ���=&Code",
        //        new Dictionary<string, object>
        //        {
        //            {"Code", contract.���}
        //        });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(ComHelpers.GetProperty(valueTable[0]["��������"], "������������"), Is.EqualTo("Test counterparty 2"));
        //}

        //[Test]
        //public void ModifyCounterparty()
        //{
        //    var counterpartyAccessObject = counterpartManager.Create(new Counterpart
        //    {
        //        Inn = "7711223344",
        //        Kpp = "771101001",
        //        FullName = "Test counterparty",
        //        Name = "Test counterparty"
        //    });

        //    var counterparty = store1C.Select<�����������>().Single(x => x.��� == counterpartyAccessObject.Code);
        //    counterparty.��� = "7711223344";
        //    counterparty.������������ = "Test counterparty 2";
        //    counterparty.������������������ = "Test counterparty 2";
        //    store1C.Save(counterparty);
        //    var valueTable = queryExecuter.Execute("������� * �� ����������.����������� ��� ���=&Inn",
        //        new Dictionary<string, object>
        //        {
        //            {"Inn", "7711223344"}
        //        });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("Test counterparty 2"));
        //}

        //[Test]
        //public void CanUnpostDocument()
        //{
        //    var ����������������������� = CreateFullFilledDocument();
        //    �����������������������.�������� = true;
        //    store1C.Save(�����������������������);

        //    �����������������������.������[0].���������� = "����";
        //    store1C.Save(�����������������������);

        //    var document = GetDocumentByNumber(�����������������������.�����);
        //    Assert.That(document.��������, Is.True);
        //    Assert.That(document.������.��������(0).����������, Is.EqualTo("����"));
        //}

        //[Test]
        //public void CanPostDocuments()
        //{
        //    var ����������������������� = CreateFullFilledDocument();
        //    store1C.Save(�����������������������);
        //    Assert.That(GetDocumentByNumber(�����������������������.�����).��������, Is.False);
        //    �����������������������.�������� = true;
        //    store1C.Save(�����������������������);
        //    Assert.That(GetDocumentByNumber(�����������������������.�����).��������);
        //}

        //private ����������������������� CreateFullFilledDocument()
        //{
        //    var ���������� = new �����������
        //    {
        //        ��� = "7711223344",
        //        ������������ = "��� �������� ����������"
        //    };
        //    var ����������� = store1C.Single<�����������>(x => x.��� == organizationInn);
        //    var �������������������� = store1C.Single<������>(x => x.��� == "643");
        //    var �������������������� = new ��������������������
        //    {
        //        �������� = ����������,
        //        ����������� = �����������,
        //        ����������� = �������������������������.������������,
        //        ������������ = "test contract",
        //        ����������� = "test contract comment",
        //        �������������������� = ��������������������
        //    };
        //    var ����26 = store1C.Single<������������>(x => x.��� == "26");
        //    var ����1904 = store1C.Single<������������>(x => x.��� == "19.04");
        //    var ����6001 = store1C.Single<������������>(x => x.��� == "60.01");
        //    var ����6002 = store1C.Single<������������>(x => x.��� == "60.02");
        //    var ������������������� = new ������������
        //    {
        //        ������������ = "������������ �������",
        //        ������������� = ��������������.�������������������
        //    };
        //    return new �����������������������
        //    {
        //        ���������������������� = new DateTime(2016, 6, 1),
        //        ���� = new DateTime(2016, 6, 1),
        //        ����������������������� = "12345",
        //        ����������� = �����������������������������������.������,
        //        ���������� = ����������,
        //        ������������������ = ��������������������,
        //        ����������� = �����������,
        //        ������������������� = ��������������������.�������������,
        //        ��������������� = ��������������������,
        //        ������������������������������ = ����6001,
        //        �������������������������� = ����6002,
        //        ������ = new List<�����������������������.��������������������>
        //        {
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "�������"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� � ����������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m,
        //                ���������� = ����26,
        //                ������������ = ����26,
        //                ������������ = ����1904,
        //                ������������� = �������������.�����������,
        //                ��������1 = �������������������,
        //                ������������������� = ����������������������.�����������������������������()
        //            },
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "����� ������"
        //                },
        //                ���������� = 10,
        //                ���������� = "����� ������ ������������� �����",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m,
        //                ���������� = ����26,
        //                ������������ = ����26,
        //                ������������ = ����1904,
        //                ������������� = �������������.�����������,
        //                ��������1 = �������������������,
        //                ������������������� = ����������������������.�����������������������������()
        //            }
        //        }
        //    };
        //}

        //private dynamic GetDocumentByNumber(string number)
        //{
        //    var valueTable = queryExecuter.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
        //        new Dictionary<string, object>
        //        {
        //            {"Number", number}
        //        });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    return valueTable[0]["������"];
        //}

        //[Test]
        //public void CanAddDocumentWithTableSection()
        //{
        //    var ����������������������� = new �����������������������
        //    {
        //        ���������������������� = new DateTime(2016, 6, 1),
        //        ���� = new DateTime(2016, 6, 1),
        //        ����������������������� = "12345",
        //        ����������� = �����������������������������������.������,
        //        ���������� = new �����������
        //        {
        //            ��� = "7711223344",
        //            ������������ = "��� �������� ����������",
        //        },
        //        ����������� = store1C.Select<�����������>().Single(x => x.��� == organizationInn),
        //        ������������������� = ��������������������.�������������,
        //        ������ = new List<�����������������������.��������������������>
        //        {
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "�������"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� � ����������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            },
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "����� ������"
        //                },
        //                ���������� = 10,
        //                ���������� = "����� ������ ������������� �����",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            }
        //        }
        //    };

        //    store1C.Save(�����������������������);

        //    Assert.That(�����������������������.�����, Is.Not.Null);
        //    Assert.That(�����������������������.����, Is.Not.EqualTo(default(DateTime)));
        //    Assert.That(�����������������������.������.Count, Is.EqualTo(2));
        //    Assert.That(�����������������������.������[0].�����������, Is.EqualTo(1));
        //    Assert.That(�����������������������.������[1].�����������, Is.EqualTo(2));

        //    var valueTable = queryExecuter.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
        //        new Dictionary<string, object>
        //        {
        //            {"Number", �����������������������.�����}
        //        });

        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("�����������������������"), Is.EqualTo("12345"));
        //    Assert.That(valueTable[0].GetDateTime("����������������������"), Is.EqualTo(new DateTime(2016, 6, 1)));

        //    var servicesTablePart = valueTable[0]["������"] as dynamic;
        //    Assert.That(servicesTablePart.Count, Is.EqualTo(2));

        //    var row1 = servicesTablePart.��������(0);
        //    Assert.That(row1.������������.������������, Is.EqualTo("�������"));
        //    Assert.That(row1.����������, Is.EqualTo(10));
        //    Assert.That(row1.����������, Is.EqualTo("������� � ����������"));
        //    Assert.That(row1.�����, Is.EqualTo(120));
        //    Assert.That(row1.����, Is.EqualTo(12));

        //    var row2 = servicesTablePart.��������(1);
        //    Assert.That(row2.������������.������������, Is.EqualTo("����� ������"));
        //    Assert.That(row2.����������, Is.EqualTo("����� ������ ������������� �����"));
        //}

        //[Test]
        //public void CanModifyTableSection()
        //{
        //    var ����������������������� = new �����������������������
        //    {
        //        ���������������������� = new DateTime(2016, 6, 1),
        //        ���� = new DateTime(2016, 6, 1),
        //        ����������������������� = "12345",
        //        ����������� = �����������������������������������.������,
        //        ���������� = new �����������
        //        {
        //            ��� = "7711223344",
        //            ������������ = "��� �������� ����������",
        //        },
        //        ����������� = store1C.Select<�����������>().Single(x => x.��� == organizationInn),
        //        ������������������� = ��������������������.�������������,
        //        ������ = new List<�����������������������.��������������������>
        //        {
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "�������"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� � ����������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            }
        //        }
        //    };
        //    store1C.Save(�����������������������);

        //    �����������������������.������[0].���������� = "������� ������";
        //    store1C.Save(�����������������������);

        //    var valueTable = queryExecuter.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
        //        new Dictionary<string, object>
        //        {
        //            {"Number", �����������������������.�����}
        //        });

        //    Assert.That(valueTable.Count, Is.EqualTo(1));

        //    var servicesTablePart = valueTable[0]["������"] as dynamic;
        //    Assert.That(servicesTablePart.Count, Is.EqualTo(1));

        //    var row1 = servicesTablePart.��������(0);
        //    Assert.That(row1.����������, Is.EqualTo("������� ������"));
        //}

        //[Test]
        //public void CanDeleteItemFromTableSection()
        //{
        //    var ����������������������� = new �����������������������
        //    {
        //        ���������������������� = new DateTime(2016, 6, 1),
        //        ���� = new DateTime(2016, 6, 1),
        //        ����������������������� = "12345",
        //        ����������� = �����������������������������������.������,
        //        ���������� = new �����������
        //        {
        //            ��� = "7711223344",
        //            ������������ = "��� �������� ����������",
        //        },
        //        ����������� = store1C.Select<�����������>().Single(x => x.��� == organizationInn),
        //        ������������������� = ��������������������.�������������,
        //        ������ = new List<�����������������������.��������������������>
        //        {
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "�������"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� � ����������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            },
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "������� ����"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� ������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            },
        //        }
        //    };
        //    store1C.Save(�����������������������);

        //    �����������������������.������.RemoveAt(0);
        //    store1C.Save(�����������������������);

        //    var valueTable = queryExecuter.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
        //        new Dictionary<string, object>
        //        {
        //            {"Number", �����������������������.�����}
        //        });

        //    Assert.That(valueTable.Count, Is.EqualTo(1));

        //    var servicesTablePart = valueTable[0]["������"] as dynamic;
        //    Assert.That(servicesTablePart.Count, Is.EqualTo(1));

        //    var row1 = servicesTablePart.��������(0);
        //    Assert.That(row1.����������, Is.EqualTo("������� ������"));
        //}

        //[Test]
        //public void CanChangeTableSectionItemsOrdering()
        //{
        //    var ����������������������� = new �����������������������
        //    {
        //        ���������������������� = new DateTime(2016, 6, 1),
        //        ���� = new DateTime(2016, 6, 1),
        //        ����������������������� = "12345",
        //        ����������� = �����������������������������������.������,
        //        ���������� = new �����������
        //        {
        //            ��� = "7711223344",
        //            ������������ = "��� �������� ����������",
        //        },
        //        ����������� = store1C.Select<�����������>().Single(x => x.��� == organizationInn),
        //        ������������������� = ��������������������.�������������,
        //        ������ = new List<�����������������������.��������������������>
        //        {
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "�������"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� � ����������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            },
        //            new �����������������������.��������������������
        //            {
        //                ������������ = new ������������
        //                {
        //                    ������������ = "������� ����"
        //                },
        //                ���������� = 10,
        //                ���������� = "������� ������",
        //                ����� = 120,
        //                ���� = 12,
        //                ��������� = ���������.���18,
        //                �������� = 21.6m
        //            },
        //        }
        //    };
        //    store1C.Save(�����������������������);

        //    var t = �����������������������.������[0];
        //    �����������������������.������[0] = �����������������������.������[1];
        //    �����������������������.������[1] = t;
        //    store1C.Save(�����������������������);

        //    var valueTable = queryExecuter.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
        //        new Dictionary<string, object>
        //        {
        //            {"Number", �����������������������.�����}
        //        });

        //    Assert.That(valueTable.Count, Is.EqualTo(1));

        //    var servicesTablePart = valueTable[0]["������"] as dynamic;
        //    Assert.That(servicesTablePart.Count, Is.EqualTo(2));

        //    var row0 = servicesTablePart.��������(0);
        //    Assert.That(row0.����������, Is.EqualTo("������� ������"));

        //    var row1 = servicesTablePart.��������(1);
        //    Assert.That(row1.����������, Is.EqualTo("������� � ����������"));
        //}

        //[Test]
        //public void ModifyReference()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Inn = "7711223344",
        //        Kpp = "771101001",
        //        FullName = "Test counterparty",
        //        Name = "Test counterparty"
        //    };
        //    var counterpartyAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartyAccessObject.Code;

        //    var counterpartContractAccessObject = counterpartContractManager.Create(counterpart,
        //        CounterpartContractKind.Incoming, "643");

        //    var contract = store1C.Select<��������������������>()
        //        .Single(x => x.��� == counterpartContractAccessObject.Code);
        //    contract.��������.��� = "7711223344";
        //    contract.��������.������������ = "Test counterparty 2";
        //    contract.��������.������������������ = "Test counterparty 2";
        //    store1C.Save(contract);

        //    var valueTable = queryExecuter.Execute("������� * �� ����������.����������� ��� ���=&Inn",
        //        new Dictionary<string, object>
        //        {
        //            {"Inn", "7711223344"}
        //        });
        //    Assert.That(valueTable.Count, Is.EqualTo(1));
        //    Assert.That(valueTable[0].GetString("������������"), Is.EqualTo("Test counterparty 2"));
        //}

        //public interface IGenericCatalog
        //{
        //    string ������������ { get; set; }
        //}

        //[Test]
        //public void CanQueryWithSourceNameViaGenericInterface()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Inn = "7711223344",
        //        Kpp = "771101001",
        //        FullName = "Test counterparty 1",
        //        Name = "Test counterparty 2"
        //    };
        //    counterpartManager.Create(counterpart);

        //    var catalogItem = store1C.Select<IGenericCatalog>("����������.�����������")
        //        .Where(x => x.������������ == "Test counterparty 2")
        //        .Cast<object>()
        //        .Single();
        //    Assert.That(catalogItem, Is.TypeOf<�����������>());
        //}

        //private CounterpartAccessObject CreateTestCounterpart()
        //{
        //    var counterpart = new Counterpart
        //    {
        //        Name = "test-counterpart-name",
        //        Inn = "0987654321",
        //        Kpp = "987654321"
        //    };
        //    var counterpartAccessObject = counterpartManager.Create(counterpart);
        //    counterpart.Code = counterpartAccessObject.Code;
        //    bankAccountManager.CreateAccount(counterpartAccessObject.Code, BankAccountOwnerType.JuridicalCounterparty,
        //        new BankAccount
        //        {
        //            Bank = new Bank
        //            {
        //                Bik = Banks.AlfaBankBik
        //            },
        //            Number = "40702810001111122222",
        //            CurrencyCode = "643"
        //        });
        //    return counterpartAccessObject;
        //}
    }
}