#if GENERATEDCODE_ON
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace QTChinnok.MvvMApp.ViewModels
{
    using TAlbumModel = Models.Album;
    using TAlbumEntity = Logic.Models.App.Album;
    using TArtistModel = Models.Artist;
    using TArtistEntity = Logic.Models.Base.Artist;
    public partial class AlbumViewModel : ModelViewModel<TAlbumModel, TAlbumEntity>
    {
        private TArtistModel? _selectedArtist;
        private readonly List<TArtistModel> _artists = new();

        public int ArtistId
        {
            get => Model.ArtistId;
            set => Model.ArtistId = value;
        }
        public string Title
        {
            get => Model.Title;
            set => Model.Title = value;
        }
        public TArtistModel? SelectedArtist
        {
            get => _selectedArtist;
            set
            {
                _selectedArtist = value;
                if (_selectedArtist != null)
                {
                    ArtistId = _selectedArtist.Id;
                }
            }
        }
        public TArtistModel[] ArtistList => _artists.ToArray();

        public AlbumViewModel()
        {
            OnPropertiesChanged(nameof(ArtistList));
        }
        public override Logic.Contracts.App.IAlbumsAccess CreateController()
        {
            return new Logic.Controllers.App.AlbumsController();
        }

        internal override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == nameof(ArtistList))
            {
                Task.Run(LoadArtistsAsync);
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        protected override void OnPropertiesChanged(params string[] propertyNames)
        {
            base.OnPropertiesChanged(propertyNames.Union(new string[] { nameof(Id), nameof(Title) }).ToArray());
        }

        private async Task LoadArtistsAsync()
        {
            using var ctrl = new Logic.Controllers.Base.ArtistsController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _artists.Clear();
            _artists.AddRange(items.OrderBy(i => i.Name).Select(i => new TArtistModel(i)).ToArray());
            if (items.Any())
            {
                SelectedArtist = _artists.FirstOrDefault();
            }
            SelectedArtist = _artists.FirstOrDefault(e => e.Id == ArtistId);

            base.OnPropertyChanged(nameof(ArtistList));
            base.OnPropertyChanged(nameof(SelectedArtist));
        }
    }
}
#endif