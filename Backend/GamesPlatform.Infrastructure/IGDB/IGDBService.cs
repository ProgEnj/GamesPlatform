using System.Net.Http.Headers;
using System.Net.Http.Json;
using GamesPlatform.Infrastructure.IGDB.DTOs;
using GamesPlatform.Infrastructure.IGDB.Models;
using Microsoft.Extensions.Configuration;

namespace GamesPlatform.Infrastructure.IGDB;

// TODO: List<T> in SendRequestAsync as api always returns lists
// TODO: Error handling
// TODO: Filling fields for IGDBGame model
// TODO: Images handling
public class IGDBService : IIGDBService
{
    private string AccessToken { get; set; }
    private DateTime TokenExpires { get; set; }
    private HttpClient _sharedClient;
    private IConfiguration _config;
    private readonly string ClientId;
    private readonly string ClientSecret;

    private string gameQueryFields = 
        "fields name," +
        "alternative_names.*," +
        "version_title," +
        "cover.*," +
        "summary," +
        "platforms.*, platforms.platform_logo.*," +
        "language_supports.*,language_supports.language.*," +
        "genres.*," +
        "involved_companies.*, involved_companies.company.*, involved_companies.company.logo.*," +
        "first_release_date;";

    public IGDBService(IConfiguration config)
    {
        this._config = config;
        this._sharedClient = new HttpClient();
        this._sharedClient.BaseAddress = new Uri("https://api.igdb.com/");
        
        this.ClientId = _config.GetValue<string>("IGDBCreds:ClientId");
        this.ClientSecret = _config.GetValue<string>("IGDBCreds:ClientSecret");
    }

    public async Task<List<T>> SendRequestAsync<T>(string url, string query)
    {
        await this.RefreshToken();

        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(query);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.AccessToken);
        request.Headers.Add("Client-ID", ClientId);

        var response = await this._sharedClient.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<List<T>>();
    }

    public async Task RefreshToken()
    {
        if (DateTime.Now > TokenExpires)
        {
           await GetAuthTokenAsync(); 
        }
    }
    
    public async Task GetAuthTokenAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, 
            $"https://id.twitch.tv/oauth2/token?" +
            $"client_id={ClientId}&" +
            $"client_secret={ClientSecret}&" +
            $"grant_type=client_credentials");
        HttpClient client = new HttpClient();

        var response = await client.SendAsync(request);
        var authData = await response.Content.ReadFromJsonAsync<IGDBAuthResponseDTO>();
        this.AccessToken = authData.AccessToken;
        this.TokenExpires = DateTime.Now.AddSeconds(authData.ExpiresIn);
    }
    
    public async Task<IGDBGame> GetGame(int id)
    {
        return (await this.SendRequestAsync<IGDBGame>("/v4/games", gameQueryFields + $"where id = {id};")).First();
    }
    
    public async Task<IGDBGenre> GetGenre(int id)
    {
        return (await this.SendRequestAsync<IGDBGenre>("/v4/genres", $"fields *; where id = {id}")).First();
    }
}