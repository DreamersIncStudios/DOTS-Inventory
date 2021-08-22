using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamersInc.Utils;

namespace DreamersInc.MagicSkill {
    public class MagicSkillGridObject 
    {
        private GridStatus Status;
        private GridGeneric<MagicSkillGridObject> grid;

        private int x, y;
        public MagicSkillGridObject(GridGeneric<MagicSkillGridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }

        public void SetStatus(GridStatus status) 
        {
            this.Status = status;
        }
        public GridStatus GetStatus() { 
            return Status;
        }

        public override string ToString()
        {
            return Status.ToString();
        }
    }

    public enum GridStatus { 
       Open, Blocked, Occupied,
    }
}