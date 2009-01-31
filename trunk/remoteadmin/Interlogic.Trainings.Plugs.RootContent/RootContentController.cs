using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class RootContentController:InstanceController
	{
		public RootContentController(ITransactionContext context)
			: base(context)
		{
		}
        void Insert(RootContent rootCont)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                factory.InternalInsert(rootCont);
            }
        }

        void Update(RootContent rootCont)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                factory.InternalUpdate(rootCont);
            }
        }

        void Delete(RootContent rootCont)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                factory.InternalDelete(rootCont);
            }
        }

        #region Loads
        public List<RootContent> LoadAll()
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadAll();
            }
        }

        public RootContent LoadById(int id)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadByPrimaryKey(id);
            }
        }

        public RootContent LoadByInstanceName(string instanceName)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadByInstanceName(instanceName);
            }
        }

        public RootContent LoadByFriendlyName(string friendlyName)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadByFriendlyName(friendlyName);
            }
        }

        public List<RootContent> LoadByClassDefinitionId(int id)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadByClassDefinitionId(id);
            }
        }

        public List<RootContent> LoadByParentInstanceId(int parentId)
        {
            using (RootContentFactory factory = RootContentFactory.GetInstance())
            {
                return factory.InternalLoadByParentInstanceId(parentId);
            }
        }
        #endregion
    }
}
