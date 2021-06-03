using ESC_POS_USB_NET.Printer;
using ESC_POS_USB_NET.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace WWBPrinter
{
    class Channels
    {

        //variables
        public string frequency;
        public string device;
        public string name;
        public string band;
        public string group;
        public string channel;

        //properties
        public bool IsActive
        {
            get {
                if(this.name != "")
                {
                    return true;
                }
                return false;

            }
        }

 
        //constructor
        public Channels(string[] csvRow)
        {
            this.frequency = csvRow[5].Remove(7);
            this.device = csvRow[0];
            this.name = csvRow[2];
            this.band = csvRow[1];
            this.group = csvRow[4].Substring(2, 2);
            this.channel = csvRow[4].Substring(8, 2); ;
        }

        
        public override string ToString()
        {
            int maxChar = Program.maxCondensedChar;
            int usedChar;
            int dotNum;
            
            usedChar = this.band.Length + this.name.Length + this.frequency.Length;
            dotNum = maxChar - usedChar - 2;
            string dots = "......................................";
            dots = dots.Remove(dotNum);
            return $"{this.band}: {this.name}{dots}{this.frequency}";
        }


       
    }
}
