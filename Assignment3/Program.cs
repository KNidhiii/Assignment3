using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;


namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"..\..\..\..\Data.csv";
            var path1 = @"..\..\..\..\Data1.csv";

            using (var reader = new StreamReader(path))
            {
                List<string> listA = new List<string>();
                List<string> listB = new List<string>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    listA.Add(values[0]);
                    listB.Add(values[1]);
                }
                listA.RemoveAt(0);
                listB.RemoveAt(0);

                List<string> listC = new List<string>();
               

                
                foreach (var i in listB)
                {
                    DateTime utcdate = DateTime.ParseExact(i, "H:mm:ss", CultureInfo.InvariantCulture);
                    var istdate = TimeZoneInfo.ConvertTimeFromUtc(utcdate,
                                  TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    var timest=istdate.ToString("H:mm:ss");
                    listC.Add(timest);
                }

                List<string> lines = File.ReadAllLines(path).ToList();
               
                lines[0] += ",Time(IST)";
                int index = 1;
                lines.Skip(1).ToList().ForEach(line =>
                {
                    lines[index] += "," + listC[index - 1];
                    index++;
                });

                File.WriteAllLines(path1, lines);
            }
        }
    }
}
