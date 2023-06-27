//@CodeCopy
//MdStart
#if ACCOUNT_ON
namespace QTChinnok.Logic.Entities.Account
{
#if SQLITE_ON
    [System.ComponentModel.DataAnnotations.Schema.Table("Users")]
#else
    [System.ComponentModel.DataAnnotations.Schema.Table("Users", Schema = "account")]
#endif
    public partial class User : VersionExtendedEntity
    {
        public IdType IdentityId { get; set; }
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;
    }
}
#endif
//MdEnd
