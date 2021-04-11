namespace Stats
{
    public interface IBaseCharacter
    {
        Attributes GetPrimaryAttribute(int index);
        void StatUpdate();
        int Level { get; set; }
    }
}