using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class StaticContentController:InstanceController
	{
		public StaticContentController(ITransactionContext context)
			: base(context)
		{
		}
        void Insert(StaticContent rootCont)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                factory.InternalInsert(rootCont);
            }
        }

        void Update(StaticContent rootCont)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                factory.InternalUpdate(rootCont);
            }
        }

        void Delete(StaticContent rootCont)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                factory.InternalDelete(rootCont);
            }
        }

        #region Loads
        public List<StaticContent> LoadAll()
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadAll();
            }
        }

        public StaticContent LoadById(int id)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadByPrimaryKey(id);
            }
        }

        public StaticContent LoadByInstanceName(string instanceName)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadByInstanceName(instanceName);
            }
        }

        public StaticContent LoadByFriendlyName(string friendlyName)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadByFriendlyName(friendlyName);
            }
        }

        public List<StaticContent> LoadByClassDefinitionId(int id)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadByClassDefinitionId(id);
            }
        }

        public List<StaticContent> LoadByParentInstanceId(int parentId)
        {
            using (StaticContentFactory factory = StaticContentFactory.GetInstance())
            {
                return factory.InternalLoadByParentInstanceId(parentId);
            }
        }
        #endregion
    }
}
