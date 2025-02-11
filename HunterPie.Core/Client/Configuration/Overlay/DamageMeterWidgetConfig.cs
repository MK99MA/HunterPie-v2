﻿using HunterPie.Core.Architecture;
using HunterPie.Core.Settings;
using HunterPie.Core.Settings.Types;

namespace HunterPie.Core.Client.Configuration.Overlay
{
    [SettingsGroup("METER_WIDGET", "ICON_STATISTICS", "unavailable")]
    public class DamageMeterWidgetConfig : IWidgetSettings, ISettings
    {
        [SettingField("MOCK", "MOCK")]
        public Observable<bool> Initialize { get; set; } = true;
        
        [SettingField("MOCK", "MOCK")]
        public Observable<bool> Enabled { get; set; } = true;

        [SettingField("HIDE_WHEN_UI_VISIBLE_STRING")]
        public Observable<bool> HideWhenUiOpen { get; set; } = false;

        [SettingField("MOCK", "MOCK")]
        public Position Position { get; set; } = new(0, 0);

        [SettingField("A", "B")]
        public Range Opacity { get; set; } = new(1, 1, 0, 0.1);

        [SettingField("A", "B")]
        public Range Scale { get; set; } = new(1, 2, 0, 0.1);

        [SettingField("MOCK", "MOCK")]
        public Observable<bool> StreamerMode { get; set; } = false;

        [SettingField("ENABLE_DAMAGE_METER_SHOULD_HIGHLIGHT_MYSELF", "ENABLE_DAMAGE_METER_SHOULD_HIGHLIGHT_MYSELF_DESC")]
        public Observable<bool> ShouldHighlightMyself { get; set; } = false;

        [SettingField("ENABLE_DAMAGE_METER_SHOULD_BLUR_NAMES", "ENABLE_DAMAGE_METER_SHOULD_BLUR_NAMES_DESC")]
        public Observable<bool> ShouldBlurNames { get; set; } = false;
    }
}
