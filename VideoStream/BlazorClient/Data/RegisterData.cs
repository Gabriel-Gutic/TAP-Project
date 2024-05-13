using System.Security.Cryptography.X509Certificates;

namespace BlazorClient.Data
{
    public class RegisterData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepetPassword { get; set; }
        
        public FileUploadData? ImageData { get; set; }

        public RegisterData() 
        {
            Username = "";
            Email = "";
            Password = "";
            RepetPassword = "";
        }
    }
}
