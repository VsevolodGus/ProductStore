using System.Collections.Generic;

namespace ProductStore.Web.App
{
    public class ConfimationModel
    {
        public int OrderId { get; set; }

        public string CellPhone { get; set; }

        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}