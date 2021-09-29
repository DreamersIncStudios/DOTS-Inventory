using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.Interfaces;
using Dreamers.InventorySystem.MissionSystem.Interfaces;
using Dreamers.InventorySystem.MissionSystem.Task;

namespace Dreamers.InventorySystem.MissionSystem.SO
{
    public class MissionQuestSO : ScriptableObject, IBase, IPurchasable
    {

        #region SO Variables
        public string Name { get { return missionName; } }
        [SerializeField] string missionName;
        public uint MissionID { get { return ID; } }
        [SerializeField] uint ID;
        public string Objective { get { return objective; } }
        [SerializeField] string objective;
        public int ReqdLevel { get { return reqdLevel; } }
        [SerializeField] int reqdLevel;
        public int GoldReward { get { return goldReward; } }
        [SerializeField] int goldReward;
        public IPurchasable ItemReward { get { return itemReward; } }
        [SerializeField] IPurchasable itemReward;
        public TaskTypes questType { get; private set; }
        public bool IsSideQuest { get; private set; }
        public uint Value { get { return 150; } }
        public uint MaxStackCount { get { return 0; } }
        public bool Stackable { get { return false; } }
        public List<TaskSO> Tasks { get { return tasks; } }
        [SerializeField] List<TaskSO> tasks;
        #endregion
        public bool Sequential;
        public int CurrentTask;
        MissionHub hub { get {  return GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>().QuestLog; } }

#if UNITY_EDITOR

        public void setItemID(uint ID)
        {
            if (!QuestDatabase.Missions.TryGetValue(ID,out _))
                this.ID = ID;
            else {
                setItemID(ID + 1);
            }
        }
        public void SetSideQuest(bool set) {
            IsSideQuest = set;
        }


        public void CreateQuest(string name,string Objective, int level, int GoldReward, IPurchasable items) {
            this.missionName = name;
            objective = Objective;
            reqdLevel = level;
            this.goldReward = GoldReward;
            itemReward = items;
        }
#endif
        // determine if virtual or abstract
        public  void AcceptQuest() {
            hub.Register(this);
        }
        public  void CompleteQuest() {
        }
        public void QuestRequirementsMet() {
            //TODO Implement UI 
            Debug.Log(Name + " has been Completed. Please see ______ to turn in quest");
            hub.Deregister(this);



        }



    }



}
