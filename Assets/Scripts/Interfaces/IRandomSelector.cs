namespace TopDownHordes.Interfaces
{
    public interface IRandomSelector<out T>
    {
        T GetRandomItem();
    }
}