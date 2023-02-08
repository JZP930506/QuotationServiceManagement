using System.Collections;
using System.Data;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace QuotationServiceManagement.EOFService;

public class ExcelHelper
{
    /// <summary>
    /// 读取Excel文件的内容
    /// </summary>
    /// <param name="path"></param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns></returns>
    public static DataTable GetDataTable(string path, string sheetName = null)
    {
        if (path.ToLower().EndsWith(".xlsx"))
            return EPPlusHelper.WorksheetToTable(path, sheetName);

        using FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
        return GetDataTable(file, sheetName);
    }

    /// <summary>
    /// 从Excel文件流读取内容
    /// </summary>
    /// <param name="file"></param>
    /// <param name="sheetName"></param>
    /// <returns></returns>
    public static DataTable GetDataTable(Stream file, string contentType, string sheetName = null)
    {
        //载入工作簿
        IWorkbook workBook = null;
        if (contentType == "application/vnd.ms-excel")
        {
            workBook = new HSSFWorkbook(file);
        }
        else if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            workBook = new XSSFWorkbook(file);
        }
        else
        {
            try
            {
                workBook = new HSSFWorkbook(file);
            }
            catch
            {
                try
                {
                    workBook = new XSSFWorkbook(file);
                }
                catch
                {
                    throw new Exception("文件格式不被支持!");
                }
            }
        }

