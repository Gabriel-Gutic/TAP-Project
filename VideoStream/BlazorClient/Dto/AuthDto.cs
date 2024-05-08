namespace BlazorClient.Dto
{
    public class AuthDto
    {
        public string Username {  get; set; }
        public string Password { get; set; }

        public AuthDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
