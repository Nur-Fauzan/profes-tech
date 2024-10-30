namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("ItemsList/{ID?}", Name = "ItemsList-Items-list")]
    [Route("Home/ItemsList/{ID?}", Name = "ItemsList-Items-list-2")]
    public async Task<IActionResult> ItemsList()
    {
        // Create page object
        itemsList = new GLOBALS.ItemsList(this);
        itemsList.Cache = _cache;

        // Run the page
        return await itemsList.Run();
    }

    // delete
    [Route("ItemsDelete/{ID?}", Name = "ItemsDelete-Items-delete")]
    [Route("Home/ItemsDelete/{ID?}", Name = "ItemsDelete-Items-delete-2")]
    public async Task<IActionResult> ItemsDelete()
    {
        // Create page object
        itemsDelete = new GLOBALS.ItemsDelete(this);

        // Run the page
        return await itemsDelete.Run();
    }
}
