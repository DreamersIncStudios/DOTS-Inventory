using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DreamersInc.MagicSkill
{
    public interface IBaseMagicSkill
    {
        string Name { get; }
        int ID { get; }
        string Description { get; }
        int Level { get; }
        AugmentGrid Grid { get; }
        int Value{ get; }
        Classification GetClassification { get; }
        Specialty GetSpecialty { get; }

        void Create( string name, int width, int Height, int Level, int value);
        void AugmentItem(MagicSkillGrid Grid, int x , int y);
        void RemoveAugment(MagicSkillGrid Grid);
    }
    public enum Classification { 
        Standard, Intermediate, Advanced, Expert, Virtuoso, Grandmaster, god
    }
    public enum Specialty { 
    none, Weapon, Bloodline, Cursed
    }
}