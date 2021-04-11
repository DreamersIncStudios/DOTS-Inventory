using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Stats;
using Unity.Entities;
using Unity.Transforms;
using Dreamers.InventorySystem.Base;
using Dreamers.InventorySystem.Interfaces;

namespace Dreamers.InventorySystem
{
    public class BlasterSO : ItemBaseSO,IEquipable ,IProjectile
    {


        #region Variable
        public Projectile ProjectilePrefab; //Move to SO later
        public GameObject ShootPoint; // may have to make this in code?????
        public Quality Quality { get { return quality; } }
        [SerializeField] Quality quality;
        public EquipmentType Equipment { get { return EquipmentType.Armor_Chest; } }
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
        public int SkillPoints { get; set; }
        public int Exprience { get; set; }
        [SerializeField] private WeaponSlot slot = WeaponSlot.Projectile;
        public WeaponSlot Slot { get { return slot; } }

        public int RoundsPerMin => throw new System.NotImplementedException();

        public int RoundsPerShot => throw new System.NotImplementedException();

        public float NormalSpeed => throw new System.NotImplementedException();

        public Vector3 ShootLocationOffset => throw new System.NotImplementedException();
        GameObject BlastModel;
#endregion


        public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var ShootingData = new ShooterComponent();
            ShootingData.ProjectileGameObject = ProjectilePrefab.GO;
            ShootingData.LastTimeShot = 0.0f;
            Entity point = dstManager.CreateEntity();
            var shootPoint = new GameObject();
            shootPoint.transform.parent = _model.transform;
            shootPoint.transform.localPosition = ShootLocationOffset;
            dstManager.AddComponentObject(point, ShootPoint.transform); // Add child transform manually
            dstManager.AddComponentData(point, new Translation()); // Have to add all this stuff manually too
            dstManager.AddComponentData(point, new Rotation());
            dstManager.AddComponentData(point, new LocalToWorld());
            dstManager.AddComponentData(point, new CopyTransformFromGameObject()); // Or CopyTransformToGameObject - Only if you need to sync transforms

            // - Only if you want the parent child relationship
            dstManager.AddComponentData(point, new Parent { Value = entity });
            ShootingData.ShootFromHere = point;
            dstManager.AddComponentData(entity, ShootingData);

        }

        public override void Use(CharacterInventory characterInventory, int IndexOf, BaseCharacter player)
        {
            //throw new System.NotImplementedException();
        }

        public override void EquipItem(CharacterInventory characterInventory, int IndexOf, BaseCharacter player)
        {
            if (player.Level >= LevelRqd)
            {
                if (Model != null)
                {
                    BlastModel = Instantiate(Model);
                    // Consider adding and enum as all character maybe not be human 
                    if (EquipToHuman)
                    {
                        Transform bone = player.GetComponent<Animator>().GetBoneTransform(EquipBone);
                        if (bone)
                        {
                            BlastModel.transform.SetParent(bone);
                        }

                    }

                }
                EquipmentUtility.ModCharacterStats(player,Modifiers, true);

            }
        }

        public override void Unequip(CharacterInventory characterInventory, BaseCharacter player)
        {
            EquipmentBase Equipment = characterInventory.Equipment;
            AddToInventory(characterInventory);
            Destroy(BlastModel);
            EquipmentUtility.ModCharacterStats(player, Modifiers, false);
            Equipment.EquippedWeapons.Remove(this.Slot);
        }

        public bool Equals(ItemBaseSO obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (obj.Type != Type)
                return false;

            // TODO: write your implementation of Equals() here

           BlasterSO Blaster = (BlasterSO)obj;

            return ItemID == Blaster.ItemID && ItemName == Blaster.ItemName && Value == Blaster.Value && Modifiers.SequenceEqual(Blaster.Modifiers) &&
                Exprience == Blaster.Exprience && LevelRqd == Blaster.LevelRqd;
        }


    }

    public class Projectile : ScriptableObject
    {
        public GameObject GO;
    }
}