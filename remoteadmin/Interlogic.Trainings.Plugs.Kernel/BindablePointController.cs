using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
    using DomainModel;
    using System.IO;
    using System.Reflection;

    public class BindablePointController : DomainController
    {
        public BindablePointController(ITransactionContext context)
            : base(context)
        {
        }

        void Insert(BindablePoint bindPoint)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                factory.InternalInsert(bindPoint);
            }
        }

        void Update(BindablePoint bindPoint)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                factory.InternalUpdate(bindPoint);
            }
        }

        void Delete(BindablePoint bindPoint)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                factory.InternalDelete(bindPoint);
            }
        }

        #region Loads
        public List<BindablePoint> LoadAll()
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                return factory.InternalLoadAll();
            }
        }

        public BindablePoint LoadById(int id)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                return factory.InternalLoadByPrimaryKey(id);
            }
        }

        public BindablePoint LoadByName(string name)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                return factory.InternalLoadByName(name);
            }
        }

        public List<BindablePoint> LoadByPointDefinitionId(int id)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                return factory.InternalLoadByPointDefinitionId(id);
            }
        }

         public List<BindablePoint> LoadByInstanceId(int id)
        {
            using (BindablePointFactory factory = BindablePointFactory.GetInstance())
            {
                return factory.InternalLoadByInstanceId(id);
            }
        }
        #endregion
    }
}
