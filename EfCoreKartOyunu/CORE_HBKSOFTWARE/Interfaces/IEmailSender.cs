using CORE_HBKSOFTWARE.Models;
using System.Threading.Tasks;

namespace CORE_HBKSOFTWARE.Interfaces
{
    public interface IEmailSender
    {
        Task EmailSendAsync(MessageModel messageModel);
        Task EmailSendWithMimeAsync(MessageModel messageModel);
    }
}