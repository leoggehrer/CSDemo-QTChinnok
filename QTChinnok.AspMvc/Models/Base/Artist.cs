﻿//@GeneratedCode
namespace QTChinnok.AspMvc.Models.Base
{
    using System;
    ///
    /// Generated by the generator
    ///
    
    public partial class Artist
    {
        ///
        /// Generated by the generator
        ///
        static Artist()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public Artist()
        {
            Constructing();
            
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        
        public System.String? Name { get; set; }
        ///
        /// Generated by the generator
        ///
        
        public System.Collections.Generic.List<QTChinnok.AspMvc.Models.App.Album> Albums { get; set; } = new();
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.AspMvc.Models.Base.Artist Create()
        {
            BeforeCreate();
            var result = new QTChinnok.AspMvc.Models.Base.Artist();
            AfterCreate(result);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.AspMvc.Models.Base.Artist Create(object other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.AspMvc.Models.Base.Artist();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTChinnok.AspMvc.Models.Base.Artist instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTChinnok.AspMvc.Models.Base.Artist instance, object other);
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.AspMvc.Models.Base.Artist Create(QTChinnok.Logic.Models.Base.Artist other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.AspMvc.Models.Base.Artist();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate(QTChinnok.Logic.Models.Base.Artist other);
        static partial void AfterCreate(QTChinnok.AspMvc.Models.Base.Artist instance, QTChinnok.Logic.Models.Base.Artist other);
        ///
        /// Generated by the generator
        ///
        public void CopyProperties(QTChinnok.Logic.Models.Base.Artist other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Name = other.Name;
                Albums = other.Albums.Select(e => QTChinnok.AspMvc.Models.App.Album.Create((object)e)).ToList();
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QTChinnok.Logic.Models.Base.Artist other, ref bool handled);
        partial void AfterCopyProperties(QTChinnok.Logic.Models.Base.Artist other);
        ///
        /// Generated by the generator
        ///
        public void CopyProperties(QTChinnok.AspMvc.Models.Base.Artist other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Name = other.Name;
                Albums = other.Albums.Select(e => QTChinnok.AspMvc.Models.App.Album.Create((object)e)).ToList();
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QTChinnok.AspMvc.Models.Base.Artist other, ref bool handled);
        partial void AfterCopyProperties(QTChinnok.AspMvc.Models.Base.Artist other);
        ///
        /// Generated by the generator
        ///
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.Artist other)
            {
                result = Id == other.Id;
            }
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public override int GetHashCode()
        {
            return this.CalculateHashCode(Id, Name, Albums);
        }
    }
}
