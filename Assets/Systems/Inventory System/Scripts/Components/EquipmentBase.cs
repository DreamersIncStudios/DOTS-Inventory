using System.Collections.Generic;
using Stats;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem.Base { 

    [System.Serializable]
public class EquipmentBase
    {


        //public Dictionary<ArmorType, ArmorSO> EquippedArmor = new Dictionary<ArmorType, ArmorSO>();
        //public Dictionary<WeaponSlot, WeaponSO> EquippedWeapons = new Dictionary<WeaponSlot, WeaponSO>();


        public int CurrentActivationPoints;
        public int MaxActivationPoints;
        public List<ItemSlot> QuickAccessItems;
        public int NumOfQuickAccessSlots;
        public bool OpenSlots { get { return QuickAccessItems.Count < NumOfQuickAccessSlots; } }



        //public void LoadEquipment(BaseCharacter PC, EquipmentSave Save) {
        //    foreach (ArmorSO SO in Save.EquippedArmors) {
        //        SO.Equip(PC);
        //        EquippedArmor[SO.ArmorType] = SO;
        //    }
        //    foreach (WeaponSO SO in Save.EquippedWeapons)
        //    {
        //        if (SO)
        //        {
        //            SO.Equip(PC);
        //            EquippedWeapons[SO.Slot] = SO;
        //        }
        //    }
        //}

    }

}
