using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class FileLocker
    {   
        public delegate IDisposable LockFileDelegate(string filePath);
        // This delegate locks file and returns object(locker object) which implements IDisposable.
        // If file action doesn't lock some resource it returns "null"
        // Unlock file just call Dispose method.


        private Dictionary<string, IDisposable> _lockedFiles = new Dictionary<string, IDisposable>();
        
        public bool IsLocked(string filePath)
        {
            return _lockedFiles.ContainsKey(filePath);
        }
        public void LockFile(IEnumerable<string> filePaths)
        {
            foreach (string fileName in filePaths)
                LockFile(fileName);
        }
        public void UnlockFile(IEnumerable<string> filePaths)
        {
            foreach (string fileName in filePaths)
                UnlockFile(fileName);
        }
        public bool UnlockFile(string filePath)
        {
            if (IsLocked(filePath) == false)
                return false;
                //throw new Exception("File " + filePath + " is not locked");

            IDisposable fileLocker = _lockedFiles[filePath];
            if (fileLocker != null)
                fileLocker.Dispose();
            _lockedFiles.Remove(filePath);
            return true;
        }

        public void LockFile(string filePath)
        {
            LockFile(filePath, DefaultLockFileFunction);
        }

        public void LockFile(string filePath, LockFileDelegate lockFileFunction)
        {
            if (IsLocked(filePath))
                throw new Exception("File " + filePath + " is already locked");

            IDisposable fileLocker = lockFileFunction(filePath);
            _lockedFiles.Add(filePath, fileLocker);
        }


        private static IDisposable DefaultLockFileFunction(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return file;
        }
    }
}
