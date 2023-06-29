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

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for News.xaml
    /// </summary>
    public partial class News
    {
        public News()
        {
            InitializeComponent();
            LoadPageSpecificJson();
        }


        //Unique page logic
        private void LoadPageSpecificJson()
        {
            Logger.Log(LogTypeEnum.Info, "Loading page-specific JSON for news page");
            BitmapImage bmp = new();
            bmp.BeginInit();
            bmp.UriSource = new Uri(Json.NewsImageUrl);
            bmp.EndInit();
            Logger.Log(LogTypeEnum.Info, "Sending web request to news image url");
            var req = (HttpWebRequest)WebRequest.Create(Json.NewsImageUrl);
            req.Method = "HEAD";
            using (var resp = req.GetResponse())
            {
                if (!resp.ContentType.ToLower().StartsWith("image/"))
                {
                    Logger.Log(LogTypeEnum.Warn, "New image url response did not return an image");
                    NewsImageBorder.Opacity = 0;
                }
                else if (resp.ContentType.ToLower().StartsWith("image/"))
                {
                    Logger.Log(LogTypeEnum.Info, "News image url response returned an image");
                    CuttingEdgeLoad.Opacity = 0;
                    bmp.UriSource = new Uri(Json.NewsImageUrl);
                }
            }
            Logger.Log(LogTypeEnum.Info, "Setting NewsImageBrush to the BitmapImage, setting NewsHeader.Content to the news header, setting NewsText.Text to the news text, setting NewsDate.Text to the news date, pulled data from locally deserialized JSON");
            NewsImageBrush.ImageSource = bmp;
            NewsHeader.Content = Json.NewsHeader;
            NewsSubheader.Text = Json.NewsSubheader;
            NewsDate.Text = Json.NewsDate;
            Logger.Log(LogTypeEnum.Info, "Completed LoadPageSpecificJson()");
        }
        //End unique page logic
    }
}
