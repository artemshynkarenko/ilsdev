using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.RootContent
{
	public class RootContent:Instance
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

		private List<RootContent> _childContent;

		public List<RootContent> ChildContent
		{
			get { return _childContent; }
			set { _childContent = value; }
		}

		private RootContent _parentContent;

		public RootContent ParentContent
		{
			get { return _parentContent; }
			set { _parentContent = value; }
		}

		protected override DomainController GetControllerInstance(ITransactionContext context)
		{
			return new RootContentController(context);
		}

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			base.Setup(dbInstance, context);
		}
	}
}
