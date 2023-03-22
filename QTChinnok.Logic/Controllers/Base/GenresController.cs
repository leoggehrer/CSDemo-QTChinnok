﻿//@GeneratedCode
namespace QTChinnok.Logic.Controllers.Base
{
    using TEntity = Entities.Base.Genre;
    using TOutModel = Models.Base.Genre;
    ///
    /// Generated by the generator
    ///
    
    public sealed partial class GenresController : EntitiesController<TEntity, TOutModel>, Contracts.Base.IGenresAccess
    {
        ///
        /// Generated by the generator
        ///
        static GenresController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public GenresController()
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        public GenresController(ControllerObject other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        ///
        /// Generated by the generator
        ///
        internal override TOutModel ToModel(TEntity entity)
        {
            var handled = false;
            TOutModel? result = default;
            
            BeforeToOutModel(entity, ref result, ref handled);
            if (handled == false || result == default)
            {
                result = new TOutModel(entity);
            }
            AfterToOutModel(entity, result);
            return result;
        }
        partial void BeforeToOutModel(TEntity entity, ref TOutModel? result, ref bool handled);
        partial void AfterToOutModel(TEntity entity, TOutModel result);
    }
}
