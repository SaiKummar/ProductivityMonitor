namespace ProductivityMonitor.Contracts
{
    public interface IJwtTokenManager
    {
        string GenerateToken(string username,IList<string> roles);
    }
}