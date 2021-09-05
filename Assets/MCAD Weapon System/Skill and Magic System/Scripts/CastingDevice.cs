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
        public GridPlaceCADObject test;
        public void Setup ( int width = 15, int height =10, float cellsize = 5f) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, cellsize, (GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y), true
                );
            this.width = width;
            this.height = height;
            this.cellsize = cellsize;
            
        }
        private void Start()
        {
            test = (GridPlaceCADObject)ScriptableObject.CreateInstance(typeof(GridPlaceCADObject));
            test.Create("FireBall", 3, 3, 1, 100);
            Setup();
        }

        public void Update()
        {
            if (Input.GetMouseButton(0)) {
                grid.GetXY(GlobalFunctions.GetMousePosition(), out Vector2Int pos);
                AddMapToGrid(pos, test.Grid);
            }
        }
        public bool AddMapToGrid(Vector2Int input, AugmentGrid addGrid) {
            List<Vector2Int> gridPositionList = addGrid.GetGridPositionList(input);
            bool canPlace = true;
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition).CanPlace())
                {
                    canPlace = false;
                }
            }
            if (canPlace) {
                foreach (var gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition).SetGridRef(addGrid);
                }
            }

            return canPlace;
        }
        //public bool RemoveMapToGrid(int x, int y)
        //{

        //}

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

        public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir= Dir.Down)
        {
            List<Vector2Int> gridPositionList = new List<Vector2Int>();
            switch (dir)
            {
                default:
                case Dir.Down:
                case Dir.Up:
                    for (int x = 0; x < Width; x++)
                    {
                        for (int y = 0; y < Height; y++)
                        {
                            if(!grid.GetGridObject(x,y).CanPlace())
                            gridPositionList.Add(offset + new Vector2Int(x, y));
                        }
                    }
                    break;
                case Dir.Left:
                case Dir.Right:
                    for (int x = 0; x < Height; x++)
                    {
                        for (int y = 0; y < Width; y++)
                        {
                            gridPositionList.Add(offset + new Vector2Int(x, y));
                        }
                    }
                    break;
            }

            return gridPositionList;
        }

    }
}