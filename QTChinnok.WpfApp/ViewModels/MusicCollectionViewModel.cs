
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QTChinnok.WpfApp.ViewModels
{
    using TAlbum = Models.App.Album;
    using TModel = Models.MusicCollection;
    public class MusicCollectionViewModel : BaseViewModel
    {
        #region fields
        private TModel? _model;
        private ICommand? cmdSave;
        private ICommand? cmdClose;

        private ICommand? _cmdAddAlbum;
        private ICommand? _cmdRemoveAlbum;
        private readonly List<TAlbum> _albums = new();
        private readonly List<TAlbum> _addAlbums = new();

        private TAlbum? _selectedAlbum;
        private TAlbum? _selectedAddAlbum;
        #endregion fields

        #region properties
        public ICommand CommandSave
        {
            get
            {
                return RelayCommand.Create(ref cmdSave, p =>
                {
                    Save();
                },
                p => true);
            }
        }
        public ICommand CommandClose
        {
            get
            {
                return RelayCommand.Create(ref cmdClose, p =>
                {
                    Close();
                },
                p => true);
            }
        }

        public ICommand CommandAddAlbum
        {
            get
            {
                return RelayCommand.Create(ref _cmdAddAlbum, p =>
                {
                    AddAlbum(Model, SelectedAddAlbum!);
                    OnPropertiesChanged();
                },
                p => SelectedAddAlbum != null);
            }
        }
        public ICommand CommandRemoveAlbum
        {
            get
            {
                return RelayCommand.Create(ref _cmdRemoveAlbum, p =>
                {
                    RemoveMember(Model, SelectedAlbum!);
                    OnPropertiesChanged();
                },
                p => SelectedAlbum != null);
            }
        }

        public TModel Model
        {
            get => _model ??= new();
            set
            {
                _model = value;
                _albums.Clear();
                _albums.AddRange(value.Albums);
                OnPropertiesChanged();
            }
        }
        public string Name
        {
            get { return Model.Name; }
            set
            {
                Model.Name = value;
                OnPropertiesChanged();
            }
        }

        public TAlbum[] Albums => _albums.ToArray();
        public TAlbum[] AddAlbums => _addAlbums.ToArray();

        public TAlbum? SelectedAlbum
        {
            get => _selectedAlbum;
            set
            {
                _selectedAlbum = value;
            }
        }
        public TAlbum? SelectedAddAlbum
        {
            get => _selectedAddAlbum;
            set
            {
                _selectedAddAlbum = value;
            }
        }
        #endregion properties

        #region Constructor
        public MusicCollectionViewModel()
        {
            OnPropertyChanged(nameof(AddAlbums));
        }
        #endregion Constructor

        protected void OnPropertiesChanged()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Albums));
            OnPropertyChanged(nameof(AddAlbums));
        }
        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(AddAlbums))
            {
                Task.Run(() => LoadAddAlbumsAsync());
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }

        #region commands
        public void Save()
        {
            var error = false;

            Task.Run(async () =>
            {
                using var mcCtrl = new Logic.Controllers.App.MusicCollectionsController();
                using var albumCtrl = new Logic.Controllers.App.AlbumsController(mcCtrl);

                try
                {
                    if (Model.Id > 0)
                    {
                        var dbEntity = await mcCtrl.GetByIdAsync(Model.Id);

                        if (dbEntity != null)
                        {
                            dbEntity.CopyFrom(this);
                            dbEntity.Albums.Clear();
                            foreach (var item in _albums)
                            {
                                dbEntity.Albums.Add(new Logic.Models.App.Album { Id = item.Id });
                            }
                            dbEntity = await mcCtrl.UpdateAsync(dbEntity).ConfigureAwait(false);
                            await mcCtrl.SaveChangesAsync().ConfigureAwait(false);
                            Model.CopyFrom(dbEntity);
                        }
                    }
                    else
                    {
                        var dbEntity = mcCtrl.Create();

                        dbEntity.CopyFrom(this);
                        foreach (var item in _albums)
                        {
                            dbEntity.Albums.Add(new Logic.Models.App.Album { Id = item.Id });
                        }
                        dbEntity = await mcCtrl.InsertAsync(dbEntity).ConfigureAwait(false);
                        await mcCtrl.SaveChangesAsync().ConfigureAwait(false);
                        Model.CopyFrom(dbEntity);
                    }
                    //foreach (var item in Model.Albums)
                    //{
                    //    await mcCtrl.RemoveAlbumAsync(Model.Id, item.Id).ConfigureAwait(false);
                    //}
                    //foreach (var item in _albums)
                    //{
                    //    await mcCtrl.AddAlbumAsync(Model.Id, item.Id).ConfigureAwait(false);
                    //}
                    //await mcCtrl.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    error = true;
                    MessageBox.Show(ex.Message, "Save", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }).Wait();

            if (error == false)
            {
                Close();
            }
        }
        public void Close()
        {
            Window?.Close();
        }
        #endregion commands

        #region methods
        private async Task LoadAddAlbumsAsync()
        {
            using var ctrl = new Logic.Controllers.App.AlbumsController();
            var items = await ctrl.GetAllAsync().ConfigureAwait(false);
            var difIds = items.Select(e => e.Id).Except(_albums.Select(e => e.Id));

            _addAlbums.Clear();
            _addAlbums.AddRange(items.Where(e => difIds.Contains(e.Id)).Select(e => TAlbum.Create(e)));

            if (_addAlbums.Any())
            {
                SelectedAddAlbum = _addAlbums.First();
            }
            else
            {
                SelectedAddAlbum = null;
            }
            base.OnPropertyChanged(nameof(SelectedAddAlbum));
            base.OnPropertyChanged(nameof(AddAlbums));
        }
        private void AddAlbum(TModel model, TAlbum album)
        {
            _albums.Add(album);
        }
        private void RemoveMember(TModel model, TAlbum album)
        {
            _albums.Remove(album);
        }
        #endregion methods
    }
}
