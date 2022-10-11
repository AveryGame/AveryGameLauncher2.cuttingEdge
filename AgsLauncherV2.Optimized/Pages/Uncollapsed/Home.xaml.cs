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
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using static AgsLauncherV2.Optimized.Services.Enums;
using static AgsLauncherV2.Optimized.Services.Public;

namespace AgsLauncherV2.Optimized.Pages.Uncollapsed
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            //var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get().Cast<ManagementObject>()
                        //select x.GetPropertyValue("Caption")).FirstOrDefault();
            //MessageBox.Show("OSVersion is " + name.ToString() + "! Unmounting drive C:\\", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);


        }


        // All NavButton logic
        private void Changelog(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedChangelog);
        }
        
        private void Bugs(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedBugs);
        }

        private void News(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedNews);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(uncollapsedSettings);
        }
        // End NavButton logic
        

        //Unique page logic
        private void LaunchButtonLogic(object sender, RoutedEventArgs e)
        {
            
        }
        //End unique page logic
    }
}
