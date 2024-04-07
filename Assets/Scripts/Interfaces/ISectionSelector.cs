namespace TopDownHordes.Interfaces
{
    public interface ISectionSelector<out T>
    {
        T GetRandomItem();
    }
}