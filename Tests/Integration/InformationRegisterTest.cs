using System;
using System.Linq;
using NUnit.Framework;
using Simple1C.Interface;
using Simple1C.Tests.Metadata1C.����������������;
using Simple1C.Tests.Metadata1C.�����������;

namespace Simple1C.Tests.Integration
{
    internal class InformationRegisterTest : COMDataContextTestBase
    {
        [Test]
        public void CanWritePreviouslyReadInformationRegister()
        {
            var ������ = new DateTime(2025, 7, 17);
            var ���� = new ����������
            {
                ������ = dataContext.Single<������>(x => x.��� == "643"),
                ��������� = 42,
                ���� = 12,
                ������ = ������
            };
            dataContext.Save(����);

            var ����2 = dataContext.Single<����������>(x => x.������ == ������);
            ����2.��������� = 43;
            dataContext.Save(����2);

            var ����3 = dataContext.Single<����������>(x => x.������ == ������);
            Assert.That(����3.������.���, Is.EqualTo("643"));
            Assert.That(����3.���������, Is.EqualTo(43));
            Assert.That(����3.����, Is.EqualTo(12));
            Assert.That(����3.������, Is.EqualTo(������));
        }

        [Test]
        public void CanReadWriteInformationRegister()
        {
            var ������ = new DateTime(2025, 7, 18);
            var ���� = new ����������
            {
                ������ = dataContext.Single<������>(x => x.��� == "643"),
                ��������� = 42,
                ���� = 12,
                ������ = ������
            };
            dataContext.Save(����);

            var ����2 = dataContext.Select<����������>()
                .OrderByDescending(x => x.������)
                .First();
            Assert.That(����2.������.���, Is.EqualTo("643"));
            Assert.That(����2.���������, Is.EqualTo(42));
            Assert.That(����2.����, Is.EqualTo(12));
            Assert.That(����2.������, Is.EqualTo(������));

            ����.��������� = 43;
            dataContext.Save(����);

            var ����3 = dataContext.Select<����������>()
                .OrderByDescending(x => x.������)
                .First();
            Assert.That(����3.������.���, Is.EqualTo("643"));
            Assert.That(����3.���������, Is.EqualTo(43));
            Assert.That(����3.����, Is.EqualTo(12));
            Assert.That(����3.������, Is.EqualTo(������));
        }
    }
}