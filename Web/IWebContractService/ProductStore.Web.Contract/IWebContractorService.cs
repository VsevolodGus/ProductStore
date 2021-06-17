namespace ProductStore.Web.Contract
{
    public interface IWebContractorService
    {
        string UniqueCode { get; }

        string GetUri { get; }
    }
}
