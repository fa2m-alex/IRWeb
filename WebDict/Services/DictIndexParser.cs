using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;

namespace WebDict.Services
{
    public class DictIndexParser
    {
        private string path;

        public Dictionary<string, int> Dictionary { get; }
        public Dictionary<int, List<int>> Index { get; }

        public DictIndexParser()
        {
            Dictionary = new Dictionary<string, int>();
            Index = new Dictionary<int, List<int>>();
        }

        public void SetDictionary(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var elems = line.Split();

                if(!Dictionary.ContainsKey(elems.First()))
                    Dictionary.Add(elems.First(), Int32.Parse(elems.Last()));
            }
        }

        public void SetIndex(string path)
        {
            StreamReader reader = new StreamReader(path);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                var elems = line.Split();

                List<int> temp = new List<int>();
                for (int i = 1; i < elems.Length; i++)
                {
                    temp.Add(Int32.Parse(elems[i]));
                }

                if(!Index.ContainsKey(Int32.Parse(elems.First())))
                    Index.Add(Int32.Parse(elems.First()), temp);
            }
        }

        public List<int> Search(string query)
        {
            var elems = query.Split();

            string temp = elems.First();
            List<int> result = GetVector(temp);

            for (int i = 1; i < elems.Length; i++)
            {
                if (elems[i].Equals("&"))
                {
                    result = AndOp(result, GetVector(elems[i + 1]));
                }
                else if (elems[i].Equals("|"))
                {
                    result = OrOp(result, GetVector(elems[i + 1]));
                }
            }

            return result;
        }

        private List<int> GetVector(string word)
        {
            return Index[Dictionary[word]];
        }

        private List<int> AndOp(List<int> word1, List<int> word2)
        {
            return word1.Intersect(word2).ToList();
        }

        private List<int> OrOp(List<int> word1, List<int> word2)
        {
            return word1.Union(word2).ToList();
        }
    }
}