using System;
using System.Collections.Generic;
using System.Linq;

namespace TeamContacts
{
    class TeamerImportWriter
    {
        public static void Write(List<PlayerDetails> players)
        {
            var teams = players.Select(x => x.Team).Distinct();

            foreach (var team in teams)
            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter($"TeamerImport-{team}.csv"))
                {
                    CsvRow header = new CsvRow();
                    header.Add("First Name");
                    header.Add("Last Name");
                    header.Add("Guardian First Name");
                    header.Add("Guardian Last Name");
                    header.Add("E-mail Address");
                    header.Add("Phone");
                    header.Add("2nd Guardian First Name");
                    header.Add("2nd Guardian Last Name");
                    header.Add("2nd Guardian E-mail Address");
                    header.Add("2nd Guardian Phone");
                    writer.WriteRow(header);
                    foreach (var player in players.FindAll(o => o.Team == team))
                    {
                        CsvRow row = new CsvRow();
                        row.Add(ReplaceExtendedCharacters(player.FirstName)); // Teamer import crashes if the text contains extended characters
                        row.Add(ReplaceExtendedCharacters(player.LastName));
                        row.Add(ReplaceExtendedCharacters(player.FirstNameG1));
                        row.Add(ReplaceExtendedCharacters(player.LastNameG1));
                        if (String.IsNullOrEmpty(player.PhoneG1))
                        {
                            row.Add(player.EmailG1);
                            row.Add(String.Empty);
                        }
                        else
                        {
                            row.Add(String.Empty);
                            row.Add(player.PhoneG1);
                        }

                        if (!String.IsNullOrEmpty(player.FirstNameG2) &&
                            !(String.IsNullOrEmpty(player.EmailG2) && String.IsNullOrEmpty(player.PhoneG2)))
                        {
                            row.Add(ReplaceExtendedCharacters(player.FirstNameG2));
                            row.Add(ReplaceExtendedCharacters(player.LastNameG2));
                            if (String.IsNullOrEmpty(player.PhoneG2))
                            {
                                row.Add(player.EmailG2);
                                row.Add(String.Empty);
                            }
                            else
                            {
                                row.Add(String.Empty);
                                row.Add(player.PhoneG2);
                            }
                        }
                        else
                        {
                            row.Add(String.Empty);
                            row.Add(String.Empty);
                            row.Add(String.Empty);
                            row.Add(String.Empty);
                        }
                        writer.WriteRow(row);
                    }
                }
            }
        }

        private static string ReplaceExtendedCharacters(string text)
        {
            return text.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");
        }
    }
}
