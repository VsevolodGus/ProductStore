using System;
using System.Collections.Generic;
using System.Linq;
using Store;

namespace Store.Contract
{
    public class DeliveryLocations : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cites = new Dictionary<string, string>
        {
            { "1", "Москва" },
            { "2", "Санкт-Петербург" },
        };

        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> locations = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",
                new Dictionary<string, string>
                {
                    { "1", "Казанский вокзал" },
                    { "2", "Охотный ряд" },
                    { "3", "Савёловский рынок" },
                }
            },
            {
                "2",
                new Dictionary<string, string>
                {
                    { "4", "Московский вокзал" },
                    { "5", "Гостиный двор" },
                    { "6", "Петропавловская крепость" },
                }
            }
        };

        public string UniqueCode => "Postamate";

        public string Title => "Доставка через пункты доставки по России";

        public Form CreateForm(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            return new Form(UniqueCode, order.Id, 1, false, 
                            new[] { new SelectionField("Город", "city", "1", cites) });
        }

        public OrderDelivery GetDelivery(Form form)
        {
            if (UniqueCode != form.UniqueCode || !form.IsFinal)
                throw new ArgumentException("Invalid form");

            var cityId = form.Fields.First(city => city.Name == "city").Value;
            var cityName = cites[cityId];
            var postamateId = form.Fields.First(postamate => postamate.Name == "postamate").Value;
            var postamateName = locations[cityId][postamateId];

            var parametrs = new Dictionary<string, string>
            {
                { nameof(cityId), cityId  },
                { nameof(cityName), cityName},
                { nameof(postamateId), postamateId },
                { nameof(postamateName), postamateName},
            };

            var description = $"Город:{cityName}\n Постамат:{postamateName}";

            return new OrderDelivery(UniqueCode,description,150m,parametrs);
        }

        public Form MoveNextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step == 1)
            {
                if (values["city"] == "1")
                    return new Form(UniqueCode,orderId,2,false,new Field[]
                    {
                        new HiddenField("Город", "city", "1"),
                        new SelectionField("Постамат", "postamate", "1", locations["1"])
                    });
                else if (values["city"] == "2")
                    return new Form(UniqueCode, orderId, 2, false, new Field[]
                    {
                        new HiddenField("Город", "city", "2"),
                        new SelectionField("Постамат", "postamate", "2", locations["2"]),
                    });
                else
                    throw new ArgumentException("Invalid postamate city");
            }
            else if (step == 2)
            {
                return new Form(UniqueCode, orderId, 3, true, new Field[]
                {
                    new HiddenField("Город", "city", values["city"]),
                    new HiddenField("Постамат", "postamate", values["postamate"]),
                });
            }
            else
                throw new ArgumentException("Invalid value step");
        }
    }
}
