using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Paragraph = iTextSharp.text.Paragraph;

namespace MiniProject.MVVM.Services
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime FinishDate { get; set; }
        public bool IsFinished { get; set; }
    }
    internal class PDF_Gerator
    {
        public static void GeneratePdf(List<Task> tasks, string filePath)
        {
            // Create a document object
            Document document = new();

            try
            {
                // Create a PDF writer instance
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

                // Open the document
                document.Open();

                // Add a title
                Paragraph title = new Paragraph("Task List", new Font(Font.FontFamily.HELVETICA, 18, Font.BOLD));
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                // Add a blank line
                document.Add(new Paragraph(" "));

                // Create a table with 4 columns
                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100; // Table width as a percentage of page width

                // Add table headers
                table.AddCell("Title");
                table.AddCell("Description");
                table.AddCell("Finish Date");
                table.AddCell("Status");

                // Add tasks to the table
                foreach (var task in tasks)
                {
                    table.AddCell(task.Title);
                    table.AddCell(task.Description);
                    table.AddCell(task.FinishDate.ToShortDateString());
                    table.AddCell(task.IsFinished ? "Finished" : "Pending");
                }

                // Add the table to the document
                document.Add(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating PDF: " + ex.Message);
            }
            finally
            {
                // Close the document
                document.Close();
            }
        }
    }
}
