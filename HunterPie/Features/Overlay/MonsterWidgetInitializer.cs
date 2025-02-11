﻿using HunterPie.Core.Client;
using HunterPie.Core.Game;
using HunterPie.UI.Architecture.Overlay;
using HunterPie.UI.Overlay;
using HunterPie.UI.Overlay.Widgets.Monster;

namespace HunterPie.Features.Overlay
{
    internal class MonsterWidgetInitializer : IWidgetInitializer
    {
        IContextHandler _handler;

        public void Load(Context context)
        {
            if (!ClientConfig.Config.Overlay.BossesWidget.Initialize)
                return;

            _handler = new MonsterWidgetContextHandler(context);
        }

        public void Unload()
        {
            _handler?.UnhookEvents();
        }
    }
}
