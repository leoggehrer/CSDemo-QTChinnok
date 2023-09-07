namespace QTChinnok.WpfApp.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using TGenre = Models.Base.Genre;

    public class GenreViewModel : BaseViewModel
    {
        #region fields
        private ICommand? _cmdSave;
        private ICommand? _cmdClose;
        private TGenre? _model;
        #endregion fields

        #region properties
        public TGenre Model
        {
            get => _model ??= new TGenre();
            set
            {
                _model = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public int Id
        {
            get => Model.Id;
            set => Model.Id = value;
        }
        public string? Name
        {
            get => Model.Name;
            set => Model.Name = value ?? string.Empty;
        }
        #endregion properties

        #region commands
        public ICommand CommandSave => RelayCommand.Create(ref _cmdSave, p => Save());
        public ICommand CommandClose => RelayCommand.Create(ref _cmdClose, p => Close());
        #endregion commands

        #region methods
        private void Save()
        {
            bool error = false;
            string errorMessage = string.Empty;

            Task.Run(async () =>
            {
                using var ctrl = new Logic.Controllers.Base.GenresController();

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
        #endregion methods
    }
}
