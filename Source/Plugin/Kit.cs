using System;
using WCSharp.Events;
using static War3Api.Common;
using static War3Api.Blizzard;
using System.Collections.Generic;
using WCSharp.Shared.Extensions;
using System.Linq;

namespace Source.Plugin {
    public class Kit {
        //Events

        public static void RegisterSpellEffectEvent(Action action, int spell) => PlayerUnitEvents.Register(SpellEvent.Effect, action, spell);

        //Groups

        public static group GroupUnitsTypeInRect(rect r, int id) {
            group g = CreateGroup();
            GroupEnumUnitsInRect(g, r, Condition(() => GetUnitTypeId(GetFilterUnit()) == id));
            return g;
        }

        public static List<unit> GroupAlliesInRangeOfLoc(location whichLocation, float range, unit whichUnit) {
            group g = CreateGroup();
            GroupEnumUnitsInRangeOfLoc(g, whichLocation, range, Condition(() => IsUnitAlly(GetFilterUnit(), GetOwningPlayer(whichUnit)) && IsUnitAliveBJ(GetFilterUnit())));
            return g.Enumerate().ToList();
        }

        public static List<unit> GroupEnemiesInRangeOfLoc(location whichLocation, float range, unit whichUnit) {
            group g = CreateGroup();
            GroupEnumUnitsInRangeOfLoc(g, whichLocation, range, Condition(() => IsUnitEnemy(GetFilterUnit(), GetOwningPlayer(whichUnit)) && IsUnitAliveBJ(GetFilterUnit())));
            return g.Enumerate().ToList();
        }

        public static List<unit> GroupCorpsesInRangeOfLoc(location whichLocation, float range) {
            group g = CreateGroup();
            GroupEnumUnitsInRangeOfLoc(g, whichLocation, range, Condition(() => IsUnitDeadBJ(GetFilterUnit())));
            return g.Enumerate().ToList();
        }

        //Units

        public static bool UnitDamagePhysical(unit whichUnit, unit target, float damage) => UnitDamageTarget(whichUnit, target, damage, true, false, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_NORMAL, null);

        public static bool UnitDamageMagical(unit whichUnit, unit target, float damage) => UnitDamageTarget(whichUnit, target, damage, true, false, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_MAGIC, null);

        public static bool UnitDamagePure(unit whichUnit, unit target, float damage) => UnitDamageTarget(whichUnit, target, damage, true, false, ATTACK_TYPE_NORMAL, DAMAGE_TYPE_UNIVERSAL, null);

        public static float GetUnitHP(unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_LIFE);

        public static float GetUnitMana(unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_MANA);

        public static float GetUnitHPPercent(unit whichUnit) => GetUnitHP(whichUnit) / GetUnitMaxHP(whichUnit) * 100;

        public static float GetUnitManaPercent(unit whichUnit) => GetUnitMana(whichUnit) / GetUnitMaxMana(whichUnit) * 100;

        public static float GetUnitMaxHP(unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_MAX_LIFE);

        public static float GetUnitMaxMana(unit whichUnit) => GetUnitState(whichUnit, UNIT_STATE_MAX_MANA);

        public static void SetUnitMaxHP(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MAX_LIFE, amount);
        public static void SetUnitMaxMana(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MAX_MANA, amount);
        public static void AddUnitMaxHP(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MAX_LIFE, GetUnitMaxHP(whichUnit) + amount);
        public static void AddUnitMaxMana(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MAX_LIFE, GetUnitMaxMana(whichUnit) + amount);

        public static void AddUnitHP(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_LIFE, GetUnitHP(whichUnit) + amount);

        public static void AddUnitMana(unit whichUnit, float amount) => SetUnitState(whichUnit, UNIT_STATE_MANA, GetUnitMana(whichUnit) + amount);

        public static void AddUnitHPPercent(unit whichUnit, float percent) => SetUnitState(whichUnit, UNIT_STATE_LIFE, GetUnitHP(whichUnit) + (GetUnitMaxHP(whichUnit) * (percent * 0.01f)));

