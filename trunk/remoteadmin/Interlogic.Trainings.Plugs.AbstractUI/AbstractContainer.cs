using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
	public abstract class AbstractContainer : AbstractComponent, IAbstractUiContainer
	{
		protected AbstractContainer()
			: base()
		{
		}

		private bool _useLazyLoadingForChildren;

		protected bool UseLazyLoadingForChildren
		{
			get { return _useLazyLoadingForChildren; }
			set { _useLazyLoadingForChildren = value; }
		}

		internal bool InternalUseLazyLoadingForChildren
		{
			get { return this._useLazyLoadingForChildren; }
		}

		private List<IAbstractUiComponent> _childControls = null;

		public List<IAbstractUiComponent> Controls
		{
			get {
				if (_childControls == null)
				{
					LoadChildControls(this.Context.TransactionContext);
				}
				return _childControls; 
			}
			set { _childControls = value; }
		}

		public override void Setup(Instance dbInstance, ITransactionContext context)
		{
			base.Setup(dbInstance, context);
			if (!this.UseLazyLoadingForChildren)
			{
				LoadChildControls(context);
			}
		}

		protected virtual void LoadChildControls(ITransactionContext context)
		{
			AbstractContainerController controller = new AbstractContainerController(context);
			this._childControls = controller.LoadChildControls(this);
			
			
		}
	}
}
