using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test.CharacterStats;

namespace Dreamers.InventorySystem {
    [System.Serializable]
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class HealthRecoverSO : ItemBaseSO, IRecoverItems
    {
        [SerializeField]uint _recoverAmount;
        public uint RecoverAmount { get { return _recoverAmount; } }
        [SerializeField] float _duration;
        public float Duration { get { return -_duration; } }
        [SerializeField] float _freq;
        public float Frequency { get { return _freq; } }
        public override void Use(InventoryBase inventoryBase, int IndexOf)
        {
            RemoveFromInvertory(inventoryBase, IndexOf);

        }
        public void Use(InventoryBase inventoryBase, int IndexOf, PlayerCharacter player) {
            Use(inventoryBase, IndexOf);
            player.IncreaseHealth((int)RecoverAmount);
        }
    }

    public interface IRecoverItems {
        uint RecoverAmount { get; }
        float Duration { get; }
        float Frequency { get; }
    }
}