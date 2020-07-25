using UnityEngine;
namespace Dreamers.InventorySystem.Interfaces
{
    public interface IEquipable
    {
        EquipmentType Equipment { get; }
        GameObject Model { get; }

        
    }

    public enum EquipmentType
    {
        Armor, 
        Weapon,
        QuickUseItem,
        Drone,
    }
    public enum Quality 
    {
        Common, Uncommon, Rare, Vintage, Lengendary, Exotic
    }
}