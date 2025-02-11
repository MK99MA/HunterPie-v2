﻿using HunterPie.Core.Client.Configuration.Overlay;
using HunterPie.Core.Game;
using HunterPie.Core.Game.Client;
using HunterPie.Core.Game.Enums;
using HunterPie.UI.Assets.Application;
using HunterPie.UI.Overlay.Enums;
using HunterPie.UI.Overlay.Widgets.Chat.ViewModels;
using HunterPie.UI.Overlay.Widgets.Chat.Views;
using System.Windows.Media;

namespace HunterPie.UI.Overlay.Widgets.Chat
{
    public class ChatWidgetContextHandler : IContextHandler
    {
        private SolidColorBrush[] playerColors =
        {
            new SolidColorBrush(Color.FromArgb(0xFF, 0xED, 0x64, 0x91)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0xB6, 0xED)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0xED, 0xAD, 0x64)),
            new SolidColorBrush(Color.FromArgb(0xFF, 0x64, 0xED, 0x99))
        };
        
        private readonly ChatView View;
        private readonly ChatViewModel ViewModel;
        private readonly Context Context;
        public ChatCategoryViewModel General { get; } = new() 
        { 
            Name = "General", 
            Description = "General chat", 
            Icon = Resources.Icon<ImageSource>("ICON_STAR")
        };

        public ChatWidgetContextHandler(Context context)
        {
            Context = context;
            View = new ChatView();
            ViewModel = View.ViewModel;

            WidgetManager.Register<ChatView, ChatWidgetConfig>(View);

            UpdateData();
            HookEvents();
        }
        private void UpdateData()
        {
            ViewModel.IsChatOpen = Context.Game.Chat.IsChatOpen;
            View.Type = ViewModel.IsChatOpen
                ? WidgetType.Window
                : WidgetType.ClickThrough;

            ViewModel.Categories.Add(General);

            foreach (IChatMessage message in Context.Game.Chat.Messages)
            {
                View.Dispatcher.Invoke(() =>
                {
                    if (message.Type != AuthorType.Player)
                        return;

                    General.Elements.Add(new ChatElementViewModel()
                    {
                        Author = message.Author,
                        Text = message.Message,
                        Color = playerColors[message.PlayerSlot % playerColors.Length]
                    });
                });
            }
        }

        public void HookEvents()
        {
            Context.Game.Chat.OnNewChatMessage += OnNewChatMessage;
            Context.Game.Chat.OnChatOpen += OnChatOpen;
        }

        private void OnChatOpen(object sender, IChat e)
        {
            ViewModel.IsChatOpen = e.IsChatOpen;
            View.Type = e.IsChatOpen
                ? WidgetType.Window
                : WidgetType.ClickThrough;
        }

        public void UnhookEvents()
        {
            Context.Game.Chat.OnNewChatMessage -= OnNewChatMessage;
            Context.Game.Chat.OnChatOpen -= OnChatOpen;

            WidgetManager.Unregister<ChatView, ChatWidgetConfig>(View);
        }
        private void OnNewChatMessage(object sender, IChatMessage e)
        {
            View.Dispatcher.Invoke(() =>
            {
                if (e.Type != AuthorType.Player)
                    return;

                General.Elements.Add(new ChatElementViewModel()
                {
                    Author = e.Author,
                    Text = e.Message,
                    Color = playerColors[e.PlayerSlot % playerColors.Length]
                });
            });
        }
    }
}
