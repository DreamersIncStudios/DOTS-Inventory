using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
   
    public interface ICADslot 
    {
        string Name { get; set; }
        
        arrayLayout Grid { get; }
        Vector2Int Size { get; }   
        int ReqdLevel { get; }
        Vector2Int InstallPosition { get; set; }
        bool Installed { get; set; }
        Skills SkillName { get; }
        bool OnCommandLine { get; set; }
        void AddAbility(SkillsManager skillsManager, Vector2Int Position);
        void RemoveAbility(SkillsManager skillsManager);
        
    }
    public enum slotType { Magic, Skill, StatMod}
    public enum Status { Normal, Not_Running, Corrupted, }
}