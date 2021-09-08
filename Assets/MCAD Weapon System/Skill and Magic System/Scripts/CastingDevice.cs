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
        public Vector2 GetDimensions => new Vector2(width, height);
        public GridPlaceCADSO test;
        public void Setup ( int width = 15, int height =10, float cellsize = 5f) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, cellsize, (GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y), true
                );
            this.width = width;
            this.height = height;
            this.cellsize = cellsize;
            
        }
        private void Start()
        {
            test = (GridPlaceCADSO)ScriptableObject.CreateInstance(typeof(GridPlaceCADSO));
            test.Create("FireBall", 2, 3, 1, 100);
            Setup();
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                grid.GetXY(GlobalFunctions.GetMousePosition(), out Vector2Int pos);
                AddMapToGrid(pos, test.Grid);
            }

            if (Input.GetMouseButtonDown(1))
            {
                grid.GetXY(GlobalFunctions.GetMousePosition(), out Vector2Int pos);
                RemoveMapToGrid(pos);
            }

            if (Input.GetKeyDown(KeyCode.R))
                test.Grid.dir = GridPlaceCADSO.GetNextDir(test.Grid.dir);
        }
        public bool AddMapToGrid(Vector2Int input, AugmentGrid addGrid) {
            List<Vector2Int> gridPositionList = addGrid.GetGridPositionList(input, test.Grid.dir);
            bool canPlace = true;
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition).CanPlace())
                {
                    canPlace = false;
                }
            }
            if (canPlace) {
                //TODO Add Visualization Implementation 
                PlacedAugmentedGrid placed = PlacedAugmentedGrid.Create(input, addGrid);
                foreach (var gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition).SetPlacedAugmentedGrid(placed);
                }
            }

            return canPlace;
        }


        public void RemoveMapToGrid(Vector2Int input)
        {
            int x = input.x;
            int y = input.y;

            List<Vector2Int> gridPositionList = new List<Vector2Int>();
               gridPositionList = grid.GetGridObject(x, y).GetPlacedAugmentedGrid().GetGridPositionList();
            foreach (Vector2Int vector in gridPositionList)
            {
                grid.GetGridObject(vector).Reset();
            }

        }

    }


    [System.Serializable]
    public class AugmentGrid {
        public int Width { get; private set; }
        public int Height{ get; private set; }
        public Dir dir;

        
        
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

        public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir= Dir.Down)
        {
            Debug.Log(dir);
            List<Vector2Int> gridPositionList = new List<Vector2Int>();
            switch (dir)
            {
                default:
                case Dir.Down:
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (!grid.GetGridObject(x, y).CanPlace())
                                gridPositionList.Add(offset - new Vector2Int(x, y-Height+1));
                        }
                    }
                    break;
                case Dir.Up:
                    for (int x = 0; x <Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if(!grid.GetGridObject(x,y).CanPlace())
                            gridPositionList.Add(offset + new Vector2Int(x, y));
                        }
                    }
                    break;
                case Dir.Left:
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (!grid.GetGridObject(x, y).CanPlace())
                                gridPositionList.Add(offset + new Vector2Int(y, x));
                        }
                    }
                    break;
                case Dir.Right:
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if (!grid.GetGridObject(x, y).CanPlace())
                                gridPositionList.Add(offset - new Vector2Int(y-Width, x));
                        }
                    }
                    break;
            }

            return gridPositionList;
        }

    }
}