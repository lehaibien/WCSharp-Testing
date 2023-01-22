using War3Api;
using WCSharp.Effects;
using WCSharp.Buffs;
using System.Collections.Generic;

namespace Source.Buff {
    public class LightningStrikeBuff : TickingBuff {
        private const string EffectModel = @"Abilities\\Weapons\\Bolt\\BoltImpact.mdl";
        private float damage;
        public LightningStrikeBuff(Common.unit caster, Common.unit target) : base(caster, target) {
            Interval = 1;
            Duration = 3;
            damage = Plugin.Kit.GetHeroIntelligence(caster) * 4;
        }

        public override void OnTick() {
            List<Common.unit> units = Plugin.Kit.GroupEnemiesInRangeOfLoc(Common.GetUnitLoc(Target), 300, Caster);
            foreach(var u in units) {
                Plugin.Kit.UnitDamagePure(Caster, u, damage);
                EffectSystem.Add(Common.AddSpecialEffectLoc(EffectModel, Common.GetUnitLoc(u)));
            }
        }
    }
}
