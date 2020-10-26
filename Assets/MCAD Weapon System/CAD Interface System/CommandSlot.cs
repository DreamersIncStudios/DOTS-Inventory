using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Dreamers.CADSystem.Interfaces
{
    [CreateAssetMenu(fileName = "Command Slot Data", menuName = "CAD System/Command Slot", order = 2)]

    public class CommandSlot : GenericCADSlot, ICommandLineAbility
    {
        public bool IsCommandLineAbility { get { return true; } }

        public override void AddAbility(SkillsManager skillsManager, Vector2Int Position)
        {
            Installed = true;
            InstallPosition = Position;
            if (OnCommandLine)
            { skillsManager.AddNewSkill(SkillName); }
        }

    }
}