using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test.CharacterStats;

namespace Dreamers.InventorySystem {
    [System.Serializable]
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class RecoveryItemSO : ItemBaseSO, IRecoverItems
    {
        [SerializeField]uint _recoverAmount;
        public uint RecoverAmount { get { return _recoverAmount; } }
        [SerializeField] uint _iterations;
        public uint Iterations { get { return _iterations; } }
        [SerializeField] float _freq;
        public float Frequency { get { return _freq; } }
        [SerializeField] RecoverType _recoverWhat;
        public RecoverType RecoverWhat { get { return _recoverWhat; } }

     
        public override void Use(InventoryBase inventoryBase, int IndexOf, PlayerCharacter player) {
            Use(inventoryBase, IndexOf);
            switch (RecoverWhat) {
                case RecoverType.Health:
                player.IncreaseHealth((int)RecoverAmount, Iterations, Frequency);

                    break;
                case RecoverType.Mana:
                    player.IncreaseMana((int)RecoverAmount, Iterations, Frequency);
                    break;
                case RecoverType.HealthMana:
                    player.IncreaseHealth((int)RecoverAmount, Iterations, Frequency);
                    player.IncreaseMana((int)RecoverAmount, Iterations, Frequency);
                    break;
            }
        }
    }

    public interface IRecoverItems {
        uint RecoverAmount { get; }
        uint Iterations { get; }
        float Frequency { get; }
        RecoverType RecoverWhat { get; }
    }


    public enum RecoverType{
        Health,Mana, HealthMana, Status,StatusPlusHealth
    }
}