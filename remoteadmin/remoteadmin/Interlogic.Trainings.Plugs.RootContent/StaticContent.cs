using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class StaticContent:Instance
	{
		private string _contentName;

		public string ContentFriendlyName
		{
			get { return _contentName; }
			set { _contentName = value; }
		}

		private string _contentDescription;

		public string ContentDescription
		{
			get { return _contentDescription; }
			set { _contentDescription = value; }
		}

		private string _contentImageSrc;

		public string ContentImageSrc
		{
			get { return _contentImageSrc; }
			set { _contentImageSrc = value; }
		}

		private List<StaticContent> _childContent;

		public List<StaticContent> ChildContent
		{
			get { return _childContent; }
			set { _childContent = value; }
		}

		private StaticContent _parentContent;

		public StaticContent ParentContent
		{
			get { return _parentContent; }
			set { _parentContent = value; }
		}

		protected override DomainController GetControllerInstance(ITransactionContext context)
		{
			return new StaticContentController(context);
		}

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			base.Setup(dbInstance, context);
		}
	}
}
