namespace ASPNETMaker2023.Models;

// Partial class
public partial class project1 {
    /// <summary>
    /// ordersDelete
    /// </summary>
    public static OrdersDelete ordersDelete
    {
        get => HttpData.Get<OrdersDelete>("ordersDelete")!;
        set => HttpData["ordersDelete"] = value;
    }

    /// <summary>
    /// Page class for Orders
    /// </summary>
    public class OrdersDelete : OrdersDeleteBase
    {
        // Constructor
        public OrdersDelete(Controller controller) : base(controller)
        {
        }

        // Constructor
        public OrdersDelete() : base()
        {
        }
    }

    /// <summary>
    /// Page base class
    /// </summary>
    public class OrdersDeleteBase : Orders
    {
        // Page ID
        public string PageID = "delete";

        // Project ID
        public string ProjectID = "{5973C4A2-FDFB-4411-833B-E22476E41E7A}";

        // Table name
        public string TableName { get; set; } = "Orders";

        // Page object name
        public string PageObjName = "ordersDelete";

        // Title
        public string? Title = null; // Title for <title> tag

        // Page headings
        public string Heading = "";

        public string Subheading = "";

        public string PageHeader = "";

        public string PageFooter = "";

        // Token
        public string? Token = null; // DN

        public bool CheckToken = Config.CheckToken;

        // Action result // DN
        public IActionResult? ActionResult;

        // Cache // DN
        public IMemoryCache? Cache;

        // Page layout
        public bool UseLayout = true;

        // Page terminated // DN
        private bool _terminated = false;

        // Is terminated
        public bool IsTerminated => _terminated;

        // Is lookup
        public bool IsLookup => IsApi() && RouteValues.TryGetValue("controller", out object? name) && SameText(name, Config.ApiLookupAction);

        // Is AutoFill
        public bool IsAutoFill => IsLookup && SameText(Post("ajax"), "autofill");

        // Is AutoSuggest
        public bool IsAutoSuggest => IsLookup && SameText(Post("ajax"), "autosuggest");

        // Is modal lookup
        public bool IsModalLookup => IsLookup && SameText(Post("ajax"), "modal");

        // Page URL
        private string _pageUrl = "";

        // Constructor
        public OrdersDeleteBase()
        {
            // Initialize
            CurrentPage = this;

            // Table CSS class
            TableClass = "table table-bordered table-hover table-sm ew-table";

            // Language object
            Language = ResolveLanguage();

            // Table object (orders)
            if (orders == null || orders is Orders)
                orders = this;

            // Start time
            StartTime = Environment.TickCount;

            // Debug message
            LoadDebugMessage();

            // Open connection
            Conn = Connection; // DN
        }

        // Page action result
        public IActionResult PageResult()
        {
            if (ActionResult != null)
                return ActionResult;
            SetupMenus();
            return Controller.View();
        }

        // Page heading
        public string PageHeading
        {
            get {
                if (!Empty(Heading))
                    return Heading;
                else if (!Empty(Caption))
                    return Caption;
                else
                    return "";
            }
        }

        // Page subheading
        public string PageSubheading
        {
            get {
                if (!Empty(Subheading))
                    return Subheading;
                if (!Empty(TableName))
                    return Language.Phrase(PageID);
                return "";
            }
        }

        // Page name
        public string PageName => "OrdersDelete";

        // Page URL
        public string PageUrl
        {
            get {
                if (_pageUrl == "") {
                    _pageUrl = PageName + "?";
                }
                return _pageUrl;
            }
        }

        // Show Page Header
        public IHtmlContent ShowPageHeader()
        {
            string header = PageHeader;
            PageDataRendering(ref header);
            if (!Empty(header)) // Header exists, display
                return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
            return HtmlString.Empty;
        }

        // Show Page Footer
        public IHtmlContent ShowPageFooter()
        {
            string footer = PageFooter;
            PageDataRendered(ref footer);
            if (!Empty(footer)) // Footer exists, display
                return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
            return HtmlString.Empty;
        }

