using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTest2.ViewsModel
{
    public class BOVerse
    {
        public List<Models.Verse> list { get; set; }
        public BOVerse()
        {
            list = new List<Models.Verse>();
        }
        public Models.Verse GetVerse(string VerseTitle)
        {
            try
            {
                VerseTitle = VerseTitle.CutString();
                VerseTitle = VerseTitle.ToUpper();
                foreach (var verse in list)
                {
                    string newName = verse.Name.CutString();
                    if (VerseTitle == newName)
                    {
                        return verse;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }


        }
    }
}
