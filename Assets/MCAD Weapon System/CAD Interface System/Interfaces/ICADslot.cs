using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
    public  class TestSlot : ICADslot
    {
        [SerializeField] private CADGridSquare[,] grid;
        [SerializeField] private Skills skill;
        [SerializeField] private int size;
        [SerializeField] private int reqdLevel;
   
        public string name { get; set; }
        public Skills SkillName { get { return skill; } }

        public CADGridSquare[,] Grid { get { return grid; } }

        public int Size { get { return size; } }

        public int ReqdLevel { get { return reqdLevel; } }
        public Vector2Int InstallPosition { get; set; }
        public bool Installed { get; set; }
        public bool OnCommandLine { get; set; }

        public TestSlot(int sized, int level) {
            size = sized;
            reqdLevel = level;
            grid = new CADGridSquare[sized, sized];
            OnCommandLine = false;
            Installed = false;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    grid[x, y] = new CADGridSquare();
                    grid[x, y].status = GridSquareStatus.Occupied;

                }
            }
        }

        public virtual void AddAbility(SkillsManager skillsManager,Vector2Int Position)
        {
            Installed = true;
            InstallPosition = Position;
            skillsManager.AddNewSkill(skill);
        }

        public virtual void RemoveAbility(SkillsManager skillsManager)
        {
            Installed = false;
            InstallPosition = new Vector2Int();
            OnCommandLine = false;
            skillsManager.RemoveSkill(skill);
        }
    }

    public interface ICADslot 
    {
         CADGridSquare[,] Grid { get; }
        int Size { get; }   
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