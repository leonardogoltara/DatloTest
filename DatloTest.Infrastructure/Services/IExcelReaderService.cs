using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public interface IExcelReaderService
    {
        public DataTable ReadExcelFile(string filePath);
        public string CreateTempFilePath(string fileName);
    }
}
