using System.Collections.Generic;
using Stats.Entities;
using Dreamers.InventorySystem.Interfaces;
using Sirenix.Serialization;
using System;

namespace Dreamers.InventorySystem.Base { 

    [System.Serializable]
public class EquipmentBase
    {
        [NonSerialized, OdinSerialize]
        public Dictionary<ArmorType, ArmorSO> EquippedArmor = new Dictionary<ArmorType, ArmorSO>();
        [NonSerialized, OdinSerialize]
        public Dictionary<WeaponSlot, WeaponSO> EquippedWeapons = new Dictionary<WeaponSlot, WeaponSO>();


        public int CurrentActivationPoints;
        public int MaxActivationPoints;
        public List<ItemSlot> QuickAccessItems;
        public int NumOfQuickAccessSlots;
        public bool OpenSlots { get { return QuickAccessItems.Count < NumOfQuickAccessSlots; } }

        public void Init() { 
            QuickAccessItems= new List<ItemSlot>();
            NumOfQuickAccessSlots= 2;
        }
        public void Init(EquipmentBase save, BaseCharacterComponent player) { 
            EquippedArmor = save.EquippedArmor;
            EquippedWeapons= save.EquippedWeapons; 
            CurrentActivationPoints= save.CurrentActivationPoints; 
            MaxActivationPoints= save.MaxActivationPoints;
            QuickAccessItems= new List<ItemSlot>();
            NumOfQuickAccessSlots=  save.NumOfQuickAccessSlots;
            reloadEquipment(player);
        }

        void reloadEquipment(BaseCharacterComponent player) {
            foreach (ArmorSO so in EquippedArmor.Values) {
                so.Equip(player);
            }
            foreach (WeaponSO so in EquippedWeapons.Values)
            {
                so.Equip(player);
            }
        }

        public void LoadEquipment(BaseCharacterComponent PC, EquipmentSave Save)
        {
            foreach (ArmorSO SO in Save.EquippedArmors)
            {
                SO.Equip(PC);
                EquippedArmor[SO.ArmorType] = SO;
            }
            foreach (WeaponSO SO in Save.EquippedWeapons)
            {
                if (SO)
                {
                    SO.Equip(PC);
                    EquippedWeapons[SO.Slot] = SO;
                }
            }
        }

    }
    [System.Serializable]
    public class EquipmentSave
    {
        public List<WeaponSO> EquippedWeapons;
        public List<ArmorSO> EquippedArmors;
    }

}
