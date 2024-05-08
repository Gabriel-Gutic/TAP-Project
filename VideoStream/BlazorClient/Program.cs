using BlazorClient.Contracts;
using BlazorClient.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorClient
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7027/") });

			// Services
			builder.Services.AddScoped<IHttpService, HttpService>();
			builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
			
			builder.Services.AddScoped<IUserService, UserService>();
			builder.Services.AddScoped<IAuthService, AuthService>();
			builder.Services.AddScoped<IVideoService, VideoService>();
			builder.Services.AddScoped<IViewService, ViewService>();
			builder.Services.AddScoped<ICommentService, CommentService>();

			builder.Services
				.AddBlazorise()
				.AddFontAwesomeIcons()
				.AddBootstrapProviders();

            await builder.Build().RunAsync();
		}
	}
}
