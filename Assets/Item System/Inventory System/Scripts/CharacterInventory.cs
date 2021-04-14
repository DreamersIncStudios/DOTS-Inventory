using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Dreamers.InventorySystem.Base;
namespace Dreamers.InventorySystem
{
    public class CharacterInventory : MonoBehaviour,IConvertGameObjectToEntity
    {

        public InventoryBase Inventory;
        public EquipmentBase Equipment;
        public Entity self { get; private set; }
        public int Gold;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            self = entity;
        }

        void Awake() {
            Inventory = new InventoryBase();
            Equipment = new EquipmentBase();
        
        }

        // Start is called before the first frame update

    }
}