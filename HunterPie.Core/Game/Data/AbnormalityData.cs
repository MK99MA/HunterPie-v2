﻿using HunterPie.Core.Game.Data.Schemas;
using HunterPie.Core.Logger;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace HunterPie.Core.Game.Data
{
    public class AbnormalityData
    {
        public const string SongPrefix = "Songs_";
        public const string ConsumablePrefix = "Consumables_";
        public const string DebuffPrefix = "Debuffs_";
        public const float TIMER_MULTIPLIER = 60.0f;

        private static XmlDocument _abnormalityData;
        public static Dictionary<string, AbnormalitySchema> Abnormalities { get; private set; }

        internal static void Init(string path)
        {
            _abnormalityData = new();
            _abnormalityData.Load(path);

            CreateFixedSizeDictionary();
            LoadAbnormalities();
        }

        private static void CreateFixedSizeDictionary()
        {
            int abnormalitiesCount = _abnormalityData.SelectNodes("//Abnormalities/*/Abnormality").Count;

            Abnormalities = new(abnormalitiesCount);
        }

        private static void LoadAbnormalities()
        {
            XmlNodeList abnormalities = _abnormalityData.SelectNodes("//Abnormalities/*/Abnormality");

            foreach (XmlNode abnormality in abnormalities)
            {
                string id = abnormality.Attributes["Id"].Value;
                string name = abnormality.Attributes["Name"]?.Value ?? "ABNORMALITY_UNKNOWN";
                string icon = abnormality.Attributes["Icon"]?.Value ?? "ICON_MISSING";
                string offset = abnormality.Attributes["Offset"]?.Value ?? "0";
                string dependsOn = abnormality.Attributes["DependsOn"]?.Value ?? "0";
                string withValue = abnormality.Attributes["WithValue"]?.Value ?? "0";
                string finalId = $"{abnormality.ParentNode.Name}_{id}";

                AbnormalitySchema schema = new()
                {
                    Id = finalId,
                    Name = name,
                    Icon = icon
                };

                int.TryParse(offset, NumberStyles.HexNumber, null, out schema.Offset);
                int.TryParse(dependsOn, NumberStyles.HexNumber, null, out schema.DependsOn);
                int.TryParse(withValue, out schema.WithValue);

                Abnormalities.Add(schema.Id, schema);
            }
        }

        public static AbnormalitySchema? GetSongAbnormalityData(int id) => GetAbnormalityData($"{SongPrefix}{id}");
        
        public static AbnormalitySchema? GetConsumableAbnormalityData(int id) => GetAbnormalityData($"{ConsumablePrefix}{id:X}");

        public static AbnormalitySchema? GetDebuffAbnormalityData(int id, int subId = int.MinValue)
        {

            string stringId = subId switch
            {
                int.MinValue => $"{DebuffPrefix}{id}",
                _ => $"{DebuffPrefix}{id}-{subId}"
            };

            return GetAbnormalityData(stringId);
        }

        private static AbnormalitySchema? GetAbnormalityData(string id)
        {
            if (!Abnormalities.ContainsKey(id))
                return null;

            AbnormalitySchema schema = Abnormalities[id];

            if (schema.Icon == "ICON_MISSING")
                Log.Info($"Missing abnormality {id}");

            return schema;
        }

        public static AbnormalitySchema[] GetAllAbnormalitiesFromCategory(string category)
        {
            AbnormalitySchema[] abnormalities = Abnormalities.Where(e => e.Key.StartsWith(category))
                                                                .Select(el => el.Value)
                                                                .ToArray();

            return abnormalities;
        }
    }
}