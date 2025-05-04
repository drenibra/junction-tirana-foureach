namespace RFMoneyMatters.CurrentUser.Abstraction
{
    public interface ICurrentUser
    {
        public string Email { get; set; }
        //  public double Longitude { get; set; }
        //  public double Latitude { get; set; }
        //  public int LanguageId { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Id { get; set; }
        bool IsInRole(string Role);

    }
}
