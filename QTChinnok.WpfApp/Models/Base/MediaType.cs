﻿//@GeneratedCode
namespace QTChinnok.WpfApp.Models.Base
{
    using System;
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    
    public partial class MediaType
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static MediaType()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public MediaType()
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        /// <summary>
        /// Property 'Name' generated by the generator.
        /// </summary>
        public System.String? Name { get; set; }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.WpfApp.Models.Base.MediaType Create()
        {
            BeforeCreate();
            var result = new QTChinnok.WpfApp.Models.Base.MediaType();
            AfterCreate(result);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.WpfApp.Models.Base.MediaType Create(object other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.WpfApp.Models.Base.MediaType();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTChinnok.WpfApp.Models.Base.MediaType instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTChinnok.WpfApp.Models.Base.MediaType instance, object other);
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.MediaType other)
            {
                result = Id == other.Id;
            }
            return result;
        }
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public override int GetHashCode()
        {
            return this.CalculateHashCode(Id, Name);
        }
    }
}
