using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.InventorySystem.Base {
    [System.Serializable]
    public class InventoryBase
    {
        public List<ItemSlot> ItemsInInventory;
        public uint MaxInventorySize;
        public bool OpenSlots { get { return ItemsInInventory.Count < MaxInventorySize; } }




    }

    [System.Serializable]
    public struct ItemSlot{
        public ItemBaseSO Item;
        public int Count;
    }

}