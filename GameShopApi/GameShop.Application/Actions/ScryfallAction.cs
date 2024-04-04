using GameShop.Application.Models.Scryfall;
using GameShop.Application.Models.Tcg;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace GameShop.Application.Actions;
public class ScryfallAction
{
    //Notes: Discuss Test driven development api side
    // ExternalIds on user, and scryflal card id
    public async Task InitializeAsync(List<CsvImportModel> models)
    {
        var client = GetClient();
        var summaries = await GetCardSummariesAsync(client, models);

        //Create our model and map to it from summaries
        //var dtos = new List<CardDto>();
        //var dtos = summaries ...

        //POST to our API for importing 
    }

    private PostCard_Request GetCardsRequest(List<CsvImportModel> models)
    {
        //TODO: Do batches of 75 max
        var request = new PostCard_Request();
        foreach (var x in models)
        {
            request.identifiers.Add(new CardIdentifier_Request(x.Name, x.SetCode));
        }
        return request;
    }

    private async Task GetAllCardNames(HttpClient client)
    {
        //TODO: Possibly use this for validations later. May not be useful. 
        var task = client.GetAsync("https://api.scryfall.com/catalog/card-names");
        var result = await task.Result.Content.ReadAsStringAsync();
    }

    private async Task<List<Scry_Card>> GetCardSummariesAsync(HttpClient client, List<CsvImportModel> models)
    {
        //TODO: add error handling for nulls and fail responses 
        var request = GetCardsRequest(models);
        request.identifiers = new List<CardIdentifier_Request> { request.identifiers[0] };
        var task = client.PostAsJsonAsync("cards/collection", request);
        var result = await task.Result.Content.ReadAsStringAsync();

        dynamic temp = JsonConvert.DeserializeObject(result);
        var first = JsonConvert.SerializeObject(temp, Formatting.Indented);

        dynamic temp1 = JsonConvert.DeserializeObject<PostCard_Response>(result);
        var second = JsonConvert.SerializeObject(temp1, Formatting.Indented);

        if (first != second)
        {
            //TODO fix model so that this is at least almost true 
            throw new Exception();
        }

        return JsonConvert.DeserializeObject<PostCard_Response>(result).Data;
    }

    private HttpClient GetClient()
    {
        return new HttpClient()
        {
            BaseAddress = new Uri("https://api.scryfall.com/"),
        };
    }
}
