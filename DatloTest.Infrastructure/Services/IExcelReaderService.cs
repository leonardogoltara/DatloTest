using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public interface IExcelReaderService
    {
        //public DataSet ReadExcelFile(Stream stream, string fileName);
        public DataSet ReadExcelFile(string filePath);
        public string CreateTempFilePath(string fileName);
    }
}
