﻿using HunterPie.Core.Client;
using HunterPie.Core.Game;
using HunterPie.Core.Game.Rise;
using HunterPie.UI.Architecture.Overlay;
using HunterPie.UI.Overlay;
using HunterPie.UI.Overlay.Widgets.Activities;
using System;

namespace HunterPie.Features.Overlay
{
    internal class ActivitiesWidgetInitializer : IWidgetInitializer
    {
        IContextHandler _handler;

        public void Load(Context context)
        {

            if (!ClientConfig.Config.Overlay.ActivitiesWidget.Initialize)
                return;

            _handler = context switch
            {
                MHRContext ctx => new RiseActivitiesWidgetContextHandler(ctx),
                _ => throw new NotImplementedException("unreachable")
            };
        }

        public void Unload()
        {
            _handler?.UnhookEvents();
        }
    }
}
