using Microsoft.AspNetCore.Http;
using ProductStore.Web.Contract;
using Store;
using Store.Contract;
using System;
using System.Collections.Generic;

namespace ProductSotre.SberKassa
{
    public class SberKassaPaymentService : IPaymentService, IWebContractorService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public SberKassaPaymentService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private HttpRequest Request => httpContextAccessor.HttpContext.Request;

        public string Name => "SberKassa";

        public string Title => "Оплата банковской картой";

        public string GetUri => "/SberKassa/Index/";

        public Form FirstForm(Order order)
        {
            return Form.CreateFirst(Name)
                       .AddParameter("orderId", order.Id.ToString());
        }

        public OrderPayment GetPayment(Form form)
        {
            return new OrderPayment(Name, "Оплатой картой", new Dictionary<string, string>());
        }

        public Form NextForm(int step, IReadOnlyDictionary<string, string> values)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid Sber.Kassa payment step.");

            return Form.CreateLast(Name, step + 1, values);
        }

        public Uri StartSession(IReadOnlyDictionary<string, string> parameters, Uri returnUri)
        {
            var queryString = QueryString.Create(parameters);
            queryString += QueryString.Create("returnUri", returnUri.ToString());

            var builder = new UriBuilder(Request.Scheme, Request.Host.Host)
            {
                Path = "SberKassa/",
                Query = queryString.ToString(),
            };

            if (Request.Host.Port != null)
                builder.Port = Request.Host.Port.Value;

            return builder.Uri;
        }
    }
}