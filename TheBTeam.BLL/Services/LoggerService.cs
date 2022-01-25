using System.IO;

namespace TheBTeam.BLL.Services
{
    public class LoggerService
    {
        private string _loggerFile = @"SourceFiles\logs.log";
        private const double _lengthFileToDelete = 100000;
        public void DeleteLogFile()
        {
            var fileInfo = new FileInfo(_loggerFile);
            var file = fileInfo.FullName;
            if (File.Exists(file) && fileInfo.Length > _lengthFileToDelete)
            {
                File.Delete(file);
            }
        }
    }
}
