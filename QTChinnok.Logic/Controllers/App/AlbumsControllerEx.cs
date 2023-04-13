﻿#if GENERATEDCODE_ON
using System.Runtime.CompilerServices;

namespace QTChinnok.Logic.Controllers.App
{
    partial class AlbumsController
    {
        internal override IEnumerable<string> Includes => new string[] { nameof(Entities.App.Album.Artist), nameof(Entities.App.Album.Tracks) };

        public Task<Logic.Models.App.Album[]> QueryByAsync(string? title, string? artistName)
        {
            var query = EntitySet.AsQueryable();

            query = query.Include(e => e.Artist);
            if (string.IsNullOrEmpty(title) == false)
            {
                query = query.Where(e => e.Title.ToUpper().Contains(title.ToUpper()));
            }

            if (string.IsNullOrEmpty(artistName) == false)
            {
                query = query.Where(e => e.Artist!.Name!.ToUpper().Contains(artistName.ToUpper()));
            }

            return query.Select(e => new Models.App.Album(e)).ToArrayAsync();
        }
    }
}
#endif