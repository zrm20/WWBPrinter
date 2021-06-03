using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESC_POS_USB_NET.Enums;
using ESC_POS_USB_NET.Printer;


namespace WWBPrinter
{
    class Program
    {
        public static string file = @"C:\Users\Zach McCoy\Documents\Shure\Report\print_me.csv";
        public static string printerName = "POS-58-Series";
        public static int maxRegChar= 16; //16
        public static int maxCondensedChar = 40; //40
        public static string docTitle = "Wireless Workbench 6 Coordination Report";
        public static Printer printer;
        public static string userName = "System.AlyssaBot";
        public string[] featuredNames = {"JM Ac", "JM El"};

        static void Main(string[] args)
        {
            EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            printer = new Printer(printerName);

         
            try
            {
                Workbench myShow = new Workbench(CSVReader.LoadCsv(file));
                CSVReader.PrintArray(CSVReader.LoadCsv(file));
                Console.WriteLine(myShow.ToString());
                Printing.PrintShow(myShow);

                MessageToJoey();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public static void MessageToJoey()
        {
            Console.WriteLine("Type a message to Joey or press enter to skip: ");

            string input = Console.ReadLine();
            string output = "";
            if (input != "")
            {
                output = "Joey,\n";
                output = output + WrapMessage(input) + $"\nLove,\n{userName}";
                printer.Append("");
                printer.BoldMode(PrinterModeState.Off);
                printer.AlignLeft();
                printer.Append(output);
                Program.printer.Append("");
                Program.printer.PrintDocument();
                Program.printer.PartialPaperCut();
                Program.printer.Clear();
            }
        }

        public static string WrapMessage(string input)
        {
            string stringToSplit = input.Trim();
            var lines = new List<string>();

            while (stringToSplit.Length > 0)
            {
                if (stringToSplit.Length <= maxCondensedChar)
                {
                    lines.Add(stringToSplit);
                    break;
                }

                var indexOfLastSpaceInLine = stringToSplit.Substring(0, maxCondensedChar).LastIndexOf(' ');
                lines.Add(stringToSplit.Substring(0, indexOfLastSpaceInLine >= 0 ? indexOfLastSpaceInLine : maxCondensedChar).Trim());
                stringToSplit = stringToSplit.Substring(indexOfLastSpaceInLine >= 0 ? indexOfLastSpaceInLine + 1 : maxCondensedChar);
            }
            string result = "";
            foreach (string l in lines)
            {
                result = result + l + "\n";
            }
            return result;
        }
    
        
    }
}
