using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.Interfaces;
namespace Dreamers.InventorySystem.MissionSystem.Interfaces
{
    public interface IBase
    {
        string Name { get; }
        uint MissionID { get; }
        string Objective { get; }
        int ReqdLevel { get; }
        int GoldReward { get; }
        IPurchasable ItemReward { get; }
        public bool IsSideQuest { get; }
#if UNITY_EDITOR
        void CreateQuest(string name, string Objective, int level, int GoldReward, IPurchasable items);
#endif
        void AcceptQuest();
        void CompleteQuest();
        void QuestRequirementsMet();
    }
}