﻿//@GeneratedCode
namespace QTChinnok.Logic.Models.Base
{
    /// <summary>
    /// Generated by the generator.
    /// </summary>
    public partial class Genre
    {
        /// <summary>
        /// Generated by the generator
        /// </summary>
        static Genre()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public Genre()
        {
            Constructing();
            _source = new Entities.Base.Genre();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        /// <summary>
        /// Generated by the generator
        /// </summary>
        internal Genre(Entities.Base.Genre source)
        {
            Constructing();
            _source = source;
            Constructed();
        }
        
        new internal Entities.Base.Genre Source
        {
            get => (Entities.Base.Genre)(_source!);
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
        internal void CopyProperties(Entities.Base.Genre other)
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
        partial void BeforeCopyProperties(Entities.Base.Genre other, ref bool handled);
        partial void AfterCopyProperties(Entities.Base.Genre other);
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public void CopyProperties(QTChinnok.Logic.Models.Base.Genre other)
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
        partial void BeforeCopyProperties(QTChinnok.Logic.Models.Base.Genre other, ref bool handled);
        partial void AfterCopyProperties(QTChinnok.Logic.Models.Base.Genre other);
        /// <summary>
        /// Generated by the generator.
        /// </summary>
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.Genre other)
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
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Genre Create()
        {
            BeforeCreate();
            var result = new QTChinnok.Logic.Models.Base.Genre();
            AfterCreate(result);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Genre Create(object other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Genre();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Genre Create(QTChinnok.Logic.Models.Base.Genre other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Genre();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        /// <summary>
        /// Generated by the generator
        /// </summary>
        public static QTChinnok.Logic.Models.Base.Genre Create(Entities.Base.Genre other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.Genre();
            result.Source = other;
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Genre instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Genre instance, object other);
        static partial void BeforeCreate(QTChinnok.Logic.Models.Base.Genre other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Genre instance, QTChinnok.Logic.Models.Base.Genre other);
        static partial void BeforeCreate(Entities.Base.Genre other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.Genre instance, Entities.Base.Genre other);
    }
}
