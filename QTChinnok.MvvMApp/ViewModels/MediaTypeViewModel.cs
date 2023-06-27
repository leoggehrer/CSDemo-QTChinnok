#if GENERATEDCODE_ON
using System.Linq;

namespace QTChinnok.MvvMApp.ViewModels
{
    using TModel = Models.MediaType;
    using TEntity = Logic.Models.Base.MediaType;

    public class MediaTypeViewModel : ModelViewModel<TModel, TEntity>
    {
        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertiesChanged();
            }
        }
        public override Logic.Contracts.Base.IMediaTypesAccess CreateController()
        {
            return new Logic.Controllers.Base.MediaTypesController();
        }
        protected override void OnPropertiesChanged(params string[] propertyNames)
        {
            base.OnPropertiesChanged(propertyNames.Union(new string[] { "Id", "Name" }).ToArray());
        }
    }
}
#endif