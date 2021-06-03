
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using ESC_POS_USB_NET;
using ESC_POS_USB_NET.Printer;
using ESC_POS_USB_NET.Enums;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace WWBPrinter
{
    class Workbench
    {
        
        //variables
        public string showName;
        public string dateCreated;
        public string venue;
        public string venuePhone;
        public string venueEmail;
        public string pointOfContact;
        public string pointOfContactPhone;
        public string pointOfContactEmail;
        public string showNotes;
        public List<Channels> channelList;

        //properties
        public string WrappedShowName
        {
            get
            {
                string stringToSplit = this.showName.Trim();
                var lines = new List<string>();

                while (stringToSplit.Length > 0)
                {
                    if (stringToSplit.Length <= Program.maxRegChar)
                    {
                        lines.Add(stringToSplit);
                        break;
                    }

                    var indexOfLastSpaceInLine = stringToSplit.Substring(0, Program.maxRegChar).LastIndexOf(' ');
                    lines.Add(stringToSplit.Substring(0, indexOfLastSpaceInLine >= 0 ? indexOfLastSpaceInLine : Program.maxRegChar).Trim());
                    stringToSplit = stringToSplit.Substring(indexOfLastSpaceInLine >= 0 ? indexOfLastSpaceInLine + 1 : Program.maxRegChar);
                }
                string result = "";
                foreach (string l in lines)
                {
                    result = result + l + "\n";
                }
                return result;
            }
        }


        //Workbench Constructor
        public Workbench(
            string showName,
            string dateCreated,
            string venue,
            string venuePhone,
            string venueEmail,
            string pointOfContact,
            string pointOfContactPhone,
            string pointOfContactEmail,
            string showNotes)
        {
            this.showName = showName;
            this.dateCreated = dateCreated;
            this.venue = venue;
            this.venuePhone = venuePhone;
            this.venueEmail = venueEmail;
            this.pointOfContact = pointOfContact;
            this.pointOfContactPhone = pointOfContactPhone;
            this.pointOfContactEmail = pointOfContactEmail;
            this.showNotes = showNotes;
            this.channelList = new List<Channels>();
        }

        public Workbench(string[][] csvData)
        {
            //assign and format name and date strings
            this.showName = csvData[1][0].Replace("\"", "");
            this.dateCreated = csvData[csvData.Length - 2][0].Replace("\"", "");

            //create a channels list
            this.channelList = new List<Channels>();
            foreach (string[] r in csvData)
            {
                if (r.Length > 4 && r[7].Contains("?"))
                {
                    this.channelList.Add(new Channels(r));
                }
            }
        }


        public override string ToString()
        {
            //header info
            string header = $"{showName.ToUpper()}\n" +
                $"{Program.docTitle}\n" +
                $"{dateCreated}\n";
            //body infor: active channels first then backup frequencies
            string channelsStrings = "Active Channels:";
            foreach (Channels c in channelList)
            {
                if (c.name != "")
                {
                    channelsStrings = channelsStrings + "\n" + c.ToString();
                }
            }
            channelsStrings = channelsStrings + "\nBackup Frequencies:";
            foreach (Channels c in channelList)
            {
                if (c.name == "")
                {
                    channelsStrings = channelsStrings + "\n" + c.ToString();
                }
            }

            //footer info
            string footer = "-WWB6 Printer by Zach McCoy-\n" +
                $"Printed at {DateTime.Now}";
            return header + "\n" + channelsStrings + "\n" + footer;
        }

    }
}


    

