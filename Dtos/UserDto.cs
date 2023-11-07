
namespace API.Dtos
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }= null;
        public string Avartar { get; set; }= null;
        public string Password { get; set; }= null;
        public string Type { get; set; }= null;
        public int? CustomerId { get; set; }
    }
}