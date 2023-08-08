#if GENERATEDCODE_ON
using System.Linq;

namespace QTChinnok.MvvMApp.ViewModels
{
    using TModel = Models.Genre;
    using TEntity = Logic.Models.Base.Genre;

    public class GenreViewModel : ModelViewModel<TModel, TEntity>
    {
        public string Name
        {
            get => Model.Name;
            set => Model.Name = value;
        }

        public override Logic.Contracts.Base.IGenresAccess CreateController()
        {
            return new Logic.Controllers.Base.GenresController();
        }

        protected override void OnPropertiesChanged(params string[] propertyNames)
        {
            base.OnPropertiesChanged(propertyNames.Union(new string[] { nameof(Id), nameof(Name) }).ToArray());
        }
    }
}
#endif