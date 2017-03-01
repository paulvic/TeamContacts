using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamContacts
{
    class EmailListWriter
    {
        public static void Write(List<PlayerDetails> players)
        {
            var teams = players.Select(x => x.Team).Distinct();

            foreach (var team in teams)
            {
                // Write sample data to CSV file
                using (StreamWriter writer = new CsvFileWriter($"EmailList-{team}.txt"))
                {
                    List<string> emailAddresses = new List<string>();
                    foreach (var player in players.FindAll(o => o.Team == team))
                    {
                        var suffix = String.Format($"*{player.FirstName} {player.LastName} {player.Team}*");
                        emailAddresses.Add($"{player.FirstNameG1} {suffix} <{player.EmailG1}>");
                        if (!String.IsNullOrEmpty(player.FirstNameG2) &&
                            !String.IsNullOrEmpty(player.EmailG2))
                        {
                            emailAddresses.Add($"{player.FirstNameG2} {suffix} <{player.EmailG2}>");
                        }
                        if (!String.IsNullOrEmpty(player.FirstNameG3) &&
                            !String.IsNullOrEmpty(player.EmailG3))
                        {
                            emailAddresses.Add($"{player.FirstNameG3} {suffix} <{player.EmailG3}>");
                        }
                    }

                    string fullEmailList = String.Join(";", emailAddresses);
                    fullEmailList = fullEmailList.Replace("(", "*").Replace(")", "*");
                    // Use Write rather than WriteLine so you won't have a new line included when you copy and paste
                    writer.Write(fullEmailList);
                }
            }
        }
    }
}
