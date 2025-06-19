namespace Applications.Interfaces
{
    public interface IUserInfoReader
    {
        public string? TenantID { get;}
        public string? UserID { get;}
        public string? UserName { get;}
    }
}
