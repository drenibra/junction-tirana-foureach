using RFMoneyMatters.Models;

namespace RFMoneyMatters.Services
{
    public interface ITokenService
    {
        string CreateToken(Person user);

    }
}