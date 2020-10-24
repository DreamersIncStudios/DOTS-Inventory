using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.CADSystem
{
    public sealed class SkillsManager : MonoBehaviour
    {
        public List<Skills> UsableSkills;

        private void Awake()
        {
            UsableSkills = new List<Skills>();
        }

        public void AddNewSkill (Skills Addition)
        {
            if (!UsableSkills.Contains(Addition))
            {
                UsableSkills.Add(Addition);
            }
            else { Debug.LogWarning("Can not add duplicates at this time"); }
        }
        public void RemoveSkill(Skills skill) {
            if (UsableSkills.Contains(skill))
            {
                UsableSkills.Remove(skill);
            }
            else { Debug.LogWarning("Skill does not existi in this List of active skills"); }

        }
        // Start is called before the first frame update
        void Start()
        {

        }


    }
}