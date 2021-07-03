using System.Collections.Generic;

namespace ProductStore.Web.App
{
    public class DeliveryModel
    {
        public int OrderId { get; set; }

        public Dictionary<string, string> Methods { get; set; }
    }
}