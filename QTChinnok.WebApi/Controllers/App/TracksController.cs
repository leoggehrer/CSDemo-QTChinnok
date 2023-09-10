﻿//@GeneratedCode
namespace QTChinnok.WebApi.Controllers.App
{
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    public sealed partial class TracksController : Controllers.GenericController<QTChinnok.Logic.Models.App.Track, QTChinnok.WebApi.Models.App.TrackEdit, QTChinnok.WebApi.Models.App.Track>
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static TracksController()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public TracksController(QTChinnok.Logic.Contracts.App.ITracksAccess other)
        : base(other)
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        new private QTChinnok.Logic.Contracts.App.ITracksAccess? DataAccess => base.DataAccess as QTChinnok.Logic.Contracts.App.ITracksAccess;
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        protected override QTChinnok.WebApi.Models.App.Track ToOutModel(QTChinnok.Logic.Models.App.Track accessModel)
        {
            var handled = false;
            var result = default(QTChinnok.WebApi.Models.App.Track);
            BeforeToOutModel(accessModel, ref result, ref handled);
            if (handled == false || result == null)
            {
                result = QTChinnok.WebApi.Models.App.Track.Create(accessModel);
            }
            AfterToOutModel(result);
            return result;
        }
        partial void BeforeToOutModel(QTChinnok.Logic.Models.App.Track accessModel, ref QTChinnok.WebApi.Models.App.Track? outModel, ref bool handled);
        partial void AfterToOutModel(QTChinnok.WebApi.Models.App.Track outModel);
    }
}
