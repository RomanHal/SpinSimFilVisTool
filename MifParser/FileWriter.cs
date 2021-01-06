using MifParser.Atlases;
using MifParser.Energies;
using MifParser.Scripts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MifParser
{
    public class FileWriter
    {
        MifObjectsContainer Objects { get; set; }

        string[] GetText()
        {
            List<string> text = new List<string>();

            //unprocessed text part1
            text.AddRange(Objects.UnprocessedText.Text[0]);
            Objects.MifScripts.Where(o => o is ScriptObject).ToList().
                ForEach(a => text.AddRange(((ScriptObject)a).GetText()));
            Objects.MifAtlases.Where(o => o is Atlas).ToList().
                ForEach(a => {
                    var atlasText = ((Atlas)a).GetText();
                    atlasText[0] = "Specify " + atlasText[0];
                    text.AddRange(atlasText);
                });
            //fields
            text.AddRange(Objects.UnprocessedText.Text[1]);

            //energies
            Objects.MifEnergies.Where(o => o is Energy).ToList().
                ForEach(e =>
                {
                    var energyText = ((Energy)e).GetText();
                    energyText[0] = "Specify " + energyText[0];
                    text.AddRange(energyText);
                });
            text.AddRange(Objects.UnprocessedText.Text[2]);

            //evolvers, drivers

            text.AddRange(Objects.UnprocessedText.Text[3]);
            return text.ToArray();
        }

        public void SaveFile(string patch,MifObjectsContainer container)
        {
            Objects = container;
            File.WriteAllLines(patch, GetText());
        }
        public void SaveFile(string patch,string[] text)
        {
            File.WriteAllLines(patch, text);
        }
        public string[] PreView(MifObjectsContainer container)
        {
            Objects = container;
            return GetText();
        }
       
    }
}
