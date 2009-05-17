using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
    using DomainModel;
    using System.IO;
    using System.Reflection;

    public class PlugLocationController : DomainController
    {
        public PlugLocationController(ITransactionContext context)
            : base(context)
        {
        }

        void Insert(PlugLocation plugLoc)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                factory.InternalInsert(plugLoc);
            }
        }

        void Update(PlugLocation plugLoc)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                factory.InternalUpdate(plugLoc);
            }
        }

        void Delete(PlugLocation plugLoc)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                factory.InternalDelete(plugLoc);
            }
        }

        #region Loads
        public List<PlugLocation> LoadAll()
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                return factory.InternalLoadAll();
            }
        }

        public PlugLocation LoadById(int id)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                return factory.InternalLoadByPrimaryKey(id);
            }
        }

        public PlugLocation LoadByName(string name)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                return factory.InternalLoadByName(name);
            }
        }

        public List<PlugLocation> LoadByPlugId(int plugId)
        {
            using (PlugLocationFactory factory = PlugLocationFactory.GetInstance())
            {
                return factory.InternalLoadByPlugId(plugId);
            }
        }
        #endregion
    }
}
