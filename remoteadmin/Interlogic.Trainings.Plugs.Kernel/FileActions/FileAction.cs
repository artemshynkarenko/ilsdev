using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public abstract class FileAction:IFileAction
    {
      
        protected class UserFileAccessRightsChecker
        {
            private string _path;
            private System.Security.Principal.WindowsIdentity _principal;

            private bool _denyAppendData = false;
            private bool _denyChangePermissions = false;
            private bool _denyCreateDirectories = false;
            private bool _denyCreateFiles = false;
            private bool _denyDelete = false;
            private bool _denyDeleteSubdirectoriesAndFiles = false;
            private bool _denyExecuteFile = false;
            private bool _denyFullControl = false;
            private bool _denyListDirectory = false;
            private bool _denyModify = false;
            private bool _denyRead = false;
            private bool _denyReadAndExecute = false;
            private bool _denyReadAttributes = false;
            private bool _denyReadData = false;
            private bool _denyReadExtendedAttributes = false;
            private bool _denyReadPermissions = false;
            private bool _denySynchronize = false;
            private bool _denyTakeOwnership = false;
            private bool _denyTraverse = false;
            private bool _denyWrite = false;
            private bool _denyWriteAttributes = false;
            private bool _denyWriteData = false;
            private bool _denyWriteExtendedAttributes = false;

            private bool _allowAppendData = false;
            private bool _allowChangePermissions = false;
            private bool _allowCreateDirectories = false;
            private bool _allowCreateFiles = false;
            private bool _allowDelete = false;
            private bool _allowDeleteSubdirectoriesAndFiles = false;
            private bool _allowExecuteFile = false;
            private bool _allowFullControl = false;
            private bool _allowListDirectory = false;
            private bool _allowModify = false;
            private bool _allowRead = false;
            private bool _allowReadAndExecute = false;
            private bool _allowReadAttributes = false;
            private bool _allowReadData = false;
            private bool _allowReadExtendedAttributes = false;
            private bool _allowReadPermissions = false;
            private bool _allowSynchronize = false;
            private bool _allowTakeOwnership = false;
            private bool _allowTraverse = false;
            private bool _allowWrite = false;
            private bool _allowWriteAttributes = false;
            private bool _allowWriteData = false;
            private bool _allowWriteExtendedAttributes = false;

            public string Path
            {
                get { return _path; }
            }

            public bool CanAppendData()
            {
                return !_denyAppendData && _allowAppendData;
            }

            public bool CanChangePermissions()
            {
                return !_denyChangePermissions && _allowChangePermissions;
            }

            public bool CanCreateDirectories()
            {
                return !_denyCreateDirectories && _allowCreateDirectories;
            }

            public bool CanCreateFiles()
            {
                return !_denyCreateFiles && _allowCreateFiles;
            }

            public bool CanDelete()
            {
                return !_denyDelete && _allowDelete;
            }

            public bool CanDeleteSubdirectoriesAndFiles()
            {
                return !_denyDeleteSubdirectoriesAndFiles &&
                       _allowDeleteSubdirectoriesAndFiles;
            }

            public bool CanExecuteFile()
            {
                return !_denyExecuteFile && _allowExecuteFile;
            }

            public bool CanFullControl()
            {
                return !_denyFullControl && _allowFullControl;
            }

            public bool CanListDirectory()
            {
                return !_denyListDirectory && _allowListDirectory;
            }

            public bool CanModify()
            {
                return !_denyModify && _allowModify;
            }

            public bool CanRead()
            {
                return !_denyRead && _allowRead;
            }

            public bool CanReadAndExecute()
            {
                return !_denyReadAndExecute && _allowReadAndExecute;
            }

            public bool CanReadAttributes()
            {
                return !_denyReadAttributes && _allowReadAttributes;
            }

            public bool CanReadData()
            {
                return !_denyReadData && _allowReadData;
            }

            public bool CanReadExtendedAttributes()
            {
                return !_denyReadExtendedAttributes &&
                       _allowReadExtendedAttributes;
            }

            public bool CanReadPermissions()
            {
                return !_denyReadPermissions && _allowReadPermissions;
            }

            public bool CanSynchronize()
            {
                return !_denySynchronize && _allowSynchronize;
            }

            public bool CanTakeOwnership()
            {
                return !_denyTakeOwnership && _allowTakeOwnership;
            }

            public bool CanTraverse()
            {
                return !_denyTraverse && _allowTraverse;
            }

            public bool CanWrite()
            {
                return !_denyWrite && _allowWrite;
            }

            public bool CanWriteAttributes()
            {
                return !_denyWriteAttributes && _allowWriteAttributes;
            }

            public bool CanWriteData()
            {
                return !_denyWriteData && _allowWriteData;
            }

            public bool CanWriteExtendedAttributes()
            {
                return !_denyWriteExtendedAttributes &&
                       _allowWriteExtendedAttributes;
            }

            /// <summary>
            /// Simple accessor
            /// </summary>
            /// <returns></returns>
            public System.Security.Principal.WindowsIdentity getWindowsIdentity()
            {
                return _principal;
            }

            /// <summary>
            /// Simple accessor
            /// </summary>
            /// <returns></returns>
            
            public String GetPath()
            {
                return Path;
            }

            /// <summary>
            /// Convenience constructor assumes the current user
            /// </summary>
            /// <param name="path"></param>
            public UserFileAccessRightsChecker(string path)
                :
                    this(path, System.Security.Principal.WindowsIdentity.GetCurrent())
            {
            }


            /// <summary>
            /// Supply the path to the file or directory and a user or group. 
            /// Access checks are done
            /// during instantiation to ensure we always have a valid object
            /// </summary>
            /// <param name="path"></param>
            /// <param name="principal"></param>
            public UserFileAccessRightsChecker(string path,
                                               System.Security.Principal.WindowsIdentity principal)
            {
                this._path = path;
                this._principal = principal;

                try
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(Path);
                    AuthorizationRuleCollection acl = fi.GetAccessControl().GetAccessRules
                        (true, true, typeof(SecurityIdentifier));
                    for (int i = 0; i < acl.Count; i++)
                    {
                        System.Security.AccessControl.FileSystemAccessRule rule =
                            (System.Security.AccessControl.FileSystemAccessRule)acl[i];
                        if (_principal.User.Equals(rule.IdentityReference))
                        {
                            if (System.Security.AccessControl.AccessControlType.Deny.Equals
                                (rule.AccessControlType))
                            {
                                if (Contains(FileSystemRights.AppendData, rule))
                                    _denyAppendData = true;
                                if (Contains(FileSystemRights.ChangePermissions, rule))
                                    _denyChangePermissions = true;
                                if (Contains(FileSystemRights.CreateDirectories, rule))
                                    _denyCreateDirectories = true;
                                if (Contains(FileSystemRights.CreateFiles, rule))
                                    _denyCreateFiles = true;
                                if (Contains(FileSystemRights.Delete, rule))
                                    _denyDelete = true;
                                if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
                                             rule)) _denyDeleteSubdirectoriesAndFiles = true;
                                if (Contains(FileSystemRights.ExecuteFile, rule))
                                    _denyExecuteFile = true;
                                if (Contains(FileSystemRights.FullControl, rule))
                                    _denyFullControl = true;
                                if (Contains(FileSystemRights.ListDirectory, rule))
                                    _denyListDirectory = true;
                                if (Contains(FileSystemRights.Modify, rule))
                                    _denyModify = true;
                                if (Contains(FileSystemRights.Read, rule)) _denyRead = true;
                                if (Contains(FileSystemRights.ReadAndExecute, rule))
                                    _denyReadAndExecute = true;
                                if (Contains(FileSystemRights.ReadAttributes, rule))
                                    _denyReadAttributes = true;
                                if (Contains(FileSystemRights.ReadData, rule))
                                    _denyReadData = true;
                                if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
                                    _denyReadExtendedAttributes = true;
                                if (Contains(FileSystemRights.ReadPermissions, rule))
                                    _denyReadPermissions = true;
                                if (Contains(FileSystemRights.Synchronize, rule))
                                    _denySynchronize = true;
                                if (Contains(FileSystemRights.TakeOwnership, rule))
                                    _denyTakeOwnership = true;
                                if (Contains(FileSystemRights.Traverse, rule))
                                    _denyTraverse = true;
                                if (Contains(FileSystemRights.Write, rule)) _denyWrite = true;
                                if (Contains(FileSystemRights.WriteAttributes, rule))
                                    _denyWriteAttributes = true;
                                if (Contains(FileSystemRights.WriteData, rule))
                                    _denyWriteData = true;

                                if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
                                    _denyWriteExtendedAttributes = true;
                            }
                            else if (System.Security.AccessControl.AccessControlType.
                                Allow.Equals(rule.AccessControlType))
                            {
                                if (Contains(FileSystemRights.AppendData, rule))
                                    _allowAppendData = true;
                                if (Contains(FileSystemRights.ChangePermissions, rule))
                                    _allowChangePermissions = true;
                                if (Contains(FileSystemRights.CreateDirectories, rule))
                                    _allowCreateDirectories = true;
                                if (Contains(FileSystemRights.CreateFiles, rule))
                                    _allowCreateFiles = true;
                                if (Contains(FileSystemRights.Delete, rule))
                                    _allowDelete = true;
                                if (Contains(FileSystemRights.DeleteSubdirectoriesAndFiles,
                                             rule)) _allowDeleteSubdirectoriesAndFiles = true;
                                if (Contains(FileSystemRights.ExecuteFile, rule))
                                    _allowExecuteFile = true;
                                if (Contains(FileSystemRights.FullControl, rule))
                                    _allowFullControl = true;
                                if (Contains(FileSystemRights.ListDirectory, rule))
                                    _allowListDirectory = true;
                                if (Contains(FileSystemRights.Modify, rule))
                                    _allowModify = true;
                                if (Contains(FileSystemRights.Read, rule)) _allowRead = true;
                                if (Contains(FileSystemRights.ReadAndExecute, rule))
                                    _allowReadAndExecute = true;
                                if (Contains(FileSystemRights.ReadAttributes, rule))
                                    _allowReadAttributes = true;
                                if (Contains(FileSystemRights.ReadData, rule))
                                    _allowReadData = true;
                                if (Contains(FileSystemRights.ReadExtendedAttributes, rule))
                                    _allowReadExtendedAttributes = true;
                                if (Contains(FileSystemRights.ReadPermissions, rule))
                                    _allowReadPermissions = true;
                                if (Contains(FileSystemRights.Synchronize, rule))
                                    _allowSynchronize = true;
                                if (Contains(FileSystemRights.TakeOwnership, rule))
                                    _allowTakeOwnership = true;
                                if (Contains(FileSystemRights.Traverse, rule))
                                    _allowTraverse = true;
                                if (Contains(FileSystemRights.Write, rule))
                                    _allowWrite = true;
                                if (Contains(FileSystemRights.WriteAttributes, rule))
                                    _allowWriteAttributes = true;
                                if (Contains(FileSystemRights.WriteData, rule))
                                    _allowWriteData = true;
                                if (Contains(FileSystemRights.WriteExtendedAttributes, rule))
                                    _allowWriteExtendedAttributes = true;
                            }
                        }
                    }

                    IdentityReferenceCollection groups = _principal.Groups;
                    for (int j = 0; j < groups.Count; j++)
                    {
                        for (int i = 0; i < acl.Count; i++)
                        {
                            System.Security.AccessControl.FileSystemAccessRule rule =
                                (System.Security.AccessControl.FileSystemAccessRule)acl[i];
                            if (groups[j].Equals(rule.IdentityReference))
                            {
                                if (System.Security.AccessControl.AccessControlType.
                                    Deny.Equals(rule.AccessControlType))
                                {
                                    if (Contains(FileSystemRights.AppendData, rule))
                                        _denyAppendData = true;
                                    if (Contains(FileSystemRights.ChangePermissions, rule))
                                        _denyChangePermissions = true;
                                    if (Contains(FileSystemRights.CreateDirectories, rule))
                                        _denyCreateDirectories = true;
                                    if (Contains(FileSystemRights.CreateFiles, rule))
                                        _denyCreateFiles = true;
                                    if (Contains(FileSystemRights.Delete, rule))
                                        _denyDelete = true;
                                    if (Contains(FileSystemRights.
                                                     DeleteSubdirectoriesAndFiles, rule))
                                        _denyDeleteSubdirectoriesAndFiles = true;
                                    if (Contains(FileSystemRights.ExecuteFile, rule))
                                        _denyExecuteFile = true;
                                    if (Contains(FileSystemRights.FullControl, rule))
                                        _denyFullControl = true;
                                    if (Contains(FileSystemRights.ListDirectory, rule))
                                        _denyListDirectory = true;
                                    if (Contains(FileSystemRights.Modify, rule))
                                        _denyModify = true;
                                    if (Contains(FileSystemRights.Read, rule))
                                        _denyRead = true;
                                    if (Contains(FileSystemRights.ReadAndExecute, rule))
                                        _denyReadAndExecute = true;
                                    if (Contains(FileSystemRights.ReadAttributes, rule))
                                        _denyReadAttributes = true;
                                    if (Contains(FileSystemRights.ReadData, rule))
                                        _denyReadData = true;
                                    if (Contains(FileSystemRights.
                                                     ReadExtendedAttributes, rule))
                                        _denyReadExtendedAttributes = true;
                                    if (Contains(FileSystemRights.ReadPermissions, rule))
                                        _denyReadPermissions = true;
                                    if (Contains(FileSystemRights.Synchronize, rule))
                                        _denySynchronize = true;
                                    if (Contains(FileSystemRights.TakeOwnership, rule))
                                        _denyTakeOwnership = true;
                                    if (Contains(FileSystemRights.Traverse, rule))
                                        _denyTraverse = true;
                                    if (Contains(FileSystemRights.Write, rule))
                                        _denyWrite = true;
                                    if (Contains(FileSystemRights.WriteAttributes, rule))
                                        _denyWriteAttributes = true;
                                    if (Contains(FileSystemRights.WriteData, rule))
                                        _denyWriteData = true;
                                    if (Contains(FileSystemRights.
                                                     WriteExtendedAttributes, rule))
                                        _denyWriteExtendedAttributes = true;
                                }
                                else if (System.Security.AccessControl.AccessControlType.
                                    Allow.Equals(rule.AccessControlType))
                                {
                                    if (Contains(FileSystemRights.AppendData, rule))
                                        _allowAppendData = true;
                                    if (Contains(FileSystemRights.ChangePermissions, rule))
                                        _allowChangePermissions = true;
                                    if (Contains(FileSystemRights.CreateDirectories, rule))
                                        _allowCreateDirectories = true;
                                    if (Contains(FileSystemRights.CreateFiles, rule))
                                        _allowCreateFiles = true;
                                    if (Contains(FileSystemRights.Delete, rule))
                                        _allowDelete = true;
                                    if (Contains(FileSystemRights.
                                                     DeleteSubdirectoriesAndFiles, rule))
                                        _allowDeleteSubdirectoriesAndFiles = true;
                                    if (Contains(FileSystemRights.ExecuteFile, rule))
                                        _allowExecuteFile = true;
                                    if (Contains(FileSystemRights.FullControl, rule))
                                        _allowFullControl = true;
                                    if (Contains(FileSystemRights.ListDirectory, rule))
                                        _allowListDirectory = true;
                                    if (Contains(FileSystemRights.Modify, rule))
                                        _allowModify = true;
                                    if (Contains(FileSystemRights.Read, rule))
                                        _allowRead = true;
                                    if (Contains(FileSystemRights.ReadAndExecute, rule))
                                        _allowReadAndExecute = true;
                                    if (Contains(FileSystemRights.ReadAttributes, rule))
                                        _allowReadAttributes = true;
                                    if (Contains(FileSystemRights.ReadData, rule))
                                        _allowReadData = true;
                                    if (Contains(FileSystemRights.
                                                     ReadExtendedAttributes, rule))
                                        _allowReadExtendedAttributes = true;
                                    if (Contains(FileSystemRights.ReadPermissions, rule))
                                        _allowReadPermissions = true;
                                    if (Contains(FileSystemRights.Synchronize, rule))
                                        _allowSynchronize = true;
                                    if (Contains(FileSystemRights.TakeOwnership, rule))
                                        _allowTakeOwnership = true;
                                    if (Contains(FileSystemRights.Traverse, rule))
                                        _allowTraverse = true;
                                    if (Contains(FileSystemRights.Write, rule))
                                        _allowWrite = true;
                                    if (Contains(FileSystemRights.WriteAttributes, rule))
                                        _allowWriteAttributes = true;
                                    if (Contains(FileSystemRights.WriteData, rule))
                                        _allowWriteData = true;
                                    if (Contains(FileSystemRights.WriteExtendedAttributes,
                                                 rule)) _allowWriteExtendedAttributes = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //Deal with IO exceptions if you want
                    throw e;
                }
            }

            /// <summary>
            /// Simply displays all allowed rights
            /// 
            /// Useful if say you want to test for write access and find
            /// it is false;
            /// <xmp>
            /// UserFileAccessRights rights = new UserFileAccessRights(txtLogPath.Text);
            /// System.IO.FileInfo fi = new System.IO.FileInfo(txtLogPath.Text);
            /// if (rights.CanWrite() && rights.CanRead()) {
            ///     lblLogMsg.Text = "R/W access";
            /// } else {
            ///     if (rights.CanWrite()) {
            ///        lblLogMsg.Text = "Only Write access";
            ///     } else if (rights.CanRead()) {
            ///         lblLogMsg.Text = "Only Read access";
            ///     } else {
            ///         lblLogMsg.CssClass = "error";
            ///         lblLogMsg.Text = rights.ToString()
            ///     }
            /// }
            /// 
            /// </xmp>
            /// 
            /// </summary>
            /// <returns></returns>
            public override String ToString()
            {
                string str = "";

                if (CanAppendData())
                {
                    if (!String.IsNullOrEmpty(str))
                        str +=
                            ",";
                    str += "AppendData";
                }
                if (CanChangePermissions())
                {
                    if (!String.IsNullOrEmpty(str))
                        str +=
                            ",";
                    str += "ChangePermissions";
                }
                if (CanCreateDirectories())
                {
                    if (!String.IsNullOrEmpty(str))
                        str +=
                            ",";
                    str += "CreateDirectories";
                }
                if (CanCreateFiles())
                {
                    if (!String.IsNullOrEmpty(str))
                        str +=
                            ",";
                    str += "CreateFiles";
                }
                if (CanDelete())
                {
                    if (!String.IsNullOrEmpty(str))
                        str +=
                            ",";
                    str += "Delete";
                }
                if (CanDeleteSubdirectoriesAndFiles())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "DeleteSubdirectoriesAndFiles";
                }
                if (CanExecuteFile())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ExecuteFile";
                }
                if (CanFullControl())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "FullControl";
                }
                if (CanListDirectory())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ListDirectory";
                }
                if (CanModify())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "Modify";
                }
                if (CanRead())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "Read";
                }
                if (CanReadAndExecute())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ReadAndExecute";
                }
                if (CanReadAttributes())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ReadAttributes";
                }
                if (CanReadData())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ReadData";
                }
                if (CanReadExtendedAttributes())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ReadExtendedAttributes";
                }
                if (CanReadPermissions())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "ReadPermissions";
                }
                if (CanSynchronize())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "Synchronize";
                }
                if (CanTakeOwnership())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "TakeOwnership";
                }
                if (CanTraverse())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "Traverse";
                }
                if (CanWrite())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "Write";
                }
                if (CanWriteAttributes())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "WriteAttributes";
                }
                if (CanWriteData())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "WriteData";
                }
                if (CanWriteExtendedAttributes())
                {
                    if (!String.IsNullOrEmpty(str))
                        str += ",";
                    str += "WriteExtendedAttributes";
                }
                if (String.IsNullOrEmpty(str))
                    str = "None";
                return str;
            }

            /// <summary>
            /// Convenience method to test if the right exists within the given rights
            /// </summary>
            /// <param name="right"></param>
            /// <param name="rule"></param>
            /// <returns></returns>
            public bool Contains(System.Security.AccessControl.FileSystemRights right,
                                 System.Security.AccessControl.FileSystemAccessRule rule)
            {
                return (((int)right & (int)rule.FileSystemRights) == (int)right);
            }
        }
        protected IFileTransactionContext _context;
        protected IFileActionInfo _fileActionInfo ;
        protected  bool _isExecuted;

        protected FileLocker Locker
        {
            get
            {
                return _context.Locker;
            }
        }

        #region IFileAction Members
        public IFileTransactionContext Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public virtual void RollBack()
        {
            _fileActionInfo.UnlockOnRollback(Locker);
            if(_isExecuted) RollbackAction(_fileActionInfo );
    	}

        public virtual void Commit()
        {
                _fileActionInfo.UnlockOnCommit(Locker);
                CommitAction(_fileActionInfo);
        }
        protected virtual void RollbackAction(IFileActionInfo fileActionInfo)
        {

        }
        protected virtual void CommitAction(IFileActionInfo fileActionInfo)
        {
            

        }
       protected virtual void ExecuteAction(IFileActionInfo fileActionInfo)
        {

        }
        #endregion

        #region ITransactionAction Members

        public ITransactionContext TransactionContext
        {
            get
            {
                return _context;
            }
            set
            {
                 _context=(IFileTransactionContext) value;
            }
        }

        #endregion

        #region IAction Members

        public void Execute()
        {
            ExecuteAction(_fileActionInfo);
            _isExecuted = true;
        }

        #endregion

        #region ITransactionContext Members

        public bool ExecutingInTransaction()
        {
            return _context == null;
        }

        public abstract void BeginTransaction();
        
        #endregion

        #region IFileAction Members


        public bool IsExecuted
        {
            get { return _isExecuted; }
        }

        #endregion
    }
}
