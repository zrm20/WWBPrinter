using System;
using System.Collections.Generic;

namespace WWBPrinter
{
    static class CSVReader
    {
        public static string[][] LoadCsv(string filename)
        {
            //Creates a jagged array of all CSV Cells
            string[] lines = System.IO.File.ReadAllLines(filename);
            string[][] jaggedArray = new string[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] cells = lines[i].Split(',');
                jaggedArray[i] = cells;
            }
            return jaggedArray;
        }

        public static void PrintArray(string[][] array)
        {
            //Prints jagged array data to console for easy viewing
            int row = 0;
            foreach (string[] r in array)
            {
                if (r != null)
                {
                    Console.Write("{0}: ", row);
                    foreach (string c in r)
                    {
                        if (c != null)
                        {
                            Console.Write(c + "\t|");
                        }
                    }
                    row++;
                    Console.Write("\n");
                }
            }
        }
    }
}
