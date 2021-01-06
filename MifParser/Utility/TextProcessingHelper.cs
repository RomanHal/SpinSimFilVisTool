using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Utility
{
    class TextProcessingHelper
    {
        Dictionary<char, char> _closingCharDictionary = new Dictionary<char, char>();
        public TextProcessingHelper()
        {
            _closingCharDictionary.Add('{', '}');
            _closingCharDictionary.Add('[', ']');
        }

        internal int SearchClosingBraces(string[] lines, int index,params char[] openingCharCandidates)
        {
            var openingChar = openingCharCandidates.Select((c, i) => new { c, i }).OrderBy(s => lines[0].IndexOf(s.c))
                .Select(s => s.c).First();
            return SearchClosingBraces(lines, openingChar, index);
        }
        internal int SearchClosingBraces(string[] lines, char openingCharacter, int index)
        {
            int count = 0;
            do
            {
               
                if (index > lines.Length - 1) throw new ArgumentException("File is corrupted");
                foreach (char Char in lines[index])
                {
                    if (Char == openingCharacter) count++;
                    if (Char == _closingCharDictionary[openingCharacter]) count--;
                }
                index++;
            }
            while (count > 0);
            return index-1;
        }
        
        internal string[] GetCodeToClosingBrace(string[] lines,int startIndex,params char[] openingCharCandidates )
        {
            var openingChar=openingCharCandidates.Select((c, i) => new { c, i }).OrderBy(s => lines[0].IndexOf(s.c))
                .Select(s=>s.c).First();
            return GetCodeToClosingBrace(lines, startIndex, openingChar);
        }

        internal string[] GetCodeToClosingBrace(string[] lines,int startIndex,  char openingCharacter)
        {
            int endIndex = SearchClosingBraces(lines, openingCharacter, startIndex);
            return lines.Skip(startIndex).Take(1 + endIndex - startIndex).ToArray();
        }
        internal string GetName(string line)
        {
            var name = line.Select((c, i) => new { c, i }).
                  Where(o => o.i > line.LastIndexOf(':')).
                  Where(o => line.IndexOf('[') != -1 ? o.i < line.IndexOf('[') : o.i > -1).
                  Where(o => o.i < line.LastIndexOf('{')).
                  Select(o => o.c);
            string nameStr = string.Concat(name);
            return nameStr.Replace(" ", "");
        }

        internal int GetIndexOfStringContaining(string[] lines,string word)
        {
            return lines.Select((s, i) => new { s, i }).Where(l => l.s.Contains(word)).
                Select(l => l.i).FirstOrDefault();
        }
        
        internal string[] RemoveFromText(string[] text,params string[] characters)
        {
            var temp = text;
            foreach(string s in characters)
            {
                temp = RemoveFromText(temp, s);
            }
            return temp;
        }
        internal string[] RemoveFromText(string[] text,string characters)
        {
            return text.Select(s => s.Replace(characters, "")).ToArray();
        }
    }
}
