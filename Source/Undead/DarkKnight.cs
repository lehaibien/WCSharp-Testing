using static War3Api.Common;
using WCSharp.Events;
using static Constants;
using WCSharp.Shared.Extensions;
using WCSharp.Buffs;
using static Source.Plugin.Kit;
using System.Linq;
using System;

namespace Source.Undead
{
    public static class DarkKnight
    {
        public static void Init()
        {
            PlayerUnitEvents.Register(HeroTypeEvent.LearnsSpell, FrostAura.Init, UNIT_DARK_KNIGHT);
            PlayerUnitEvents.Register(SpellEvent.Effect, Resurrection.Init, ABILITY_RESURRECTION);
        }

        private class FrostAura {
            private static unit caster;
            public static bool isDisabled = false;
            public static void Init() {
                caster = GetTriggerUnit();
                PeriodicEvents.AddPeriodicEvent(Spell, 1);
            }

            public static bool Spell() {
                if(!isDisabled) {
                    var enemies = Plugin.Kit.GroupEnemiesInRangeOfLoc(GetUnitLoc(caster), 300, caster);
                    foreach (var enemy in enemies) {
                        BuffSystem.Add(new Buff.FrostAuraBuff(caster, enemy));
                    }
                }
                return true;
            }
        }

        private class Resurrection {
            private static unit caster;
            public static void Init() {
                caster = GetSpellAbilityUnit();
                Spell();
            }

            public static void Spell() {
                var corpses = GroupCorpsesInRangeOfLoc(GetUnitLoc(caster), 500);
                if(corpses.Count > 0) {
                    var chosenOne = corpses[new Random().Next(corpses.Count)];
                    while(IsUnitEnemy(chosenOne, GetOwningPlayer(caster))) {
                        corpses.Remove(chosenOne);
                        chosenOne = corpses[new Random().Next(corpses.Count)];
                    }
                    ReviveHeroLoc(chosenOne, GetUnitLoc(chosenOne), true);
                    DisplayTextToAllPlayer(0, 0, GetUnitName(chosenOne));
                }
                BlzUnitDisableAbility(caster, ABILITY_FROST_AURA, true, false);
                FrostAura.isDisabled = true;
                timer t = CreateTimer();
                TimerStart(t, 3, false, PauseAura);
            }

            public static void PauseAura() {
                BlzUnitDisableAbility(caster, ABILITY_FROST_AURA, false, false);
                FrostAura.isDisabled = false;
                DestroyTimer(GetExpiredTimer());
            }
        }
    }   
}
