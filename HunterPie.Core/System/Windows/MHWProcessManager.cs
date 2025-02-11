﻿using HunterPie.Core.Address.Map;
using HunterPie.Core.Client;
using HunterPie.Core.Logger;
using System.Diagnostics;
using System.IO;

namespace HunterPie.Core.System.Windows
{
    internal class MHWProcessManager : WindowsProcessManager
    {
        public override string Name => "MonsterHunterWorld"; 

        protected override bool ShouldOpenProcess(Process process)
        {
            // If our process is in either another window, or not initialized yet
            if (!process.MainWindowTitle.ToUpperInvariant().StartsWith("MONSTER HUNTER: WORLD"))
                return false;

            string version = process.MainWindowTitle.Split('(')[1].Trim(')');
            bool parsed = int.TryParse(version, out int _);

            if (!parsed)
            {
                Log.Error("Failed to get Monster Hunter: World build version. Loading latest map version instead.");
                AddressMap.ParseLatest(ClientInfo.AddressPath);
            } else
                AddressMap.Parse(Path.Combine(ClientInfo.AddressPath, $"MonsterHunterWorld.{version}.map"));


            if (!AddressMap.IsLoaded)
            {
                Log.Error("Failed to parse map file");
                return false;
            }

            return true;
        }
    }
}
