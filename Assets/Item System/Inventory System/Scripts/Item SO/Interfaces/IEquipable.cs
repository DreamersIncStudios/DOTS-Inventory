﻿using UnityEngine;
using System.Collections.Generic;
using Stats;
namespace Dreamers.InventorySystem.Interfaces
{
    public interface IEquipable
    {
        Quality Quality {get;}
        uint LevelRqd { get; }
        GameObject Model { get; }
         bool EquipToHuman { get; }
        bool Equipped { get; }
        HumanBodyBones EquipBone { get; }
       List<StatModifier> Modifiers { get; } // consider adding a set for levelUp equippment?
        bool EquipItem(CharacterInventory characterInventory,  BaseCharacter player);
        bool Unequip(CharacterInventory characterInventory, BaseCharacter player);

    }


    public enum EquipmentType
    {
        Primary_Weapon,
        Secondary_Weapon,
        Blaster, // Rename Later
        Armor_Chest,
        Armor_Head,
        Armor_Legs,
        QuickUseItem1, //Special code here 
        QuickUseItem2,
        QuickUseItem3,

        Special,
    }
    public enum Quality 
    {
        Common, Uncommon, Rare, Vintage, Lengendary, Exotic
    }
    [System.Serializable]
    public struct StatModifier
    {
        public AttributeName Stat;
        public int BuffValue;

    }
}