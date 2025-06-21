namespace Applications.Interfaces
{
    public interface IUserInfoReader
    {
        public string? TenantId { get;}
        public string? UserId { get;}
        public string? UserName { get;}
    }
}
