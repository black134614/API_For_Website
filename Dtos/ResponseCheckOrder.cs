
namespace API.Dtos
{
    public class ResponseCheckOrder
    {
        private string error_code = string.Empty;

        public string errorCode
        {
            get { return error_code; }
            set { error_code = value; }
        }
        private string error_description = string.Empty;

        public string description
        {
            get { return error_description; }
            set { error_description = value; }
        }
        private string time_limit = string.Empty;

        public string timeLimit
        {
            get { return time_limit; }
            set { time_limit = value; }
        }
        private string _token = string.Empty;

        public string token
        {
            get { return _token; }
            set { _token = value; }
        }
        private string transaction_id = string.Empty;

        public string transactionId
        {
            get { return transaction_id; }
            set { transaction_id = value; }
        }
        private string amount = string.Empty;

        public string paymentAmount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string _order_code = string.Empty;

        public string order_code
        {
            get { return _order_code; }
            set { _order_code = value; }
        }
        private string transaction_type = string.Empty;

        public string transactionType
        {
            get { return transaction_type; }
            set { transaction_type = value; }
        }
        private string transaction_status = string.Empty;

        public string transactionStatus
        {
            get { return transaction_status; }
            set { transaction_status = value; }
        }
        private string payer_name = string.Empty;

        public string payerName
        {
            get { return payer_name; }
            set { payer_name = value; }
        }
        private string payer_email = string.Empty;

        public string payerEmail
        {
            get { return payer_email; }
            set { payer_email = value; }
        }
        private string payer_mobile = string.Empty;

        public string payerMobile
        {
            get { return payer_mobile; }
            set { payer_mobile = value; }
        }
        private string receiver_name = string.Empty;

        public string merchantName
        {
            get { return receiver_name; }
            set { receiver_name = value; }
        }
        private string receiver_address = string.Empty;

        public string merchantAddress
        {
            get { return receiver_address; }
            set { receiver_address = value; }
        }
        private string receiver_mobile = string.Empty;

        public string merchantMobile
        {
            get { return receiver_mobile; }
            set { receiver_mobile = value; }
        }
        private string payment_method = string.Empty;

        public string paymentMethod
        {
            get { return payment_method; }
            set { payment_method = value; }
        }
    }
}