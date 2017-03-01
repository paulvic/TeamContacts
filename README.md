# TeamContacts
A command line tool for exporting a team's contact list into formats suitable for importing into other tools such as teamer.net and google contacts (to create a whatsapp group on your phone).

## REQUIREMENTS
Excel must be installed on your PC

## Usage Instructions
1. Ensure your team's contacts file matches the format in the supplied sample Excel spreadsheet. A team member can have up to 3 guardian's details included. Select a team name that is reasonably unique such as "U10_Vikings".
2. Run: TeamContacts.exe <path to xlsx file>
3. 3 files will be generated for each team listed in the spreadsheet:
 * EmailList-<TeamName>.txt
   - This is a list of all the guardian email addresses for that team
 * GoogleContactsImport-<TeamName>.csv
   - This is a list of each guardian's contact details which can be imported into Google Contacts
 * TeamerImport-<TeamName>.csv
   - This is a list of each player and their guardian's details which can be imported into Teamer.net
   
## Creating a WhatApp group from the Google Contacts import file
1. Open Gmail in your browser and go to Contacts
2. Click on "Import..." in the "More" menu and select the GoogleContacts csv file generated above
3. On your Android phone, open Contacts and swipe down and hold to force a refresh the contacts list. The guardians' names will appear in the format: FirstName (PlayerFirstName PlayerSecondName TeamName).
4. Open WhatsApp and choose "New group".
5. Click search and enter the team name which will filter the list of guardians down to that team

## Importing the player and guardian details into Teamer.net
1. Open teamer.net and create a team
2. Click on Add to add members and select "Import Spreadsheet"
3. Click "Ok I'm ready to upload", "Upload .csv file" and then select the TeamerImport csv file
