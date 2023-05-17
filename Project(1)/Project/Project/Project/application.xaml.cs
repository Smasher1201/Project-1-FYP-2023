using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Project
{
    /// <summary>
    /// Interaction logic for application.xaml
    /// </summary>
    public partial class application : Window
    {
        public ObservableCollection<LuggageItem> LuggageItems { get; set; }

        public application()
        {
            InitializeComponent();
            LuggageItems = new ObservableCollection<LuggageItem>();
            lvLuggage.ItemsSource = LuggageItems;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (!IsTextBoxInputDouble(txtHeight) || !IsTextBoxInputDouble(txtWeight) || !IsTextBoxInputDouble(txtLength) || !IsTextBoxInputDouble(txtWidth))
            {
                MessageBox.Show("Please enter valid numbers for height, weight, length, and width.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else {
                LuggageItem item = new LuggageItem
                {
                    Origin = txtFrom.Text,
                    Destination = txtTo.Text,
                    Date = DP.Text,
                    Height = double.Parse(txtHeight.Text),
                    Weight = double.Parse(txtWeight.Text),
                    Length = double.Parse(txtLength.Text),
                    Width = double.Parse(txtWidth.Text),
                };

                LuggageItems.Add(item);
                lbTotalWeight.Content = GetTotalWeight().ToString();
            }

            // Clear input fields after adding the item
            txtHeight.Clear();
            txtWeight.Clear();
            txtLength.Clear();
            txtWidth.Clear();
        }

        public class LuggageItem
        {
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string Date { get; set; }
            public double Height { get; set; }
            public double Weight { get; set; }
            public double Length { get; set; }
            public double Width { get; set; }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            LuggageItem selectedItem = (LuggageItem)lvLuggage.SelectedItem;

            if (selectedItem != null)
            {
                LuggageItems.Remove(selectedItem);
            }
            lbTotalWeight.Content = GetTotalWeight().ToString();
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            DateTime currentDateTime = DateTime.Now;
            string Title = currentDateTime.ToString("yyyy_MMM_dd_HH_mm_ss");

            //string outputFilePath = @"C:\Users\jingw\Desktop\Project\Label_" + Title + ".pdf";
            string outputFilePath = @"C:\Users\Kozmic\Downloads\Project(1)\Project\Label_" + Title + ".pdf";
            CreateLuggageInfoPdf(outputFilePath);

            // Show a message to inform the user that the PDF has been created
            MessageBox.Show($"Luggage information has been exported to {outputFilePath}.", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool IsTextBoxInputDouble(TextBox textBox)
        {
            double result;
            return double.TryParse(textBox.Text, out result);
        }

        private void CreateLuggageInfoPdf(string outputFilePath)
        {
            // Initialize the PDF writer
            PdfWriter writer = new PdfWriter(outputFilePath);

            // Initialize the PDF document
            PdfDocument pdf = new PdfDocument(writer);

            PageSize customPageSize = new PageSize(800, 400);
            // Initialize the document layout
            Document document = new Document(pdf, customPageSize);

            // Calculate total weight and total row number
            double totalWeight = GetTotalWeight();
            int totalRows = LuggageItems.Count;

            // Add total weight and total row number to the document
            iText.Layout.Element.Paragraph summary = new iText.Layout.Element.Paragraph($"Total Weight: {totalWeight} kg \nTotal Luggage Number: {totalRows}");
            document.Add(summary);

            // Create a table with 4 columns (one for each property of LuggageItem)
            iText.Layout.Element.Table table = new iText.Layout.Element.Table(7, false);

            // Add column headers
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Origin")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Destination")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Date and Time")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Weight(kg)")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Height(cm)")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Length(cm)")));
            table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Width(cm)")));

            // Add the luggage items to the table
            foreach (LuggageItem item in LuggageItems)
            {
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Origin.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Destination.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Date.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Height.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Weight.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Length.ToString())));
                table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(item.Width.ToString())));
            }

            // Add the table to the document
            document.Add(table);

            // Close the document
            document.Close();
        }

        private double GetTotalWeight()
        {
            double totalWeight = 0;

            foreach (LuggageItem item in LuggageItems)
            {
                totalWeight += item.Weight;
            }

            return totalWeight;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            txtHeight.Clear();
            txtWeight.Clear();
            txtLength.Clear();
            txtWidth.Clear();
            txtFrom.Clear();
            txtTo.Clear();
            DP.SelectedDate = null;
            LuggageItems.Clear();
            lbTotalWeight.Content = "0.0";
        }
    }
}
