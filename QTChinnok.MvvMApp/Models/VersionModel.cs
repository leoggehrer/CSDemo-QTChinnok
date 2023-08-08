//@CodeCopy
//MdStart
using System;
namespace QTChinnok.MvvMApp.Models
{
	public abstract partial class VersionModel : ModelObject, BaseContracts.IVersionable
	{
#if ROWVERSION_ON
        /// <summary>
        /// Row version of the entity.
        /// </summary>
        [Timestamp]
        public byte[]? RowVersion { get; set; }
#endif
    }
}
//MdEnd
