﻿using HunterPie.Core.Game.Enums;
using System.Linq;

namespace HunterPie.UI.Overlay.Widgets.Monster.ViewModels
{
    public class MockMonstersViewModel : MonstersViewModel
    {
        public MockMonstersViewModel()
        {
            Monsters.Add(new MockBossMonsterViewModel() 
            {
                Name = "Monster",
                Em = "Rise_32",
                MaxHealth = 35000,
                Health = 35000,
                Stamina = 10000,
                MaxStamina = 10000,
                Crown = Crown.Gold,
                TargetType = Target.Another,
                IsTarget = false,
                IsAlive = true
            });
            Monsters.Add(new MockBossMonsterViewModel()
            {
                Name = "Monster 2",
                Em = "Rise_32",
                MaxHealth = 35000,
                Health = 35000,
                Stamina = 10000,
                MaxStamina = 10000,
                Crown = Crown.Silver,
                TargetType = Target.Another,
                IsTarget = false,
                IsAlive = true
            });
            Monsters.Add(new MockBossMonsterViewModel()
            {
                Name = "Monster 3",
                Em = "Rise_32",
                MaxHealth = 35000,
                Health = 35000,
                Stamina = 10000,
                MaxStamina = 10000,
                Crown = Crown.Mini,
                TargetType = Target.Self,
                IsTarget = true,
                IsAlive = true
            });
            VisibleMonsters = 1;
            MonstersCount = 3;
            foreach (BossMonsterViewModel vm in Monsters)
                vm.FetchMonsterIcon();
        }
    }
}
