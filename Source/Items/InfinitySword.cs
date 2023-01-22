using WCSharp.Events;
using static War3Api.Common;
using static War3Api.Blizzard;
using static Source.Plugin.Kit;

namespace Source.Items {
    public class InfinitySword {
        public const string EffectString = @"Abilities\Spells\Orc\Devour\DevourEffectArt.mdl";
        public static void Init() {
            PlayerUnitEvents.Register(UnitTypeEvent.Damaging, Action);
        }

        public static void Action() {
            unit u = GetEventDamageSource();
            if(HasItem(u, Constants.ITEM_INFINITY_SWORD)) {
                unit target = BlzGetEventDamageTarget();
                float damage = GetEventDamage();
                UnitDamageTarget(u, target, damage * 0.5f, true, false, ATTACK_TYPE_HERO, BlzGetEventDamageType(), null);
                CreateTextTagUnitBJ((damage * 1.5f).ToString(), u, 1, 4, 255, 0, 0, 1);
            }
        }
    }
}
