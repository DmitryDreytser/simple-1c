using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.���������;
using Simple1C.Tests.Metadata1C.������������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.TestEntities;

namespace Simple1C.Tests.Integration
{
    internal class TableSectionsTest : COMDataContextTestBase
    {
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
                })
                .Unload();

            Assert.That(valueTable.Count, Is.EqualTo(1));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(2));

            var row0 = servicesTablePart.��������(0);
            Assert.That(row0.����������, Is.EqualTo("������� ������"));

            var row1 = servicesTablePart.��������(1);
            Assert.That(row1.����������, Is.EqualTo("������� � ����������"));
        }

        [Test]
        public void DoNotOverwriteDocumentWhenObservedListDoesNotChange()
        {
            var ��� = new �����������������������
            {
                ���� = new DateTime(2016, 6, 1),
                ���������� = new �����������
                {
                    ������������ = "contractor name"
                }
            };
            dataContext.Save(���);

            var docVersion = GetDocumentByNumber(���.�����).DataVersion;

            var ���2 = dataContext.Single<�����������������������>(x => x.����� == ���.����� && x.���� == ���.����);
            Assert.That(���2.������.Count, Is.EqualTo(0));
            ���2.����������.������������ = "changed contractor name";
            dataContext.Save(���2);

            var ���������� = dataContext.Single<�����������>(x => x.��� == ���.����������.���);
            Assert.That(����������.������������, Is.EqualTo("changed contractor name"));
            var newDocVersion = GetDocumentByNumber(���.�����).DataVersion;
            Assert.That(newDocVersion, Is.EqualTo(docVersion));
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
                }).Unload();

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

            �����������������������.������.RemoveAt(0);
            dataContext.Save(�����������������������);

            var valueTable = globalContext.Execute("������� * �� ��������.����������������������� ��� ����� = &Number",
                new Dictionary<string, object>
                {
                    {"Number", �����������������������.�����}
                }).Unload();

            Assert.That(valueTable.Count, Is.EqualTo(1));

            var servicesTablePart = valueTable[0]["������"] as dynamic;
            Assert.That(servicesTablePart.Count, Is.EqualTo(1));

            var row1 = servicesTablePart.��������(0);
            Assert.That(row1.����������, Is.EqualTo("������� ������"));
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
                }).Unload();

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
    }
}