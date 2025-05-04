using RFMoneyMatters.CurrentUser.Abstraction;

namespace RFMoneyMatters.CurrentUser
{
    public class CurrentUser : ICurrentUser
    {
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
        public string Id { get; set; }
        public bool IsInRole(string role) => Roles.Contains(role);
    }
}
