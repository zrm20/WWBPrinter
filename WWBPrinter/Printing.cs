using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;

namespace WWBPrinter
{
    static class Printing
    {
        public static string condensedBreak = "----------------------------------------";

        public static void PrintShow(Workbench myShow)
        {
            Program.printer.Clear();
            //Title header
            Program.printer.AlignCenter();
            Program.printer.CondensedMode(PrinterModeState.Off);
            Program.printer.BoldMode(PrinterModeState.On);
            Program.printer.Append(myShow.WrappedShowName);
            Program.printer.BoldMode(PrinterModeState.Off);
            Program.printer.CondensedMode(PrinterModeState.On);
            Program.printer.Append(Program.docTitle);
            Program.printer.Append(myShow.dateCreated);
            Program.printer.AlignLeft();
            Program.printer.Append(condensedBreak);

            Program.printer.CondensedMode(PrinterModeState.Off);
            Program.printer.Append("Active Channels:");
            Program.printer.CondensedMode(PrinterModeState.On);
            Program.printer.Append(condensedBreak);

            foreach (Channels c in myShow.channelList)
            {
                if (c.IsActive)
                {
                    Program.printer.Append(c.ToString());
                }
            }
            Program.printer.Append("");

            Program.printer.Append("Backup Frequencies:");
            Program.printer.Append(condensedBreak);
            foreach (Channels c in myShow.channelList)
            {
                if (!c.IsActive)
                {
                    Program.printer.Append(c.ToString());
                }
            }

            Program.printer.Append("");
            Program.printer.AlignCenter();
            Program.printer.BoldMode(PrinterModeState.On);
            Program.printer.Append("-WWB6 Printer by Zach McCoy-");
            Program.printer.Append($"Printed at {DateTime.Now}");
            Program.printer.Append("");

            Program.printer.PrintDocument();
            Program.printer.Clear();
        }
    }
}
