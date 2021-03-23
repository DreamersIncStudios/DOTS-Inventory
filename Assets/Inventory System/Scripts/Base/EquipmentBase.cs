using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem.Base { 

    [System.Serializable]
public class EquipmentBase
    {


        public Dictionary<EquipmentType, ItemBaseSO> equippedItem = new Dictionary<EquipmentType, ItemBaseSO>();

        public int CurrentActivationPoints;
        public int MaxActivationPoints;
        public List<ItemSlot> QuickAccessItems;
        public int NumOfQuickAccessSlots;
        public bool OpenSlots { get { return QuickAccessItems.Count < NumOfQuickAccessSlots; } }
 
    }   

}
