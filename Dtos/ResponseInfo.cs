
namespace API.Dtos
{
    public class ResponseInfo
    {
        private String _error_code = string.Empty;

        public String Error_code
        {
            get { return _error_code; }
            set { _error_code = value; }
        }
        private String _checkout_url = String.Empty;

        public String Checkout_url
        {
            get { return _checkout_url; }
            set { _checkout_url = value; }
        }
        private String _Token = String.Empty;

        public String Token
        {
            get { return _Token; }
            set { _Token = value; }
        }

        private String _description = String.Empty;

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}