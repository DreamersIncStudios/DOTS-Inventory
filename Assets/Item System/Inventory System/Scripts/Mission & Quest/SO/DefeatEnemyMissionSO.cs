using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.MissionSystem.Interfaces;
using Dreamers.InventorySystem.Interfaces;

namespace Dreamers.InventorySystem.MissionSystem.SO
{
    public class DefeatEnemyMissionSO : MissionQuestSO, IDefeat, IPurchasable
    {
        public NPC DefeatWhat { get { return defeatWhat; } }
        [SerializeField] NPC defeatWhat;
        public uint DefeatHowMany { get { return defeatHowMany; } }
        [SerializeField] uint defeatHowMany;
    public uint HowMany { get; private set; }
        public bool ResetOnDeath { get; private set; }
        public uint Value { get { return 150; } }
        public uint MaxStackCount { get { return 0; } }
        public bool Stackable { get { return false; } }
        bool Completed => HowMany >= DefeatHowMany;
        public void IncrementCounter(NPC whatDied) {
            if (whatDied == DefeatWhat)
                HowMany++;
            Debug.Log(HowMany + "Defeated");
            if (Completed) {
            QuestRequirementsMet();
//#if UNITY_EDITOR
//                this.ResetCount();
//#endif
            }
        }

        public void ResetCount() {
            HowMany = 0;
        }
#if UNITY_EDITOR

        public void CreateQuest(string name, string Objective, int level, int GoldReward, IPurchasable items, NPC Defeatwhat) {
            base.CreateQuest(name, Objective, level, GoldReward, items);
           base.SetSideQuest(true);
        }
#endif

       
    }
}