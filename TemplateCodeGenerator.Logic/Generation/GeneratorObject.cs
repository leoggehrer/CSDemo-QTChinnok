﻿//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.Logic.Generation
{
    using System.Reflection;
    using TemplateCodeGenerator.Logic.Common;
    using TemplateCodeGenerator.Logic.Contracts;
    using TemplateCodeGenerator.Logic.Extensions;
    internal abstract partial class GeneratorObject
    {
        static GeneratorObject()
        {
            ClassConstructing();

            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        #region properties
        public Configuration Configuration { get; init; }
        public ISolutionProperties SolutionProperties { get; init; }
        #endregion properties

        #region constructor
        public GeneratorObject(ISolutionProperties solutionProperties)
        {
            Constructing();
            SolutionProperties = solutionProperties;
            Configuration = new Configuration(solutionProperties);
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        #endregion constructor

        #region query settings
        public string QueryGenerationSettingValue(string unitType, string itemType, string itemName, string valueName, string defaultValue)
        {
            return Configuration.QuerySettingValue(unitType, itemType, itemName, valueName, defaultValue);
        }
        public string QueryGenerationSettingValue(UnitType unitType, ItemType itemType, string itemName, string valueName, string defaultValue)
        {
            return Configuration.QuerySettingValue(unitType, itemType, itemName, valueName, defaultValue);
        }
        #endregion query settings

        #region Helpers
        public static bool IsArrayType(Type type)
        {
            return type.IsArray;
        }
        public static bool IsListType(Type type)
        {
            return type.FullName!.StartsWith("System.Collections.Generic.List")
                || type.FullName!.StartsWith("System.Collections.Generic.IList");
        }

        /// <summary>
        /// Diese Methode ermittelt den Teilnamensraum aus einem Typ.
        /// </summary>
        /// <param name="type">Typ</param>
        /// <returns>Teil-Namensraum</returns>
        public static string CreateSubNamespaceFromType(Type type)
        {
            var result = string.Empty;
            var data = type.Namespace?.Split('.');

            for (var i = 2; i < data?.Length; i++)
            {
                if (string.IsNullOrEmpty(result))
                {
                    result = $"{data[i]}";
                }
                else
                {
                    result = $"{result}.{data[i]}";
                }
            }
            return result;
        }
        /// <summary>
        /// Diese Methode ermittelt den Teil-Path aus einem Typ.
        /// </summary>
        /// <param name="type">Typ</param>
        /// <returns>Teil-Path</returns>
        public static string CreateSubPathFromType(Type type)
        {
            return CreateSubNamespaceFromType(type).Replace(".", Path.DirectorySeparatorChar.ToString());
        }

        /// <summary>
        /// Diese Methode ermittelt den Entity Namen aus seinem Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Name der Entitaet.</returns>
        public static string CreateEntityNameFromType(Type type)
        {
            return type.Name;
        }
        /// <summary>
        /// Diese Methode ermittelt den Model Namen aus seinem Typ.
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Name des Models.</returns>
        public static string CreateModelName(Type type)
        {
            return type.Name;
        }
        /// <summary>
        /// Diese Methode ermittelt den Entity-Typ aus seiner Type (eg. App.Type).
        /// </summary>
        /// <param name="type">Schnittstellen-Typ</param>
        /// <returns>Typ der Entitaet.</returns>
        public static string CreateEntitiesSubTypeFromType(Type type)
        {
            var entityName = CreateEntityNameFromType(type);

            return $"{CreateSubNamespaceFromType(type)}.{entityName}".Replace($"{StaticLiterals.EntitiesFolder}.", string.Empty);
        }

        #region Comment-Helpers
        protected virtual IEnumerable<string> CreateComment()
        {
            var result = new List<string>()
            {
                "/// <summary>",
                "/// Generated by the generator",
                "/// </summary>",
            };
            return result;
        }
        protected virtual IEnumerable<string> CreateComment(Type type)
        {
            var result = new List<string>()
            {
                "/// <summary>",
                "/// Generated by the generator.",
                "/// </summary>",
            };
            return result;
        }
        protected virtual IEnumerable<string> CreateComment(PropertyInfo propertyInfo)
        {
            var result = new List<string>()
            {
                "/// <summary>",
               $"/// Property '{propertyInfo.Name}' generated by the generator.",
                "/// </summary>",
            };
            return result;
        }
        #endregion Comment-Helpers

        #region Property-Helpers
        /// <summary>
        /// Determines whether the specified <see cref="PropertyInfo"/> object represents a reference property like a Id or ArtistId or MenuId_Parent.
        /// </summary>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/> object to check.</param>
        /// <returns>
        ///   <c>true</c> if the property is a reference property; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsReferenceProperty(PropertyInfo propertyInfo)
        {
            var result = false;
            var idText = "Id";

            if (propertyInfo.Name.Length > 2 && propertyInfo.Name.EndsWith(idText))
            {
                result = true;
            }
            else if (propertyInfo.Name.Contains($"{idText}_"))
            {
                var idx = propertyInfo.Name.IndexOf($"{idText}_");

                if (idx > 0 && idx + idText.Length + 1 < propertyInfo.Name.Length)
                {
                    result = true;
                }
            }
            return result;
        }
        protected virtual string ConvertPropertyType(string type) => type;
        /// <summary>
        /// This method analyzes a property and determines its type, 
        /// considering if it is nullable and whether it is a reference property or not. 
        /// It returns the type as a string, potentially appending a question mark to indicate nullability if necessary.
        /// </summary>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/> object to check.</param>
        /// <returns>The type as a string.</returns>
        protected virtual string GetPropertyType(PropertyInfo propertyInfo)
        {
            var nullable = propertyInfo.IsNullable();
            var result = IsReferenceProperty(propertyInfo) ? StaticLiterals.IdType : propertyInfo.PropertyType.GetCodeDefinition();

            if (nullable && result.EndsWith('?') == false)
            {
                result += '?';
            }
            return ConvertPropertyType(result);
        }
        /// <summary>
        /// Diese Methode ermittelt den Feldnamen der Eigenschaft.
        /// </summary>
        /// <param name="propertyInfo">Das Eigenschaftsinfo-Objekt.</param>
        /// <param name="prefix">Prefix der dem Namen vorgestellt ist.</param>
        /// <returns>Der Feldname als Zeichenfolge.</returns>
        public static string CreateFieldName(PropertyInfo propertyInfo, string prefix)
        {
            return $"{prefix}{char.ToLower(propertyInfo.Name.First())}{propertyInfo.Name[1..]}";
        }
        public static string GetDefaultValue(PropertyInfo propertyInfo)
        {
            string result = string.Empty;

            if (propertyInfo.IsNullable() == false)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    result = "string.Empty";
                }
                else if (IsArrayType(propertyInfo.PropertyType))
                {
                    result = $"Array.Empty<{StaticLiterals.TProperty}>()";
                }
                else if (IsListType(propertyInfo.PropertyType))
                {
                    result = "new()";
                }
            }
            return result;
        }
        public static string CreateParameterName(PropertyInfo propertyInfo) => $"_{char.ToLower(propertyInfo.Name[0])}{propertyInfo.Name[1..]}";
        #endregion Property-Helpers
        #endregion Helpers
    }
}
//MdEnd
