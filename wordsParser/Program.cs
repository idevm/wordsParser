using System;
using System.Collections.Generic;
using System.Text;

namespace wordsParser
{
    public class Program
    {
        private static readonly string rusObscene = "блядь,гандон,говно,говнюк,дерьмо,дрочер,ебало,ебальник,елда,жопа,задрот,залупа,манда,мудак,мудень,муди,мудила,педераст,педерастия,пердун,пидор,пизда,пиздец,хер,хуй,шалава,шлюха,шмара";


        private static void Main()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine($"Start at {start.ToLongTimeString()}");
            Span<string> ob = rusObscene.Split(",").AsSpan();
            string[] lines = ReadFile("wordsRus.txt");
            List<string> words = new();
            StringBuilder txt = new();
            Console.WriteLine("Progress: ");
            foreach (string line in lines)
            {
                if (!words.Contains(line) && !ob.Contains(line) && !line.Contains('-'))
                {
                    words.Add(line);
                    txt.Append(line + "\n");
                }
                Console.Write($"\r{words.Count} out of {lines.Length} words added");
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