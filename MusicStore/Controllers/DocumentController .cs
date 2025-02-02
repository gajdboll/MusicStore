using Microsoft.AspNetCore.Mvc;
using MusicStoreData.Data;
using Xceed.Words.NET; // For working with Word documents using DocX
using System.IO;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class DocumentController : Controller
    {
        private readonly MusicStoreContext _context;

        public DocumentController(MusicStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult GenerateDocument(string status)
        {
            if (status == "completed")
            {
                status = "shipped";
            }
            // Fetch all orders if the status is "all", otherwise filter by the specified status
            var orders = _context.OrderHeader
                                 .Where(o => status == "all" || o.OrderStatus == status)
                                 .Select(o => new
                                 {
                                     o.Id,
                                     o.ApplicationUser,
                                     o.OrderDate,
                                     o.OrderStatus,
                                     o.PaymentStatus,
                                     o.OrderTotal
                                 }).ToList();

            // Validate the status - assign a default value if the status is null
            if (string.IsNullOrEmpty(status))
            {
                status = "Unknown";
            }

            // Ensure the directory for reports exists
            var reportDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/reports");
            if (!Directory.Exists(reportDirectory))
            {
                Directory.CreateDirectory(reportDirectory);
            }

            // If no orders are available, return a "No data available" PDF
            if (!orders.Any())
            {
                // Create a temporary file for the "No data available" PDF
                var noDataDocxPath = Path.Combine(reportDirectory, $"NoDataReport_{DateTime.Now:yyyyMMddHHmmss}.docx");
                var noDataPdfPath = Path.Combine(reportDirectory, $"NoDataReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");

                // Use DocX to create a simple .docx with "No data available"
                using (var document = DocX.Create(noDataDocxPath))
                {
                    document.InsertParagraph("No data available").FontSize(16).Bold();
                    document.SaveAs(noDataDocxPath);
                }

                // Convert the .docx to PDF using LibreOffice
                var process = new Process();
                process.StartInfo.FileName = @"C:\Program Files\LibreOffice\program\soffice.exe"; // Update path for Windows
                process.StartInfo.Arguments = $"--headless --convert-to pdf --outdir \"{Path.GetDirectoryName(noDataPdfPath)}\" \"{noDataDocxPath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.WaitForExit();

                // Return the "No data available" PDF to the user
                byte[] noDataPdfBytes = System.IO.File.ReadAllBytes(noDataPdfPath);
                return File(noDataPdfBytes, "application/pdf", $"NoDataReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
            }

            // Path to the Word template (docx file)
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/templates", "OrderTemplate.docx");

            // Path to save the generated .docx file
            var docxOutputPath = Path.Combine(reportDirectory, $"OrderReport_{DateTime.Now:yyyyMMddHHmmss}.docx");

            // Path to save the generated PDF
            var pdfOutputPath = Path.Combine(reportDirectory, $"OrderReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");

            // Load the Word template
            using (DocX document = DocX.Load(templatePath))
            {
                // Set the order status in the template
                document.ReplaceText("{OrderStatus}", status);

                // Find the table in the document
                var table = document.Tables[0]; // Assuming the table is in the first position

                // Iterate through each order and add data to the table
                foreach (var order in orders)
                {
                    var displayStatus = order.OrderStatus == "Completed" ? "Shipped" : order.OrderStatus;

                    var row = table.InsertRow();
                    row.Cells[0].Paragraphs[0].Append(order.Id.ToString());
                    row.Cells[1].Paragraphs[0].Append(order.ApplicationUser.UserName);
                    row.Cells[2].Paragraphs[0].Append(order.OrderDate.ToString("yyyy-MM-dd"));
                    row.Cells[3].Paragraphs[0].Append(displayStatus);  
                    row.Cells[4].Paragraphs[0].Append(order.PaymentStatus);
                    row.Cells[5].Paragraphs[0].Append(order.OrderTotal.ToString());
                }

                // Save the generated document as a .docx file
                document.SaveAs(docxOutputPath);
            }

            // Convert the .docx document to PDF using LibreOffice
            var convertProcess = new Process();
            convertProcess.StartInfo.FileName = @"C:\Program Files\LibreOffice\program\soffice.exe"; // Update path for Windows
            convertProcess.StartInfo.Arguments = $"--headless --convert-to pdf --outdir \"{Path.GetDirectoryName(pdfOutputPath)}\" \"{docxOutputPath}\"";
            convertProcess.StartInfo.RedirectStandardOutput = true;
            convertProcess.StartInfo.UseShellExecute = false;
            convertProcess.Start();
            convertProcess.WaitForExit();

            // Download the generated PDF for the user
            byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfOutputPath);
            return File(pdfBytes, "application/pdf", $"OrderReport_{DateTime.Now:yyyyMMddHHmmss}.pdf");
        }
    }
}
