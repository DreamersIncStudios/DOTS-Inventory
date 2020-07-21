using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

/*Generic Player Character Class
 * Will be using BurgZergArcade Derived Player Character System  that is already in main project file
 */
namespace Test.CharacterStats
{
    public class PlayerCharacter : MonoBehaviour, IConvertGameObjectToEntity
    {
        [Range(0, 999)]
        [SerializeField] int _curHealth;
        public int CurHealth {
            get { return _curHealth; }
            private set { if (value > MaxHealth)
                    _curHealth = MaxHealth;
                else
                    _curHealth = value;
            }
        }

        [Range(0, 999)]
        public int MaxHealth;

        [Range(0, 999)]
        public int CurMana;
        [Range(0, 999)]
        public int MaxMana;
        Entity selfEntityRef;
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Stats() { CurHealth = _curHealth, CurMana = CurMana, MaxHealth = MaxHealth, MaxMana = MaxMana };
            dstManager.AddComponentData(entity, data);
            dstManager.AddComponent<Unity.Transforms.CopyTransformFromGameObject>(entity);
            selfEntityRef = entity;

        }

        private void Start()
        {
            _curHealth = MaxHealth;
            CurMana = MaxMana;
        }
        //need to make a job to account for resistence and level in the future
        public void IncreaseHealth(int Change){
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(selfEntityRef, new IncreaseHealthTag() { value = Change });
        }

        public void IncreaseMana(int Change) {
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(selfEntityRef, new IncreaseManaTag() { value = Change });

        }
        public void DecreaseHealth(int Change)
        {
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(selfEntityRef, new DecreaseHealthTag() { value = Change });
        }

        public void DecreaseMana(int Change)
        {
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(selfEntityRef, new DecreaseManaTag() { value = Change });

        }
    }
    public struct IncreaseHealthTag : IComponentData {
        public int value;
        public uint Iterations;
        public float Frequency;
        public float Timer;
    }
    public struct IncreaseManaTag : IComponentData
    {
        public int value;
        public uint Iterations;
        public float Frequency;
        public float Timer;

    }
    public struct DecreaseHealthTag : IComponentData
    {
        public int value;
        public uint Iterations; // consider -1 to be until release ??
        public float Frequency;
        public float Timer;

    }
    public struct DecreaseManaTag : IComponentData
    {
        public int value;
        public uint Iterations;
        public float Frequency;
        public float Timer;

    }
    public class EnemyCharacter : PlayerCharacter, IConvertGameObjectToEntity
    {

    }

    // add safe checks
    public struct Stats: IComponentData {
        [Range(0, 999)]
        [SerializeField] int _curHealth;
        public int CurHealth { get { return _curHealth; } 
            set {

                if (value <= 0)
                    _curHealth = 0;
                else if (value > MaxHealth)
                    _curHealth = MaxHealth;
                else
                    _curHealth = value;
            } }
        [Range(0, 999)]
        [SerializeField] int _curMana;

        public int CurMana
        {
            get { return _curMana; }
            set
            {

                if (value <= 0)
                    _curMana= 0;
                else if (value > MaxMana)
                    _curMana = MaxMana;
                else
                    _curMana = value;
            }
        }
        [Range(0, 999)]
        public int MaxHealth;
        [Range(0, 999)]
        public int MaxMana;
    }

}