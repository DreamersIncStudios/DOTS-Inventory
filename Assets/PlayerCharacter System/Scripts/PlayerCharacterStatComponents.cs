using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

/*Generic Player Character Class
 * Will be using BurgZergArcade Derived Player Character System  that is already in main project file
 */
namespace Stats
{


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

    // add safe checks
    public struct PlayerStatComponent: IComponentData {
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

        public float MagicDef;
        public float MeleeAttack;
        public float MeleeDef;
    }

}