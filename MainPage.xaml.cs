using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BaiTest2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string path = @"Assets\book1.txt";
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
                StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFile file = await folder.GetFileAsync(path);
                listLine = await FileIO.ReadLinesAsync(file);
                for (int i = 0; i < listLine.Count(); i++)
                {
                    string newLine = listLine[i].Replace(" ", "");
                    newLine = newLine.Replace("*", "");
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

        private Models.Verse GetVerse(string VerseTitle)
        {
            try
            {
                VerseTitle = CutString(VerseTitle);
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
                Models.Verse verse = GetVerse(tbox.Text);
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
            str = str.ToUpper();
            return str;
        }
    }
}
