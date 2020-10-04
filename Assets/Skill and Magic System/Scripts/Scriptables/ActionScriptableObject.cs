using Dreamers.SkillMagicSystem.Interfaces;
using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dreamers.SkillMagicSystem
{
    public abstract class ActionScriptableObject : ScriptableObject, IAnimatorControl
    {
        [SerializeField] private string triggerKey;
        public string TriggerKey { get { return triggerKey; } }

        [SerializeField] private int manaCost;
        public int ManaCost { get { return manaCost; } }
        [SerializeField] private TargetType targetWhat;
        public TargetType TargetWhat { get { return targetWhat; } }
        public float ResetTime => throw new System.NotImplementedException();

        public float ResetTimer { get; set; }
        public bool InCooldown { get { return ResetTimer >= 0.0f; } } 

        public void TriggerAnimation()
        {
            throw new System.NotImplementedException();
        }

        public virtual void UseSkill(BaseCharacter character)
        {
            if (character.CurMana > ManaCost && !InCooldown) {
                TriggerAnimation();
                character.DecreaseMana(ManaCost,0,0);
                Debug.Log("Skill Trigger");
            }

        }
    }
}
