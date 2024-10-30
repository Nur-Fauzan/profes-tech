namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("ItemList", Name = "ItemList-Item-list")]
    [Route("Home/ItemList", Name = "ItemList-Item-list-2")]
    public async Task<IActionResult> ItemList()
    {
        // Create page object
        itemList = new GLOBALS.ItemList(this);
        itemList.Cache = _cache;

        // Run the page
        return await itemList.Run();
    }
}
