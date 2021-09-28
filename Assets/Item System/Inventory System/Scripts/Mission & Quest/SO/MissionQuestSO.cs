using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.Interfaces;
using Dreamers.InventorySystem.MissionSystem.Interfaces;

namespace Dreamers.InventorySystem.MissionSystem.SO
{
    public abstract class MissionQuestSO : ScriptableObject, IBase
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
        public QuestType questType { get; private set; }
        public bool IsSideQuest { get; private set; }
        #endregion

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


        public virtual void CreateQuest(string name,string Objective, int level, int GoldReward, IPurchasable items) {
            this.missionName = name;
            objective = Objective;
            reqdLevel = level;
            this.goldReward = GoldReward;
            itemReward = items;
        }
#endif
        // determine if virtual or abstract
        public virtual void AcceptQuest() {
            hub.Register(this);
        }
        public virtual void CompleteQuest() {
        }
        public virtual void QuestRequirementsMet() {
            //TODO Implement UI 
            Debug.Log(Name + " has been Completed. Please see ______ to turn in quest");
            hub.Deregister(this);



        }



    }



}
