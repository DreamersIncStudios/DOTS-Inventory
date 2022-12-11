using Dreamers.InventorySystem.Base;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace Dreamers.InventorySystem
{
    public class CharacterInventory : IComponentData
    {
        public int Gold { get; private set; }

        public InventoryBase Inventory;
        public EquipmentBase Equipment;

        public void Setup() {
            Inventory = new (10);
            Equipment = new();

        }
        public void RemoveGold(uint amount) {

            if (amount <= Gold)
            {
                Gold = (int)Mathf.Clamp(Gold - amount, 0, Mathf.Infinity);
            }
        }
        public void AddGold(uint amount)
        {
                Gold = (int)Mathf.Clamp(Gold + amount, 0, Mathf.Infinity);
        }
    }
}