        // Valid post
        protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || Antiforgery != null && HttpContext != null && await Antiforgery.IsRequestValidAsync(HttpContext);

        // Create token
        public void CreateToken()
        {
            Token ??= HttpContext != null ? Antiforgery?.GetAndStoreTokens(HttpContext).RequestToken : null;
            CurrentToken = Token ?? ""; // Save to global variable
        }

        // Set field visibility
        public void SetVisibility()
        {
            ID.Visible = false;
            SalesOrder.SetVisibility();
            OrderDate.SetVisibility();
            Customer.SetVisibility();
            Address.SetVisibility();
        }

        // Constructor
        public OrdersDeleteBase(Controller? controller = null): this() { // DN
            if (controller != null)
                Controller = controller;
        }

        /// <summary>
        /// Terminate page
        /// </summary>
        /// <param name="url">URL to rediect to</param>
        /// <returns>Page result</returns>
        public override IActionResult Terminate(string url = "") { // DN
            if (_terminated) // DN
                return new EmptyResult();

            // Page Unload event
            PageUnload();

            // Global Page Unloaded event
            PageUnloaded();
            if (!IsApi())
                PageRedirecting(ref url);

            // Gargage collection
            Collect(); // DN

            // Terminate
            _terminated = true; // DN

            // Return for API
            if (IsApi()) {
                var result = new Dictionary<string, string> { { "version", Config.ProductVersion } };
                if (!Empty(url)) // Add url
                    result.Add("url", GetUrl(url));
                foreach (var (key, value) in GetMessages()) // Add messages
                    result.Add(key, value);
                return Controller.Json(result);
            } else if (ActionResult != null) { // Check action result
                return ActionResult;
            }

            // Go to URL if specified
            if (!Empty(url)) {
                if (!Config.Debug)
                    ResponseClear();
                if (Response != null && !Response.HasStarted) {
                    SaveDebugMessage();
                    return Controller.LocalRedirect(AppPath(url));
                }
            }
            return new EmptyResult();
        }

        // Get all records from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader? dr)
        {
            var rows = new List<Dictionary<string, object>>();
            while (dr != null && await dr.ReadAsync()) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                if (GetRecordFromDictionary(GetDictionary(dr)) is Dictionary<string, object> row)
                    rows.Add(row);
            }
            return rows;
        }

        // Get all records from the list of records
        #pragma warning disable 1998

        protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(List<Dictionary<string, object>>? list)
        {
            var rows = new List<Dictionary<string, object>>();
            if (list != null) {
                foreach (var row in list) {
                    if (GetRecordFromDictionary(row) is Dictionary<string, object> d)
                       rows.Add(row);
                }
            }
            return rows;
        }
        #pragma warning restore 1998

        // Get the first record from datareader
        [return: NotNullIfNotNull("dr")]
        protected async Task<Dictionary<string, object>?> GetRecordFromRecordset(DbDataReader? dr)
        {
            if (dr != null) {
                await LoadRowValues(dr); // Set up DbValue/CurrentValue
                return GetRecordFromDictionary(GetDictionary(dr));
            }
            return null;
        }

        // Get the first record from the list of records
        protected Dictionary<string, object>? GetRecordFromRecordset(List<Dictionary<string, object>>? list) =>
            list != null && list.Count > 0 ? GetRecordFromDictionary(list[0]) : null;

