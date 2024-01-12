using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public static class DataTableService
    {
        public static List<Dictionary<string, string>> ConvertDataTableToListDictionary(DataTable dataTable)
        {
            List<Dictionary<string, string>> keyValuePairs = [];
            var dictionary = new Dictionary<string, string>();
            var columns = dataTable.Rows[0].Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dataTable.Rows[0][col.ColumnName]).ToArray();
            
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                for (int y = 0; y < dr.Table.Columns.Count; y++)
                {
                    DataColumn dc = dr.Table.Columns[y];
                    string? chave = columns[y].Value?.ToString();
                    string? valor = dr[dc.ColumnName]?.ToString();

                    if (!string.IsNullOrWhiteSpace(chave) && !string.IsNullOrWhiteSpace(valor))
                        dictionary.Add(chave.Trim(), valor.Trim());
                }

                keyValuePairs.Add(dictionary);
                dictionary = [];
            }

            return keyValuePairs;
        }
    }
}
