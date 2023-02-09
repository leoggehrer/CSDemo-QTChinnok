﻿//@GeneratedCode
namespace QTChinnok.Logic.Models.Base
{
    ///
    /// Generated by the generator
    ///
    public partial class MediaType
    {
        ///
        /// Generated by the generator
        ///
        static MediaType()
        {
            ClassConstructing();
            ClassConstructed();
        }
        static partial void ClassConstructing();
        static partial void ClassConstructed();
        
        ///
        /// Generated by the generator
        ///
        public MediaType()
        {
            Constructing();
            _source = new Entities.Base.MediaType();
            Constructed();
        }
        partial void Constructing();
        partial void Constructed();
        ///
        /// Generated by the generator
        ///
        internal MediaType(Entities.Base.MediaType source)
        {
            Constructing();
            _source = source;
            Constructed();
        }
        
        new internal Entities.Base.MediaType Source
        {
            get => (Entities.Base.MediaType)(_source ??= new Entities.Base.MediaType());
            set => _source = value;
        }
        
        public System.String? Name
        {
            get => Source.Name;
            set => Source.Name = value;
        }
        ///
        /// Generated by the generator
        ///
        internal void CopyProperties(Entities.Base.MediaType other)
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
        partial void BeforeCopyProperties(Entities.Base.MediaType other, ref bool handled);
        partial void AfterCopyProperties(Entities.Base.MediaType other);
        ///
        /// Generated by the generator
        ///
        public void CopyProperties(QTChinnok.Logic.Models.Base.MediaType other)
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
        partial void BeforeCopyProperties(QTChinnok.Logic.Models.Base.MediaType other, ref bool handled);
        partial void AfterCopyProperties(QTChinnok.Logic.Models.Base.MediaType other);
        ///
        /// Generated by the generator
        ///
        public override bool Equals(object? obj)
        {
            bool result = false;
            if (obj is Models.Base.MediaType other)
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
            return this.CalculateHashCode(Id, Name);
        }
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.Logic.Models.Base.MediaType Create()
        {
            BeforeCreate();
            var result = new QTChinnok.Logic.Models.Base.MediaType();
            AfterCreate(result);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.Logic.Models.Base.MediaType Create(object other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.MediaType();
            CommonBase.Extensions.ObjectExtensions.CopyFrom(result, other);
            AfterCreate(result, other);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.Logic.Models.Base.MediaType Create(QTChinnok.Logic.Models.Base.MediaType other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.MediaType();
            result.CopyProperties(other);
            AfterCreate(result, other);
            return result;
        }
        ///
        /// Generated by the generator
        ///
        public static QTChinnok.Logic.Models.Base.MediaType Create(Entities.Base.MediaType other)
        {
            BeforeCreate(other);
            var result = new QTChinnok.Logic.Models.Base.MediaType();
            result.Source = other;
            AfterCreate(result, other);
            return result;
        }
        static partial void BeforeCreate();
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.MediaType instance);
        static partial void BeforeCreate(object other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.MediaType instance, object other);
        static partial void BeforeCreate(QTChinnok.Logic.Models.Base.MediaType other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.MediaType instance, QTChinnok.Logic.Models.Base.MediaType other);
        static partial void BeforeCreate(Entities.Base.MediaType other);
        static partial void AfterCreate(QTChinnok.Logic.Models.Base.MediaType instance, Entities.Base.MediaType other);
    }
}
