using Simple1C.Impl;

namespace Simple1C.Tests.TestEntities
{
    internal class SimpleCounterpartManager
    {
        private readonly GlobalContext globalContext;

        public SimpleCounterpartManager(GlobalContext globalContext)
        {
            this.globalContext = globalContext;
        }

        public dynamic ComObject
        {
            get { return globalContext.ComObject; }
        }

        public void Create(Counterpart counterpart)
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
        }
    }
}