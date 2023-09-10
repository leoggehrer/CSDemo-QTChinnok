//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.Logic.Generation
{
    /*
     * Contracts:
     *  - AccessContract
     *  - ServcieContract
     *  Models:
     *  - AccessModel
     *  Controllers:
     *  - Controller
     *  - Service
     *  DataContext:
     *  - ProjectDbContextGeneration
     *  Facades:
     *  - Facades
     */
    using System.Reflection;
    using TemplateCodeGenerator.Logic.Common;
    using TemplateCodeGenerator.Logic.Contracts;

    internal sealed partial class LogicGenerator : ModelGenerator
    {
        #region fields
        private ItemProperties? _itemProperties;
        #endregion fields

        #region properties
        protected override ItemProperties ItemProperties => _itemProperties ??= new ItemProperties(SolutionProperties.SolutionName, StaticLiterals.LogicExtension);

        public bool GenerateDbContext { get; set; }
        public bool GenerateAllAccessModels { get; set; }

        public bool GenerateAllModelContracts { get; set; }
        public bool GenerateAllAccessContracts { get; set; }
        public bool GenerateAllControllers { get; set; }

        public bool GenerateAllServiceContracts { get; set; }
        public bool GenerateAllServices { get; set; }
        public string BaseAddress { get; set; }

        private bool GenerateAllFacades { get; set; }
        #endregion properties

        #region constructors
        public LogicGenerator(ISolutionProperties solutionProperties) : base(solutionProperties)
        {
            var generateAll = QuerySetting<string>(ItemType.AllItems, StaticLiterals.AllItems, StaticLiterals.Generate, "True");

            GenerateDbContext = QuerySetting<bool>(ItemType.DbContext, "All", StaticLiterals.Generate, generateAll);
            GenerateAllAccessModels = QuerySetting<bool>(ItemType.AccessModel, "All", StaticLiterals.Generate, generateAll);

            GenerateAllModelContracts = QuerySetting<bool>(ItemType.ModelContract, "All", StaticLiterals.Generate, generateAll);

            GenerateAllAccessContracts = QuerySetting<bool>(ItemType.ServiceAccessContract, "All", StaticLiterals.Generate, generateAll);
            GenerateAllControllers = QuerySetting<bool>(ItemType.Controller, "All", StaticLiterals.Generate, generateAll);

            GenerateAllServiceContracts = QuerySetting<bool>(ItemType.ServiceContract, "All", StaticLiterals.Generate, generateAll);
            GenerateAllServices = QuerySetting<bool>(ItemType.Service, "All", StaticLiterals.Generate, generateAll);
            BaseAddress = QuerySetting<string>(ItemType.Service, "All", nameof(BaseAddress), "https://localhost:7085/api");

            GenerateAllFacades = QuerySetting<bool>(ItemType.Facade, "All", StaticLiterals.Generate, generateAll);
        }
        #endregion constructors

        #region overrides
        public override IEnumerable<string> CreateDelegateAutoProperty(PropertyInfo propertyInfo, string delegateObjectName, PropertyInfo delegatePropertyInfo)
        {
            var result = new List<string>();

            if (IsListType(propertyInfo.PropertyType))
            {
                var entityType = propertyInfo.PropertyType.GenericTypeArguments[0].FullName;
                var modelType = ItemProperties.ConvertEntityToModelType(propertyInfo.PropertyType.GenericTypeArguments[0].FullName!);
                var fieldName = CreateFieldName(propertyInfo, "_");
                var internalPropertyName = $"Internal{propertyInfo.Name}";

                result.Add(string.Empty);
                result.Add($"private CommonBase.Modules.Collection.DelegateList<{entityType}, {modelType}>? {fieldName};");
                result.Add($"internal CommonBase.Modules.Collection.DelegateList<{entityType}, {modelType}>? {internalPropertyName}");
                result.Add("{");
                result.Add($"get => {fieldName};");
                result.Add($"set => {fieldName} = value;");
                result.Add("}");
                result.AddRange(CreateComment(propertyInfo));
                CreatePropertyAttributes(propertyInfo, result);
                result.Add($"public System.Collections.Generic.IList<{modelType}> {propertyInfo.Name}");
                result.Add("{");
                result.Add($"get => {fieldName} ??= new CommonBase.Modules.Collection.DelegateList<{entityType}, {modelType}>({delegateObjectName}.{delegatePropertyInfo.Name}, e => {modelType}.Create(e));");
                result.Add("}");
            }
            else
            {
                result.AddRange(base.CreateDelegateAutoProperty(propertyInfo, delegateObjectName, delegatePropertyInfo));
            }
            return result;
        }
        public override IEnumerable<string> CreateDelegateAutoGet(PropertyInfo propertyInfo, string delegateObjectName, PropertyInfo delegatePropertyInfo)
        {
            var result = new List<string>();
            var propertyType = GetPropertyType(propertyInfo);
            var delegateProperty = $"{delegateObjectName}.{delegatePropertyInfo.Name}";
            var visibility = propertyInfo.GetGetMethod(true)!.IsPublic ? string.Empty : "internal ";

            if (ItemProperties.IsModelType(propertyType))
            {
                if (IsArrayType(propertyInfo.PropertyType))
                {
                    var modelType = ItemProperties.ConvertEntityToModelType(propertyInfo.PropertyType.GetElementType()!.FullName!);

                    result.Add($"{visibility}get => {delegateProperty}.Select(e => {modelType}.Create(e)).ToArray();");
                }
                else if (IsListType(propertyInfo.PropertyType))
                {
                    var modelType = ItemProperties.ConvertEntityToModelType(propertyInfo.PropertyType.GenericTypeArguments[0].FullName!);

                    result.Add($"{visibility}get => {delegateProperty}.Select(e => {modelType}.Create(e)).ToList();");
                }
                else
                {
                    result.Add($"{visibility}get => {delegateProperty} != null ? {propertyType.Replace("?", string.Empty)}.Create({delegateProperty}) : null;");
                }
            }
            else
            {
                result.Add($"{visibility}get => {delegateObjectName}.{delegatePropertyInfo.Name};");
            }
            return result;
        }
        public override IEnumerable<string> CreateDelegateAutoSet(PropertyInfo propertyInfo, string delegateObjectName, PropertyInfo delegatePropertyInfo)
        {
            var result = new List<string>();
            var propertyType = GetPropertyType(propertyInfo);
            var delegateProperty = $"{delegateObjectName}.{delegatePropertyInfo.Name}";
            var visibility = propertyInfo.GetSetMethod(true)!.IsPublic ? string.Empty : "internal ";

            if (ItemProperties.IsModelType(propertyType))
            {
                if (IsArrayType(propertyInfo.PropertyType))
                {
                    result.Add($"{visibility}set => {delegateProperty} = value.Select(e => e.{delegateObjectName}).ToArray();");
                }
                else if (IsListType(propertyInfo.PropertyType))
                {
                    //result.Add($"{visibility}set => {delegateProperty} = value.Select(e => e.{delegateObjectName}).ToList();");
                }
                else
                {
                    result.Add($"{visibility}set => {delegateProperty} = value?.{delegateObjectName};");
                }
            }
            else
            {
                result.Add($"{visibility}set => {delegateObjectName}.{delegatePropertyInfo.Name} = value;");
            }
            return result;
        }
        #endregion overrides

        #region generations
        public IEnumerable<IGeneratedItem> GenerateAll()
        {
            var result = new List<IGeneratedItem>
            {
                CreateDbContext()
            };

            result.AddRange(CreateAccessModels());
            result.AddRange(CreateModelContracts());
            result.AddRange(CreateAccessContracts());
            result.AddRange(CreateControllers());
            result.AddRange(CreateServices());
            result.AddRange(CreateFacades());
            //result.Add(CreateFactory());
            return result;
        }

        private static bool GetGenerateDefault(Type type)
        {
            return !EntityProject.IsNotAGenerationEntity(type);
        }
        private static string GetVisiblityDefault(Type type)
        {
            return type.IsPublic ? "public" : "internal";
        }

        private IGeneratedItem CreateDbContext()
        {
            var entityProject = EntityProject.Create(SolutionProperties);
            var dataContextNamespace = $"{ItemProperties.Namespace}.DataContext";
            var result = new Models.GeneratedItem(UnitType.Logic, ItemType.DbContext)
            {
                FullName = $"{dataContextNamespace}.ProjectDbContext",
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = $"DataContext{Path.DirectorySeparatorChar}ProjectDbContextGeneration{StaticLiterals.CSharpFileExtension}",
            };
            result.AddRange(CreateComment());
            result.Add($"partial class ProjectDbContext");
            result.Add("{");

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateDbContext && GetGenerateDefault(type)).ToString();

                if (QuerySetting<bool>(ItemType.DbContext, type, StaticLiterals.Generate, defaultValue))
                {
                    var entityType = ItemProperties.CreateSolutionTypeSubName(type);

                    result.AddRange(CreateComment(type));
                    result.Add($"public DbSet<{entityType}> {type.Name}Set" + "{ get; set; }");
                }
            }

            result.Add(string.Empty);
            result.AddRange(CreateComment());
            result.Add($"partial void GetGeneratorDbSet<E>(ref DbSet<E>? dbSet, ref bool handled) where E : Entities.{StaticLiterals.EntityObjectName}");
            result.Add("{");

            bool first = false;

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateDbContext && GetGenerateDefault(type)).ToString();

                if (QuerySetting<bool>(ItemType.DbContext, type, StaticLiterals.Generate, defaultValue))
                {
                    var entityType = ItemProperties.CreateSolutionTypeSubName(type);

                    result.Add($"{(first ? "else " : string.Empty)}if (typeof(E) == typeof({entityType}))");
                    result.Add("{");
                    result.Add($"dbSet = {type.Name}Set as DbSet<E>;");
                    result.Add("handled = true;");
                    result.Add("}");
                    first = true;
                }
            }
            result.Add("}");

            result.Add("}");
            result.EnvelopeWithANamespace(dataContextNamespace);
            result.FormatCSharpCode();
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateAccessModels()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateAllAccessModels && GetGenerateDefault(type)).ToString();

                if (CanCreate(type)
                    && QuerySetting<bool>(ItemType.AccessModel, type, StaticLiterals.Generate, defaultValue))
                {
                    result.Add(CreateDelegateModelFromType(type, UnitType.Logic, ItemType.AccessModel));
                    result.Add(CreateDelegateModelNavigationsFromType(type, UnitType.Logic, ItemType.AccessModel));
                    result.Add(CreateModelInheritance(type, UnitType.Logic, ItemType.AccessModel));
                }
            }
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateModelContracts()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateAllModelContracts && GetGenerateDefault(type)).ToString();

                if (CanCreate(type)
                    && QuerySetting<bool>(ItemType.ModelContract, type, StaticLiterals.Generate, defaultValue))
                {
                    result.Add(CreateModelContract(type, UnitType.Logic, ItemType.ModelContract));
                    result.Add(CreateModelNavigationsContract(type, UnitType.Logic, ItemType.ModelContract));
                }
            }
            return result;
        }
        private IGeneratedItem CreateModelContract(Type type, UnitType unitType, ItemType itemType)
        {
            var contractName = ItemProperties.CreateModelContractName(type);
            var typeProperties = type.GetAllPropertyInfos();
            var generateProperties = typeProperties.Where(e => StaticLiterals.NoGenerationProperties.Any(p => p.Equals(e.Name)) == false) ?? Array.Empty<PropertyInfo>();
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = ItemProperties.CreateModelContractName(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateModelContractSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };
            result.AddRange(CreateComment(type));
            result.Add($"public partial interface {contractName}");
            result.Add("{");
            foreach (var propertyItem in generateProperties.Where(pi => ItemProperties.IsEntityType(pi.PropertyType) == false
                                                                     && ItemProperties.IsEntityListType(pi.PropertyType) == false))
            {
                if (QuerySetting<bool>(unitType, ItemType.InterfaceProperty, propertyItem.DeclaringName(), StaticLiterals.Generate, "True"))
                {
                    var getAccessor = string.Empty;
                    var setAccessor = string.Empty;
                    var propertyType = GetPropertyType(propertyItem);

                    if (QuerySetting<bool>(unitType, ItemType.InterfaceProperty, propertyItem.DeclaringName(), StaticLiterals.GetAccessor, "True"))
                    {
                        getAccessor = "get;";
                    }
                    if (QuerySetting<bool>(unitType, ItemType.InterfaceProperty, propertyItem.DeclaringName(), StaticLiterals.SetAccessor, "True"))
                    {
                        setAccessor = "set;";
                    }
                    result.Add($"{propertyType} {propertyItem.Name}" + " { " + $"{getAccessor} {setAccessor}" + " } ");
                }
            }
            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateLogicContractNamespace(type));
            result.FormatCSharpCode();
            return result;
        }
        private IGeneratedItem CreateModelNavigationsContract(Type type, UnitType unitType, ItemType itemType)
        {
            var contractPostfix = "Navigation";
            var contractName = ItemProperties.CreateModelNavigationContractName(type);
            var typeProperties = type.GetAllPropertyInfos();
            var generateProperties = typeProperties.Where(e => StaticLiterals.NoGenerationProperties.Any(p => p.Equals(e.Name)) == false) ?? Array.Empty<PropertyInfo>();
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = ItemProperties.CreateModelNavigationContractName(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateModelContractSubPathFromType(type, contractPostfix, StaticLiterals.CSharpFileExtension),
            };
            result.AddRange(CreateComment(type));
            result.Add($"public partial interface {contractName}");
            result.Add("{");
            foreach (var propertyItem in generateProperties.Where(pi => ItemProperties.IsEntityType(pi.PropertyType)
                                                                     || ItemProperties.IsEntityListType(pi.PropertyType)))
            {
                if (QuerySetting<bool>(unitType, ItemType.InterfaceProperty, propertyItem.DeclaringName(), StaticLiterals.Generate, "True"))
                {
                    var propertyType = GetPropertyType(propertyItem);

                    if (ItemProperties.IsModelType(propertyType))
                    {
                        if (IsArrayType(propertyItem.PropertyType))
                        {
                            var modelType = ItemProperties.ConvertEntityToModelType(propertyItem.PropertyType.GetElementType()!.FullName!);

                            result.Add($"{modelType}[] {propertyItem.Name}" + " { get; }");
                        }
                        else if (IsListType(propertyItem.PropertyType))
                        {
                            var modelType = ItemProperties.ConvertEntityToModelType(propertyItem.PropertyType.GenericTypeArguments[0].FullName!);

                            result.Add($"System.Collections.Generic.IList<{modelType}> {propertyItem.Name}" + " { get; }");
                        }
                        else
                        {
                            var modelType = ItemProperties.ConvertEntityToModelType(type.FullName!);

                            result.Add($"{modelType} {propertyItem.Name}" + " { get; }");
                        }
                    }
                }
            }
            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateLogicContractNamespace(type));
            result.FormatCSharpCode();
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateAccessContracts()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateAllAccessContracts && GetGenerateDefault(type)).ToString();

                if (CanCreate(type)
                    && QuerySetting<bool>(ItemType.AccessContract, type, StaticLiterals.Generate, defaultValue))
                {
                    result.Add(CreateAccessContract(type, UnitType.Logic, ItemType.AccessContract));
                }
            }

            foreach (var type in entityProject.ServiceTypes)
            {
                var defaultValue = (GenerateAllServiceContracts && GetGenerateDefault(type)).ToString();

                if (CanCreate(type)
                    && QuerySetting<bool>(ItemType.ServiceContract, type, StaticLiterals.Generate, defaultValue))
                {
                    result.Add(CreateServiceContract(type, UnitType.Logic, ItemType.ServiceContract));
                }
            }
            return result;
        }
        private IGeneratedItem CreateAccessContract(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var contractName = ItemProperties.CreateAccessContractName(type);
            var outModelType = ItemProperties.CreateModelSubType(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = ItemProperties.CreateAccessContractType(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateAccessContractSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };
            result.Add($"using TOutModel = {outModelType};");
            result.AddRange(CreateComment(type));
            result.Add($"public partial interface {contractName} : BaseContracts.IDataAccess<TOutModel>");
            result.Add("{");
            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateLogicContractNamespace(type));
            result.FormatCSharpCode();
            return result;
        }
        private IGeneratedItem CreateServiceContract(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var contractName = ItemProperties.CreateServiceContractName(type);
            var outModelType = ItemProperties.CreateModelSubType(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = ItemProperties.CreateServiceContractType(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateServiceContractSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };
            result.Add($"using TOutModel = {outModelType};");
            result.AddRange(CreateComment(type));
            result.Add($"public partial interface {contractName} : BaseContracts.IBaseAccess<TOutModel>");
            result.Add("{");
            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateLogicContractNamespace(type));
            result.FormatCSharpCode();
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateControllers()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            foreach (var type in entityProject.EntityTypes)
            {
                var defaultValue = (GenerateAllAccessContracts && GenerateAllControllers && GetGenerateDefault(type)).ToString();

                if (CanCreate(type)
                    && QuerySetting<bool>(ItemType.Controller, type, StaticLiterals.Generate, defaultValue))
                {
                    result.Add(CreateControllerFromType(type, UnitType.Logic, ItemType.Controller));
                }
            }
            return result;
        }
        private IGeneratedItem CreateControllerFromType(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var visibility = QuerySetting<string>(itemType, type, StaticLiterals.Visibility, GetVisiblityDefault(type));
            var attributes = QuerySetting<string>(itemType, type, StaticLiterals.Attributes, string.Empty);
            var controllerGenericType = QuerySetting<string>(itemType, "All", StaticLiterals.ControllerGenericType, "EntitiesController");
            var entityType = ItemProperties.CreateSolutionTypeSubName(type);
            var outModelType = ItemProperties.CreateModelSubType(type);
            var controllerName = ItemProperties.CreateControllerClassName(type);
            var contractSubType = ItemProperties.CreateAccessContractSubType(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = $"{ItemProperties.CreateControllerType(type)}",
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateControllersSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };

            controllerGenericType = QuerySetting<string>(itemType, type, StaticLiterals.ControllerGenericType, controllerGenericType);
            result.Add($"using TEntity = {entityType};");
            result.Add($"using TOutModel = {outModelType};");
            result.AddRange(CreateComment(type));
            CreateControllerAttributes(type, result.Source);
            result.Add($"{(attributes.HasContent() ? $"[{attributes}]" : string.Empty)}");
            result.Add($"{visibility} sealed partial class {controllerName} : {controllerGenericType}<TEntity, TOutModel>, {contractSubType}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName));
            result.AddRange(CreatePartialConstrutor("public", controllerName, "ControllerObject other", "base(other)", null, false));

            result.AddRange(CreateComment(type));
            result.Add($"internal override TOutModel ToModel(TEntity entity)");
            result.Add("{");
            result.Add("var handled = false;");
            result.Add("TOutModel? result = default;");
            result.Add(string.Empty);
            result.Add("BeforeToOutModel(entity, ref result, ref handled);");
            result.Add("if (handled == false || result == default)");
            result.Add("{");
            result.Add("result = new TOutModel(entity);");
            result.Add("}");
            result.Add("AfterToOutModel(entity, result);");
            result.Add("return result;");
            result.Add("}");
            result.Add("partial void BeforeToOutModel(TEntity entity, ref TOutModel? result, ref bool handled);");
            result.Add("partial void AfterToOutModel(TEntity entity, TOutModel result);");

            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateControllerNamespace(type));
            result.FormatCSharpCode();
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateServices()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            if (GenerateAllServices)
            {
                foreach (var type in entityProject.ServiceTypes)
                {
                    var defaultValue = (GenerateAllServiceContracts && GenerateAllServices && GetGenerateDefault(type)).ToString();

                    if (CanCreate(type)
                        && QuerySetting<bool>(ItemType.Service, type, StaticLiterals.Generate, defaultValue))
                    {
                        result.Add(CreateServiceFromType(type, UnitType.Logic, ItemType.Service));
                    }
                }
            }
            return result;
        }
        private IGeneratedItem CreateServiceFromType(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var visibility = QuerySetting<string>(itemType, type, StaticLiterals.Visibility, type.IsPublic ? "public" : "internal");
            var attributes = QuerySetting<string>(itemType, type, StaticLiterals.Attributes, string.Empty);
            var baseAddress = QuerySetting<string>(itemType, type, nameof(BaseAddress), BaseAddress);
            var requestUri = QuerySetting<string>(itemType, type, "RequestUri", type.Name.CreatePluralWord());
            var serviceGenericType = QuerySetting<string>(itemType, "All", StaticLiterals.ServiceGenericType, "BaseServices.GenericService");
            var serviceType = ItemProperties.CreateSolutionTypeSubName(type);
            var serviceName = ItemProperties.CreateServiceClassName(type);
            var contractSubType = ItemProperties.CreateServiceContractSubType(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = $"{ItemProperties.CreateServiceType(type)}",
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateServicesSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };

            serviceGenericType = QuerySetting<string>(itemType, type, StaticLiterals.ServiceGenericType, serviceGenericType);
            result.Add($"using TModel = {serviceType};");
            result.AddRange(CreateComment(type));
            CreateControllerAttributes(type, result.Source);
            result.Add($"{(attributes.HasContent() ? $"[{attributes}]" : string.Empty)}");
            result.Add($"{visibility} sealed partial class {serviceName} : {serviceGenericType}<TModel>, {contractSubType}");
            result.Add("{");
            result.Add($"private static string ServiceBaseAddress = \"{baseAddress}\";");
            result.Add($"private static string ServiceRequestUri = \"{requestUri}\";");
            result.AddRange(CreatePartialStaticConstrutor(serviceName));
            result.AddRange(CreatePartialConstrutor("public", serviceName, null, "base(ServiceBaseAddress, ServiceRequestUri)"));
            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateServiceNamespace(type));
            result.FormatCSharpCode();
            return result;
        }

        private IEnumerable<IGeneratedItem> CreateFacades()
        {
            var result = new List<IGeneratedItem>();
            var entityProject = EntityProject.Create(SolutionProperties);

            if (GenerateAllFacades)
            {
                foreach (var type in entityProject.EntityTypes)
                {
                    var defaultValue = (GenerateAllAccessContracts && GenerateAllControllers && GenerateAllFacades && GetGenerateDefault(type)).ToString();

                    if (CanCreate(type)
                        && QuerySetting<bool>(ItemType.Facade, type, StaticLiterals.Generate, defaultValue))
                    {
                        result.Add(CreateFacadeFromType(type, UnitType.Logic, ItemType.Facade));
                    }
                }
            }

            return result;
        }
        private IGeneratedItem CreateFacadeFromType(Type type, Common.UnitType unitType, Common.ItemType itemType)
        {
            var facadeGenericType = QuerySetting<string>(itemType, "All", StaticLiterals.FacadeGenericType, "ControllerFacade");
            var outModelType = $"{ItemProperties.CreateModelSubType(type)}";
            var controllerType = ItemProperties.CreateControllerType(type);
            var facadeName = ItemProperties.CreateFactoryFacadeMethodName(type);
            var contractSubType = ItemProperties.CreateFacadeContractSubType(type);
            var result = new Models.GeneratedItem(unitType, itemType)
            {
                FullName = ItemProperties.CreateFacadeType(type),
                FileExtension = StaticLiterals.CSharpFileExtension,
                SubFilePath = ItemProperties.CreateFacadesSubPathFromType(type, string.Empty, StaticLiterals.CSharpFileExtension),
            };

            facadeGenericType = QuerySetting<string>(itemType, type, StaticLiterals.FacadeGenericType, facadeGenericType);
            result.Add($"using TOutModel = {outModelType};");
            result.AddRange(CreateComment(type));
            result.Add($"public sealed partial class {facadeName} : {facadeGenericType}<TOutModel>, {contractSubType}");
            result.Add("{");
            result.AddRange(CreatePartialStaticConstrutor(facadeName));
            result.AddRange(CreatePartialConstrutor("public", facadeName, null, $"base(new {controllerType}())"));
            result.AddRange(CreatePartialConstrutor("public", facadeName, "FacadeObject facadeObject", $"base(new {controllerType}(facadeObject.ControllerObject))", withPartials: false));

            result.Add("}");
            result.EnvelopeWithANamespace(ItemProperties.CreateFacadeNamespace(type));
            result.FormatCSharpCode();
            return result;
        }

        private T QuerySetting<T>(Common.ItemType itemType, Type type, string valueName, string defaultValue)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(QueryGenerationSettingValue(UnitType.Logic, itemType, CreateEntitiesSubTypeFromType(type), valueName, defaultValue), typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(defaultValue, typeof(T));
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
            return result;
        }
        private T QuerySetting<T>(Common.ItemType itemType, string itemName, string valueName, string defaultValue)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(QueryGenerationSettingValue(UnitType.Logic, itemType, itemName, valueName, defaultValue), typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(defaultValue, typeof(T));
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
            return result;
        }
        #endregion generations

        #region partial methods
        partial void CreateControllerAttributes(Type type, List<string> codeLines);
        #endregion partial methods
    }
}
//MdEnd
