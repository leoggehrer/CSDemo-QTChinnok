﻿//@GeneratedCode
namespace QTChinnok.WebApi.Controllers.App
{
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    public sealed partial class MusicCollectionsController : Controllers.GenericController<QTChinnok.Logic.Models.App.MusicCollection, QTChinnok.WebApi.Models.App.MusicCollectionEdit, QTChinnok.WebApi.Models.App.MusicCollection>
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static MusicCollectionsController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public MusicCollectionsController(QTChinnok.Logic.Contracts.App.IMusicCollectionsAccess other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new private QTChinnok.Logic.Contracts.App.IMusicCollectionsAccess? DataAccess => base.DataAccess as QTChinnok.Logic.Contracts.App.IMusicCollectionsAccess;
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        protected override QTChinnok.WebApi.Models.App.MusicCollection ToOutModel(QTChinnok.Logic.Models.App.MusicCollection accessModel)
        {
            var handled = false;
            var result = default(QTChinnok.WebApi.Models.App.MusicCollection);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = QTChinnok.WebApi.Models.App.MusicCollection.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(QTChinnok.Logic.Models.App.MusicCollection accessModel, ref QTChinnok.WebApi.Models.App.MusicCollection? outModel, ref bool handled);
        partial void AfterToOutModel(QTChinnok.WebApi.Models.App.MusicCollection outModel);
    }
}
