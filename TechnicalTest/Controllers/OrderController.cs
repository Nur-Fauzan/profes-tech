namespace ASPNETMaker2023.Controllers;

// Partial class
public partial class HomeController : Controller
{
    // list
    [Route("OrderList/{ID?}", Name = "OrderList-Order-list")]
    [Route("Home/OrderList/{ID?}", Name = "OrderList-Order-list-2")]
    public async Task<IActionResult> OrderList()
    {
        // Create page object
        orderList = new GLOBALS.OrderList(this);
        orderList.Cache = _cache;

        // Run the page
        return await orderList.Run();
    }

    // add
    [Route("OrderAdd/{ID?}", Name = "OrderAdd-Order-add")]
    [Route("Home/OrderAdd/{ID?}", Name = "OrderAdd-Order-add-2")]
    public async Task<IActionResult> OrderAdd()
    {
        // Create page object
        orderAdd = new GLOBALS.OrderAdd(this);

        // Run the page
        return await orderAdd.Run();
    }

    // view
    [Route("OrderView/{ID?}", Name = "OrderView-Order-view")]
    [Route("Home/OrderView/{ID?}", Name = "OrderView-Order-view-2")]
    public async Task<IActionResult> OrderView()
    {
        // Create page object
        orderView = new GLOBALS.OrderView(this);

        // Run the page
        return await orderView.Run();
    }

    // edit
    [Route("OrderEdit/{ID?}", Name = "OrderEdit-Order-edit")]
    [Route("Home/OrderEdit/{ID?}", Name = "OrderEdit-Order-edit-2")]
    public async Task<IActionResult> OrderEdit()
    {
        // Create page object
        orderEdit = new GLOBALS.OrderEdit(this);

        // Run the page
        return await orderEdit.Run();
    }

    // delete
    [Route("OrderDelete/{ID?}", Name = "OrderDelete-Order-delete")]
    [Route("Home/OrderDelete/{ID?}", Name = "OrderDelete-Order-delete-2")]
    public async Task<IActionResult> OrderDelete()
    {
        // Create page object
        orderDelete = new GLOBALS.OrderDelete(this);

        // Run the page
        return await orderDelete.Run();
    }
}
