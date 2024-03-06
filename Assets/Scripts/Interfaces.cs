public interface IRepairable
{
    public bool Whole { get; }
    public void Repair();
}
