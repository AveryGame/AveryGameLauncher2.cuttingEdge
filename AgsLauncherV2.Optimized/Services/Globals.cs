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

global using System;
global using System.IO;
global using System.Net;
global using DiscordRPC;
global using System.Linq;
global using System.Windows;
global using Newtonsoft.Json;
global using System.Diagnostics;
global using DiscordRPC.Logging;
global using Newtonsoft.Json.Linq;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Forms;
global using System.IO.Compression;
global using System.Threading.Tasks;
global using System.Windows.Controls;
global using System.Collections.Generic;
global using System.Windows.Media.Imaging;
global using System.Runtime.InteropServices;
global using System.Windows.Media.Animation;
global using AgsLauncherV2.Optimized.Services;
global using Microsoft.Toolkit.Uwp.Notifications;
global using MessageBox = System.Windows.MessageBox;
global using Application = System.Windows.Application;
global using static AgsLauncherV2.Optimized.Services.Enums;
global using static AgsLauncherV2.Optimized.Services.Public;