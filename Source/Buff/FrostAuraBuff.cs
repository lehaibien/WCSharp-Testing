using static Source.Plugin.Kit;
using WCSharp.Buffs;

namespace Source.Buff {
    public class FrostAuraBuff : PassiveBuff {
        public FrostAuraBuff(War3Api.Common.unit caster, War3Api.Common.unit target) : base(caster, target) => Duration = 1f;

        public override void OnApply() {
            float damage = GetHeroStrength(Caster) * 3;
            UnitDamagePure(Caster, Target, damage);
            UnitDamagePure(Caster, Caster, damage * 0.05f * -1);
        }
    }
}
