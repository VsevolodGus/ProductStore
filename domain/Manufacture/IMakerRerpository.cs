namespace StoreManufacture
{
    public interface IMakerRerpository
    {
        Maker GetById(int id);

        Maker GetByTitle(string title);
    }
}
