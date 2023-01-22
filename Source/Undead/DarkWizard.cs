using static War3Api.Common;
using static War3Api.Blizzard;
using static Constants;
using static Source.Plugin.Kit;
using WCSharp.Shared.Extensions;
using WCSharp.Buffs;
using WCSharp.Effects;
using Source.Buff;

namespace Source.Undead {
    public static class DarkWizard {
        public static void Init() {
            RegisterSpellEffectEvent(Teleport.Init, ABILITY_TELEPORT);
            RegisterSpellEffectEvent(LightningStrike.Init, ABILITY_LIGHTNING_STRIKE);
            RegisterSpellEffectEvent(LightningPole.Init, ABILITY_LIGHTNING_POLE);
        }

        private class LightningPole {
            private const string EffectString = @"Abilities\Spells\Undead\AnimateDead\AnimateDeadTarget.mdl";
            const float DAMAGE_MULTIPLIER = 5f;
            const float DAMAGE_AREA = 150;
            private static unit caster;
            public static void Init() {
                caster = GetSpellAbilityUnit();
                Spell();
            }

            public static void Spell() {
                location unitLoc = GetUnitLoc(caster);
                location spellLoc = GetSpellTargetLoc();
                for (int i = 1; i <= 4; i++) {
                    location loc = PolarProjectionBJ(unitLoc, 200 * i, AngleBetweenPoints(unitLoc, spellLoc));
                    EffectSystem.Add(AddSpecialEffectLoc(EffectString, loc));
                    var enemies = GroupEnemiesInRangeOfLoc(loc, DAMAGE_AREA, caster);
                    foreach(var enemy in enemies) {
                        UnitDamagePure(caster, enemy, GetHeroIntelligence(caster) * DAMAGE_MULTIPLIER);
                    }
                }
                caster = null;
            }
        }

        private class Teleport {
            const float SPELL_DAMAGE_MULTIPLIER = 2;
            const float SPELL_DAMAGE_RADIUS = 250;
            static unit caster;
            public static void Init() {
                caster = GetSpellAbilityUnit();
                Spell();
            }

            private static void Spell() {
                AddUnitHP(caster, 200);
                var enemies = GroupEnemiesInRangeOfLoc(GetSpellTargetLoc(), SPELL_DAMAGE_RADIUS, caster);
                foreach (unit enemy in enemies) {
                    float damage = GetHeroIntelligence(caster) * SPELL_DAMAGE_MULTIPLIER;
                    UnitDamagePure(caster, enemy, damage);
                }
                caster = null;
            }
        }

        private class LightningStrike {
            public static void Init() {
                unit caster = GetSpellAbilityUnit();
                unit target = GetSpellTargetUnit();
                BuffSystem.Add(new LightningStrikeBuff(caster, target));
                caster = null;
                target = null;
            }
        }
    }
}
