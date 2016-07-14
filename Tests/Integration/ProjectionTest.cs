using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.���������;
using Simple1C.Tests.Metadata1C.�����������;
using Simple1C.Tests.Metadata1C.�����������;

namespace Simple1C.Tests.Integration
{
    internal class ProjectionTest : COMDataContextTestBase
    {
        public class TestDataContract
        {
            public DateTime ���� { get; set; }
            public string ����������_��� { get; set; }
            public string ����������_������������ { get; set; }
            public string ������������������_������������ { get; set; }
            public bool ������������������_�������� { get; set; }
            public string ���������������_������������ { get; set; }
            public string ���������������_��� { get; set; }
        }

        [Test]
        public void ProjectionToRegularType()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            var ��� = new �����������������������
            {
                ���� = new DateTime(2016, 6, 1),
                ���������� = ����������,
                ������������������ = new ��������������������
                {
                    �������� = ����������,
                    ������������ = "test contract",
                    �������� = true
                }
            };
            dataContext.Save(���);

            var ���2 = dataContext.Select<�����������������������>()
                .Where(x => x.����� == ���.����� && x.���� == ���.����)
                .Select(x => new TestDataContract
                {
                    ���� = x.����.GetValueOrDefault(),
                    ����������_��� = x.����������.���,
                    ����������_������������ = x.����������.������������,
                    ������������������_������������ = x.������������������.������������,
                    ������������������_�������� = x.������������������.��������,
                    ���������������_������������ = x.���������������.������������,
                    ���������������_��� = x.���������������.���
                })
                .ToArray();
            Assert.That(���2.Length, Is.EqualTo(1));
            Assert.That(���2[0].����, Is.EqualTo(new DateTime(2016, 6, 1)));
            Assert.That(���2[0].����������_���, Is.EqualTo("test-inn"));
            Assert.That(���2[0].����������_������������, Is.EqualTo("test contractor name"));
            Assert.That(���2[0].������������������_������������, Is.EqualTo("test contract"));
            Assert.That(���2[0].������������������_��������, Is.True);
            Assert.That(���2[0].���������������_������������, Is.Null);
            Assert.That(���2[0].���������������_���, Is.Null);
        }

        [Test]
        public void ProjectionToAnonymousType()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            var ��� = new �����������������������
            {
                ���� = new DateTime(2016, 6, 1),
                ���������� = ����������,
                ������������������ = new ��������������������
                {
                    �������� = ����������,
                    ������������ = "test contract",
                    �������� = true
                }
            };
            dataContext.Save(���);

