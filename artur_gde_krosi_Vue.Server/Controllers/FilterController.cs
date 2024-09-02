using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using artur_gde_krosi_Vue.Server.Services.ControlerService;
using artur_gde_krosi_Vue.Server.Models.ContelerViews.FilterViews;
using Microsoft.AspNetCore.Authorization;

namespace artur_gde_krosi_Vue.Server.Controller;

[Route("api/[controller]/")]
[ApiController]
public class FilterController : ControllerBase
{
    private readonly FilterService _filterService;

    public FilterController( FilterService filterService)
    {
        _filterService = filterService;
    }

    [HttpGet]
    [Route("Brends")]
    public async Task<IActionResult> GetBrends()
    {
        List<Models.BdModel.Brend> brend = await _filterService.GetBrends();
        return Ok(brend);
    }
    [HttpGet]
    [Route("ModelKrosovocks")]
    public async Task<IActionResult> GetModelKrosovocks([FromHeader] List<string> brendsIds = null)
    {
        List<ModelKrosovoksView> modelKrosovocks = await _filterService.GetModelKrosovocks(brendsIds);
        return Ok(modelKrosovocks);
    }
    [HttpGet]
    [Route("ShoeSizes")]
    public async Task<IActionResult> GetShoeSizes()
    {
        List<double> shoeSizes = await _filterService.GetShoeSizes();
        return Ok(shoeSizes);
    }
    [HttpGet]
    [Route("MinMaxPrise")]
    public async Task<IActionResult> MinMaxPrise()
    {
        MinMaxPriseView minMaxPriseView = await _filterService.MinMaxPrise();
        return Ok(minMaxPriseView);
    }
}

