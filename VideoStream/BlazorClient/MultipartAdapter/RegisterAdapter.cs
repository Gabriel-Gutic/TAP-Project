using System.Text;
using BlazorClient.Data;


namespace BlazorClient.MultipartAdapter
{
    public class RegisterAdapter : IMultipartAdapter
    {
        public MultipartFormDataContent Adapt(object data)
        {
            if (data is not RegisterData)
            {
                throw new ArgumentException("Argument must be a Register Data");
            }

            var registerData = (RegisterData)data;

            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
            if (registerData.ImageData != null)
            {
                var file = new StreamContent(registerData.ImageData.Stream);
                file.Headers.ContentType = registerData.ImageData.MediaType;
                content.Add(file, "Image", registerData.ImageData.Name);
            }

            content.Add(new StringContent(registerData.Username, Encoding.UTF8, "application/json"), "Username");
            content.Add(new StringContent(registerData.Email, Encoding.UTF8, "application/json"), "Email");
            content.Add(new StringContent(registerData.Password, Encoding.UTF8, "application/json"), "Password");
            return content;
        }
    }
}
