﻿//@CodeCopy
//MdStart
namespace TemplateCodeGenerator.Logic.Generation
{
    using System.Reflection;
    using TemplateCodeGenerator.Logic.Common;
    using TemplateCodeGenerator.Logic.Contracts;
    using TemplateCodeGenerator.Logic.Models;
    internal partial class Configuration
    {
        static Configuration()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();

        #region fields
        private GenerationSetting[]? generationSettings = null;
        #endregion fields

        #region properties
        public GenerationSetting[] GenerationSettings
        {
            get
            {
                if (generationSettings == null)
                {
                    var filePath = Path.Combine(SolutionProperties.SolutionPath, "CodeGeneration.csv");

                    if (File.Exists(filePath))
                    {
                        try
                        {
                            generationSettings = File.ReadAllLines(filePath, System.Text.Encoding.Default)
                                                     .Skip(1)
                                                     .Where(l => l.HasContent() && l.StartsWith("#") == false)
                                                     .Select(l => l.Split(';'))
                                                     .Select(d =>
                                                     {
                                                         var gs = new GenerationSetting();

                                                         try
                                                         {
                                                             gs.UnitType = d[0];
                                                             gs.ItemType = d[1];
                                                             gs.ItemName = d[2];
                                                             gs.Name = d[3];
                                                             gs.Value = d[4];
                                                         }
                                                         catch (Exception ex)
                                                         {
                                                             System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
                                                         }
                                                         return gs;
                                                     })
                                                     .ToArray();
                        }
                        catch (Exception ex)
                        {
                            generationSettings = Array.Empty<GenerationSetting>();
                            System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
                        }
                    }
                    else
                    {
                        generationSettings = Array.Empty<GenerationSetting>();
                    }
                }
                return generationSettings;
            }
        }
        public ISolutionProperties SolutionProperties { get; init; }
        #endregion properties

        #region construction
        public Configuration(ISolutionProperties solutionProperties)
        {
            Constructing();
            SolutionProperties = solutionProperties;
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        #endregion construction

        #region methods
        public T QuerySettingValue<T>(string unitType, string itemType, string itemName, string valueName, string defaultValue)
        {
            T result;

            try
            {
                result = (T)Convert.ChangeType(QuerySettingValue(unitType, itemType, itemName, valueName, defaultValue), typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(defaultValue, typeof(T));
                System.Diagnostics.Debug.WriteLine($"Error in {MethodBase.GetCurrentMethod()!.Name}: {ex.Message}");
            }
            return result;
        }
        public string QuerySettingValue(UnitType unitType, ItemType itemType, string itemName, string valueName, string defaultValue)
        {
            var result = defaultValue;
            var generationSetting = GenerationSettings.FirstOrDefault(e => e.UnitType.Equals(unitType.ToString(), StringComparison.CurrentCultureIgnoreCase)
                                                                        && e.ItemType.Equals(itemType.ToString(), StringComparison.CurrentCultureIgnoreCase)
                                                                        && itemName.EndsWith(e.ItemName)
                                                                        && e.Name.Equals(valueName, StringComparison.CurrentCultureIgnoreCase));

            if (generationSetting != null)
            {
                result = generationSetting.Value;
            }
            return result;
        }
        public string QuerySettingValue(string unitType, string itemType, string itemName, string valueName, string defaultValue)
        {
            var result = defaultValue;
            var generationSetting = GenerationSettings.FirstOrDefault(e => e.UnitType.Equals(unitType, StringComparison.CurrentCultureIgnoreCase)                                                                       && e.ItemType == itemType
                                                                        && itemName.EndsWith(e.ItemName)
                                                                        && e.Name.Equals(valueName, StringComparison.CurrentCultureIgnoreCase));

            if (generationSetting != null)
            {
                result = generationSetting.Value;
            }
            return result;
        }
        #endregion methods
    }
}
//MdEnd
