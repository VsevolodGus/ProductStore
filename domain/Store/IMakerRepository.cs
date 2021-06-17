namespace Store
{
    public interface IMakerRepository
    {
        Maker GetById(int id);

        Maker GetByTitle(string title);
    }
}
