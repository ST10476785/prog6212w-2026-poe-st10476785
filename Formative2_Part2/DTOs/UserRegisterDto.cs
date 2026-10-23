namespace Formative2_Part2.DTOs
{
    public class UserRegisterDto
    {

        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int UserTypeId { get; set; }
    }
}
