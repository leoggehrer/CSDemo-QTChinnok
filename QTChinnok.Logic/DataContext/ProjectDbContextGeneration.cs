﻿//@GeneratedCode
namespace QTChinnok.Logic.DataContext
{
    ///
    /// Generated by the generator
    ///
    partial class ProjectDbContext
    {
        ///
        /// Generated by the generator
        ///
        public DbSet<Entities.Base.Artist>? ArtistSet{ get; set; }
        ///
        /// Generated by the generator
        ///
        public DbSet<Entities.Base.Genre>? GenreSet{ get; set; }
        ///
        /// Generated by the generator
        ///
        public DbSet<Entities.Base.MediaType>? MediaTypeSet{ get; set; }
        ///
        /// Generated by the generator
        ///
        public DbSet<Entities.App.Album>? AlbumSet{ get; set; }
        
        ///
        /// Generated by the generator
        ///
        partial void GetGeneratorDbSet<E>(ref DbSet<E>? dbSet, ref bool handled) where E : Entities.EntityObject
        {
            if (typeof(E) == typeof(Entities.Base.Artist))
            {
                dbSet = ArtistSet as DbSet<E>;
                handled = true;
            }
            else if (typeof(E) == typeof(Entities.Base.Genre))
            {
                dbSet = GenreSet as DbSet<E>;
                handled = true;
            }
            else if (typeof(E) == typeof(Entities.Base.MediaType))
            {
                dbSet = MediaTypeSet as DbSet<E>;
                handled = true;
            }
            else if (typeof(E) == typeof(Entities.App.Album))
            {
                dbSet = AlbumSet as DbSet<E>;
                handled = true;
            }
        }
    }
}
