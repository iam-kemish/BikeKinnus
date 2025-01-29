namespace BikeKinnus.Models.Models
{
    public class StaticDetails
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";


        public const string SummaryStatusForCostumer = "Costumer's payment was accoplished via digital wallet.";
        public const string SummaryStatusForEmployee = "Employee payment was hold and reviewed and will be received.";
        public const string SummaryStatusForCompany = "The company payment was handled via swift.";
        
        public const string PaymentStatusDelayedPayment = "Pending";
       
        public const string PaymentStatusApproved = "Approved";


        public const string SessionCart = "SessionShoppingCart";
    }
}