        public static void AddUnitManaPercent(unit whichUnit, float percent) => SetUnitState(whichUnit, UNIT_STATE_LIFE, GetUnitMana(whichUnit) + (GetUnitMaxMana(whichUnit) * (percent * 0.01f)));

        public static void AddMovementSpeed(unit whichUnit, float amount) => SetUnitMoveSpeed(whichUnit, GetUnitMoveSpeed(whichUnit) + amount);

        public static void AddMovementSpeedPercent(unit whichUnit, float percent) => SetUnitMoveSpeed(whichUnit, GetUnitMoveSpeed(whichUnit) + (GetUnitMoveSpeed(whichUnit) * (percent * 0.01f)));

        public static void ResetMovementSpeed(unit whichUnit) => SetUnitMoveSpeed(whichUnit, GetUnitDefaultMoveSpeed(whichUnit));

        public static bool IsUnitSpellImmune(unit whichUnit) => IsUnitType(whichUnit, UNIT_TYPE_MAGIC_IMMUNE);

        public static float GetUnitMinDamage(unit whichUnit) {
            // min damage = base damage + dice;
            return BlzGetUnitBaseDamage(whichUnit, 0) + BlzGetUnitDiceNumber(whichUnit, 0);
        }

        public static float GetUnitMaxDamage(unit whichUnit) {
            // max damage = base + (dice * sides);
            return BlzGetUnitBaseDamage(whichUnit, 0) + (BlzGetUnitDiceNumber(whichUnit, 0) * BlzGetUnitDiceSides(whichUnit, 0));
        }

        public static void BlockDamage(unit whichUnit, float amount) {
            float damage = GetEventDamage();
            if (damage <= 0)
                return;
            if (amount >= damage) {
                AddUnitHP(whichUnit, damage);
            } else {
                AddUnitHP(whichUnit, amount);
            }
            if (GetWidgetLife(whichUnit) < damage) {
                UnitAddAbility(whichUnit, 0x64707276);
                AddUnitHP(whichUnit, damage);
            }
        }

        // Hero

        public static int GetHeroStrength(unit whichHero) => BlzGetUnitIntegerField(whichHero, UNIT_IF_STRENGTH);

        public static int GetHeroAgility(unit whichHero) => BlzGetUnitIntegerField(whichHero, UNIT_IF_AGILITY);

        public static int GetHeroIntelligence(unit whichHero) => BlzGetUnitIntegerField(whichHero, UNIT_IF_INTELLIGENCE);

        // Items

        public static bool IsInventoryFull(unit whichUnit) {
            int invSize = UnitInventorySize(whichUnit);
            for (int i = 0; i < invSize; i++) {
                if (UnitItemInSlot(whichUnit, i) == null)
                    return false;
            }
            return true;
        }

        public static item GetItem(unit whichUnit, int itemId) {
            int invSize = UnitInventorySize(whichUnit);
            if (invSize < 0)
                return null;
            for (int i = 0; i < invSize; i++) {
                item currentItem = UnitItemInSlot(whichUnit, i);
                if (currentItem != null && GetItemTypeId(currentItem) == itemId)
                    return currentItem;
            }
            return null;
        }

        public static bool HasItem(unit whichUnit, int itemId) => UnitHasItem(whichUnit, GetItem(whichUnit, itemId));

        // Calculations

        public static bool GetChance(float chance) => GetRandomReal(0, 100) <= chance;

        public static float GetDistanceBetweenTwoLocation(location loc1, location loc2) {
            float deltaX = GetLocationX(loc1) - GetLocationX(loc2);
            float deltaY = GetLocationY(loc1) - GetLocationY(loc2);
            float distance = (float) Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            return distance;
        } 

        // Player

        public static void DisplayTextToAllPlayer(float x, float y, string message) {
            for (int i = 0; i < GetPlayers(); i++) {
                DisplayTextToPlayer(Player(i), x, y, message);
            }
        }

        // Misc
    }
}
