using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamersInc.Utils;
using Utilities;
using Unity.Mathematics;
namespace DreamersInc.MagicSkill
{
    public class MagicSkillGrid :MonoBehaviour
    {
        private int width, height;
        private float cellsize;
        public GridGeneric<MagicSkillGridObject> grid;

        public AugmentGrid test;
        public void Setup ( int width = 15, int height =10, float cellsize = 5f) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, cellsize, (GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y));
            this.width = width;
            this.height = height;
            this.cellsize = cellsize;
            
        }
        private void Start()
        {
            test = new AugmentGrid(2, 3, "Test");
            Setup();
        }

        public void Update()
        {
            //test.setFalse();
            if (Input.GetMouseButtonDown(0)) {
               
                //TODO figure out how to relate mouse position to grid

                grid.GetXY(GlobalFunctions.GetMousePosition(), out int x, out int y);
                Debug.Log(AddMapToGrid(x, y, test));
            }
            if (Input.GetMouseButtonDown(1))
            {

                //TODO figure out how to relate mouse position to grid

                grid.GetXY(GlobalFunctions.GetMousePosition(), out int x, out int y);
                if (grid.GetGridObject(x, y) != null)
                    RemoveMapToGrid(x, y);
            }
        }
        //TODO make sure we are on the grid;
        public bool AddMapToGrid(int x, int y, AugmentGrid addGrid) {
            bool output = true;
            if (width >= addGrid.Width && height >= addGrid.Height)
            {
                for (int i = 0; i < addGrid.Width; i++)
                {
                    for (int j = 0; j < addGrid.Height; j++)
                    {
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
                    grid.GetGridObject(x + i, y + j).SetStatus(addGrid.grid.GetGridObject(i, j).GetStatus);
                    grid.GetGridObject(x + i, y + j).SetFirstCell(new int2(x, y));
                    grid.GetGridObject(x + i, y + j).SetGridRef(addGrid);
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
                        if (grid.GetGridObject(firstCell.x+ i, firstCell.y + j).GetStatus == GridStatus.Occupied)
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
            grid = new GridGeneric<MagicSkillGridObject>(width, height, 1.0f,new Vector3(-20,0,20),(GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y));
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