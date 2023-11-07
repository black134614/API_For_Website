
namespace API.Helpers.Enums
{
    public class MailContents
    {
        public static string GenerateCodeResetPass
        {
            get
            {
                return string.Format("<div style='width: 800px; font-family: Arial; font-size:14px;'><p><b>Xin chào:</b></p><div style='line-height:30px; padding-left:50px;'><p>Đây là mã số đổi mật khẩu: <p style='font-size: 16px; font-weight: bold;'>CODERESET</p><br/>Vui lòng xác nhận trước ngày xx/xx/xxxx (dd/mm/yyyy).<br/>Không chia sẻ mã này cho người không có thẩm quyền.<br/></p></div><p><i>Trân trọng, chúc một ngày tốt lành !</i></p><p style='font-size:12px; color:#ff0000;'>**********************************************************************************************</p><div style='font-size:12px; color:#085060; line-height:24px;'><table style='font-size:12px; color:#085060; line-height:24px;'><tr><td style='padding-right:22px;'><img alt='logo' src='http://admin.pixelstrap.com/cuba/assets/images/landing/landing_logo.png' width='88' style='width:88px;' /></td><td style='border-left:2px solid #b49327; padding-left:22px;'>Email tự động được gởi bởi hệ thống. Không thể nhận thư. Vui lòng không hồi đáp lại. <br/>Mọi thắc mắc xin vui lòng liên hệ các bộ phận liên quan. <br/><b> Kỹ thuật:</b> 0000(IT)</td></tr></table></div>");
            }
        }

        public static string CreateRenter
        {
            get
            {
                return string.Format("<div style='width: 800px; font-family: Arial; font-size:14px;'><p><b>Xin chào:</b></p><div style='line-height:30px; padding-left:50px;'><p>Truy cập vào link này để hoàn thành thông tin cá nhân: <p style='font-size: 16px; font-weight: bold;'> LINK</p><br/>Vui lòng xác nhận trước ngày xx/xx/xxxx (dd/mm/yyyy).<br/>Không chia sẻ link này cho người không có thẩm quyền.<br/></p></div><p><i>Trân trọng, chúc một ngày tốt lành !</i></p><p style='font-size:12px; color:#ff0000;'>**********************************************************************************************</p><div style='font-size:12px; color:#085060; line-height:24px;'><table style='font-size:12px; color:#085060; line-height:24px;'><tr><td style='padding-right:22px;'><img alt='logo' src='http://admin.pixelstrap.com/cuba/assets/images/landing/landing_logo.png' width='88' style='width:88px;' /></td><td style='border-left:2px solid #b49327; padding-left:22px;'>Email tự động được gởi bởi hệ thống. Không thể nhận thư. Vui lòng không hồi đáp lại. <br/>Mọi thắc mắc xin vui lòng liên hệ các bộ phận liên quan. <br/><b> Kỹ thuật:</b> 0000(IT)</td></tr></table></div>");
            }
        }

        public static string AccountInfo
        {
            get
            {
                return string.Format("<div style='width: 800px; font-family: Arial; font-size:14px;'><p><b>Xin chào:</b></p><div style='line-height:30px; padding-left:50px;'><p>Thông tin tài khoản hệ thống: <p style='font-size: 16px; font-weight: bold;'></p>.<br/>Tài Khoản: USERNAME<br/>Mật khẩu: PASS<br/>Link truy cập hệ thống: https://manager.vnsohome.com<br/>Không chia sẻ thông tin này cho người không có thẩm quyền.<br/></p></div><p><i>Trân trọng, chúc một ngày tốt lành !</i></p><p style='font-size:12px; color:#ff0000;'>**********************************************************************************************</p><div style='font-size:12px; color:#085060; line-height:24px;'><table style='font-size:12px; color:#085060; line-height:24px;'><tr><td style='padding-right:22px;'><img alt='logo' src='http://admin.pixelstrap.com/cuba/assets/images/landing/landing_logo.png' width='88' style='width:88px;' /></td><td style='border-left:2px solid #b49327; padding-left:22px;'>Email tự động được gởi bởi hệ thống. Không thể nhận thư. Vui lòng không hồi đáp lại. <br/>Mọi thắc mắc xin vui lòng liên hệ các bộ phận liên quan. <br/><b> Kỹ thuật:</b> 0000(IT)</td></tr></table></div>");
            }
        }
    }
}