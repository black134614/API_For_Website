namespace API.Helpers.Enums
{
    public class SettingConstants
    {
        // Menu
        //// set IsShow dynamic - cấu hình hiển thị menu động
        public static List<string> SysMenuFunctions = new()
        {
            // Systems
            "About", "ArticleCategory", "Article", "AccountCashType", "Account",
            "AccountType", "CashType", "ConfigSystem", "Contact", "ContentType", "ContractStatus",
            "Customer", "CustomerType", "District", "Extra", "FormType", "FunctionSystem", "GroupFunctionSystem",
            "InvoiceType", "MeterType", "Order", "OrderStatus", "Package", "PackageType", "PayType",
            "Period", "PriceType", "Province", "RoomStatus", "Suggest", "SuggestType",
            "SystemAccount", "SystemTicketType", "SystemTicketStatus", "SystemTicket",
            "SystemNotification", "SystemNotificationType", "SystemNotificationChannel",
            "Unit", "UnitConverted", "Ward", "IntroductionType","Introduction","ServiceCategory","ServiceWeb","Page",
            // Customers
            "AccountCash", "Broker", "BrokerLevel", "CashCategory", "CashPayment", "CashReceipts",
            "Client", "ClientType", "Commission", "Contract", "Department",
            "Deposit", "Document", "Floor", "Form", "GroupFunction", "House",
            "InvestmentRatio", "Investor", "Invoice", "Meter", "MeterReading", "Position",
            "Room", "RoomType", "Service", "ServiceType", "Staff", "Utility",
            // Reports
            "FinancialReport", "DebtReport", "StatisticReport"
        };

        // Systems
        public static List<string> SysCategoryManager = new()
        {
            "AccountCashType", "CashType", "ContentType", "CustomerType",
            "FormType", "InvoiceType", "MeterType", "PayType"
        }; // Danh mục hệ thống

        public static List<string> SysSettingManager = new() { "ContractStatus", "OrderStatus", "RoomStatus", "Period", "PriceType", "ConfigSystem" }; // Cấu hình chung
        public static List<string> SysSystemManager = new() { "SystemAccount", "Account", "AccountType" }; // Quản lý tài khoản
        public static List<string> SysFunctionManager = new() { "FunctionSystem", "GroupFunctionSystem" }; // Quản lý chức năng
        public static List<string> SysPackageManager = new() { "PackageType", "Package", "Extra" }; // Quản lý gói dịch vụ
        public static List<string> SysNotiManager = new()
        {
            "SystemNotificationType", "SystemNotificationChannel",
            "SystemNotificationReceiver", "SystemNotification"
        }; // Quản lý thông báo

        public static List<string> SysTicketManager = new() { "SystemTicketType", "SystemTicketStatus", "SystemTicket" }; // Quản lý yêu cầu hỗ trợ
        public static List<string> SysUnitManager = new() { "Unit", "UnitConverted" }; // Quản lý đơn vị
        public static List<string> SysAddresManager = new() { "Province", "District", "Ward" }; // Quản lý địa chỉ
        public static List<string> SysSuggestManager = new() { "SuggestType", "Suggest" }; // Quản lý gợi ý
        public static List<string> SysWebsiteManager = new() { "About", "ArticleCategory", "Article", "IntroductionType", "Introduction", "ServiceCategory", "ServiceWeb", "PageType","Page" }; // Quản lý website

        // Custemers
        public static List<string> CusInfrastructureManager = new() { "House", "Floor" }; // Quản lý cơ sở
        public static List<string> CusRoomManager = new() { "RoomType", "Room" }; // Quản lý phòng / căn hộ
        public static List<string> CusMeterManager = new() { "Meter", "MeterReading" }; // Quản lý công tơ
        public static List<string> CusServiceManager = new() { "ServiceType", "Service" }; // Quản lý dịch vụ
        public static List<string> CusCategoryManager = new() { "CashCategory", "PriceOfRoom", "Form", "Document", "Utility", "AccountCash" }; // Danh mục
        public static List<string> CusStaffManager = new() { "Department", "Position", "GroupFunction", "Staff" }; // Quản lý nhân viên
        public static List<string> CusInvestorManager = new() { "Investor", "InvestmentRatio" }; // Quản lý nhà đầu tư
        public static List<string> CusBrokerManager = new() { "BrokerLevel", "Broker", "Commission" }; // Quản lý môi giới
        public static List<string> CusClientManager = new() { "ClientType", "Client" }; // Quản lý khách hàng
        public static List<string> CusContractManager = new() { "Contract", "Deposit" }; // Quản lý hợp đồng
        public static List<string> CusFinancialManager = new() { "CashReceipts", "CashPayment", "Invoice" }; // Quản lý tài chính

        // Reports
        public static List<string> CusReportManager = new() { "Report-Financial", "Report-Debt", "Report-Statistical" }; // Báo cáo thống kê
    }
}