            var ���2 = dataContext.Select<�����������������������>()
                .Where(x => x.����� == ���.����� && x.���� == ���.����)
                .Select(x => new
                {
                    x.����,
                    ����������_��� = x.����������.���,
                    ����������_������������ = x.����������.������������,
                    ������������������_������������ = x.������������������.������������,
                    ������������������_�������� = x.������������������.��������,
                    ���������������_������������ = x.���������������.������������,
                    ���������������_��� = x.���������������.���
                })
                .ToArray();
            Assert.That(���2.Length, Is.EqualTo(1));
            Assert.That(���2[0].����, Is.EqualTo(new DateTime(2016, 6, 1)));
            Assert.That(���2[0].����������_���, Is.EqualTo("test-inn"));
            Assert.That(���2[0].����������_������������, Is.EqualTo("test contractor name"));
            Assert.That(���2[0].������������������_������������, Is.EqualTo("test contract"));
            Assert.That(���2[0].������������������_��������, Is.True);
            Assert.That(���2[0].���������������_������������, Is.Null);
            Assert.That(���2[0].���������������_���, Is.Null);
        }

        [Test]
        public void ProjectionToAnonymousTypeWithConstants()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            dataContext.Save(����������);
            var ����������2 = dataContext.Select<�����������>()
                .Where(x => x.������������ == "test contractor name")
                .Select(x => new
                {
                    ����������_��� = x.���,
                    SomeConstant = GetConstant(),
                    SomeNullContant = (string) null
                })
                .ToArray();
            Assert.That(����������2.Length, Is.EqualTo(1));
            Assert.That(����������2[0].����������_���, Is.EqualTo("test-inn"));
            Assert.That(����������2[0].SomeConstant, Is.EqualTo("test-constant"));
            Assert.That(����������2[0].SomeNullContant, Is.Null);
        }

        private static string GetConstant()
        {
            return "test-constant";
        }

        [Test]
        public void CanEvaluateExpressionsLocally()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            dataContext.Save(����������);
            var selectedContractor = dataContext.Select<�����������>()
                .Where(x => x.������������ == "test contractor name")
                .Select(x => new
                {
                    x.������������,
                    ���������������� = x.������������ + "$$$" + x.���
                })
                .ToArray()
                .Single();
            Assert.That(selectedContractor.������������, Is.EqualTo("test contractor name"));
            Assert.That(selectedContractor.����������������, Is.EqualTo("test contractor name$$$test-inn"));
        }

        private class NameWithDescription
        {
            public string name;
            public string description;
        }

        [Test]
        public void CanMapLocalExpresionToNamedType()
        {
            var ����������1 = new �����������
            {
                ������������ = "test-shortname1",
                ��� = "test-inn1",
                ��� = "test-kpp",
                ����������� = "test-comment1"
            };
            var ����������2 = new �����������
            {
                ������������ = "test-shortname2",
                ������������������ = "test-fullname2",
                ��� = "test-inn2",
                ��� = "test-kpp",
                ����������� = "test-comment2"
            };
            dataContext.Save(����������1);
            dataContext.Save(����������2);
            var selectedContractors = dataContext.Select<�����������>()
                .Where(x => x.��� == "test-kpp")
                .Select(x => new NameWithDescription
                {
                    name = x.������������,
                    description = (string.IsNullOrEmpty(x.������������������) ? x.������������ : x.������������������)
                                  + "(" + x.��� + ") " + x.�����������
                })
                .OrderBy(x => x.name)
                .ToArray();
            Assert.That(selectedContractors[0].name, Is.EqualTo("test-shortname1"));
            Assert.That(selectedContractors[0].description, Is.EqualTo("test-shortname1(test-inn1) test-comment1"));

            Assert.That(selectedContractors[1].name, Is.EqualTo("test-shortname2"));
            Assert.That(selectedContractors[1].description, Is.EqualTo("test-fullname2(test-inn2) test-comment2"));
        }

        [Test]
        public void CanUseLocalMethodsInProjection()
        {
            var ����������1 = new �����������
            {
                ������������ = "test-shortname1",
                ��� = "test-kpp"
            };

            dataContext.Save(����������1);
            var selectedContractors = dataContext.Select<�����������>()
                .Where(x => x.��� == "test-kpp")
                .Select(x => new
                {
                    GetWrap(x.������������).description
                })
                .Single();
            Assert.That(selectedContractors.description, Is.EqualTo("test-shortname1"));
        }

        private static DescriptionHolder GetWrap(string description)
        {
            return new DescriptionHolder {description = description};
        }

        private class DescriptionHolder
        {
            public string description;
        }

        [Test]
        public void CanSelectSameFieldWithDifferentAliases()
        {
            var ����2001 = dataContext.Single<������������>(x => x.��� == "20.01");
            var ����26 = dataContext.Single<������������>(x => x.��� == "26");
            var ���������� = new �����������();
            var ������������������� = new �������������������
            {
                ���� = new DateTime(2016, 6, 1),
                ���������� = ����������,
                ���������� = ����2001,
                ��������� = new List<�������������������.�����������������������>()
                {
                    new �������������������.�����������������������()
                    {
                        ���������� = ����26
                    }
                }
            }; 
            dataContext.Save(�������������������);

            var result = dataContext.Select<�������������������>()
                .Where(x => x.���������� == ����������)
                .SelectMany(��������� => ���������.���������.Select(x =>
                    new
                    {
                        ItemCode = x.����������.���,
                        DocCode = ���������.����������.���
                    }))
                .Single();
            Assert.That(result.DocCode, Is.EqualTo("20.01"));
            Assert.That(result.ItemCode, Is.EqualTo("26"));
        }
    }
}