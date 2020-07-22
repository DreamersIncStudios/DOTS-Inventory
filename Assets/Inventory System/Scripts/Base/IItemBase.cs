using Unity.Entities;
using UnityEngine;
using Test.CharacterStats;
namespace Dreamers.InventorySystem
{

    public interface IItemBase
    {
        uint ItemID { get; }
        string ItemName { get; }
        string Description {get;}
        int Value { get; }
        ItemType Type { get; }
        bool Stackable { get; }
        bool Disposible { get; }
        bool QuestItem { get; }

        void Use(InventoryBase inventoryBase, int IndexOf);

        void AddToInventory(InventoryBase inventory);
        void RemoveFromInvertory(InventoryBase inventory, int IndexOf);

    }
    [System.Serializable]
    public abstract class ItemBaseSO : ScriptableObject, IItemBase
    {
        [SerializeField] uint _itemID;
        public uint ItemID { get { return _itemID; } } // To be implemented with Database system/CSV Editor creator 
        [SerializeField] string _itemName;
        public string ItemName { get { return _itemName; } }
        [SerializeField] string _desc;
        public string Description { get { return _desc; } }
        [SerializeField] int _value;
        public int Value { get { return _value; } }
        [SerializeField] ItemType _type;
        public ItemType Type { get { return _type; } }
        [SerializeField] bool _stackable;
        public bool Stackable { get { return _stackable; } }
        //[SerializeField] bool _disposible;
        public bool Disposible { get { return !QuestItem; } }
        [SerializeField] bool _questItem;
        public bool QuestItem { get { return _questItem; } }

        public  void Use(InventoryBase inventoryBase, int IndexOf)
        {
            RemoveFromInvertory(inventoryBase, IndexOf);

        }
        public abstract void Use(InventoryBase inventoryBase, int IndexOf, PlayerCharacter player);

        public virtual void AddToInventory(InventoryBase inventory)
        {
            bool addNewSlot = true; ;
            for (int i = 0; i < inventory.ItemsInInventory.Count; i++)
            {
                ItemSlot itemInInventory = inventory.ItemsInInventory[i];
                if (itemInInventory.Item.ItemID == ItemID && itemInInventory.Count < 99)
                {
                    itemInInventory.Count++;
                    addNewSlot = false;
                }
                inventory.ItemsInInventory[i] = itemInInventory;
            }

            if (inventory.OpenSlots && addNewSlot) 
                inventory.ItemsInInventory.Add(
                    new ItemSlot() {
                    Item = Instantiate(this),
                    Count=1});

        }

        public void RemoveFromInvertory(InventoryBase inventory, int IndexOf) // consider having inventory
        {
            ItemSlot updateItem = inventory.ItemsInInventory[IndexOf];
            if (Stackable && updateItem.Count > 1)
            {
                updateItem.Count--;
                inventory.ItemsInInventory[IndexOf] = updateItem;
            }
            else { inventory.ItemsInInventory.RemoveAt(IndexOf); }
        }
    }
    public enum ItemType
    {
        Weapon, Potion, Armor
    }
}