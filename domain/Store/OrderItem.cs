using System;


namespace Store
{
    public class OrderItem
    {
        public int ProductId;

        private int count;

        public int Count
        {
            get => count;
            set 
            {
                ThrowExceptionForNoCorrectCount(value);

                count = value;
            }
        }

        public decimal Price;

        public OrderItem(int id, int count, decimal price)
        {
            ProductId = id;
            Count = count;
            Price = price;
        }

        private static void ThrowExceptionForNoCorrectCount(int count)
        {
            if (count <= 0)
                throw new ArgumentException("no correct value count");
        }
    }
}
