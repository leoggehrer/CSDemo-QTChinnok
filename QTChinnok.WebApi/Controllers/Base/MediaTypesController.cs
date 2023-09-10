﻿//@GeneratedCode
namespace QTChinnok.WebApi.Controllers.Base
{
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    public sealed partial class MediaTypesController : Controllers.GenericController<QTChinnok.Logic.Models.Base.MediaType, QTChinnok.WebApi.Models.Base.MediaTypeEdit, QTChinnok.WebApi.Models.Base.MediaType>
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static MediaTypesController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public MediaTypesController(QTChinnok.Logic.Contracts.Base.IMediaTypesAccess other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new private QTChinnok.Logic.Contracts.Base.IMediaTypesAccess? DataAccess => base.DataAccess as QTChinnok.Logic.Contracts.Base.IMediaTypesAccess;
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        protected override QTChinnok.WebApi.Models.Base.MediaType ToOutModel(QTChinnok.Logic.Models.Base.MediaType accessModel)
        {
            var handled = false;
            var result = default(QTChinnok.WebApi.Models.Base.MediaType);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = QTChinnok.WebApi.Models.Base.MediaType.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(QTChinnok.Logic.Models.Base.MediaType accessModel, ref QTChinnok.WebApi.Models.Base.MediaType? outModel, ref bool handled);
        partial void AfterToOutModel(QTChinnok.WebApi.Models.Base.MediaType outModel);
    }
}
