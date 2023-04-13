namespace QTChinnok.Logic.Contracts.App
{
    partial interface IAlbumsAccess
    {
        Task<Logic.Models.App.Album[]> QueryByAsync(string? title, string? artistName);
   }
}
