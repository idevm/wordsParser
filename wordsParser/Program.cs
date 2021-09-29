using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace wordsParser
{
    public class Program
    {
        #region
        private static readonly string rusObscene = "блядь,гандон,говно,говнюк,дерьмо,дрочер,ебало,ебальник,елда,жопа,задрот,залупа,манда,мудак,мудень,муди,мудила,педераст,педерастия,пердун,пидор,пизда,пиздец,хер,хуй,шалава,шлюха,шмара";
        #endregion

        private static void Main()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine($"Start at {start.ToLongTimeString()}");
            List<string> ob = rusObscene.Split(",").ToList();
            string[] lines = ReadFile("wordsRus.txt");
            List<string> words = new();
            StringBuilder txt = new();
            Console.WriteLine("Progress: ");
            var ls = from line in lines where !words.Contains(line) && !ob.Contains(line) && !line.Contains('-') && !line.Contains(' ') select line;
            foreach (string line in ls)
            {
                words.Add(line);
                txt.AppendLine(line);
                Console.Write($"\r{words.Count} out of {lines.Length} words added");
                Console.Write($" {(new string[] { "|", "/", "-", @"\" })[words.Count / 500 % 4]} ");
            }
            WriteFile("out.txt", txt.ToString());
            DateTime end = DateTime.Now;
            TimeSpan d = end - start;
            Console.WriteLine($"\nEnd at {end.ToLongTimeString()}");
            Console.WriteLine($"Complete in {d}");
        }


        public static string[] ReadFile(string path)
        {
            try
            {
                return System.IO.File.ReadAllLines(path);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.IO.FileNotFoundException noFile = new("Не найден файл с данными, а именно: " + ex.Message);
                throw noFile;
            }
        }


        public static void WriteFile(string path, string txt) => System.IO.File.AppendAllText(path, txt);
    }
}