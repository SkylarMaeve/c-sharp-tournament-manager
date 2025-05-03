using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using PV178_Project.Models;

public class MatchExporter
{
    public static async Task ExportMatchesToPdfAsync(List<Match> matches)
    {
        // Open Save File Dialog
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            Filter = "PDF files (*.pdf)|*.pdf",
            Title = "Save Match Spider Export",
            FileName = "Matches.pdf"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            string filePath = saveFileDialog.FileName;

            // Perform export asynchronously
            await Task.Run(() => ExportMatchesToPdf(matches, filePath));
            MessageBox.Show("Export completed successfully!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private static void ExportMatchesToPdf(List<Match> matches, string filePath)
    {
        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
        document.Open();

        // Add title
        Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
        document.Add(new Paragraph("Match Spider Export", titleFont));
        document.Add(new Paragraph("\n"));

        // Add sections
        AddSection(document, "Round of 16 A", matches.Where(t => t.Name.Contains("Round of 16 A")).ToList());
        AddSection(document, "Quarterfinals A", matches.Where(t => t.Name.Contains("Quarterfinals A")).ToList());
        AddSection(document, "Semifinals A", matches.Where(t => t.Name.Contains("Semifinals A")).ToList());
        AddSection(document, "Round of 16 B", matches.Where(t => t.Name.Contains("Round of 16 B")).ToList());
        AddSection(document, "Quarterfinals B", matches.Where(t => t.Name.Contains("Quarterfinals B")).ToList());
        AddSection(document, "Semifinals B", matches.Where(t => t.Name.Contains("Semifinals B")).ToList());

        // Final and 3rd Place Matches
        var finalMatches = new List<Match>
        {
            matches.FirstOrDefault(m => m.Name.Contains("Final")),
            matches.FirstOrDefault(m => m.Name.Contains("3rd Place Match"))
        };
        AddSection(document, "Final Matches", finalMatches);

        document.Close();
    }

    private static void AddSection(Document document, string sectionTitle, List<Match> matches)
    {
        Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        Font matchFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

        document.Add(new Paragraph(sectionTitle, sectionFont));
        foreach (var match in matches)
        {
            if (match != null)
            {
                document.Add(new Paragraph(match.Name, matchFont));
            }
        }
        document.Add(new Paragraph("\n"));
    }
}

// Example usage in an asynchronous context
// await MatchExporter.ExportMatchesToPdfAsync(matches);
