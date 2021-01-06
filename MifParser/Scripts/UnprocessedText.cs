using MifParser.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MifParser.Scripts
{
    public class UnprocessedText
    {
        public List<List<string>> Text { get => _text; set => _text = value; }
        List<List<string>> _text = new List<List<string>>();
        public List<string> Lines { get => _lines; set => _lines = value; }
        List<string> _lines = new List<string>();
        List<RangeInt> parts = new List<RangeInt>();
        public UnprocessedText()
        {
            _text.Add(new List<string>() { "" });
            _text.Add(new List<string>() { "" });
            _text.Add(new List<string>() { "" });
            _text.Add(new List<string>() { "" });
        }
        public void AddPart(int startLine, int endLine)
        {
            parts.Add(new RangeInt(startLine, endLine));
        }
        public List<List<string>> GetTextParts()
        {
            List<List<string>> textParts = new List<List<string>>();
            parts.ForEach(p => textParts.Add(_lines.Skip(p.Min)
                .Take(p.Max - p.Min - 1).ToList()));
            return textParts;
        }
        public void AddLine(string line)
        {
            _text[0].Add(line);
            //_lines.Add(line);
        }
        public void AddLines(IEnumerable<string> lines)
        {
            _text[0].AddRange(lines);
            //_lines.AddRange(lines);
        }

    }
}
