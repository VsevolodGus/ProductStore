using System;
using System.Collections.Generic;

namespace ProductStore.Web.Contract
{
    public interface IWebContractorService
    {
        string Name { get; }

        Uri StartSession(IReadOnlyDictionary<string, string> parameters, Uri returnUri);
    }
}
