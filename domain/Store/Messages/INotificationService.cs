namespace Store.Messages
{
    public interface INotificationService
    {
        /// <summary>
        /// Отправка кода подтверждения на телефон
        /// </summary>
        /// <param name="cellPhone">номер телефона</param>
        /// <param name="code">код подтверждения</param>
        void SendConfirmationCodeToPhone(string cellPhone, int code);

        /// <summary>
        /// Отправка кода подтверждения на телефон
        /// </summary>
        /// <param name="cellPhone">номер телефона</param>
        /// <param name="code">код подтверждения</param>
        void SendConfirmationCodeToEmail(string email, int code);

        /// <summary>
        /// Отправка чека на почту
        /// </summary>
        /// <param name="order">модель заказа</param>
        void SendOrderNotification(Order order);
    }
}
