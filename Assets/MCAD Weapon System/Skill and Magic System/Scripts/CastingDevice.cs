using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamersInc.Utils;
using Utilities;
using Unity.Mathematics;
namespace DreamersInc.MagicSkill
{
    public class CastingDevice :MonoBehaviour
    {
        private int width, height;
        private float cellsize;
        public GridGeneric<MagicSkillGridObject> grid;
        public List<string> SpellNames;

        public BaseSkillSpellSO test;
        public void Setup ( int width = 15, int height =10, float cellsize = 5f) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, cellsize, (GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y));
            this.width = width;
            this.height = height;
            this.cellsize = cellsize;
            
        }
        private void Start()
        {
            test = (BaseSkillSpellSO)ScriptableObject.CreateInstance(typeof(BaseSkillSpellSO));
            test.Create("FireBall", 3, 3, 1, 100);
            Setup();
        }

        public void Update()
        {
    
        }
        public bool AddMapToGrid(int x, int y, AugmentGrid addGrid) {
            bool output = true;
            if (width >= addGrid.Width && height >= addGrid.Height)
            {
                for (int i = 0; i < addGrid.Width; i++)
                {
                    for (int j = 0; j < addGrid.Height; j++)
                    {
                        if (addGrid.grid.GetGridObject(i, j).GetStatus == GridStatus.Open)
                        {
                            continue;
                        }
                        if (grid.GetGridObject(x + i, y + j) != null)
                        {
                            if (grid.GetGridObject(x + i, y + j).GetStatus != GridStatus.Open)
                            {
                                output = false;
                                goto finish;
                            }
                        }
                        else {
                            output = false;
                            goto finish;

                        }
                    }
                }
            }
            else
            {
                output = false;
                goto finish;
            }
            for (int i = 0; i < addGrid.Width; i++)
            {
                for (int j = 0; j < addGrid.Height; j++)
                {
                    if (addGrid.grid.GetGridObject(i, j).GetStatus != GridStatus.Open)
                    {
                        grid.GetGridObject(x + i, y + j).SetStatus(addGrid.grid.GetGridObject(i, j).GetStatus);
                        grid.GetGridObject(x + i, y + j).SetFirstCell(new int2(x, y));
                        grid.GetGridObject(x + i, y + j).SetGridRef(addGrid);
                    }
                }
            }



                    finish:
            return output;
        }
        public bool RemoveMapToGrid(int x, int y)
        {
            bool output = true;
            if (grid.GetGridObject(x, y).GetStatus == GridStatus.Occupied) {
                int2 firstCell = grid.GetGridObject(x, y).GetFirstCell;
                AugmentGrid augmentedGrid = grid.GetGridObject(x, y).GetSkillMap;

                for (int i = 0; i < augmentedGrid.Width; i++)
                {
                    for (int j = 0; j < augmentedGrid.Height; j++)
                    {
                        if (grid.GetGridObject(firstCell.x+ i, firstCell.y + j).GetStatus == GridStatus.Occupied
                            &&  augmentedGrid.grid.GetGridObject(i, j).GetStatus != GridStatus.Open
                            )
                        {
                            grid.GetGridObject(firstCell.x + i, firstCell.y + j).SetStatus(GridStatus.Open);
                            grid.GetGridObject(firstCell.x + i, firstCell.y + j).SetFirstCell(new int2());
                            
                        }
                    }
                }

                    } else {
                output = false;
            }
            return output;
        }

    }


    [System.Serializable]
    public class AugmentGrid {
        public int Width { get; private set; }
        public int Height{ get; private set; }
      
        
        public GridGeneric<MagicSkillGridObject> grid;

        public AugmentGrid(int width, int height, string name) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, 5.0f,new Vector3(-20,0,20),(GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y));
            this.Width = width;
            this.Height = height;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid.GetGridObject(x, y).SetStatus(GridStatus.Occupied);
                    grid.GetGridObject(x, y).SetName(name);
                }
            }

        }

    }
}