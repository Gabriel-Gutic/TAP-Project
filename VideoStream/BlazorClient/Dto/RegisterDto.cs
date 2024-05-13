namespace BlazorClient.Dto
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public MultipartFormDataContent? Image { get; set; }
    }
}
