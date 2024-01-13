using System.Data;

namespace DatloTest.Infrastructure.Services
{
    public static class DataTableService
    {
        /// <summary>
        /// Função para repassar os dados do DataTable para uma lista de Dictionaries para facilitar a manipulação dos dados.
        /// </summary>
        /// <param name="dataTable">DataTable lido do Excel</param>
        /// <returns>
        /// O item Dictionary na lista representa uma linha da tabela.
        /// Cada KeyValue do Dictionary representa coluna e valor.
        /// </returns>
        public static List<Dictionary<string, string>> ConvertDataTableToListDictionary(DataTable dataTable)
        {
            List<Dictionary<string, string>> keyValuePairs = [];
            if(dataTable == null)
                return keyValuePairs;

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
