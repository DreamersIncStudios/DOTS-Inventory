using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Dreamers.Global;

namespace Dreamers.InventorySystem.MissionSystem.SO
{

#if UNITY_EDITOR
    public static class CreateSO
    {
        [MenuItem("Assets/Create/Missions")]
        static public void CreateDefeatQuest()
        {
            ScriptableObjectUtility.CreateAsset<DefeatEnemyMissionSO>("Defeat Enemy Mission", out DefeatEnemyMissionSO Item);
            QuestDatabase.LoadDatabaseForced();
            Item.setItemID((uint)QuestDatabase.Missions.Count);
            Debug.Log(Item.MissionID);
            // need to deal with duplicate itemID numbers 

        }
    }
#endif
}