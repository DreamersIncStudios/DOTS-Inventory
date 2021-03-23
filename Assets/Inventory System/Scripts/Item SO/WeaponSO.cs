using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
using Unity.Entities;

namespace Dreamers.InventorySystem
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Item System/Weapon", order = 2)]
    public class WeaponSO : ItemBaseSO, IEquipable,IWeapon
    {
        public new ItemType Type { get { return ItemType.Weapon; } }
        [SerializeField] Quality quality;
        public Quality Quality { get { return quality; } }
        public EquipmentType Equipment { get { return _equipment; } }
        [SerializeField] EquipmentType _equipment;

        [SerializeField] GameObject _model;
        public GameObject Model { get { return _model; } }
        [SerializeField] private bool _equipToHuman;
        public bool EquipToHuman { get { return _equipToHuman; } }
        [SerializeField] private HumanBodyBones _equipBone;
        public HumanBodyBones EquipBone { get { return _equipBone; } }
        [SerializeField] private List<StatModifier> _modifiers;
        public List<StatModifier> Modifiers { get { return _modifiers; } }

        [SerializeField] private uint _levelRQD;
        public uint LevelRqd { get { return _levelRQD; } }

        [SerializeField] private WeaponType _weaponType;
        public WeaponType WeaponType { get { return _weaponType; } }
        [SerializeField] private WeaponSlot slot;
        public WeaponSlot Slot { get { return slot; } }
        [SerializeField] private float maxDurable;
        public float MaxDurability { get { return maxDurable; } }
        public float CurrentDurablity { get; set; }
        [SerializeField] private bool breakable;
        public bool Breakable { get { return breakable; } }
        [SerializeField] private bool _upgradeable;
        public bool Upgradeable { get { return _upgradeable; } }

        public int SkillPoints { get; set; }
        public int Exprience { get; set; }

        public override void EquipItem(InventoryBase inventoryBase, EquipmentBase Equipment, int IndexOf,BaseCharacter player)
        {
            if (player.Level >= LevelRqd)
            {
                if (Model != null)
                {
                    GameObject armorModel = Instantiate(Model);
                    // Consider adding and enum as all character maybe not be human 
                    if (EquipToHuman)
                    {
                        Transform bone = player.GetComponent<Animator>().GetBoneTransform(EquipBone);
                        if (bone)
                        {
                            armorModel.transform.SetParent(bone);
                        }

                    }

                }
                        EquipmentUtility.ModCharacterStats(player,Modifiers, true);

                if (Equipment.equippedItem.TryGetValue(this.Equipment, out ItemBaseSO value))
                {
                    Equipment.equippedItem[this.Equipment].Unequip(inventoryBase, Equipment, player, 0);
                }
                Equipment.equippedItem[this.Equipment] = this;

                RemoveFromInventory(inventoryBase, IndexOf);

            }
            else { Debug.LogWarning("Level required to Equip is " + LevelRqd + ". Character is currently level " + player.Level); }

        }

        public override void Unequip(InventoryBase inventoryBase, EquipmentBase Equipment, BaseCharacter player, int IndexOf)
        {
            EquipmentUtility.ModCharacterStats(player,Modifiers, false);
            AddToInventory(inventoryBase);
            Equipment.equippedItem.Remove(this.Equipment);
            Object.Destroy(Model);

        }
        public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        { }

        public override void Use(InventoryBase inventoryBase, int IndexOf, BaseCharacter player)
        {
            throw new System.NotImplementedException();
        }


    }

    
}