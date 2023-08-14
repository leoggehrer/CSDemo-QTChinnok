#if GENERATEDCODE_ON
namespace QTChinnok.Logic.Controllers.App
{
    partial class TracksController
    {
        internal override IEnumerable<string> Includes => new string[] { nameof(Entities.App.Album), nameof(Entities.Base.Genre), nameof(Entities.Base.MediaType) };

        protected override void AfterActionExecute(ActionType actionType, Entities.App.Track entity)
        {
            base.AfterActionExecute(actionType, entity);

            if (actionType == ActionType.Insert || actionType == ActionType.Update)
            {
                using var mtCtrl = new Base.MediaTypesController(this);

                entity.MediaType = Task.Run(async () => await mtCtrl.ExecuteGetByIdAsync(entity.MediaTypeId).ConfigureAwait(false)).Result;
                if (entity.AlbumId != null) 
                {
                    using var aCtrl = new AlbumsController(this);

                    entity.Album = Task.Run(async () => await aCtrl.ExecuteGetByIdAsync(entity.AlbumId.Value).ConfigureAwait(false)).Result;
                }
                if (entity.GenreId != null)
                {
                    using var gCtrl = new Base.GenresController(this);

                    entity.Genre = Task.Run(async () => await gCtrl.ExecuteGetByIdAsync(entity.GenreId.Value).ConfigureAwait(false)).Result;
                }
            }
        }
    }
}
#endif