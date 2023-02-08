using System.Data;
using OfficeOpenXml;

namespace QuotationServiceManagement.EOFService
{
    public class EPPlusHelper : IDisposable
    {
        private static string GetString(object obj)
        {
            if (obj == null)
                return "";

            return obj.ToString();
        }

        /// <summary>
        ///将指定的Excel的文件转换成DataTable （Excel的第一个sheet）
        /// </summary>
        /// <param name="fullFielPath">文件的绝对路径</param>
        /// <returns></returns>
        public static DataTable WorksheetToTable(string fullFielPath, string sheetName = null)
        {
            //如果是“EPPlus”，需要指定LicenseContext。
            //EPPlus.Core 不需要指定。
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            FileInfo existingFile = new FileInfo(fullFielPath);

            ExcelPackage package = new ExcelPackage(existingFile);
            ExcelWorksheet worksheet = null;

            if (string.IsNullOrEmpty(sheetName))
            {
                //不传入 sheetName 默认取第1个sheet。
                //EPPlus 索引是0
                //EPPlus.Core 索引是1
                worksheet = package.Workbook.Worksheets[0];
            }
            else
            {
                worksheet = package.Workbook.Worksheets[sheetName];
            }
            if (worksheet == null)
                throw new Exception("指定的sheetName不存在");

            return WorksheetToTable(worksheet);
        }

        /// <summary>
        /// 将worksheet转成datatable
        /// </summary>
        /// <param name="worksheet">待处理的worksheet</param>
        /// <returns>返回处理后的datatable</returns>
        public static DataTable WorksheetToTable(ExcelWorksheet worksheet)
        {
            //获取worksheet的行数
            int rows = worksheet.Dimension.End.Row;
            //获取worksheet的列数
            int cols = worksheet.Dimension.End.Column;

            DataTable dt = new DataTable(worksheet.Name);
            DataRow? dr = null;
            for (int i = 1; i <= rows; i++)
            {
                if (i > 1)
                    dr = dt.Rows.Add();

                for (int j = 1; j <= cols; j++)
                {
                    //默认将第一行设置为datatable的标题
                    if (i == 1)
                        dt.Columns.Add(GetString(worksheet.Cells[i, j].Value));
                    //剩下的写入datatable
                    else
                        dr[j - 1] = GetString(worksheet.Cells[i, j].Value);
                }
            }
            return dt;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
        }
    }
}