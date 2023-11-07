using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class Account_Dto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Avatar { get; set; }

        public string Thumb { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobi { get; set; }

        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }

        public DateTime? CreateTime { get; set; }

        public string AccountCategoryID { get; set; }

        public string FileUpload { get; set; }
        public List<IFormFile> ListThumb { get; set; }
        public string AccountCategory_Title { get; set; }

    }
}