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
    public class BlasterSO :WeaponSO,IEquipable ,IProjectile
    {


        #region Variable
        public GameObject ProjectilePrefab; //Move to SO later
        public GameObject ShootPoint; // may have to make this in code?????
       

        public int RoundsPerMin => throw new System.NotImplementedException();

        public int RoundsPerShot => throw new System.NotImplementedException();

        public float NormalSpeed => throw new System.NotImplementedException();

        [SerializeField] Vector3 offset;
        public Vector3 ShootLocationOffset { get { return offset; }  }
   
#endregion


        public override void Convert(Entity entity, EntityManager dstManager)
        {
            var ShootingData = new ShooterComponent();
            ShootingData.ProjectileGameObject = ProjectilePrefab;
            ShootingData.LastTimeShot = 0.0f;
            ShootingData.Offset = ShootLocationOffset;
            Entity point = dstManager.CreateEntity();
            var shootPoint = new GameObject();
            shootPoint.transform.parent = weaponModel.transform;
            shootPoint.transform.localPosition = ShootLocationOffset;
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
            base.EquipItem(characterInventory, IndexOf, player);
                Convert(characterInventory.self, World.DefaultGameObjectInjectionWorld.EntityManager);
            
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

    public class Projectiles : ScriptableObject
    {
        public GameObject GO;
    }
}