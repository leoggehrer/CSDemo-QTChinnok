//@CodeCopy
//MdStart
namespace QTChinnok.AspMvc.Models
{
    public abstract partial class ModelObject : BaseModels.ModelObject
    {
        /// <summary>
        /// Indicates whether the id has a default value.
        /// </summary>
        [ScaffoldColumn(false)]
        public bool IsIdDefault => Id == default;
    }
}
//MdEnd
