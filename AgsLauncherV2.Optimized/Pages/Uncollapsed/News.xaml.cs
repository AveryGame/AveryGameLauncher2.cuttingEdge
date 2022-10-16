/*
 * AveryGame Launcher
 *  Copyright (C) 2022, Avery Fiebig-Dorey & Tristan Gee
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AgsLauncherV2.Optimized.Services.Public;
using System.Net;

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for News.xaml
    /// </summary>
    public partial class News : Page
    {
        public News()
        {
            InitializeComponent();
            for (int i = 0; i < 1243134123; i++)
            {
                //if ((bool)(mainWindow.pageHost.Content = this))
                //{
                /*
                    MessageBox.Show("initialized");
                    string DATA = File.ReadAllText(appData + "clientSettings.json");
                    Services.AGCloud json = JsonConvert.DeserializeObject<Services.AGCloud>(DATA);
                    NewsHeader.Text = i.ToString();
                    NewsSubheader.Text = json.newsSubHeader;
                    NewsDate.Text = json.newsDate;
                    BitmapImage btpImg = new BitmapImage();
                    btpImg.BeginInit();
                    /*try
                    {
                        var req = (HttpWebRequest)WebRequest.Create(json.newsImageUrl);
                        req.Method = "HEAD";
                        using (var resp = req.GetResponse())
                        {
                            if (!resp.ContentType.ToLower().StartsWith("image/"))
                            {

                            }
                            else if (resp.ContentType.ToLower().StartsWith("image/"))
                            {
                                btpImg.UriSource = new Uri(json.newsImageUrl);
                            }
                        }
                    }
                    catch
                    {

                    }
                    btpImg.UriSource = new Uri(json.newsImageUrl);
                    imageBrush.ImageSource = btpImg;
                    break;
                //}
                */
            }
        }


        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedHome);
        }

        private void Changelog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedChangelog);
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedBugs);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedSettings);
        }
        // End NavButton logic


        //Unique page logic
        
        //End unique page logic
    }
}
