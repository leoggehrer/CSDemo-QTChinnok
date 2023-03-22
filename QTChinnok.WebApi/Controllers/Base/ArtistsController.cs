﻿//@GeneratedCode
namespace QTChinnok.WebApi.Controllers.Base
{
    ///
    /// Generated by the generator
    ///
    public sealed partial class ArtistsController : Controllers.GenericController<QTChinnok.Logic.Models.Base.Artist, QTChinnok.WebApi.Models.Base.ArtistEdit, QTChinnok.WebApi.Models.Base.Artist>
    {
        ///
        /// Generated by the generator
        ///
        static ArtistsController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public ArtistsController(QTChinnok.Logic.Contracts.Base.IArtistsAccess other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        protected override QTChinnok.WebApi.Models.Base.Artist ToOutModel(QTChinnok.Logic.Models.Base.Artist accessModel)
        {
            var handled = false;
            var result = default(QTChinnok.WebApi.Models.Base.Artist);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = QTChinnok.WebApi.Models.Base.Artist.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(QTChinnok.Logic.Models.Base.Artist accessModel, ref QTChinnok.WebApi.Models.Base.Artist? outModel, ref bool handled);
        partial void AfterToOutModel(QTChinnok.WebApi.Models.Base.Artist outModel);
    }
}
