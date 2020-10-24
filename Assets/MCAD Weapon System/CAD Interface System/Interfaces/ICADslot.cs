using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
    public abstract class TestSlot : ICADslot
    {
        [SerializeField] private CADGridSquare[,] grid;
        [SerializeField] private int size;
        [SerializeField] private int reqdLevel;
        [SerializeField] private bool isCommand;
        public string name { get; set; }

        public CADGridSquare[,] Grid { get { return grid; } }

        public int Size { get { return size; } }

        public int ReqdLevel { get { return reqdLevel; } }
        public Vector2Int InstallPosition { get; set; }
        public bool Installed { get; set; }
        public bool IsCommandLineAbility { get { return isCommand; } }
        public bool IsOnCommandLine { get; set; }

        public TestSlot(int sized, int level) {
            size = sized;
            reqdLevel = level;
            grid = new CADGridSquare[sized, sized];
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

        public virtual void AddAbility(Vector2Int Position)
        {
            Installed = true;
            InstallPosition = Position;

        }

        public virtual void RemoveAbility()
        {
            Installed = false;
            InstallPosition = new Vector2Int();
        }
    }

    public interface ICADslot 
    {
         CADGridSquare[,] Grid { get; }
        int Size { get; }   
        int ReqdLevel { get; }
        Vector2Int InstallPosition { get; set; }
        bool Installed { get; set; }

        void AddAbility(Vector2Int Position);
        void RemoveAbility();
    }
    public enum slotType { Magic, Skill, StatMod}
}