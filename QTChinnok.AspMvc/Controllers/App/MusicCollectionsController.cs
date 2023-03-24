﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QTChinnok.AspMvc.Controllers.App
{
    public class MusicCollectionsController : Controller
    {
        private readonly Logic.Contracts.App.IMusicCollectionsAccess _musicCollectionsAccess;
        private readonly Logic.Contracts.App.IAlbumsAccess _albumsAccess;

        public MusicCollectionsController(Logic.Contracts.App.IMusicCollectionsAccess musicCollectionsAccess, Logic.Contracts.App.IAlbumsAccess albumsAccess)
        {
            _musicCollectionsAccess = musicCollectionsAccess;
            _albumsAccess = albumsAccess;
        }

        // GET: MusicCollectionsController
        public async Task<ActionResult> Index()
        {
            var entities = await _musicCollectionsAccess.GetAllAsync();
            var models = entities.Select(e => Models.App.MusicCollection.Create(e)).ToArray();

            return View(models);
        }

        // GET: MusicCollectionsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = default(Models.App.MusicCollection);
            var entity = await _musicCollectionsAccess.GetByIdAsync(id);

            if (entity != null)
            {
                model = Models.App.MusicCollection.Create(entity);
            }
            return model != null ? View(model) : NotFound();
        }

        // GET: MusicCollectionsController/Create
        public ActionResult Create()
        {
            var entity = _musicCollectionsAccess.Create();

            return View(Models.App.MusicCollection.Create(entity));
        }

        // POST: MusicCollectionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Models.App.MusicCollection model)
        {
            try
            {
                var entity = _musicCollectionsAccess.Create();

                entity.Name = model.Name;
                entity = await _musicCollectionsAccess.InsertAsync(entity);
                await _musicCollectionsAccess.SaveChangesAsync();

                return RedirectToAction(nameof(Edit), new { id = entity.Id});
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View(model);
            }
        }

        // GET: MusicCollectionsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = default(Models.App.MusicCollection);
            var entity = await _musicCollectionsAccess.GetByIdAsync(id);

            if (entity != null)
            {
                var existIds = entity.Albums.Select(e => e.Id).ToArray();
                var albums = await _albumsAccess.GetAllAsync();
                var addAlbums = albums.Where(a => existIds.Contains(a.Id) == false);

                model = Models.App.MusicCollection.Create(entity);
                model.AddAlbums = addAlbums.Select(a => Models.App.Album.Create(a)).ToList();
            }
            return model != null ? View(model) : NotFound();
        }

        // POST: MusicCollectionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Models.App.MusicCollection model)
        {
            try
            {
                var entity = await _musicCollectionsAccess.GetByIdAsync(id);

                if (entity != null)
                {
                    entity.Name = model.Name;

                    await _musicCollectionsAccess.UpdateAsync(entity);
                    await _musicCollectionsAccess.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }

        public async Task<ActionResult> AddAlbum(int musicCollectionId, int albumId)
        {
            var entity = await _musicCollectionsAccess.GetByIdAsync(musicCollectionId);
            var album = await _albumsAccess.GetByIdAsync(albumId);

            if (entity != null && album != null)
            {
                entity.Albums.Add(album);

                await _musicCollectionsAccess.UpdateAsync(entity);
                await _musicCollectionsAccess.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = musicCollectionId });
        }
        public async Task<ActionResult> RemoveAlbum(int musicCollectionId, int albumId)
        {
            var entity = await _musicCollectionsAccess.GetByIdAsync(musicCollectionId);
            var album = await _albumsAccess.GetByIdAsync(albumId);

            if (entity != null && album != null)
            {
                entity.Albums.Remove(album);

                await _musicCollectionsAccess.UpdateAsync(entity);
                await _musicCollectionsAccess.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Edit), new { id = musicCollectionId });
        }
        // GET: MusicCollectionsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = default(Models.App.MusicCollection);
            var entity = await _musicCollectionsAccess.GetByIdAsync(id);

            if (entity != null)
            {
                model = Models.App.MusicCollection.Create(entity);
            }
            return model != null ? View(model) : RedirectToAction(nameof(Index));
        }

        // POST: MusicCollectionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Models.App.MusicCollection model)
        {
            try
            {
                var entity = await _musicCollectionsAccess.GetByIdAsync(id);

                if (entity != null)
                {
                    foreach (var item in entity.Albums)
                    {
                        await _musicCollectionsAccess.RemoveAlbumAsync(id, item.Id);
                    }
                }

                await _musicCollectionsAccess.DeleteAsync(id);
                await _musicCollectionsAccess.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }
    }
}
