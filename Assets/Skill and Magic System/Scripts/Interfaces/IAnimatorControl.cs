using Stats;

namespace Dreamers.SkillMagicSystem.Interfaces
{
    public interface IAnimatorControl
    {

        string TriggerKey { get; }
        int ManaCost { get; }
        TargetType TargetWhat { get; }
        float ResetTimer { get; set; }
        float ResetTime { get; }
        
        void TriggerAnimation();
        void UseSkill(BaseCharacter character);

    }

    public enum TargetType { 
        Ally, Enemy, All_Allies, All_Enemies, All, AoE
    }
}