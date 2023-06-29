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

namespace AgsLauncherV2.Optimized.Services
{
    internal class Enums
    {
        public enum LauncherStatusEnum
        {
            Intializing,
            Idle,
            Downloading,
            GameLaunched,
            Updating,
            WaitingRpc
        }
        public static LauncherStatusEnum LauncherStatus;
        public enum LogTypeEnum
        { 
            Warn,
            Info,
            Error,
            Debug
        }
        public static LogTypeEnum LogType;
        public static string LogAsString(LogTypeEnum lt)
        {
            return lt switch
            {
                LogTypeEnum.Warn => "![WARN]!: ",
                LogTypeEnum.Info => "[INFO]: ",
                LogTypeEnum.Error => "!![ERROR]!!: ",
                LogTypeEnum.Debug => "[DEBUG]: ",
                _ => "[INFO]: ",
            };
        }
    }
}
