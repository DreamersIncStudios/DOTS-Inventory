using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
    [System.Serializable]
    public class CADGridSquare {
        public GridSquareStatus status;
        public TypeOfGridSquare Type;
        public Sprite image = Resources.Load("square") as Sprite;
        public CADGridSquare() {
            image = Resources.Load<Sprite>("square");
            status = GridSquareStatus.Available;
        }
    }
    [System.Serializable]
    public enum GridSquareStatus { Locked, Available, Occupied, Blocked }
    [System.Serializable]
    public enum TypeOfGridSquare { Normal, CommandLine, Block}


    [System.Serializable]
    public class CADGridSystem
    {
        int CurSize;
        public CADGridSquare[,] Grid = new CADGridSquare[200, 200];
         
        public List<ICADslot> InstalledSlots;
        
        public CADGridSystem(int size) {
            CurSize = size;
           InstalledSlots = new List<ICADslot>();
            Grid = new CADGridSquare[200, 200];
            LayoutGrid();
            UnlockSquares(size);
           // Grid= new CADGridSquare [size,size];
        }
        public  void LayoutGrid() {

            for (int x = 0; x < 199; x++)
            {
                for (int y = 0; y < 199; y++)
                {
                    Grid[x, y] = new CADGridSquare();
            
                }
            }

                for (int x = 0; x < 199; x++)
            {
                //Command line layout to be determine
                Grid[x, 6].Type = TypeOfGridSquare.CommandLine;
                Grid[x, 30].Type = TypeOfGridSquare.CommandLine;
                Grid[x, 90].Type = TypeOfGridSquare.CommandLine;

            }

            for (int y = 0; y < 199; y++)
            {
                //Command line layout to be determine
                Grid[6, y].Type = TypeOfGridSquare.CommandLine;
                Grid[30, y].Type = TypeOfGridSquare.CommandLine;
                Grid[90, y].Type = TypeOfGridSquare.CommandLine;

            }
        }


        void UnlockSquares(int Size) {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if(Grid[x, y].status == GridSquareStatus.Locked)
                        Grid[x, y].status = GridSquareStatus.Available;
                }
            }
        }

        public void addSlotToCAD(ICADslot slot, SkillsManager Mgr,int X, int Y) {
           

            if (SlotAvailable(slot, X, Y)) 
            {
                InstalledSlots.Add(slot);
                slot.AddAbility(Mgr,new Vector2Int(X, Y));
                int xLimit = X + slot.Size.x;
                int yLimit = Y + slot.Size.y;
                for (int i = X; i < xLimit; i++)
                {
                    for (int j = Y; j < yLimit; j++)
                    {
                        Grid[i, j].status = GridSquareStatus.Occupied;
                        if (Grid[i, j].Type == TypeOfGridSquare.CommandLine)
                            slot.OnCommandLine = true;
                    }
                }
            }
        }
        public void RemoveSlotFromCAD(SkillsManager Mgr, int index)
        {
            UnequipSlot(InstalledSlots[index], InstalledSlots[index].InstallPosition.x, InstalledSlots[index].InstallPosition.y);
            InstalledSlots[index].RemoveAbility(Mgr);

        }



        public bool SlotAvailable(ICADslot slot, int X, int Y) {

            int xLimit = X + slot.Size.x;
            int yLimit = Y + slot.Size.y;
            if (xLimit > CurSize || yLimit > CurSize ) {
            
                return false;
            }
            for (int i = X; i < xLimit; i++)
            {
                for (int j = Y; j < yLimit; j++)
                {

                    if (slot.Grid.rows[i-X].row[j-Y] &&
                 Grid[i, j].status != GridSquareStatus.Available)
                    {

                        return false;
                    }
                }
            }



            return true;
        }

        public void UnequipSlot(ICADslot slot, int X, int Y) {

            int xLimit = X + slot.Size.x;
            int yLimit = Y + slot.Size.y;


            for (int i = X; i < xLimit; i++)
            {
                for (int j = Y; j < yLimit; j++)
                {
                    Grid[i, j].status = GridSquareStatus.Available;
                }
            }

        }


    }
}