        //获取工作表(sheetName为空则默认获取第一个工作表)
        var sheet = string.IsNullOrEmpty(sheetName) ? workBook.GetSheetAt(0) : workBook.GetSheet(sheetName);
        //生成DataTable
        if (sheet != null)
            return GetDataTable(sheet);
        throw new Exception(string.Format("exe {0} is not exist!", sheetName ?? ""));

    }

    /// <summary>
    /// 读取工作表数据
    /// </summary>
    /// <param name="sheet"></param>
    /// <returns></returns>
    private static DataTable GetDataTable(ISheet sheet)
    {
        IEnumerator rows = sheet.GetRowEnumerator();

        DataTable dt = new DataTable(sheet.SheetName);

        //默认第一个非空行为列头
        bool isTitle = true;
        //标题行索引
        int titleRowIndex = 0;
        //默认列头后的第一个数据行，作为DataTable列类型的依据
        IRow firstDataRow = null;

        while (rows.MoveNext())
        {
            IRow row = null;
            if (rows.Current is XSSFRow)//*.xlsx
            {
                row = (XSSFRow)rows.Current;
            }
            else//*.xls
            {
                row = (HSSFRow)rows.Current;
            }

            //是否空行
            if (IsEmptyRow(row))
            {
                if (isTitle)
                {
                    titleRowIndex++;
                }
                continue;
            }
            else
            {
                if (isTitle)
                {
                    firstDataRow = sheet.GetRow(titleRowIndex + 1);//默认列头后的第一个数据行，作为DataTable列类型的依据
                }
            }

            DataRow dr = dt.NewRow();

            for (int i = 0; i < row.LastCellNum; i++)
            {
                var cell = row.GetCell(i);

                if (isTitle)
                {
                    var firstDataRowCell = firstDataRow.GetCell(i);
                    if (firstDataRowCell != null || cell != null)
                    {
                        dt.Columns.Add(cell.StringCellValue.Trim());
                    }
                    else
                    {
                        dt.Columns.Add(string.Format("未知列{0}", i + 1));
                    }
                }
                else
                {
                    if (i > dt.Columns.Count - 1) break;
                    dr[i] = GetCellValue(cell, dt.Columns[i].DataType);
                }

            }
            if (!isTitle && !IsEmptyRow(dr, dt.Columns.Count))
            {
                dt.Rows.Add(dr);
            }
            isTitle = false;
        }

        return dt;
    }

    /// <summary>
    /// 获取单元格值
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="colType"></param>
    /// <returns></returns>
    private static object GetCellValue(ICell cell, Type colType)
    {
        if (cell == null || cell.ToString().ToUpper().Equals("NULL") || cell.CellType == NPOI.SS.UserModel.CellType.Blank)
            return DBNull.Value;

        object val = null;
        switch (cell.CellType)
        {
            case NPOI.SS.UserModel.CellType.Boolean:
                val = cell.BooleanCellValue;
                break;
            case NPOI.SS.UserModel.CellType.Numeric:
                var cellValueStr = cell.ToString().Trim();
                if (cellValueStr.IndexOf('-') >= 0 || cellValueStr.IndexOf('/') >= 0)
                {
                    DateTime d = DateTime.MinValue;
                    DateTime.TryParse(cellValueStr, out d);
                    if (!d.Equals(DateTime.MinValue)) val = cellValueStr;
                }
                if (val == null)
                {
                    decimal vNum = 0;
                    decimal.TryParse(cellValueStr, out vNum);
                    val = vNum;
                }
                break;
            case NPOI.SS.UserModel.CellType.String:
                val = cell.StringCellValue;
                break;
            case NPOI.SS.UserModel.CellType.Error:
                val = cell.ErrorCellValue;
                break;
            case NPOI.SS.UserModel.CellType.Formula:
            default:
                val = "=" + cell.CellFormula;
                break;
        }

        return val;
    }

    /// <summary>
    /// 检查是否空数据行
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    private static bool IsEmptyRow(DataRow dr, int colCount)
    {
        bool isEmptyRow = true;
        for (int i = 0; i < colCount; i++)
        {
            if (dr[i] != null && !dr[i].Equals(DBNull.Value))
            {
                isEmptyRow = false;
                break;
            }
        }
        return isEmptyRow;
    }

    /// <summary>
    /// 检查是否空的Excel行
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private static bool IsEmptyRow(IRow row)
    {
        bool isEmptyRow = true;
        for (int i = 0; i < row.LastCellNum; i++)
        {
            if (row.GetCell(i) != null)
            {
                isEmptyRow = false;
                break;
            }
        }

        return isEmptyRow;
    }
    /// <summary>
    /// 生成Excel数据到路径
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public static void GenerateExcel(DataTable data, string path)
    {
        var workBook = GenerateExcelData(data);
        //保存至路径
        using (FileStream fs = File.OpenWrite(path)) //打开一个xls文件，如果没有则自行创建，如果存在则在创建时不要打开该文件！
        {
            workBook.Write(fs, true);   //向打开的这个xls文件中写入mySheet表并保存。
        }
    }

    /// <summary>
    /// 生成Excel数据到字节流
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    public static byte[] GenerateExcel(DataTable data)
    {
        var workBook = GenerateExcelData(data);
        using MemoryStream ms = new MemoryStream();
        workBook.Write(ms, true);
        return ms.GetBuffer();
    }

    /// <summary>
    /// 生成DataTable到Excel
    /// </summary>
    /// <param name="data"></param>
    /// <param name="path"></param>
    private static IWorkbook GenerateExcelData(DataTable data)
    {
        //创建工作簿
        var workBook = new HSSFWorkbook();
        //生成文件基本信息
        GenerateSummaryInformation(workBook);
        //创建工作表
        var sheet = workBook.CreateSheet("Sheet1");
        //创建标题行
        if (data != null && data.Columns.Count > 0)
        {
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                var cell = row.CreateCell(i);
                cell.SetCellValue(data.Columns[i].ColumnName);
            }
        }
        //创建数据行
        if (data != null && data.Rows.Count > 0)
        {
            for (int rowIndex = 1; rowIndex <= data.Rows.Count; rowIndex++)
            {
                IRow row = sheet.CreateRow(rowIndex);
                for (int colIndex = 0; colIndex < data.Columns.Count; colIndex++)
                {
                    var cell = row.CreateCell(colIndex);
                    var cellValue = data.Rows[rowIndex - 1][colIndex];
                    switch (data.Columns[colIndex].DataType.Name)
                    {
                        case "Byte":
                        case "Int16":
                        case "Int32":
                        case "Int64":
                        case "Decimal":
                        case "Single":
                        case "Double":
                            double doubleVal = 0;
                            if (cellValue != null && !cellValue.Equals(System.DBNull.Value))
                            {
                                double.TryParse(cellValue.ToString(), out doubleVal);
                                cell.SetCellValue(doubleVal);
                            }
                            break;
                        case "DateTime":
                            DateTime dtVal = DateTime.MinValue;
                            if (cellValue != null && !cellValue.Equals(System.DBNull.Value))
                            {
                                DateTime.TryParse(cellValue.ToString(), out dtVal);
                                if (dtVal != DateTime.MinValue)
                                {
                                    cell.SetCellValue(dtVal);
                                }
                            }
                            break;
                        default:
                            if (cellValue != null && !cellValue.Equals(System.DBNull.Value))
                            {
                                cell.SetCellValue(cellValue.ToString());
                            }
                            break;
                    }

                }
            }
        }

        return workBook;
    }

    /// <summary>
    /// 创建文档的基本信息(右击文件属性可看到的)
    /// </summary>
    /// <param name="workBook"></param>
    private static void GenerateSummaryInformation(HSSFWorkbook workBook)
    {
        DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
        dsi.Company = "Company";

        SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
        si.Subject = "Subject";//主题
        si.Author = "Author";//作者

        workBook.DocumentSummaryInformation = dsi;
        workBook.SummaryInformation = si;
    }

}