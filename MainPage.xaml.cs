using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace BaiTest2
{
    
    public sealed partial class MainPage : Page
    {
        IList<string> listLine;
        ViewsModel.BOVerse BOverse = new ViewsModel.BOVerse();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = @"Assets\book1.txt";
                StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFile file = await folder.GetFileAsync(path);
                listLine = await FileIO.ReadLinesAsync(file);
                for (int i = 0; i < listLine.Count(); i++)
                {
                    string newLine = CutString(listLine[i]);
                    if (newLine == String.Empty)
                    {
                        continue;
                    }
                    else
                    {
                        //Check co phai title hay ko
                        if (IsLineTitle(newLine))
                        {
                            //tao doi tuong verse
                            if (BOverse.list.Count == 0)
                            {
                                BOverse.list.Add(new Models.Verse()
                                {
                                    Name = listLine[i],
                                    Begin = i+1,

                                });
                            }
                            else
                            {
                                BOverse.list.Add(new Models.Verse()
                                {
                                    Name = listLine[i],
                                    Begin = i+1,

                                });
                                BOverse.list[BOverse.list.Count - 2].End = i;
                            }
                        }
                    }
                }
                // them end line cho doi tuong cuoi cung.
                BOverse.list[BOverse.list.Count - 1].End = listLine.Count - 1;
            }
            catch
            {

            }
        }

        private bool IsLineTitle(string line)
        {
            try
            {
                foreach (var c in line)
                {
                    if (Char.IsLower(c) || Char.IsNumber(c))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private Models.Verse GetVerseDetail(string VerseTitle)
        {
            try
            {
                VerseTitle = CutString(VerseTitle);
                VerseTitle = VerseTitle.ToUpper();
                foreach (var verse in BOverse.list)
                {
                    string newName = CutString(verse.Name);
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

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (tbox.Text != string.Empty)
            {
                List<string> fullVerse = new List<string>();
                Models.Verse verse = GetVerseDetail(tbox.Text);
                if (verse != null)
                {
                    fullVerse.Add(verse.Name);
                    for (int i = verse.Begin; i < verse.End; i++)
                    {
                        fullVerse.Add(listLine[i]);
                    }
                }
                else
                {
                    fullVerse.Add("Không tìm thấy bài thơ này.");
                }
                lb.ItemsSource = fullVerse;

            }
        }

        private string CutString(string str)
        {
            str = str.NonUnicode();
            str = str.Replace(" ", "");
            str = str.Replace("-", "");
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("*", "");
            return str;
        }
    }
}
