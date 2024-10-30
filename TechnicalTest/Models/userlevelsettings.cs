namespace ASPNETMaker2023.Models;

// Partial class
public partial class project1 {
    // Configuration
    public static partial class Config
    {
        //
        // ASP.NET Maker 2023 user level settings
        //

        // User level info
        public static List<UserLevel> UserLevels = new ()
        {
            new () { Id = -2, Name = "Anonymous" }
        };

        // User level priv info
        public static List<UserLevelPermission> UserLevelPermissions = new ()
        {
            new () { Table = "{5973C4A2-FDFB-4411-833B-E22476E41E7A}Item", Id = -2, Permission = 0 },
            new () { Table = "{5973C4A2-FDFB-4411-833B-E22476E41E7A}Order", Id = -2, Permission = 0 }
        };

        // User level table info // DN
        public static List<UserLevelTablePermission> UserLevelTablePermissions = new ()
        {
            new () { TableName = "Item", TableVar = "Item", Caption = "Item", Allowed = true, ProjectId = "{5973C4A2-FDFB-4411-833B-E22476E41E7A}", Url = "ItemList" },
            new () { TableName = "Order", TableVar = "Order", Caption = "Order", Allowed = true, ProjectId = "{5973C4A2-FDFB-4411-833B-E22476E41E7A}", Url = "OrderList" }
        };
    }
} // End Partial class
