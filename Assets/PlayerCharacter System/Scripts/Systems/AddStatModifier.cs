using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Stats
{
    public struct AddStatModifier : IBufferElementData 
    {
        public StatModifier StatMod;

        public static implicit operator AddStatModifier(StatModifier e) { return new AddStatModifier() {StatMod = e }; }
        public static implicit operator StatModifier(AddStatModifier e) { return e.StatMod; }
    }

    public struct StatModifier 
    {
        public StatToModify Stat;
        public int Mod;
            
    }


    public enum StatToModify {  Health, Mana, Strength, etc}
}