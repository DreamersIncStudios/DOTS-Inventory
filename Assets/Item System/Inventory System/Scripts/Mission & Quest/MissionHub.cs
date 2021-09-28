using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Dreamers.InventorySystem.MissionSystem.SO;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem.MissionSystem {

    [System.Serializable]
    public class MissionHub
    {
        public MissionQuestSO SelectedMission;
        List<MissionQuestSO> OpenStoryMissions;
        Dictionary<uint,MissionQuestSO> SideQuests;
        UnityEvent<NPC> onKillEvent;
        UnityEvent<ItemBaseSO> onCollectEvent;
        UnityEvent OnDeathEvent;
        public MissionHub(MissionQuestSO CurrentStoryMission = default, List<MissionQuestSO> OpenStoryMissions = default, List<MissionQuestSO> OpenSideQuest = default) {
            onKillEvent = new UnityEvent<NPC>();
            onCollectEvent = new UnityEvent<ItemBaseSO>();
            OnDeathEvent = new UnityEvent();

            SelectedMission = CurrentStoryMission;
            this.OpenStoryMissions = new List<MissionQuestSO>();
            this.OpenStoryMissions = OpenStoryMissions;
            SideQuests = new Dictionary<uint, MissionQuestSO>();
            if (OpenSideQuest.Count > 0)
            {
                foreach (var item in OpenSideQuest)
                {
                    SideQuests.Add(item.MissionID, item);
                    Register(item);
                }
            }
        }

        public bool AddMissionSide(MissionQuestSO SO) {
            if (!SideQuests.ContainsKey(SO.MissionID))
            {
                SideQuests.Add(SO.MissionID, SO);
                Register(SO);
                return true;
            } else
                return false;
        }
        public void Register(MissionQuestSO SO) {
            switch (SO.questType) {
                case Interfaces.QuestType.Collect:
                    DefeatEnemyMissionSO defeatEnemyMissionSO = (DefeatEnemyMissionSO)SO;
                 onKillEvent.AddListener(defeatEnemyMissionSO.IncrementCounter);
                    if (defeatEnemyMissionSO.ResetOnDeath)
                        OnDeathEvent.AddListener(defeatEnemyMissionSO.ResetCount);

                    break;
            
            }
        
        }
        public void Deregister( MissionQuestSO SO) {
            switch (SO.questType)
            {
                case Interfaces.QuestType.Collect:
                    DefeatEnemyMissionSO defeatEnemyMissionSO = (DefeatEnemyMissionSO)SO;
                    onKillEvent.RemoveListener(defeatEnemyMissionSO.IncrementCounter);
                    if (defeatEnemyMissionSO.ResetOnDeath)
                        OnDeathEvent.RemoveListener(defeatEnemyMissionSO.ResetCount);

                    break;

            }
   
        }
        public void OnCollect(ItemBaseSO item) {
            onCollectEvent.Invoke( item);
        }

        
        public void OnKill(NPC npc) {
            onKillEvent.Invoke(npc);
        }
        public void OnDeath() { }
    }
    //Todo change to SO Type of something later;
    public enum NPC { test1,test2, test3,test4,test5 }

}