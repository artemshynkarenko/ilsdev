using System;
using System.Collections.Generic;
using System.Text;


namespace Interlogic.Trainings.Plugs.Kernel
{
	using DomainModel;
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// Note to inheritors: Override GetControllerInstance for correct work
	/// </remarks>
	public class Instance : DomainObject, IInstantiatable
	{
		private int _instanceId;

		public int InstanceId
		{
			get { return _instanceId; }
			set { _instanceId = value; }
		}

		private int _classDefinitionId;

		public int ClassDefinitionId
		{
			get { return _classDefinitionId; }
			set { _classDefinitionId = value; }
		}

		private string _instanceName;

		public string InstanceName
		{
			get { return _instanceName; }
			set { _instanceName = value; }
		}

		private List<BindablePoint> _bindablePoints = new List<BindablePoint>();

		public List<BindablePoint> BindablePoints
		{
			get { return _bindablePoints; }
			set { _bindablePoints = value; }
		}

		public BindablePoint GetBindablePointBySystemName(string systemName)
		{
			foreach (BindablePoint point in this.BindablePoints)
			{
				if (string.Compare(point.SystemName, systemName, StringComparison.InvariantCulture) == 0)
				{
					return point;
				}
			}
			throw new ArgumentOutOfRangeException("systemName");
		}

		protected virtual DomainController GetControllerInstance(ITransactionContext context)
		{
			return new InstanceController(context);
		}


		#region IInstantiatable Members

		public virtual void Setup(Instance dbInstance, ITransactionContext context)
		{
			this.InstanceId = dbInstance.InstanceId;
			this.InstanceName = dbInstance.InstanceName;
			this.BindablePoints = dbInstance.BindablePoints;
			this.ClassDefinitionId = dbInstance.ClassDefinitionId;
		}

		#endregion
	}
}
