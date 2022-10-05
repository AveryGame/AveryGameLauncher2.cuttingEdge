﻿/*
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using static AgsLauncherV2.Optimized.Services.Enums;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgsLauncherV2.Optimized
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            launcherStatus = LauncherStatus.initialized;
        }

        
        // All NavButton logic
        private void Home(object sender, RoutedEventArgs e)
        {
            Pages.Uncollapsed.Home home = new Pages.Uncollapsed.Home();
            pageHost.NavigationService.Navigate(home);
            pageHost.Visibility = Visibility.Visible;
        }
        
        private void Changelog(object sender, RoutedEventArgs e)
        {
            Pages.Uncollapsed.Changelog changelog = new Pages.Uncollapsed.Changelog();
            pageHost.NavigationService.Navigate(changelog);
            pageHost.Visibility = Visibility.Visible;
        }

        private void Bugs(object sender, RoutedEventArgs e)
        {
            Pages.Uncollapsed.Bugs bugs = new Pages.Uncollapsed.Bugs();
            pageHost.NavigationService.Navigate(bugs);
            pageHost.Visibility = Visibility.Visible;
        }

        private void News(object sender, RoutedEventArgs e)
        {
            Pages.Uncollapsed.News news = new Pages.Uncollapsed.News();
            pageHost.NavigationService.Navigate(news);
            pageHost.Visibility = Visibility.Visible;
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

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
