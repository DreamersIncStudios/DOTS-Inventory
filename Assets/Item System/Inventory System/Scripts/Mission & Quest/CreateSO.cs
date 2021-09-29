using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Dreamers.Global;

#if UNITY_EDITOR

namespace Dreamers.InventorySystem.MissionSystem.SO
{

    public static class CreateSO
    {
        [MenuItem("Assets/Create/Missions")]
        static public void CreateDefeatQuest()
        {
            ScriptableObjectUtility.CreateAsset<MissionQuestSO>("Defeat Enemy Mission", out MissionQuestSO Item);
            QuestDatabase.LoadDatabaseForced();
            Item.setItemID((uint)QuestDatabase.Missions.Count);
            Debug.Log(Item.MissionID);
            // need to deal with duplicate itemID numbers 

        }
    }
}

#endif
