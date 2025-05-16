using System.ComponentModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using PV178_Project.Models;

namespace PV178_Project.Services;

public static class TournamentExportService
{
    private static string? GetFilePath()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog
        {
            FileName = "Matches",
            DefaultExt = ".pdf",
            Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
        };

        bool? result = saveFileDialog.ShowDialog();
        return result == true ? saveFileDialog.FileName : null;
    }

    public static async Task ExportTournamentMatchesSchedule(List<Match> matches, string tournamentName)
    {
        string? filePath = GetFilePath();
        if (filePath == null) return;
        await Task.Run(() =>
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            document.Add(new Paragraph(tournamentName, font) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });

            //Column Headers
            AddCellToTable(table, "Name", true);
            AddCellToTable(table, "Team A", true);
            AddCellToTable(table, "Score Team A", true);
            AddCellToTable(table, "Start Time", true);
            AddCellToTable(table, "Score Team B", true);
            AddCellToTable(table, "Team B", true);
            AddCellToTable(table, "Winner", true);

            //Matches
            foreach (var match in matches)
            {
                AddCellToTable(table, match.Name);
                AddCellToTable(table, match.TeamA?.Name ?? "N/A");
                AddCellToTable(table, match.PointsTeamA.ToString());
                AddCellToTable(table, match.StartTime.ToString("g")); // general format
                AddCellToTable(table, match.PointsTeamB.ToString());
                AddCellToTable(table, match.TeamB?.Name ?? "N/A");
                AddCellToTable(table, match.Winner?.Name ?? "TBD");
            }

            document.Add(table);
            document.Close();
        });
    }

    private static void AddCellToTable(PdfPTable table, string text, bool header = false, int rowSpan = 1)
    {
        var font = header
            ? FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14)
            : FontFactory.GetFont(FontFactory.HELVETICA, 12);
        table.AddCell(new PdfPCell(new Phrase(text, font))
        {
            HorizontalAlignment = Element.ALIGN_CENTER,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Padding = 5,
            Rowspan = rowSpan,
            BackgroundColor = header ? BaseColor.LIGHT_GRAY.Brighter() : BaseColor.WHITE,
        });
    }

    public static async Task ExportTournamentMatchesSpider(List<List<Match>> matchesColumns, string tournamentName)
    {
        string? filePath = GetFilePath();
        if (filePath == null) return;
        await Task.Run(() =>
        {
            Document document = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            document.Add(new Paragraph(tournamentName, font) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("\n"));

            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            table.SetWidths(new float[] { 1, 1, 1, 1, 1, 1, 1 });
            //Matches
            for (int i = 0; i < matchesColumns.Count; i++)
            {
                var matchesColumn = matchesColumns[i];
                
                PdfPTable matchTable = new PdfPTable(1);
                
                foreach (var match in matchesColumn)
                {
                    AddCellToTable(matchTable, GetFormattedObject(match), rowSpan: 4 / matchesColumn.Count);
                }
                PdfPCell cell = new PdfPCell(matchTable);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table.AddCell(cell);
            }
            document.Add(table);
            document.Close();
        });
    }

    private static string GetFormattedObject(Match match)
    {
        return string.Join("\n",
            match.Name,
            (match.TeamA?.Name ?? "N/A") + " " + match.PointsTeamA,
            (match.TeamB?.Name ?? "N/A") + " " + match.PointsTeamB
        );
    }
}