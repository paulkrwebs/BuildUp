namespace BuildUp
{
    public interface IPropertyMapper
    {
        void Map<TFrom, TTo>(TFrom @from, TTo to);
    }
}