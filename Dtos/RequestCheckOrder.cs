
namespace API.Dtos
{
    public class RequestCheckOrder
    {
        private String _Funtion = "GetTransactionDetail";

        public String Funtion
        {
            get { return _Funtion; }
        }

        private String _Version = "3.1";

        public String Version
        {
            get { return _Version; }
        }
        private String _Merchant_id = String.Empty;
        public String Merchant_id
        {
            get { return _Merchant_id; }
            set { _Merchant_id = value; }
        }
        private String _Merchant_password = String.Empty;

        public String Merchant_password
        {
            get { return _Merchant_password; }
            set { _Merchant_password = value; }
        }
        private String _token = String.Empty;

        public String Token
        {
            get { return _token; }
            set { _token = value; }
        }
    }
}