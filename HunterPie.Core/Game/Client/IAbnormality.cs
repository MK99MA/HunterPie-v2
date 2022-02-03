﻿using System;

namespace HunterPie.Core.Game.Client
{
    public interface IAbnormality
    {
        public string Id { get; }
        public float Timer { get; }
        public float MaxTimer { get; }
        public bool IsInfinite { get; }
        public int Level { get; }

        public event EventHandler<IAbnormality> OnTimerUpdate;
    }
}