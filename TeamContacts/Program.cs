using System;
using System.IO;

namespace TeamContacts
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1 ||
                String.IsNullOrEmpty(args[0]))
            {
                PrintUsage();
                return;
            }
            
            try
            {
                var teamDetails = new TeamDetails(Path.GetFullPath(args[0]));
                GoogleContactsWriter.Write(teamDetails.Players);
                TeamerImportWriter.Write(teamDetails.Players);
                EmailListWriter.Write(teamDetails.Players);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Invalid path!! {e.Message}\n");
                PrintUsage();
                return;
            }
            
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Reads a master contact list and outputs the data in a CSV format for importing into Teamer, another for importing into your google contacts (to create a whatsapp group) and a text file of email addresses.\n");
            Console.WriteLine("TeamContacts.exe <path to master contact list>");
        }
    }
}
