using System;
using System.Linq;
using NUnit.Framework;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.�����������;

namespace Simple1C.Tests.Integration
{
    internal class UniqueIdentifierTest : COMDataContextTestBase
    {
        [Test]
        public void CanSearchByRef()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            dataContext.Save(����������);

            var ����������2 = dataContext.Select<�����������>().Single(x => x == ����������);
            Assert.That(����������2.������������, Is.EqualTo("test contractor name"));
            Assert.That(����������2.���, Is.EqualTo("test-inn"));
        }

        [Test]
        public void CanSaveUniqueIdentifier()
        {
            var ���������� = new �����������
            {
                ������������ = "test contractor name",
                ��� = "test-inn"
            };
            Assert.That(����������.�����������������������, Is.Null);
            dataContext.Save(����������);
            Assert.That(����������.�����������������������, Is.Not.Null);
            Assert.That(����������.�����������������������, Is.Not.EqualTo(Guid.Empty));

            var ����������2 = dataContext.Single<�����������>(x => x.��� == ����������.���);
            Assert.That(����������2.�����������������������, Is.EqualTo(����������.�����������������������));
        }

        [Test]
        public void CanSelectUniqueIdentifier()
        {
            var ����������1 = new �����������
            {
                ������������ = "test contractor name1",
                ��� = "test-inn1"
            };
            dataContext.Save(����������1);
            var ����������2 = new �����������
            {
                ������������ = "test contractor name2",
                ��� = "test-inn2"
            };
            dataContext.Save(����������2);

            var ����������� = dataContext.Select<�����������>()
                .Where(x => x.��� == ����������1.��� || x.��� == ����������2.���)
                .OrderByDescending(x => x.������������)
                .Select(x => new
                {
                    x.�����������������������,
                    x.������������
                })
                .ToArray();
            Assert.That(�����������.Length, Is.EqualTo(2));
            Assert.That(�����������[0].�����������������������, Is.EqualTo(����������2.�����������������������));
            Assert.That(�����������[0].������������, Is.EqualTo(����������2.������������));
            Assert.That(�����������[1].�����������������������, Is.EqualTo(����������1.�����������������������));
            Assert.That(�����������[1].������������, Is.EqualTo(����������1.������������));
        }

        [Test]
        public void CanQueryByUniqueIdentifier()
        {
            var ����������1 = new �����������
            {
                ������������ = "test contractor name1",
                ��� = "test-inn1"
            };
            dataContext.Save(����������1);

            var ����������2 = new �����������
            {
                ������������ = "test contractor name2",
                ��� = "test-inn2"
            };
            dataContext.Save(����������2);

            var ����������3 =
                dataContext.Single<�����������>(x => x.����������������������� == ����������2.�����������������������);
            Assert.That(����������3.������������, Is.EqualTo("test contractor name2"));
        }
    }
}