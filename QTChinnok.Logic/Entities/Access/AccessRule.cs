﻿//@CodeCopy
//MdStart
#if ACCOUNT_ON && ACCESSRULES_ON
namespace QTChinnok.Logic.Entities.Access
{
    using QTChinnok.Logic.Modules.Access;
#if SQLITE_ON
    [System.ComponentModel.DataAnnotations.Schema.Table("AccessRules")]
#else
    [System.ComponentModel.DataAnnotations.Schema.Table("AccessRules", Schema = "access")]
#endif
    [Microsoft.EntityFrameworkCore.Index(nameof(EntityType), IsUnique = false)]
    internal partial class AccessRule : VersionExtendedEntity
    {
        public RuleType Type { get; set; }
        [MaxLength(128)]
        public string EntityType { get; set; } = string.Empty;
        [MaxLength(128)]
        public string? RelationshipEntityType { get; set; }
        [MaxLength(128)]
        public string? PropertyName { get; set; }
        [MaxLength(36)]
        public string? EntityValue { get; set; }
        public AccessType AccessType { get; set; }
        [MaxLength(128)]
        public string? AccessRoleType { get; set; }
        [MaxLength(36)]
        public string? AccessValue { get; set; }
        public bool Creatable { get; set; } = true;
        public bool Readable { get; set; } = true;
        public bool Updatable { get; set; } = true;
        public bool Deletable { get; set; } = true;
        public bool Viewable { get; set; } = true;
    }
}
#endif
//MdEnd
