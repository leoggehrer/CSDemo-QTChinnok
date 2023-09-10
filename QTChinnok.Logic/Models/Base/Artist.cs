﻿//@GeneratedCode
namespace QTChinnok.Logic.Models.Base
{
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    public partial class Artist
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static Artist()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public Artist()
        {
            Constructing();
            _source = new Entities.Base.Artist();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        /// <summary>
        /// Generated by the generator
        /// </summary>
        internal Artist(Entities.Base.Artist source)
        {
            Constructing();
            _source = source;
            Constructed();
        }
        
        new internal Entities.Base.Artist Source
        {
            get => (Entities.Base.Artist)(_source!);
            set => _source = value;
        }
        
        /// <summary>
        /// Property 'Name' generated by the generator.
        /// </summary>
        public System.String? Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        internal void CopyProperties(Entities.Base.Artist other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Name = other.Name;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(Entities.Base.Artist other, ref bool handled);
        partial void AfterCopyProperties(Entities.Base.Artist other);
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public void CopyProperties(QTChinnok.Logic.Models.Base.Artist other)
        {
            bool handled = false;
            BeforeCopyProperties(other, ref handled);
            if (handled == false)
            {
                Id = other.Id;
                Name = other.Name;
            }
            AfterCopyProperties(other);
        }
        partial void BeforeCopyProperties(QTChinnok.Logic.Models.Base.Artist other, ref bool handled);
        partial void AfterCopyProperties(QTChinnok.Logic.Models.Base.Artist other);
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.Artist other)
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
            return this.CalculateHashCode(Id, Name, Albums);
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Artist Create()
        {
            BeforeCreate();
            var result = new QTChinnok.Logic.Models.Base.Artist();
            AfterCreate(result);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Artist Create(object other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Artist();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Artist Create(QTChinnok.Logic.Models.Base.Artist other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Artist();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Artist Create(Entities.Base.Artist other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Artist();
            result.Source = other;
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Artist instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Artist instance, object other);
        static partial void BeforeCreate(QTChinnok.Logic.Models.Base.Artist other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Artist instance, QTChinnok.Logic.Models.Base.Artist other);
        static partial void BeforeCreate(Entities.Base.Artist other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Artist instance, Entities.Base.Artist other);
    }
}
