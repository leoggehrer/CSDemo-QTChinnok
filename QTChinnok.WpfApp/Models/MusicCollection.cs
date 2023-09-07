using System.Collections.Generic;

namespace QTChinnok.WpfApp.Models
{
    public class MusicCollection
    {
        public MusicCollection()
        {

        }
        public MusicCollection(Logic.Models.App.MusicCollection entity)
        {
            Id = entity.Id;
            Name = entity.Name;

            foreach (var item in entity.Albums)
            {
                Albums.Add(Models.App.Album.Create(item));
                if (AlbumText.Length > 0)
                {
                    AlbumText += $", {item.Title}";
                }
                else
                {
                    AlbumText = $"{item.Title}";
                }
            }
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AlbumText { get; set; } = string.Empty;
        public List<App.Album> Albums { get; set;} = new List<App.Album>();
    }
}
