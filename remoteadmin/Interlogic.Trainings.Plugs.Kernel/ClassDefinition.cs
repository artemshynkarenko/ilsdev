using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class ClassDefinition : DomainObject
	{
        private int _classDefinitionId;
        public int ClassDefinitionId
        {
            get { return _classDefinitionId; }
            set { _classDefinitionId = value; }
        }

        private int? _parentClassDefinitionId;
        public int? ParentClassDefinitionId
        {
            get { return _parentClassDefinitionId; }
            set { _parentClassDefinitionId = value; }
        }

        private string _className;
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        private string _classDefinitionDescrition;
        public string ClassDefinitionDescrition
        {
            get { return _classDefinitionDescrition; }
            set { _classDefinitionDescrition = value; }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        private int _fileId;
        public int FileId
        {
            get { return _fileId; }
            set { _fileId = value; }
        }

        private int _plugId;
        public int PlugId
        {
            get { return _plugId; }
            set { _plugId = value; }
        }
	}
}
