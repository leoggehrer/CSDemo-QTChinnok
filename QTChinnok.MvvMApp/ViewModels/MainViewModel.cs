//@CustomCode
//MdStart
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using QTChinnok.Logic.Contracts;
using QTChinnok.MvvMApp.Views;

namespace QTChinnok.MvvMApp.ViewModels
{
    using TGenreEntity = Logic.Models.Base.Genre;
    using TGenreModel = Models.Genre;

    using TMediaTypeEntity = Logic.Models.Base.MediaType;
    using TMediaTypeModel = Models.MediaType;

    using TArtistEntity = Logic.Models.Base.Artist;
    using TArtistModel = Models.Artist;

    using TAlbumEntity = Logic.Models.App.Album;
    using TAlbumModel = Models.Album;

    using TTrackEntity = Logic.Models.App.Track;
    using TTrackModel = Models.Track;
    public partial class MainViewModel : BaseViewModel
    {
        #region delegates
        private class GenresViewModel : ModelsViewModel<TGenreModel, TGenreEntity>
        {
            protected override ModelView<TGenreModel, TGenreEntity>? ModelView => new GenreWindow();

            protected override Func<TGenreEntity, TGenreModel> ConvertTo => (e) => new TGenreModel(e);

            protected override Predicate<TGenreEntity> LoadPredicate => (e) => e.Name != null && e.Name.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase);

            public GenresViewModel(BaseViewModel otherViewModel)
                : base(otherViewModel)
            {
            }

            public override Logic.Contracts.Base.IGenresAccess CreateController()
            {
                return new Logic.Controllers.Base.GenresController();
            }
        }
        private class MediaTypesViewModel : ModelsViewModel<TMediaTypeModel, TMediaTypeEntity>
        {
            protected override ModelView<TMediaTypeModel, TMediaTypeEntity>? ModelView => new MediaTypeWindow();
            protected override Func<TMediaTypeEntity, TMediaTypeModel> ConvertTo => (e) => new TMediaTypeModel(e);
            protected override Predicate<TMediaTypeEntity> LoadPredicate => e => e.Name != null && e.Name.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase);

            public MediaTypesViewModel(BaseViewModel otherViewModel)
                : base(otherViewModel)
            {
            }

