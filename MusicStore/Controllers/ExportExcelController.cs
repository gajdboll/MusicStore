using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;
using OfficeOpenXml;
using System.ComponentModel;
using System.Data;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace MusicStore.Controllers
{
    public class ExportExcelController : Controller
    {
        private readonly MusicStoreContext _context;

        public ExportExcelController(MusicStoreContext context)
        {
            _context = context;
        }

        // Method for converting data to a DataTable
        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        [HttpPost]
        public IActionResult ExportExcel(string status)
        {
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                using (var package = new ExcelPackage())
                {
                    // List of statuses you want to include in the Excel file
                    var statuses = new[] { "inprocess", "pending", "completed", "approved", "all" };

                    foreach (var currentStatus in statuses)
                    {
                        // Fetch data for the current status
                        var orders = _context.OrderHeader
                                             .Where(o => currentStatus == "all" || o.OrderStatus == currentStatus)
                                             .Select(o => new
                                             {
                                                 o.Id,
                                                 o.ApplicationUser,
                                                 o.OrderDate,
                                                 o.OrderStatus,
                                                 o.PaymentStatus,
                                                 o.OrderTotal
                                             }).ToList();

                        // Convert the data to DataTable
                        DataTable tableData = ConvertToDataTable(orders);

                        // Create a new worksheet for each status
                        var worksheet = package.Workbook.Worksheets.Add(currentStatus.ToUpper());

                        // Add column headers
                        for (int i = 0; i < tableData.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = tableData.Columns[i].ColumnName;
                            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                        }

                        // Add row data
                        for (int i = 0; i < tableData.Rows.Count; i++)
                        {
                            for (int j = 0; j < tableData.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 2, j + 1].Value = tableData.Rows[i][j];
                            }
                        }
                    }

                    // Save to a memory stream
                    using (var stream = new MemoryStream())
                    {
                        package.SaveAs(stream);
                        stream.Position = 0;

                        var fileName = $"Orders_Report_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                        var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                        return File(stream.ToArray(), mimeType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generating Excel file: {ex.Message}");
            }
        }
    }
}
