using UnityEngine;

namespace Dreamers.CADSystem.Interfaces
{
   [System.Serializable]
    public class GenericCADSlot : ScriptableObject, ICADslot
    {
        
        [SerializeField] private Skills skill;
        [SerializeField] private int reqdLevel;
        [SerializeField] private arrayLayout grid = new arrayLayout() { Size = new Vector2Int(2,2)};

        public string Name { get; set; }
        public Skills SkillName { get { return skill; } }
        public arrayLayout Grid { get { return grid; } }

        public Vector2Int Size { get { return grid.Size; } }

        public int ReqdLevel { get { return reqdLevel; } }
        public Vector2Int InstallPosition { get; set; }
        public bool Installed { get; set; }
        public bool OnCommandLine { get; set; }


        public virtual void AddAbility(SkillsManager skillsManager, Vector2Int Position)
        {
            Installed = true;
            InstallPosition = Position;
            skillsManager.AddNewSkill(skill);
        }

        public virtual void RemoveAbility(SkillsManager skillsManager)
        {
            Installed = false;
            InstallPosition = new Vector2Int();
            OnCommandLine = false;
            skillsManager.RemoveSkill(skill);
        }
    }
    [System.Serializable]
    public class arrayLayout
    {
        [System.Serializable]
        public struct rowData
        {
            public bool[] row;
        }
        public rowData[] rows = new rowData[4];
        public Vector2Int Size= new Vector2Int(2,2);

    }
}