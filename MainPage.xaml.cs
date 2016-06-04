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
                    string newLine = listLine[i].CutString();
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


        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (tbox.Text != string.Empty)
            {
                List<string> VerseDetail = new List<string>();
                Models.Verse verse = BOverse.GetVerse(tbox.Text);
                if (verse != null)
                {
                    VerseDetail.Add(verse.Name);
                    for (int i = verse.Begin; i < verse.End; i++)
                    {
                        VerseDetail.Add(listLine[i]);
                    }
                }
                else
                {
                    VerseDetail.Add("Không tìm thấy bài thơ này.");
                }
                lb.ItemsSource = VerseDetail;

            }
        }

    }
}
