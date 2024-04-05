using GameShop.Application.Models.Scryfall;
using GameShop.Application.Models.Tcg;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GameShop.Application.Actions;
public class ScryfallAction
{
    //Notes: Discuss Test driven development api side
    // ExternalIds on user, and scryfall card id
    public async Task InitializeAsync(List<ImportModel> models)
    {
        var client = GetClient();
        var summaries = await GetCardSummariesAsync(client, models);

        var temp = JsonConvert.SerializeObject(summaries);

        //TODO: Merge TcgModel (models) properties with Scryfall properties (summaries) into our dto/dbo model
        //Set it to the context 
    }

    private async Task<List<Scry_Card>> GetCardSummariesAsync(HttpClient client, List<ImportModel> models)
    {
        //TODO: add error handling for nulls and fail responses 
        var responses = new List<Scry_Card>();
        for (var i = 0; i < Math.Ceiling((decimal)models.Count / 75); i++)
        {
            var request = new PostCard_Request();
            foreach (var x in models.Skip(i * 75).Take(75))
            {
                request.identifiers.Add(new CardIdentifier_Request(x.Name, x.SetCode));
            }
            var task = client.PostAsJsonAsync("cards/collection", request);
            var result = await task.Result.Content.ReadAsStringAsync();
            responses.AddRange(JsonConvert.DeserializeObject<PostCard_Response>(result).Data);
            await DelayAsync();
        }

        return responses;
    }

    private static async Task DelayAsync() => await Task.Delay(100);

    private HttpClient GetClient()
    {
        return new HttpClient()
        {
            BaseAddress = new Uri("https://api.scryfall.com/"),
        };
    }
}
