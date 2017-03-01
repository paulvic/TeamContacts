using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;       // Microsoft Excel 16 object in references-> COM tab

namespace TeamContacts
{
    public class TeamDetails
    {
        private List<PlayerDetails> players;
        public List<PlayerDetails> Players
        {
            get { return players; }
            set { players = value; }
        }

        public TeamDetails(string masterContactsFile)
        {
            if (!File.Exists(masterContactsFile))
            {
                throw new FileNotFoundException(masterContactsFile);

            }
            this.players = new List<PlayerDetails>();
            PopulateFromExcel(masterContactsFile);
        }

        private void PopulateFromExcel(string masterContactsFile)
        {
            // Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(masterContactsFile, ReadOnly: true);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            
            // Note: Excel is not zero based, Row 1 is the header
            for (int i = 2; i <= rowCount; i++)
            {
                if (!String.IsNullOrEmpty(xlRange.Cells[i, 1].Value2.ToString()))
                {
                    PlayerDetails player = new PlayerDetails();
                    player.FirstName = GetCellString(xlRange.Cells[i, 2]);
                    player.LastName = GetCellString(xlRange.Cells[i, 3]);
                    player.Team = GetCellString(xlRange.Cells[i, 7]);
                    player.FirstNameG1 = GetCellString(xlRange.Cells[i, 8]);
                    player.LastNameG1 = GetCellString(xlRange.Cells[i, 9]);
                    player.EmailG1 = GetCellString(xlRange.Cells[i, 11]);
                    player.PhoneG1 = GetCellString(xlRange.Cells[i, 10]).Replace("-", String.Empty);
                    player.FirstNameG2 = GetCellString(xlRange.Cells[i, 12]);
                    player.LastNameG2 = GetCellString(xlRange.Cells[i, 13]);
                    player.EmailG2 = GetCellString(xlRange.Cells[i, 15]);
                    player.PhoneG2 = GetCellString(xlRange.Cells[i, 14]).Replace("-", String.Empty);
                    player.FirstNameG3 = GetCellString(xlRange.Cells[i, 16]);
                    player.LastNameG3 = GetCellString(xlRange.Cells[i, 17]);
                    player.EmailG3 = GetCellString(xlRange.Cells[i, 19]);
                    player.PhoneG3 = GetCellString(xlRange.Cells[i, 18]).Replace("-", String.Empty);
                    players.Add(player);
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }

        private static string GetCellString(Excel.Range cell)
        {
            return (cell.Value2 == null) ? String.Empty : cell.Value2.ToString().Trim();
        }
    }
}
