namespace SqliteDAL.DAL;

public class Account : ModelBase
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
