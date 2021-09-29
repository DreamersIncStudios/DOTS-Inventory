using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamers.InventorySystem.MissionSystem.Interfaces;// Remove later;
namespace Dreamers.InventorySystem.MissionSystem.Task
{
    public abstract class TaskSO : ScriptableObject, ITask
    {
        public TaskTypes TaskType { get { return taskTypes; } }
        [SerializeField] TaskTypes taskTypes;

        public bool Complete { get { return complete; } }
            [SerializeField] bool complete;

        public void Deregister()
        {
            throw new System.NotImplementedException();
        }

        public void Register()
        {
            throw new System.NotImplementedException();
        }
        public void QuestRequirementsMet() { }
    }

    public interface ITask {
        TaskTypes TaskType{get;}
        bool Complete { get; }
        void Register();
        void Deregister();
        void QuestRequirementsMet();
    }
}