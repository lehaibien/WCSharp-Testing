using War3Api;

namespace Source.Buff
{
    class ConstantDamageBuff : WCSharp.Buffs.AutoBuff {
        public string EffectModel { get; set; }
        public ConstantDamageBuff(Common.unit caster, Common.unit target, float damage, float duration, string effectString) : base(caster, target)
        {
            Interval = 1f;
            Duration = duration;
            DamagePerInterval = damage;
            EffectModel = effectString;
            AttackType = Common.ATTACK_TYPE_CHAOS;
            DamageType = Common.DAMAGE_TYPE_UNIVERSAL;
        }

        public override void OnTick()
        {
            WCSharp.Effects.EffectSystem.Add(Common.AddSpecialEffectLoc(EffectModel, Common.GetUnitLoc(Target)));
        }
    }
}
