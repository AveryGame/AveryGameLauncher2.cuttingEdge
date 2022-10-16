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
using System.Net;
using System.Windows;
using System.Windows.Input;
using static AgsLauncherV2.Optimized.Services.Enums;
using static AgsLauncherV2.Optimized.Services.Public;

namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\AveryGame Launcher\\CuttingEdge\\";
        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show(localAppData + "clientStrings.json");
            Services.BasicLogic.CheckAppData();
            launcherStatus = LauncherStatus.initialized;
        }

        
        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(uncollapsedHome);
            pageHost.Visibility = Visibility.Visible;
        }
        
        private void Changelog(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(uncollapsedChangelog);
            pageHost.Visibility = Visibility.Visible;
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(uncollapsedBugs);
            pageHost.Visibility = Visibility.Visible;
        }

        private void News(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(uncollapsedNews);
            pageHost.Visibility = Visibility.Visible;
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            pageHost.NavigationService.Navigate(uncollapsedSettings);
            pageHost.Visibility = Visibility.Visible;
        }
        // End NavButton logic

        // Basic window logic
        private void Close(object sender, RoutedEventArgs e)
        {
            Services.BasicLogic.HandleClose(launcherStatus, false, true, 1);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        
        private void DragBar(object sender, MouseButtonEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        // End basic window logic
    }
}
