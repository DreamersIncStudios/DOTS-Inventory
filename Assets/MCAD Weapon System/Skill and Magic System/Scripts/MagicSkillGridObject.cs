
using DreamersInc.Utils;
using Unity.Mathematics;
namespace DreamersInc.MagicSkill {

    [System.Serializable]
    public class MagicSkillGridObject 
    {
        private GridStatus Status;
        private string SkillSpellName;
        private GridGeneric<MagicSkillGridObject> grid;
        private AugmentGrid refernceToSkill;
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
        public void SetName(string name) {
            SkillSpellName = name;
            grid.TriggerGridObjectChanged(x, y);
        }
        public void SetFirstCell(int2 location) {
            StartPos = location;
            grid.TriggerGridObjectChanged(x, y);
        }
        public void SetGridRef(AugmentGrid grid) {
            refernceToSkill = grid;
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
            return Status.ToString();
        }
    }

    public enum GridStatus { 
       Open, Blocked, Occupied,
    }
}