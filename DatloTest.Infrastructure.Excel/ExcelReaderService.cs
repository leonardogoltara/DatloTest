using DatloTest.Infrastructure.Services;
using System.Data;
using OfficeOpenXml;
using ExcelDataReader;

namespace DatloTest.Infrastructure.Excel
{
    public class ExcelReaderService : IExcelReaderService
    {
        public ExcelReaderService()
        {
            // If you use EPPlus in a noncommercial context
            // according to the Polyform Noncommercial license:
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public string CreateTempFilePath(string fileName)
        {
            var filename = $@"{DateTime.Now.Ticks}_{fileName}";

            var directoryPath = Path.Combine("temp", "uploads");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return Path.Combine(directoryPath, filename);
        }

        public DataTable ReadExcelFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {

                if (filePath.EndsWith(".csv"))
                {
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration()
                    {
                        AutodetectSeparators = [',']
                    });

                    DataTable result = excelReader.AsDataSet().Tables[0];

                    return result;
                }
                else
                {
                    using (var excelPackage = new ExcelPackage())
                    {
                        excelPackage.Load(stream);
                        try
                        {
                            var worksheet = excelPackage.Workbook.Worksheets.First();
                            //working fine with EPPlus for .xlsx files 
                        }
                        catch (Exception)//open xls file
                        {
                            //if its a .xls file it will throw an Exception              
                            throw;
                        }
                    }

                    IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
                    DataTable result = excelReader.AsDataSet().Tables[0];

                    return result;
                }
            }
        }
    }
}
