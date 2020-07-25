using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test.CharacterStats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem
{
    public class ArmorSO : ItemBaseSO, IEquipable, IArmor
    {
        public new ItemType Type { get { return ItemType.Armor; } }
        public EquipmentType Equipment { get { return EquipmentType.Armor; } }

        [SerializeField] GameObject _model;
        public GameObject Model { get { return _model; } }
        [SerializeField] ArmorType _armorType;
        public ArmorType ArmorType { get { return _armorType; } }
        [SerializeField] uint _levelRqd;
        public uint LevelRqd { get { return _levelRqd; } }
        [SerializeField] int _healthIncrease;
        public int HealthIncrease { get { return _healthIncrease; } }
        [SerializeField] int _manaIncrease;
        public int ManaIncrease { get { return _manaIncrease; } }

        public override void Equip(InventoryBase inventoryBase, EquipmentBase Equipment, int IndexOf, PlayerCharacter player)
        {
            RemoveFromInventory(inventoryBase, IndexOf);
            if (player.Level >= LevelRqd)
            {
                switch (ArmorType)
                {
                    case ArmorType.Arms:
                        Equipment.Arms.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Arms = this;
                        GameObject armPiece = Instantiate(Model);
                        RemoveFromInventory(inventoryBase, IndexOf);
                        break;
                    case ArmorType.Chest:
                        Equipment.Chest.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Chest = this;
                        GameObject chestPiece = Instantiate(Model);

                        RemoveFromInventory(inventoryBase, IndexOf);

                        break;
                    case ArmorType.Helmet:
                        Equipment.Helmet.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Helmet = this;
                        GameObject HelmetPiece = Instantiate(Model);

                        RemoveFromInventory(inventoryBase, IndexOf);

                        break;
                    case ArmorType.Legs:
                        Equipment.Legs.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Legs = this;
                        GameObject legPiece = Instantiate(Model);
                        RemoveFromInventory(inventoryBase, IndexOf);

                        break;
                    case ArmorType.Signature:
                        Equipment.Signature.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Signature = this;
                        GameObject signaturePiece = Instantiate(Model);
                        RemoveFromInventory(inventoryBase, IndexOf);

                        break;
                }

            }
            else { Debug.LogWarning("Level required to Equip is " + LevelRqd); }
        }

        public override void Unequip(InventoryBase inventoryBase, EquipmentBase Equipment, PlayerCharacter player, int IndexOf)
        {
            AddToInventory(inventoryBase);
            switch (ArmorType)
            {
                case ArmorType.Arms:
                    Equipment.Arms = null;
                    break;
                case ArmorType.Chest:
                    Equipment.Chest = null;

                    break;
                case ArmorType.Helmet:
                    Equipment.Helmet = null;
                    break;
                case ArmorType.Legs:
                    Equipment.Legs = null;
                    break;
                case ArmorType.Signature:
                    Equipment.Signature = null;
                    break;
            }

        }
        void Unequip() { }
        public override void Use(InventoryBase inventoryBase, int IndexOf, PlayerCharacter player)
        {
            throw new System.NotImplementedException();
        }
    }


    
}