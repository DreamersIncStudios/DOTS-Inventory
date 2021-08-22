using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamersInc.Utils;

namespace DreamersInc.MagicSkill
{
    public class MagicSkillGrid :MonoBehaviour
    {
        private int width, height;
        private float cellsize;
        public GridGeneric<MagicSkillGridObject> grid;

        public MagicSkillGrid( int width = 15, int height =10, float cellsize = 2.5f) {
            grid = new GridGeneric<MagicSkillGridObject>(width, height, cellsize, (GridGeneric<MagicSkillGridObject> g, int x, int y) => new MagicSkillGridObject(g, x, y));
        
        }

    }
}