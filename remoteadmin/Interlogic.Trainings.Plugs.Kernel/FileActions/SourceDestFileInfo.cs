namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class SourceDestFileInfo : SourceFileInfo
    {
        protected  string _destinationFileName;

        public SourceDestFileInfo(string sourceFileName, string destinationFileName) : base(sourceFileName)
        {
            _destinationFileName = destinationFileName;
        }

        public string DestinationFileName
        {
            get { return _destinationFileName; }
            set { _destinationFileName = value; }
        }
        protected override void Lock(FileLocker locker)
        {
            base.Lock(locker);
            locker.LockFile(_destinationFileName);
        }
        protected override void Unlock(FileLocker locker)
        {
            base.Unlock(locker);
            locker.UnlockFile(_destinationFileName);
        }
    }
}