        // Get record from Dictionary
        protected Dictionary<string, object>? GetRecordFromDictionary(Dictionary<string, object>? dict) {
            if (dict == null)
                return null;
            var row = new Dictionary<string, object>();
            foreach (var (key, value) in dict) {
                if (Fields.TryGetValue(key, out DbField? fld)) {
                    if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
                        if (fld.HtmlTag == "FILE") { // Upload field
                            if (Empty(value)) {
                                // row[key] = null;
                            } else {
                                if (fld.DataType == DataType.Blob) {
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(dict)); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType((byte[])value) }, { "url", url }, { "name", fld.Param + ContentExtension((byte[])value) } };
                                } else if (!fld.UploadMultiple || !ConvertToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
                                    string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + ConvertToString(value))); // Query string format
                                    row[key] = new Dictionary<string, object> { { "type", ContentType(ConvertToString(value)) }, { "url", url }, { "name", ConvertToString(value) } };
                                } else { // Multiple files
                                    var files = ConvertToString(value).Split(Config.MultipleUploadSeparator);
                                    row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + Encrypt(fld.PhysicalUploadPath + file)) }, { "name", file } });
                                }
                            }
                        } else {
                            string val = ConvertToString(value);
                            if (fld.DataType == DataType.Date && value is DateTime dt)
                                val = dt.ToString("s");
                            row[key] = ConvertToString(val);
                        }
                    }
                }
            }
            return row;
        }

        // Get record key value from array
        protected string GetRecordKeyValue(Dictionary<string, object> dict) {
            string key = "";
            key += UrlEncode(ConvertToString(dict.ContainsKey("ID") ? dict["ID"] : ID.CurrentValue));
            return key;
        }

        // Hide fields for Add/Edit
        protected void HideFieldsForAddEdit() {
            if (IsAdd || IsCopy || IsGridAdd)
                ID.Visible = false;
        }

        public string DbMasterFilter = "";

        public string DbDetailFilter = "";

        public int StartRecord;

        public int TotalRecords;

        public int RecordCount;

        public List<string> RecordKeys = new ();

        public DbDataReader? Recordset;

        public int StartRowCount = 1;

        public bool IsModal = false;

        /// <summary>
        /// Page run
        /// </summary>
        /// <returns>Page result</returns>
        public override async Task<IActionResult> Run()
        {
            // Use layout
            if (!Empty(Param("layout")) && !Param<bool>("layout"))
                UseLayout = false;

            // User profile
            Profile = ResolveProfile();

            // Security
            Security = ResolveSecurity();
            if (TableVar != "")
                Security.LoadTablePermissions(TableVar);
            CurrentAction = Param("action"); // Set up current action
            SetVisibility();

            // Do not use lookup cache
            if (!Config.LookupCachePageIds.Contains(PageID))
                SetUseLookupCache(false);

            // Global Page Loading event
            PageLoading();

            // Page Load event
            PageLoad();

            // Check token
            if (!await ValidPost())
                End(Language.Phrase("InvalidPostRequest"));

            // Check action result
            if (ActionResult != null) // Action result set by server event // DN
                return ActionResult;

            // Create token
            CreateToken();

            // Hide fields for add/edit
            if (!UseAjaxActions)
                HideFieldsForAddEdit();
            // Use inline delete
            if (UseAjaxActions)
                InlineDelete = true;

            // Set up lookup cache
            await SetupLookupOptions(Customer);

            // Set up Breadcrumb
            SetupBreadcrumb();

            // Load key parameters
            RecordKeys = GetRecordKeys(); // Load record keys
            string filter = GetFilterFromRecordKeys();
            if (Empty(filter))
                return Terminate("OrdersList"); // Prevent SQL injection, return to List page

            // Set up filter (WHERE Clause)
            CurrentFilter = filter;

            // Get action
            if (IsApi()) {
                CurrentAction = "delete"; // Delete record directly
            } else if (!Empty(Param("action"))) {
                CurrentAction = Param("action") == "delete" ? "delete" : "show";
            } else {
                CurrentAction = InlineDelete ?
                    "delete" : // Delete record directly
                    "show"; // Display record
            }
            if (IsDelete) { // DN
                SendEmail = true; // Send email on delete success
                var res = await DeleteRows();
                if (res) { // Delete rows
                    if (Empty(SuccessMessage))
                        SuccessMessage = Language.Phrase("DeleteSuccess"); // Set up success message
                    if (IsJsonResponse()) {
                        ClearMessages(); // Clear messages for Json response
                        return res;
                    } else {
                        return Terminate(ReturnUrl); // Return to caller
                    }
                } else { // Delete failed
                    if (IsJsonResponse()) {
                        return Terminate();
                    }
                    // Return JSON error message if UseAjaxActions
                    if (UseAjaxActions)
                        return Controller.Json(new { success = false, error = GetFailureMessage() });
                    if (InlineDelete)
                        return Terminate(ReturnUrl); // Return to caller
                    else
                        CurrentAction = "show"; // Display record
                }
            }
            if (IsShow) { // Load records for display // DN
                Recordset = await LoadRecordset();
                TotalRecords = await ListRecordCountAsync(); // Get record count
                if (TotalRecords <= 0) { // No record found, exit
                    CloseRecordset(); // DN
                    return Terminate("OrdersList"); // Return to list
                }
            }

            // Set LoginStatus, Page Rendering and Page Render
            if (!IsApi() && !IsTerminated) {
                // Pass login status to client side
                SetClientVar("login", LoginStatus);

                // Global Page Rendering event
                PageRendering();

                // Page Render event
                ordersDelete?.PageRender();
            }
            return PageResult();
        }

        // Load recordset // DN
        public async Task<DbDataReader?> LoadRecordset(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load recordset // DN
            var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));

            // Call Recordset Selected event
            RecordsetSelected(dr);
            return dr;
        }

        // Load rows // DN
        public async Task<List<Dictionary<string, object>>> LoadRows(int offset = -1, int rowcnt = -1)
        {
            // Load list page SQL
            string sql = ListSql;

            // Load rows // DN
            using var dr = await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
            var rows = await Connection.GetRowsAsync(dr);
            dr.Close(); // Close datareader before return
            return rows;
        }

        // Load row based on key values
        public async Task<bool> LoadRow()
        {
            string filter = GetRecordFilter();

            // Call Row Selecting event
            RowSelecting(ref filter);

            // Load SQL based on filter
            CurrentFilter = filter;
            string sql = CurrentSql;
            bool res = false;
            try {
                var row = await Connection.GetRowAsync(sql);
                if (row != null) {
                    await LoadRowValues(row);
                    res = true;
                } else {
                    return false;
                }
            } catch {
                if (Config.Debug)
                    throw;
            }
            return res;
        }

        #pragma warning disable 162, 168, 1998, 4014
        // Load row values from data reader
        public async Task LoadRowValues(DbDataReader? dr = null) => await LoadRowValues(GetDictionary(dr));

        // Load row values from recordset
        public async Task LoadRowValues(Dictionary<string, object>? row)
        {
            row ??= NewRow();

            // Call Row Selected event
            RowSelected(row);
            ID.SetDbValue(row["ID"]);
            SalesOrder.SetDbValue(row["SalesOrder"]);
            OrderDate.SetDbValue(row["OrderDate"]);
            Customer.SetDbValue(row["Customer"]);
            Address.SetDbValue(row["Address"]);
        }
        #pragma warning restore 162, 168, 1998, 4014

        // Return a row with default values
        protected Dictionary<string, object> NewRow() {
            var row = new Dictionary<string, object>();
            row.Add("ID", ID.DefaultValue ?? DbNullValue); // DN
            row.Add("SalesOrder", SalesOrder.DefaultValue ?? DbNullValue); // DN
            row.Add("OrderDate", OrderDate.DefaultValue ?? DbNullValue); // DN
            row.Add("Customer", Customer.DefaultValue ?? DbNullValue); // DN
            row.Add("Address", Address.DefaultValue ?? DbNullValue); // DN
            return row;
        }

        #pragma warning disable 1998
        // Render row values based on field settings
        public async Task RenderRow()
        {
            // Call Row Rendering event
            RowRendering();

            // Common render codes for all row types

            // ID
            ID.CellCssStyle = "white-space: nowrap;";

            // SalesOrder
            SalesOrder.CellCssStyle = "white-space: nowrap;";

            // OrderDate
            OrderDate.CellCssStyle = "white-space: nowrap;";

            // Customer
            Customer.CellCssStyle = "white-space: nowrap;";

            // Address
            Address.CellCssStyle = "white-space: nowrap;";

            // View row
            if (RowType == RowType.View) {
                // SalesOrder
                SalesOrder.ViewValue = ConvertToString(SalesOrder.CurrentValue); // DN
                SalesOrder.ViewCustomAttributes = "";

                // OrderDate
                OrderDate.ViewValue = OrderDate.CurrentValue;
                OrderDate.ViewValue = FormatDateTime(OrderDate.ViewValue, OrderDate.FormatPattern);
                OrderDate.ViewCustomAttributes = "";

                // Customer
                if (!Empty(Customer.CurrentValue)) {
                    Customer.ViewValue = Customer.HighlightLookup(ConvertToString(Customer.CurrentValue), Customer.OptionCaption(ConvertToString(Customer.CurrentValue)));
                } else {
                    Customer.ViewValue = DbNullValue;
                }
                Customer.ViewCustomAttributes = "";

                // Address
                Address.ViewValue = Address.CurrentValue;
                Address.ViewCustomAttributes = "";

                // SalesOrder
                SalesOrder.HrefValue = "";
                SalesOrder.TooltipValue = "";

                // OrderDate
                OrderDate.HrefValue = "";
                OrderDate.TooltipValue = "";

                // Customer
                Customer.HrefValue = "";
                Customer.TooltipValue = "";

                // Address
                Address.HrefValue = "";
                Address.TooltipValue = "";
            }

            // Call Row Rendered event
            if (RowType != RowType.AggregateInit)
                RowRendered();
        }
        #pragma warning restore 1998

        // Delete records (based on current filter)
        protected async Task<JsonBoolResult> DeleteRows() { // DN
            List<Dictionary<string, object>>? rsold = null;
            bool result = true;
            try {
                string sql = CurrentSql;
                using var rs = await Connection.GetDataReaderAsync(sql);
                if (rs == null) {
                    return JsonBoolResult.FalseResult;
                } else if (!rs.HasRows) {
                    FailureMessage = Language.Phrase("NoRecord"); // No record found
                    return JsonBoolResult.FalseResult;
                } else { // Clone old rows
                    rsold = await Connection.GetRowsAsync(rs);
                }
            } catch (Exception e) {
                if (Config.Debug)
                    throw;
                FailureMessage = e.Message;
                return JsonBoolResult.FalseResult;
            }
            if (UseTransaction)
                Connection.BeginTrans();
            List<string> successKeys = new (), failKeys = new ();
            try {
                // Call Row Deleting event
                if (result)
                    result = rsold.All(row => RowDeleting(row));
                if (result) {
                    foreach (var row in rsold) {
                        try {
                            result = await DeleteAsync(row) > 0;
                        } catch (Exception e) {
                            if (Config.Debug)
                                throw;
                            FailureMessage = e.Message; // Set up error message
                            result = false;
                        }
                        if (!result) {
                            if (UseTransaction) {
                                successKeys.Clear();
                                break;
                            }
                            failKeys.Add(GetKey(row)); // DN
                        } else {
                            if (Config.DeleteUploadFiles)
                                DeleteUploadedFiles(row);
                            RowDeleted(row);
                            successKeys.Add(GetKey(row)); // DN
                        }
                    }
                }
                result = successKeys.Count > 0;
                if (!result) {
                    // Set up error message
                    if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {
                        // Use the message, do nothing
                    } else if (!Empty(CancelMessage)) {
                        FailureMessage = CancelMessage;
                        CancelMessage = "";
                    } else {
                        FailureMessage = Language.Phrase("DeleteCancelled");
                    }
                }
            } catch (Exception e) {
                FailureMessage = e.Message;
                result = false;
            }
            if (result) {
                if (UseTransaction)
                    Connection.CommitTrans(); // Commit the changes

                // Set warning message if delete some records failed
                if (failKeys.Count > 0)
                    WarningMessage = Language.Phrase("DeleteRecordsFailed").Replace("%k", String.Join(", ", failKeys));
            } else {
                if (UseTransaction)
                    Connection.RollbackTrans(); // Rollback changes
            }

            // Write JSON for API request
            Dictionary<string, object> d = new ();
            d.Add("success", result);
            if (IsJsonResponse() && result) {
                var rows = await GetRecordsFromRecordset(rsold);
                string table = TableVar;
                d.Add(table, RouteValues.Count > 2 && rows.Count == 1 ? rows[0] : rows); // If single-delete, route values are controller/action/id (count > 2)
                d.Add("action", Config.ApiDeleteAction);
                d.Add("version", Config.ProductVersion);
                return new JsonBoolResult(d, true);
            }
            return new JsonBoolResult(d, result);
        }

        // Set up Breadcrumb
        protected void SetupBreadcrumb() {
            var breadcrumb = new Breadcrumb();
            string url = CurrentUrl();
            breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("OrdersList")), "", TableVar, true);
            string pageId = "delete";
            breadcrumb.Add("delete", pageId, url);
            CurrentBreadcrumb = breadcrumb;
        }

        // Setup lookup options
        public async Task SetupLookupOptions(DbField fld)
        {
            if (fld.Lookup == null)
                return;
            Func<string>? lookupFilter = null;
            dynamic conn = Connection;
            if (fld.Lookup.Options.Count is int c && c == 0) {
                // Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup Selecting server event
                var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

                // Set up lookup cache
                if (!fld.HasLookupOptions && fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
                    int totalCnt = await TryGetRecordCountAsync(sql, conn);
                    if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
                        return;
                    var dict = new Dictionary<string, Dictionary<string, object>>();
                    var values = new List<object>();
                    List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
                    if (rs != null) {
                        for (int i = 0; i < rs.Count; i++) {
                            var row = rs[i];
                            row = fld.Lookup?.RenderViewRow(row, Resolve(fld.Lookup.LinkTable));
                            string key = row?.Values.First()?.ToString() ?? String.Empty;
                            if (!dict.ContainsKey(key) && row != null)
                                dict.Add(key, row);
                        }
                    }
                    fld.Lookup?.SetOptions(dict);
                }
            }
        }

        // Close recordset
        public void CloseRecordset()
        {
            using (Recordset) {} // Dispose
        }

        // Page Load event
        public virtual void PageLoad() {
            //Log("Page Load");
        }

        // Page Unload event
        public virtual void PageUnload() {
            //Log("Page Unload");
        }

        // Page Redirecting event
        public virtual void PageRedirecting(ref string url) {
            //url = newurl;
        }

        // Message Showing event
        // type = ""|"success"|"failure"|"warning"
        public virtual void MessageShowing(ref string msg, string type) {
            // Note: Do not change msg outside the following 4 cases.
            if (type == "success") {
                //msg = "your success message";
            } else if (type == "failure") {
                //msg = "your failure message";
            } else if (type == "warning") {
                //msg = "your warning message";
            } else {
                //msg = "your message";
            }
        }

        // Page Load event
        public virtual void PageRender() {
            //Log("Page Render");
        }

        // Page Data Rendering event
        public virtual void PageDataRendering(ref string header) {
            // Example:
            //header = "your header";
        }

        // Page Data Rendered event
        public virtual void PageDataRendered(ref string footer) {
            // Example:
            //footer = "your footer";
        }

        // Page Breaking event
        public void PageBreaking(ref bool brk, ref string content) {
            // Example:
            //	brk = false; // Skip page break, or
            //	content = "<div style=\"page-break-after:always;\">&nbsp;</div>"; // Modify page break content
        }
    } // End page class
} // End Partial class
