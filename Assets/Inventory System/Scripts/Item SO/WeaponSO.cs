using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Item System/Weapon", order = 1)]
    public class WeaponSO : ItemBaseSO, IEquipable
    {
        public new ItemType Type { get { return ItemType.Weapon; } }
        public EquipmentType Equipment { get { return EquipmentType.Armor; } }
        [SerializeField] GameObject _model;
        public GameObject Model { get { return _model; } }
        [SerializeField] private bool _equipToHuman;
        public bool EquipToHuman { get { return _equipToHuman; } }
        [SerializeField] private HumanBodyBones _equipBone;
        public HumanBodyBones EquipBone { get { return _equipBone; } }
        public List<Interfaces.StatModifier> Modifiers => throw new System.NotImplementedException();

        public uint LevelRqd => throw new System.NotImplementedException();

        public override void Equip(InventoryBase inventoryBase, EquipmentBase Equipment, int IndexOf,BaseCharacter player)
        {
            RemoveFromInventory(inventoryBase, IndexOf);
        }

        public override void Unequip(InventoryBase inventoryBase, EquipmentBase Equipment, BaseCharacter player, int IndexOf)
        {
            throw new System.NotImplementedException();
        }

        public override void Use(InventoryBase inventoryBase, int IndexOf, BaseCharacter player)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum WeaponSlot { Primary, Secondary, Projectile}
}