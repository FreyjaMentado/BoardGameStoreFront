using GameShop.Application.Actions;
using GameShop.Application.Models.Tcg;
using Microsoft.AspNetCore.Mvc;

namespace GameShop.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : Controller
{
    private readonly ILogger<CardsController> _logger;
    public CardsController(ILogger<CardsController> logger) => _logger = logger;

    [HttpPost("import")]
    public async Task<ActionResult> Import([FromBody] List<ImportModel> models)
    {
        //TODO: if any model has a property that is null, alert user, but continue upload?

        var action = new ScryfallAction();
        await action.InitializeAsync(models);

        return Ok();
    }
}
