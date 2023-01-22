using static War3Api.Common;
using static Source.Plugin.Kit;
using WCSharp.Events;
using WCSharp.Effects;

namespace Source.Items {
    public class InfinityShield {
        public const string EffectString = @"Abilities\Spells\Human\Defend\DefendCaster.mdl";
        public static void Init() {
            PlayerUnitEvents.Register(UnitTypeEvent.Damaging, Action);
        }

        public static void Action() {
            unit target = BlzGetEventDamageTarget();
            if (HasItem(target, Constants.ITEM_INFINITY_SHIELD)) {
                if(GetChance(20)) {
                    EffectSystem.Add(AddSpecialEffectTarget(EffectString, target, "chest"));
                    BlockDamage(target, GetEventDamage());
                }
            }
        }
    }
}
