using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.MissionSystem.Interfaces;
using Dreamers.InventorySystem.Interfaces;

namespace Dreamers.InventorySystem.MissionSystem.SO
{
    public class DefeatEnemyMissionSO : MissionQuestSO, IDefeat, IPurchasable
    {
        public GameObject DefeatWhat => throw new System.NotImplementedException();

        public uint DefeatHowMany => throw new System.NotImplementedException();
        public uint Value { get { return 150; } }
        public uint MaxStackCount { get { return 0; } }
        public bool Stackable { get { return false; } }

    }
}