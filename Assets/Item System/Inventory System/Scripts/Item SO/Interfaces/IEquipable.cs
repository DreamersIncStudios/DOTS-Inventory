using UnityEngine;
using System.Collections.Generic;
using Stats;
namespace Dreamers.InventorySystem.Interfaces
{
    public interface IEquipable
    {
        Quality Quality {get;}
        uint LevelRqd { get; }
        EquipmentType Equipment { get; }
        GameObject Model { get; }
         bool EquipToHuman { get; }
        HumanBodyBones EquipBone { get; }
       List<StatModifier> Modifiers { get; } // consider adding a set for levelUp equippment?

        
    }

    public enum EquipmentType
    {
        Weapon,
        Armor,
        QuickUseItem1, //Special code here 
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