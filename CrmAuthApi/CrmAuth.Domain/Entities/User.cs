namespace CrmAuth.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
    }
}