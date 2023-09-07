using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QTChinnok.WpfApp.ViewModels
{
    using TTrack = Models.App.Track;
    using TGenre = Models.Base.Genre;
    using TAlbum = Models.App.Album;
    using TMediaType = Models.Base.MediaType;

    public class TrackViewModel : BaseViewModel
    {
        private ICommand? _cmdSave;
        private ICommand? _cmdClose;

        private readonly List<TGenre> _genres = new();
        private readonly List<TAlbum> _albums = new();
        private readonly List<TMediaType> _mediaTypes = new();

        private TTrack? _model;

        public TGenre[] Genres => _genres.ToArray();
        public TAlbum[] Albums => _albums.ToArray();
        public TMediaType[] MediaTypes => _mediaTypes.ToArray();

        public ICommand CommandSave => RelayCommand.Create(ref _cmdSave, p => Save());
        public ICommand CommandClose => RelayCommand.Create(ref _cmdClose, p => Close());

        public TrackViewModel()
        {
            OnPropertyChanged(nameof(Genres));
            OnPropertyChanged(nameof(Albums));
            OnPropertyChanged(nameof(MediaTypes));
        }
        protected override void OnPropertyChanged(string propertyName)
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
        public TTrack Model
        {
            get => _model ??= new TTrack();
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Composer));
                OnPropertyChanged(nameof(MediaTypeId));
                OnPropertyChanged(nameof(GenreId));
                OnPropertyChanged(nameof(AlbumId));
            }
        }

        public int Id
        {
            get => Model.Id;
            set => Model.Id = value;
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
            get => Model.Composer ?? string.Empty;
            set => Model.Composer = value;
        }

        private async Task LoadGenresAsync()
        {
            using var ctrl = new Logic.Controllers.Base.GenresController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _genres.Clear();
            _genres.AddRange(items.OrderBy(e => e.Name).Select(e => TGenre.Create(e)));

            if (GenreId == 0 && _genres.Any())
            {
                GenreId = _genres.First().Id;
            }
            base.OnPropertyChanged(nameof(GenreId));
            base.OnPropertyChanged(nameof(Genres));
        }

        private async Task LoadAlbumsAsync()
        {
            using var ctrl = new Logic.Controllers.App.AlbumsController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _albums.Clear();
            _albums.AddRange(items.OrderBy(e => e.Title).Select(e => TAlbum.Create(e)));

            if (AlbumId == 0 && _albums.Any())
            {
                AlbumId = _albums.First().Id;
            }
            base.OnPropertyChanged(nameof(AlbumId));
            base.OnPropertyChanged(nameof(Albums));
        }

        private async Task LoadMediaTypesAsync()
        {
            using var ctrl = new Logic.Controllers.Base.MediaTypesController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _mediaTypes.Clear();
            _mediaTypes.AddRange(items.OrderBy(e => e.Name).Select(e => TMediaType.Create(e)));

            if (MediaTypeId == 0 && _mediaTypes.Any())
            {
                MediaTypeId = _mediaTypes.First().Id;
            }
            base.OnPropertyChanged(nameof(MediaTypeId));
            base.OnPropertyChanged(nameof(MediaTypes));
        }

        private void Save()
        {
            bool error = false;
            string errorMessage = string.Empty;

            Task.Run(async () =>
            {
                try
                {
                    using var ctrl = new Logic.Controllers.App.TracksController();

                    if (Id == 0)
                    {
                        var entity = ctrl.Create();

                        entity.CopyFrom(Model);
                        await ctrl.InsertAsync(entity).ConfigureAwait(false);
                    }
                    else
                    {
                        var entity = await ctrl.GetByIdAsync(Model.Id).ConfigureAwait(false);

                        if (entity != null)
                        {
                            entity.CopyFrom(Model);
                            await ctrl.UpdateAsync(entity).ConfigureAwait(false);
                        }
                    }
                    await ctrl.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {
                    error = true;
                    errorMessage = ex.Message;
                    throw;
                }
            }).Wait();

            if (error)
            {
                MessageBox.Show(errorMessage, "Speichern", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Close();
            }
        }
        private void Close()
        {
            Window?.Close();
        }
    }
}
