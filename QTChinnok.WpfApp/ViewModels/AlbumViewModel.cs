using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QTChinnok.WpfApp.ViewModels
{
    using TAlbum = Models.App.Album;
    using TArtist = Models.Base.Artist;
    public class AlbumViewModel : BaseViewModel
    {
        private ICommand? _cmdSave;
        private ICommand? _cmdClose;

        private TAlbum? _model;
        private readonly List<TArtist> _artists = new();

        public ICommand CommandSave => RelayCommand.Create(ref _cmdSave, p => Save());
        public ICommand CommandClose => RelayCommand.Create(ref _cmdClose, p => Close());

        public TAlbum Model
        {
            get => _model ??= new TAlbum();
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Title));
                OnPropertyChanged(nameof(ArtistId));
            }
        }
        public int Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }
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

        public TArtist[] ArtistList => _artists.ToArray();
        public AlbumViewModel()
        {
            OnPropertyChanged(nameof(ArtistList));
        }
        protected override void OnPropertyChanged(string propertyName)
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
        private async Task LoadArtistsAsync()
        {
            using var ctrl = new Logic.Controllers.Base.ArtistsController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);

            _artists.Clear();
            _artists.AddRange(items.OrderBy(i => i.Name).Select(i => TArtist.Create(i)).ToArray());

            base.OnPropertyChanged(nameof(ArtistList));
            base.OnPropertyChanged(nameof(ArtistId));
        }

        private void Save()
        {
            bool error = false;
            string errorMessage = string.Empty;

            Task.Run(async () =>
            {
                using var ctrl = new Logic.Controllers.App.AlbumsController();

                try
                {
                    var entity = ctrl.Create();

                    entity.CopyFrom(Model);
                    if (Model.Id == 0)
                    {
                        await ctrl.InsertAsync(entity).ConfigureAwait(false);
                    }
                    else
                    {
                        await ctrl.UpdateAsync(entity).ConfigureAwait(false);
                    }
                    await ctrl.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (System.Exception ex)
                {
                    error = true;
                    errorMessage = ex.Message;
                }

            }).Wait();

            if (error)
            {
                MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Window?.Close();
            }
        }
        private void Close()
        {
            Window?.Close();
        }
    }
}
