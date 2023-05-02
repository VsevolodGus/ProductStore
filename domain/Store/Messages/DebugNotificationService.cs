using System.Diagnostics;
using System.Net.Mail;
using System.Text;

namespace Store.Messages
{
    public class DebugNotificationService : INotificationService
    {
        public void SendConfirmationCodeToEmail(string email, int code)
        {
            using (var client = new SmtpClient())
            {
                var message = new MailMessage("gusakseva8@gmail.com", email)
                {
                    Subject = "Подтвердите заказ"
                };

                message.Body = "Код подтверждения: {1:0000}";

                SendEmailMessage(client, message);
            }
        }

        public void SendConfirmationCodeToPhone(string cellPhone, int code)
        {
            Debug.WriteLine("CellPhone: {0}, code: {1:0000}", cellPhone, code);
        }

        public void SendOrderNotification(Order order)
        {
            using (var client = new SmtpClient())
            {
                var message = new MailMessage("gusakseva8@gmail.com", order.Email)
                {
                    Subject = "Заказ отправлен"
                };

                var builder = new StringBuilder();
                foreach(var item in order.Items)
                {
                    builder.Append("Товар:{0}   Колво:{1}", item.ProductId, item.Count);
                    builder.AppendLine();
                }

                message.Body = builder.ToString();

                SendEmailMessage(client, message);
            }
        }

        private void SendEmailMessage(SmtpClient client, MailMessage message)
        {
            // to do to realize HOST
            //client.Send(message);
        }
    }
}
