using System.Threading.Tasks;

namespace CORE_HBKSOFTWARE.Interfaces
{
    public interface IRecaptchaValidator
    {
        Task<bool> RecaptchaVerifyAsync(string token);
    }
}