using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NewTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Task();
        }
        static void Task()
        {
            string path = @"TestTask.txt";
            string str = "";
            
            string fileName = @"\TaskResult.txt";
            var pathRes = Environment.CurrentDirectory;

            Dictionary<string, string> SortedDict = new Dictionary<string, string>();

            try
            {
                // reading from file
                //ReadFile();
                using (StreamReader sr = new StreamReader(path))
                {
                    //str = await sr.ReadToEndAsync();
                    str = sr.ReadToEnd();
                    //Console.WriteLine(str);
                }
                //
                var nstr = str.ToLower();

                string pattern = @"[a-zA-Z0-9^']+";
                var res = Regex.Matches(nstr, pattern);

                List<string> l = new List<string>();
                foreach (var v in res)
                {
                    l.Add(v.ToString());

                }

                var indexes = l.Select((item, index) => new { Item = item, Index = index + 1 });
                var sorted = indexes.OrderBy(k => k.Item);

                foreach (var r in sorted)
                {
                    if (!SortedDict.ContainsKey(r.Item))
                    {
                        SortedDict.Add(r.Item, r.Index.ToString());
                    }
                    else
                    {
                        string value = "";

                        var tr = SortedDict.TryGetValue(r.Item, out value);

                        var e = value + ", " + r.Index;
                        SortedDict[r.Item] = e;
                    }
                }

                //writing to file
                //WriteFile();
                using (StreamWriter sw = new StreamWriter(pathRes + fileName))
                {

                    foreach (var res1 in SortedDict)
                    {
                        //await sw.WriteLineAsync(res1.Key + " " + res1.Value);
                        sw.WriteLineAsync(res1.Key + " - " + res1.Value);
                        Console.WriteLine(res1.Key + " - " + res1.Value);

                    }
                    sw.Close();
                }
                //
            }

            catch
            {
            }

            /* async void ReadFile()
             {
                 using (StreamReader sr = new StreamReader(path))
                 {
                    str = await sr.ReadToEndAsync();
                 }

                 async void WriteFile()
             {
             using (StreamWriter sw = new StreamWriter(pathRes + fileName))
                {

                    foreach (var res1 in SortedDict)
                    {
                        await sw.WriteLineAsync(res1.Key + " " + res1.Value);
                    }
                sw.Close();
                }
             }
             */


        }
    }
}
