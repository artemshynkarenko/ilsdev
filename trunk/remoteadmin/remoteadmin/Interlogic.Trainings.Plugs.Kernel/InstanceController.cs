using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel
{
	using DomainModel;
	using System.IO;
	using System.Reflection;
using System.Collections;

	public class InstanceController:DomainController
	{
		public InstanceController(ITransactionContext context)
			: base(context)
		{
		}

		public Instance GetBySystemName(string systemName)
		{
			using (InstanceFactory factory = InstanceFactory.GetInstance())
			{
				return factory.InternalLoadByName(systemName);
			}
		}
		/// <summary>
		/// This is incorrect method - do not call it
		/// </summary>
		/// <param name="instanceId"></param>
		/// <returns></returns>
		public DomainObject GetObjectByInstanceId(int instanceId)
		{
            using (InstanceFactory factory = InstanceFactory.GetInstance())
            {
                return factory.InternalLoadByPrimaryKey(instanceId);
            }
		}
		#region Caching
		private static Hashtable _cachedInstances = Hashtable.Synchronized(new Hashtable());
		protected virtual bool IsInCache(string key)
		{
			return _cachedInstances[key] == null;
		}
		protected virtual DomainObject GetFromCache(string key)
		{
			return _cachedInstances[key] as DomainObject;
		}
		protected virtual void SetToCache(string key, DomainObject instance)
		{
			_cachedInstances[key] = instance;
		}
		protected virtual bool NeedCaching(string key)
		{
			return true;
		}
		#endregion
		public DomainObject GetObjectByInstanceName(string instanceName)
		{
			if (this.NeedCaching(instanceName) && this.IsInCache(instanceName))
			{
				return this.GetFromCache(instanceName);
			}
			Instance instance = GetBySystemName(instanceName);
			string className = null;
			int fileId = 0;
			using (ClassDefinitionFactory classFactory = ClassDefinitionFactory.GetInstance())
			{
				classFactory.Context = this.FactoryContext;
				ClassDefinition definition = classFactory.InternalLoadByPrimaryKey(instance.ClassDefinitionId);
				fileId = definition.FileId;
				className = definition.ClassName;
			}
			string fileName = null;
			int fileLocationId = 0;
			using (PlugFileFactory fileFactory = PlugFileFactory.GetInstance())
			{
				fileFactory.Context = this.FactoryContext;
				PlugFile file = fileFactory.InternalLoadByPrimaryKey(fileId);
				fileName = file.PlugFileName;
				fileLocationId = file.DestinationLocationId;
			}
			string path = null;
			using (PlugLocationFactory locationFactory = PlugLocationFactory.GetInstance())
			{
				locationFactory.Context = this.FactoryContext;
				PlugLocation location = locationFactory.InternalLoadByPrimaryKey(fileLocationId);
				path = location.PlugLocationPath;
			}
			string filePath = Path.Combine(path, fileName);
			string assemblyName = Path.GetFileNameWithoutExtension(filePath);
			Type requiredType = null;
			Assembly[] loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			foreach (Assembly assembly in loadedAssemblies)
			{
				if (assembly.GetName().Name == assemblyName)
				{
					requiredType = assembly.GetType(className, false, false);
					if (requiredType != null)
						break;
				}
			}
			if (requiredType == null)
			{
				Assembly newDll = Assembly.LoadFrom(filePath);
				requiredType = newDll.GetType(className, false, false);
			}

			if (requiredType == null)
				throw new ArgumentException(string.Format("Type '{0}' was not found.", className));
			DomainObject instanceObject = (DomainObject)Activator.CreateInstance(requiredType);
			IInstantiatable instantinable = instanceObject as IInstantiatable;
			if (instantinable != null)
			{
				instantinable.Setup(instance, this.FactoryContext);
			}
			if (NeedCaching(instanceName))
			{
				SetToCache(instanceName, instanceObject);
			}
			return instanceObject;
		}
	}
}
