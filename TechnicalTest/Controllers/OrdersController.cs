namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("OrdersList/{ID?}", Name = "OrdersList-Orders-list")]
    [Route("Home/OrdersList/{ID?}", Name = "OrdersList-Orders-list-2")]
    public async Task<IActionResult> OrdersList()
    {
        // Create page object
        ordersList = new GLOBALS.OrdersList(this);
        ordersList.Cache = _cache;

        // Run the page
        return await ordersList.Run();
    }

    // add
    [Route("OrdersAdd/{ID?}", Name = "OrdersAdd-Orders-add")]
    [Route("Home/OrdersAdd/{ID?}", Name = "OrdersAdd-Orders-add-2")]
    public async Task<IActionResult> OrdersAdd()
    {
        // Create page object
        ordersAdd = new GLOBALS.OrdersAdd(this);

        // Run the page
        return await ordersAdd.Run();
    }

    // view
    [Route("OrdersView/{ID?}", Name = "OrdersView-Orders-view")]
    [Route("Home/OrdersView/{ID?}", Name = "OrdersView-Orders-view-2")]
    public async Task<IActionResult> OrdersView()
    {
        // Create page object
        ordersView = new GLOBALS.OrdersView(this);

        // Run the page
        return await ordersView.Run();
    }

    // edit
    [Route("OrdersEdit/{ID?}", Name = "OrdersEdit-Orders-edit")]
    [Route("Home/OrdersEdit/{ID?}", Name = "OrdersEdit-Orders-edit-2")]
    public async Task<IActionResult> OrdersEdit()
    {
        // Create page object
        ordersEdit = new GLOBALS.OrdersEdit(this);

        // Run the page
        return await ordersEdit.Run();
    }

    // delete
    [Route("OrdersDelete/{ID?}", Name = "OrdersDelete-Orders-delete")]
    [Route("Home/OrdersDelete/{ID?}", Name = "OrdersDelete-Orders-delete-2")]
    public async Task<IActionResult> OrdersDelete()
    {
        // Create page object
        ordersDelete = new GLOBALS.OrdersDelete(this);

        // Run the page
        return await ordersDelete.Run();
    }
}
