#if GENERATEDCODE_ON
using QTChinnok.Logic.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace QTChinnok.MvvMApp.ViewModels
{
    using TTrackModel = Models.Track;
    using TTrackEntity = Logic.Models.App.Track;
    using TGenreModel = Models.Genre;
    using TAlbumModel = Models.Album;
    using TMediaTypeModel = Models.MediaType;

    public class TrackViewModel : ModelViewModel<TTrackModel, TTrackEntity>
    {
        private TGenreModel? _selectedGenre;
        private TAlbumModel? _selectedAlbum;
        private TMediaTypeModel? _selectedMediaType;

        private List<TGenreModel> _genres = new();
        private List<TAlbumModel> _albums = new();
        private List<TMediaTypeModel> _mediaTypes = new();

        public TGenreModel[] Genres => _genres.ToArray();
        public TAlbumModel[] Albums => _albums.ToArray();
        public TMediaTypeModel[] MediaTypes => _mediaTypes.ToArray();

        public TGenreModel? SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                if (_selectedGenre != null)
                {
                    GenreId = _selectedGenre.Id;
                }
            }
        }
        public TAlbumModel? SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
                if (_selectedAlbum != null)
                {
                    AlbumId = _selectedAlbum.Id;
                }
            }
        }
        public TMediaTypeModel? SelectedMediaType
        {
            get => _selectedMediaType;
            set
            {
                _selectedMediaType = value;
                if (_selectedMediaType != null)
                {
                    MediaTypeId = _selectedMediaType.Id;
                }
            }
        }

        public int? GenreId
        {
            get => Model.GenreId;
            set => Model.GenreId = value;
        }
        public int? AlbumId
        {
            get => Model.AlbumId;
            set => Model.AlbumId = value;
        }
        public int MediaTypeId
        {
            get => Model.MediaTypeId;
            set => Model.MediaTypeId = value;
        }
        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }
        public string Composer
        {
            get => Model.Composer;
            set => Model.Composer = value;
        }

        public TrackViewModel()
        {
            OnPropertyChanged(nameof(Genres));
            OnPropertyChanged(nameof(Albums));
            OnPropertyChanged(nameof(MediaTypes));
        }
        internal override void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            if (propertyName == nameof(Genres))
            {
                Task.Run(LoadGenresAsync);
            }
            else if (propertyName == nameof(Albums))
            {
                Task.Run(LoadAlbumsAsync);
            }
            else if (propertyName == nameof(MediaTypes))
            {
                Task.Run(LoadMediaTypesAsync);
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }
        protected override void OnPropertiesChanged(params string[] propertyNames)
        {
            base.OnPropertiesChanged(propertyNames.Union(new string[] { nameof(Id), nameof(GenreId), nameof(AlbumId), nameof(MediaTypeId), nameof(Name), nameof(Composer), nameof(Genres), nameof(Albums), nameof(MediaTypes) }).ToArray());
        }

        public override Logic.Contracts.App.ITracksAccess CreateController()
        {
            return new Logic.Controllers.App.TracksController();
        }
        private async Task LoadGenresAsync()
        {
            using var ctrl = new Logic.Controllers.Base.GenresController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _genres.Clear();
            _genres.AddRange(items.OrderBy(e => e.Name).Select(e => new TGenreModel(e)));

            if (_genres.Any())
            {
                SelectedGenre = _genres.FirstOrDefault();
            }
            SelectedGenre = _genres.FirstOrDefault(e => e.Id == GenreId);

            base.OnPropertyChanged(nameof(Genres));
            base.OnPropertyChanged(nameof(SelectedGenre));
        }
        private async Task LoadAlbumsAsync()
        {
            using var ctrl = new Logic.Controllers.App.AlbumsController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _albums.Clear();
            _albums.AddRange(items.OrderBy(e => e.Title).Select(e => new TAlbumModel(e)));

            if (_albums.Any())
            {
                SelectedAlbum = _albums.FirstOrDefault();
            }
            SelectedAlbum = _albums.FirstOrDefault(e => e.Id == AlbumId);

            base.OnPropertyChanged(nameof(Albums));
            base.OnPropertyChanged(nameof(SelectedAlbum));
        }
        private async Task LoadMediaTypesAsync()
        {
            using var ctrl = new Logic.Controllers.Base.MediaTypesController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _mediaTypes.Clear();
            _mediaTypes.AddRange(items.OrderBy(e => e.Name).Select(e => new TMediaTypeModel(e)));

            if (_mediaTypes.Any())
            {
                SelectedMediaType = _mediaTypes.FirstOrDefault();
            }
            SelectedMediaType = _mediaTypes.FirstOrDefault(e => e.Id == MediaTypeId);

            base.OnPropertyChanged(nameof(MediaTypes));
            base.OnPropertyChanged(nameof(SelectedMediaType));
        }
    }
}
#endif