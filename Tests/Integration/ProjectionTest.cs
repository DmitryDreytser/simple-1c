using System;
using System.Linq;
using NUnit.Framework;
using Simple1C.Tests.Metadata1C.���������;
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
    }
}