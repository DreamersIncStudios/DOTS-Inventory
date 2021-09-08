
using DreamersInc.Utils;
using Unity.Mathematics;
using UnityEngine;
namespace DreamersInc.MagicSkill {

    [System.Serializable]
    public class MagicSkillGridObject 
    {
        private GridStatus Status;
        private string SkillSpellName;
        private GridGeneric<MagicSkillGridObject> grid;
        private AugmentGrid refernceToSkill;
        private Color gridColor;
        private PlacedAugmentedGrid placedAugmentedGrid;

       private int2 StartPos;
        private int x, y;
        public MagicSkillGridObject(GridGeneric<MagicSkillGridObject> grid, int x, int y, string name = default) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            SkillSpellName = name;
        }

        public void SetStatus(GridStatus status) 
        {
            Status = status;
            grid.TriggerGridObjectChanged(x,y);
        }

        public void SetPlacedAugmentedGrid(PlacedAugmentedGrid grid) {
            this.placedAugmentedGrid = grid;
            SetStatus(GridStatus.Occupied);
            
        }

        public void SetName(string name) {
            SkillSpellName = name;
            grid.TriggerGridObjectChanged(x, y);
        }
        public void SetFirstCell(int2 location) {
            StartPos = location;
            grid.TriggerGridObjectChanged(x, y);
        }
        public void SetGridRef(AugmentGrid grid) {
            Status = GridStatus.Occupied;
            refernceToSkill = grid;
            this.grid.TriggerGridObjectChanged(x, y);
        }
        public void SetGridColor(Color color) {
            this.gridColor = color;
            grid.TriggerGridObjectChanged(x, y);

        }
        public void ClearGridColor() {
            this.gridColor = Color.white;
            grid.TriggerGridObjectChanged(x, y);

        }

        public void Reset() {
            placedAugmentedGrid = null;
            gridColor = Color.white;
            refernceToSkill = null;
            SetStatus(GridStatus.Open);
            //grid.TriggerGridObjectChanged(x, y);
        }

        public bool CanPlace() {
            return Status == GridStatus.Open;
        }
        public PlacedAugmentedGrid GetPlacedAugmentedGrid() {
            return placedAugmentedGrid;
        }
        public GridStatus GetStatus
        {
            get
            {
                return Status;
            }
        }
        public string GetName
        {
            get { return SkillSpellName;}
        }
        public int2 GetFirstCell { get { return StartPos; } }
        public AugmentGrid GetSkillMap { get { return refernceToSkill; } }
        public override string ToString()
        {
            return x+", " + y+ "\n" + Status+ "\n" + SkillSpellName;
        }
    }

    public enum GridStatus { 
       Open, Blocked, Occupied,
    }
}