            public override Logic.Contracts.Base.IMediaTypesAccess CreateController()
            {
                return new Logic.Controllers.Base.MediaTypesController();
            }
        }
        private class ArtistsViewModel : ModelsViewModel<TArtistModel, TArtistEntity>
        {
            protected override ModelView<TArtistModel, TArtistEntity>? ModelView => new ArtistWindow();
            protected override Func<TArtistEntity, TArtistModel> ConvertTo => e => new TArtistModel(e);
            protected override Predicate<TArtistEntity> LoadPredicate => e => e.Name != null && e.Name.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase);

            public ArtistsViewModel(BaseViewModel otherViewModel)
                : base(otherViewModel)
            {
            }

            public override Logic.Contracts.Base.IArtistsAccess CreateController()
            {
                return new Logic.Controllers.Base.ArtistsController();
            }
        }
        private class AlbumsViewModel : ModelsViewModel<TAlbumModel, TAlbumEntity>
        {
            protected override ModelView<TAlbumModel, TAlbumEntity>? ModelView => new AlbumWindow();
            protected override Func<TAlbumEntity, TAlbumModel> ConvertTo => e => new TAlbumModel(e);
            protected override Predicate<TAlbumEntity> LoadPredicate => e => e.Title != null && e.Title.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase);

            public AlbumsViewModel(BaseViewModel otherViewModel) : base(otherViewModel)
            {
            }

            public override Logic.Contracts.App.IAlbumsAccess CreateController()
            {
                return new Logic.Controllers.App.AlbumsController();
            }
        }
        private class TracksViewModel : ModelsViewModel<TTrackModel, TTrackEntity>
        {
            protected override ModelView<TTrackModel, TTrackEntity>? ModelView => new TrackWindow();
            protected override Func<TTrackEntity, TTrackModel> ConvertTo => e => new TTrackModel(e);
            protected override Predicate<TTrackEntity> LoadPredicate => e => (e.Composer != null && e.Composer.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase)
                                                                                || (e.Name.Contains(ModelFilter, StringComparison.CurrentCultureIgnoreCase)));
            public TracksViewModel(BaseViewModel otherViewModel) : base(otherViewModel)
            {
            }

            public override Logic.Contracts.App.ITracksAccess CreateController()
            {
                return new Logic.Controllers.App.TracksController();
            }
        }
        #endregion delegates
      
        #region fields
        private readonly GenresViewModel _genresViewModel;
        private readonly MediaTypesViewModel _mediaTypesViewModel;
        private readonly ArtistsViewModel _artistsViewModel;
        private readonly AlbumsViewModel _albumsViewModel;
        private readonly TracksViewModel _tracksViewModel;
        #endregion fields

        #region properties

        public TGenreModel[] Genres => _genresViewModel.Models;
        public TMediaTypeModel[] MediaTypes => _mediaTypesViewModel.Models;
        public TArtistModel[] Artists => _artistsViewModel.Models;
        public TAlbumModel[] Albums => _albumsViewModel.Models;
        public TTrackModel[] Tracks => _tracksViewModel.Models;

        public string GenreFilter
        {
            get => _genresViewModel.ModelFilter;
            set => _genresViewModel.ModelFilter = value;
        }
        public string MediaTypeFilter
        {
            get => _mediaTypesViewModel.ModelFilter;
            set => _mediaTypesViewModel.ModelFilter = value;
        }
        public string ArtistFilter
        {
            get => _artistsViewModel.ModelFilter;
            set => _artistsViewModel.ModelFilter = value;
        }
        public string AlbumFilter
        {
            get => _albumsViewModel.ModelFilter;
            set => _albumsViewModel.ModelFilter = value;
        }
        public string TrackFilter
        {
            get => _tracksViewModel.ModelFilter;
            set => _tracksViewModel.ModelFilter = value;
        }

        public TGenreModel? SelectedGenre
        {
            get => _genresViewModel.SelectedModel;
            set => _genresViewModel.SelectedModel = value;
        }
        public TMediaTypeModel? SelectedMediaType
        {
            get => _mediaTypesViewModel.SelectedModel;
            set => _mediaTypesViewModel.SelectedModel = value;
        }
        public TArtistModel? SelectedArtist
        {
            get => _artistsViewModel.SelectedModel;
            set => _artistsViewModel.SelectedModel = value;
        }
        public TAlbumModel? SelectedAlbum
        {
            get => _albumsViewModel.SelectedModel;
            set => _albumsViewModel.SelectedModel = value;
        }
        public TTrackModel? SelectedTrack
        {
            get => _tracksViewModel.SelectedModel;
            set => _tracksViewModel.SelectedModel = value;
        }
        #endregion properties

        #region commands
        public ICommand CommandAddGenre => _genresViewModel.CommandAddModel;
        public ICommand CommandEditGenre => _genresViewModel.CommandEditModel;
        public ICommand CommandDeleteGenre => _genresViewModel.CommandDeleteModel;

        public ICommand CommandAddMediaType => _mediaTypesViewModel.CommandAddModel;
        public ICommand CommandEditMediaType => _mediaTypesViewModel.CommandEditModel;
        public ICommand CommandDeleteMediaType => _mediaTypesViewModel.CommandDeleteModel;

        public ICommand CommandAddArtist => _artistsViewModel.CommandAddModel;
        public ICommand CommandEditArtist => _artistsViewModel.CommandEditModel;
        public ICommand CommandDeleteArtist => _artistsViewModel.CommandDeleteModel;

        public ICommand CommandAddAlbum => _albumsViewModel.CommandAddModel;
        public ICommand CommandEditAlbum => _albumsViewModel.CommandEditModel;
        public ICommand CommandDeleteAlbum => _albumsViewModel.CommandDeleteModel;

        public ICommand CommandAddTrack => _tracksViewModel.CommandAddModel;
        public ICommand CommandEditTrack => _tracksViewModel.CommandEditModel;
        public ICommand CommandDeleteTrack => _tracksViewModel.CommandDeleteModel;
        #endregion commands

        public MainViewModel()
        {
            _genresViewModel = new GenresViewModel(this);
            _mediaTypesViewModel = new MediaTypesViewModel(this);
            _artistsViewModel = new ArtistsViewModel(this);
            _albumsViewModel = new AlbumsViewModel(this);
            _tracksViewModel = new TracksViewModel(this);
        }
        internal override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var data = propertyName?.Split("_");

            if (data?.Length == 2)
            {
                var delegatePropName = $"{data[1].Replace("Model", data[0])}";

                base.OnPropertyChanged(delegatePropName);
            }
            else
            {
                base.OnPropertyChanged(propertyName);
            }
        }
    }
}
//MdEnd