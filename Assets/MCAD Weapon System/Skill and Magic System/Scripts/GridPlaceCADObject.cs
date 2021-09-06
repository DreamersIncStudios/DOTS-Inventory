using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DreamersInc.MagicSkill {
    public  class GridPlaceCADObject : ScriptableObject, IBaseMagicSkill
    {
        public string Name { get { return SkillSpellname; } }
        [SerializeField] string SkillSpellname;
        public int ID { get { return id; } }
        [SerializeField] int id;
        public string Description { get { return description; } }
        [SerializeField] string description;
        public int Level { get { return level; } }
            [SerializeField] int level;
        public AugmentGrid Grid { get { return grid; } }
        [SerializeField] AugmentGrid grid;
        public int Value { get { return value; } }
        [SerializeField] int value;
        public Classification GetClassification { get { return classification; } }
        [SerializeField] Classification classification;
        public Specialty GetSpecialty { get { return specialty; } }
        [SerializeField] Specialty specialty;
        public int width, height;

        public void AugmentItem(CastingDevice Grid, int x, int y) { }


        public  void RemoveAugment(CastingDevice Grid) { }

        public void Create(string name, int width, int height, int Level, int value)
        {
            this.SkillSpellname = name;
            this.level = Level;
            this.value = value;
            this.grid = new AugmentGrid(width, height, name);
            grid.grid.GetGridObject(1, 0).SetStatus(GridStatus.Open);
            grid.grid.GetGridObject(1, 2).SetStatus(GridStatus.Open);
        }
        public static Dir GetNextDir(Dir dir) {
            switch (dir) {
                default:
                case Dir.Down: return Dir.Left;
                case Dir.Left: return Dir.Up;
                case Dir.Up: return Dir.Right;
                case Dir.Right: return Dir.Down;

            }
        }



    }

    public enum Dir { Down, Up, Left, Right}
}