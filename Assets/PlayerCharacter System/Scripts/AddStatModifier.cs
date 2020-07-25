using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace Test.CharacterStats
{
    public struct AddStatModifier : IComponentData
    {

    }


    public enum ValueToModify {  Health, Mana, Strength, etc}
}