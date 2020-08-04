using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem
{
    [CreateAssetMenu(fileName = "Armor Item Data", menuName = "Item System/Armor Item", order = 1)]

    public class ArmorSO : ItemBaseSO, IEquipable, IArmor
    {

        public EquipmentType Equipment { get { return EquipmentType.Armor; } }

        [SerializeField] private GameObject _model;
        public GameObject Model { get { return _model; } }
        [SerializeField] private bool _equipToHuman;
        public bool EquipToHuman { get { return _equipToHuman; } }

        [SerializeField] private HumanBodyBones _equipBone;
        public HumanBodyBones EquipBone { get { return _equipBone; } }
        [SerializeField] private ArmorType _armorType;
        public ArmorType ArmorType { get { return _armorType; } }
        [SerializeField] private uint _levelRqd;
        public uint LevelRqd { get { return _levelRqd; } }

        [SerializeField] private List<StatModifier> _modifiers;
        public List<StatModifier> Modifiers { get { return _modifiers; } }

        public override void EquipItem(InventoryBase inventoryBase, EquipmentBase Equipment, int IndexOf, BaseCharacter player)
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
                switch (ArmorType)
                {
                    case ArmorType.Arms:
                        ModCharacterStats(player, Equipment.Arms);
                        Equipment.Arms.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Arms = this;
                        if (Model != null)
                        RemoveFromInventory(inventoryBase, IndexOf);
                        break;
                    case ArmorType.Chest:
                        ModCharacterStats(player, Equipment.Chest);
                        Equipment.Chest.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Chest = this;
                        RemoveFromInventory(inventoryBase, IndexOf);
                        break;
                    case ArmorType.Helmet:
                        ModCharacterStats(player, Equipment.Helmet);
                        Equipment.Helmet.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Helmet = this;
                        RemoveFromInventory(inventoryBase, IndexOf);
                        break;
                    case ArmorType.Legs:
                        ModCharacterStats((BaseCharacter)player, Equipment.Legs);
                        Equipment.Legs.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Legs = this;
                        RemoveFromInventory(inventoryBase, IndexOf);
                        break;
                    case ArmorType.Signature:
                        ModCharacterStats((BaseCharacter)player, Equipment.Signature);
                        Equipment.Signature.Unequip(inventoryBase, Equipment, player, 0);
                        Equipment.Signature = this;
                        RemoveFromInventory(inventoryBase, IndexOf);

                        break;
                }
                player.StatUpdate();
            }
            else { Debug.LogWarning("Level required to Equip is " + LevelRqd +". Character is currently level "+ player.Level); }
        }

        public override void Unequip(InventoryBase inventoryBase, EquipmentBase Equipment, BaseCharacter player, int IndexOf)
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
        void ModCharacterStats(BaseCharacter character, ArmorSO PreviousMod) {
            //Remove Old Mods
            if (PreviousMod != null)
            {
                foreach (StatModifier mod in PreviousMod.Modifiers)
                {
                    switch (mod.Stat)
                    {
                        case AttributeName.Level:
                            Debug.LogWarning("Level Modding is not allowed at this time. Please contact Programming is needed");
                            break;
                        case AttributeName.Strength:
                            character.GetPrimaryAttribute((int)AttributeName.Strength).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Vitality:
                            character.GetPrimaryAttribute((int)AttributeName.Vitality).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Awareness:
                            character.GetPrimaryAttribute((int)AttributeName.Awareness).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Speed:
                            character.GetPrimaryAttribute((int)AttributeName.Speed).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Skill:
                            character.GetPrimaryAttribute((int)AttributeName.Skill).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Resistance:
                            character.GetPrimaryAttribute((int)AttributeName.Resistance).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Concentration:
                            character.GetPrimaryAttribute((int)AttributeName.Concentration).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.WillPower:
                            character.GetPrimaryAttribute((int)AttributeName.WillPower).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Charisma:
                            character.GetPrimaryAttribute((int)AttributeName.Charisma).BuffValue -= mod.BuffValue;
                            break;
                        case AttributeName.Luck:
                            character.GetPrimaryAttribute((int)AttributeName.Luck).BuffValue -= mod.BuffValue;
                            break;
                    }
                }
            }
            // Add new Mods
            foreach (StatModifier mod in Modifiers)
            {
                switch (mod.Stat)
                {
                    case AttributeName.Level:
                        Debug.LogWarning("Level Modding is not allowed at this time. Please contact Programming is needed");
                        break;
                    case AttributeName.Strength:
                        character.GetPrimaryAttribute((int)AttributeName.Strength).BuffValue += mod.BuffValue;
                        character.GetPrimaryAttribute((int)AttributeName.Strength).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Vitality:
                        character.GetPrimaryAttribute((int)AttributeName.Vitality).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Awareness:
                        character.GetPrimaryAttribute((int)AttributeName.Awareness).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Speed:
                        character.GetPrimaryAttribute((int)AttributeName.Speed).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Skill:
                        character.GetPrimaryAttribute((int)AttributeName.Skill).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Resistance:
                        character.GetPrimaryAttribute((int)AttributeName.Resistance).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Concentration:
                        character.GetPrimaryAttribute((int)AttributeName.Concentration).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.WillPower:
                        character.GetPrimaryAttribute((int)AttributeName.WillPower).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Charisma:
                        character.GetPrimaryAttribute((int)AttributeName.Charisma).BuffValue += mod.BuffValue;
                        break;
                    case AttributeName.Luck:
                        character.GetPrimaryAttribute((int)AttributeName.Luck).BuffValue += mod.BuffValue;
                        break;
                }
            }
        
        }

        public override void Use(InventoryBase inventoryBase, int IndexOf, BaseCharacter player)
        {
            throw new System.NotImplementedException();
        }
    }


    
}