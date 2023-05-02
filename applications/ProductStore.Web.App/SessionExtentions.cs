using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace ProductStore.Web.App
{
    /// <summary>
    /// Расширение для сессий
    /// </summary>
    public static class SessionExtentions
    {
        private const string key = "cart";

        /// <summary>
        /// Удаление заказа из сессии
        /// </summary>
        /// <param name="session"></param>
        public static void RemoveCart(this ISession session)
        {
            session.Remove(key);
        }

        /// <summary>
        /// Запись заказа в сессию
        /// </summary>
        /// <param name="session">сессия</param>
        /// <param name="value">заказ</param>
        public static void Set(this ISession session, Cart value)
        {
            if (value == null)
                return;

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);

                session.Set(key, stream.ToArray());
            }
        }

        /// <summary>
        /// Попытка получения заказа
        /// </summary>
        /// <param name="session">сессия</param>
        /// <param name="value">инициализируемая модель корзины</param>
        /// <returns>получилось инициализировать заказ или нет</returns>
        public static bool TryGetCart(this ISession session, out Cart value)
        {
            if (session.TryGetValue(key, out byte[] buffer))
            {
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var orderId = reader.ReadInt32();
                    var totalCount = reader.ReadInt32();
                    var totalPrice = reader.ReadDecimal();

                    value = new Cart(orderId, totalCount, totalPrice);
                        
                    return true;
                }
            }

            value = null;
            return false;
        }

    }
}
