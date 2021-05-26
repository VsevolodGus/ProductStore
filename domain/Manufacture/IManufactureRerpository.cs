namespace StoreManufacture
{
    public interface IManufactureRerpository
    {
        Manufacture GetById(int id);

        Manufacture GetByTitle(string title);
    }
}
