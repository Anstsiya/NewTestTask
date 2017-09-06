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
            string pathRes = Environment.CurrentDirectory;

            
            try
            {
                // reading from file
                using (StreamReader sr = new StreamReader(path))
                {
                    str = sr.ReadToEnd();
                }
                
                var nstr = str.ToLower();       //to lower case

                string pattern = @"[a-zA-Z0-9^']+";
                var res = Regex.Matches(nstr, pattern); // split on words

                List<string> l = new List<string>();
                foreach (var v in res)
                {
                    l.Add(v.ToString());
                }

                var indexes = l.Select((item, index) => new { Item = item, Index = index + 1 });    //making pairs of word and number of string 
                var sorted = indexes.OrderBy(k => k.Item);  //alphabetical sorting

                Dictionary<string, string> SortedDict = new Dictionary<string, string>();
                foreach (var r in sorted)
                {
                    if (!SortedDict.ContainsKey(r.Item))                //if dictionary not contains the word, write word 
                    {                                                   //to dictionary like Key and number of string like Value
                        SortedDict.Add(r.Item, r.Index.ToString());
                    }
                    else
                    {                                                           //if dictionary already contains the word,
                        string value = "";                                      //write to Value last and new string numbers join

                        var tr = SortedDict.TryGetValue(r.Item, out value);

                        var e = value + ", " + r.Index;
                        SortedDict[r.Item] = e;
                    }
                }

                //writing to file
               using (StreamWriter sw = new StreamWriter(pathRes + fileName))
                {
                    foreach (var res1 in SortedDict)
                    {
                        sw.WriteLineAsync(res1.Key + " - " + res1.Value);
                        Console.WriteLine(res1.Key + " - " + res1.Value);

                    }
                    sw.Close();
                }
            }
            catch
            {
            }
        }
    }
}
