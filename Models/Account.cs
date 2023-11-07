
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
            ArticleMainCategories = new HashSet<ArticleMainCategory>();
            Articles = new HashSet<Article>();
            ClientCategories = new HashSet<ClientCategory>();
            Clients = new HashSet<Client>();
            ContactCategories = new HashSet<ContactCategory>();
            Contacts = new HashSet<Contact>();
            PictureCategories = new HashSet<PictureCategory>();
            PictureMainCategories = new HashSet<PictureMainCategory>();
            Pictures = new HashSet<Picture>();
            ProductCategories = new HashSet<ProductCategory>();
            ProductMainCategories = new HashSet<ProductMainCategory>();
            Products = new HashSet<Product>();
            Staff = new HashSet<Staff>();
            StaffCategories = new HashSet<StaffCategory>();
            StaffMainCategories = new HashSet<StaffMainCategory>();
        }

        [Key]
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string AccountCategoryID { get; set; }

        [ForeignKey(nameof(AccountCategoryID))]
        [InverseProperty("Accounts")]
        public virtual AccountCategory AccountCategory { get; set; }
        [InverseProperty(nameof(ArticleCategory.CreateByNavigation))]
        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
        [InverseProperty(nameof(ArticleMainCategory.CreateByNavigation))]
        public virtual ICollection<ArticleMainCategory> ArticleMainCategories { get; set; }
        [InverseProperty(nameof(Article.CreateByNavigation))]
        public virtual ICollection<Article> Articles { get; set; }
        [InverseProperty(nameof(ClientCategory.CreateByNavigation))]
        public virtual ICollection<ClientCategory> ClientCategories { get; set; }
        [InverseProperty(nameof(Client.ApproveByNavigation))]
        public virtual ICollection<Client> Clients { get; set; }
        [InverseProperty(nameof(ContactCategory.CreateByNavigation))]
        public virtual ICollection<ContactCategory> ContactCategories { get; set; }
        [InverseProperty(nameof(Contact.ApproveByNavigation))]
        public virtual ICollection<Contact> Contacts { get; set; }
        [InverseProperty(nameof(PictureCategory.CreateByNavigation))]
        public virtual ICollection<PictureCategory> PictureCategories { get; set; }
        [InverseProperty(nameof(PictureMainCategory.CreateByNavigation))]
        public virtual ICollection<PictureMainCategory> PictureMainCategories { get; set; }
        [InverseProperty(nameof(Picture.CreateByNavigation))]
        public virtual ICollection<Picture> Pictures { get; set; }
        [InverseProperty(nameof(ProductCategory.CreateByNavigation))]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        [InverseProperty(nameof(ProductMainCategory.CreateByNavigation))]
        public virtual ICollection<ProductMainCategory> ProductMainCategories { get; set; }
        [InverseProperty(nameof(Product.CreateByNavigation))]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("CreateByNavigation")]
        public virtual ICollection<Staff> Staff { get; set; }
        [InverseProperty(nameof(StaffCategory.CreateByNavigation))]
        public virtual ICollection<StaffCategory> StaffCategories { get; set; }
        [InverseProperty(nameof(StaffMainCategory.CreateByNavigation))]
        public virtual ICollection<StaffMainCategory> StaffMainCategories { get; set; }
    }
}
