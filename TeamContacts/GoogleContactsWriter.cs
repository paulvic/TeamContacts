using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TeamContacts
{
    class GoogleContactsWriter
    {
        public static void Write(List<PlayerDetails> players)
        {
            var teams = players.Select(x => x.Team).Distinct();

            foreach (var team in teams)
            {
                using (StreamWriter writer = new CsvFileWriter($"GoogleContactsImport-{team}.csv"))
                {
                    writer.WriteLine("Name,Given Name,Additional Name,Family Name,Yomi Name,Given Name Yomi,Additional Name Yomi,Family Name Yomi,Name Prefix,Name Suffix,Initials,Nickname,Short Name,Maiden Name,Birthday,Gender,Location,Billing Information,Directory Server,Mileage,Occupation,Hobby,Sensitivity,Priority,Subject,Notes,Group Membership,E-mail 1 - Type,E-mail 1 - Value,E-mail 2 - Type,E-mail 2 - Value,Phone 1 - Type,Phone 1 - Value,Address 1 - Type,Address 1 - Formatted,Address 1 - Street,Address 1 - City,Address 1 - PO Box,Address 1 - Region,Address 1 - Postal Code,Address 1 - Country,Address 1 - Extended Address,Website 1 - Type,Website 1 - Value");
                    foreach (var player in players.FindAll(o => o.Team == team))
                    {
                        var suffix = String.Format($"({player.FirstName} {player.LastName} {player.Team})");
                        writer.WriteLine(String.Format($"{player.FirstNameG1} {suffix},{player.FirstNameG1},,{suffix},,,,,,,,,,,,,,,,,,,,,,,* My Contacts ::: {player.Team},*,{player.EmailG1},,,Mobile,{GetPhoneInGoogleFormat(player.PhoneG1)},,,,,,,,,,,"));

                        if (!String.IsNullOrEmpty(player.FirstNameG2) &&
                            !String.IsNullOrEmpty(player.PhoneG2))
                        {
                            writer.WriteLine(String.Format($"{player.FirstNameG2} {suffix},{player.FirstNameG2},,{suffix},,,,,,,,,,,,,,,,,,,,,,,* My Contacts ::: {player.Team},*,{player.EmailG2},,,Mobile,{GetPhoneInGoogleFormat(player.PhoneG2)},,,,,,,,,,,"));
                        }
                        if (!String.IsNullOrEmpty(player.FirstNameG3) &&
                            !String.IsNullOrEmpty(player.PhoneG3))
                        {
                            writer.WriteLine(String.Format($"{player.FirstNameG3} {suffix},{player.FirstNameG3},,{suffix},,,,,,,,,,,,,,,,,,,,,,,* My Contacts ::: {player.Team},*,{player.EmailG3},,,Mobile,{GetPhoneInGoogleFormat(player.PhoneG3)},,,,,,,,,,,"));
                        }
                    }
                }
            }
        }

        private static string GetPhoneInGoogleFormat(string phoneNumber)
        {
            // input format 087123456
            // output format +353 87 123 4567
            if (phoneNumber.Length == 9 && phoneNumber.StartsWith("08"))
            {
                string prefix = phoneNumber.Substring(1, 2);
                string partOne = phoneNumber.Substring(3, 3);
                string partTwo = phoneNumber.Substring(6, 4);

                return String.Format($"+353 {prefix} {partOne} {partTwo}");
            }
            else
            {
                return phoneNumber;
            }
        }
    }
}
