using System;
using System.Collections.Generic;
using System.Text;

namespace GUIProject.Models
{
    class UnInterpretedTextModel
    {
        public string GetTextPart(int number)
        {
            return string.Join(Environment.NewLine, DataStorage.ObjectContainer.UnprocessedText[number]);
        }

        public void SetTextPart(string text,int number)
        {
           var list=new List<string>(text.Split(Environment.NewLine));
           SetTextPart(list, number);
        }
        public void SetTextPart(List<string>text,int number )
        {
            DataStorage.ObjectContainer.UnprocessedText[number] = text;
        }

    }
}
