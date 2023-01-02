using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using Khoaluan.Helpper;
using Khoaluan.Models;
using Khoaluan;
using Microsoft.AspNetCore.Http;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminTinDangsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public INotyfService _notyfService { get; }
        public AdminTinDangsController(IUnitOfWork unitOfWork, INotyfService notyfService)
        {
            _unitOfWork = unitOfWork;
            _notyfService = notyfService;
        }

        // GET: Admin/AdminTinDangs
        public IActionResult Index()
        {
            var collection = _unitOfWork.BlogRepository.GetAll().ToList();
            return View(collection);
        }

        // GET: Admin/AdminTinDangs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinDang = _unitOfWork.BlogRepository.GetById((int)id);
            if (tinDang == null)
            {
                return NotFound();
            }

            return View(tinDang);
        }

        // GET: Admin/AdminTinDangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminTinDangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Ad_Username,Name,Image,Published,Alias,Contents,CreatedDate")] Blog blog, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                //Xu ly Image
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(blog.Title) + extension;
                        blog.Image = await Utilities.UploadFileBlog(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(blog.Image)) blog.Image = "default.jpg";
                    var taikhoanID = HttpContext.Session.GetString("AccountId");
                    var name = _unitOfWork.AdminRepository.GetAll().Where(t=>t.TaiKhoan == taikhoanID).FirstOrDefault();
                    blog.Alias = Utilities.SEOUrl(blog.Title);
                    blog.CreatedDate = DateTime.Now;
                    blog.Ad_Username = taikhoanID;
                    blog.Name = name.HoTen;

                    _unitOfWork.BlogRepository.Create(blog);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Create Success");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return View(blog);
        }

        // GET: Admin/AdminTinDangs/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinDang = _unitOfWork.BlogRepository.GetById((int)id);
            if (tinDang == null)
            {
                return NotFound();
            }
            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Ad_Username,Name,Image,Published,Alias,Contents,CreatedDate")] Blog blog, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Xu ly Image
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(blog.Title) + extension;
                        blog.Image = await Utilities.UploadFileBlog(fThumb, @"news", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(blog.Image)) blog.Image = "default.jpg";
                    blog.Alias = Utilities.SEOUrl(blog.Title);
                    
                    _unitOfWork.BlogRepository.Update(blog);
                    _unitOfWork.SaveChange();
                    _notyfService.Success("Update Success");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinDangExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Admin/AdminTinDangs/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinDang = _unitOfWork.BlogRepository.GetById((int)id);
            if (tinDang == null)
            {
                return NotFound();
            }

            return View(tinDang);
        }

        // POST: Admin/AdminTinDangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var tinDang = _unitOfWork.BlogRepository.GetById((int)id);
                _unitOfWork.BlogRepository.Delete(tinDang);
                _unitOfWork.SaveChange();
                _notyfService.Success("Xóa thành công");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        private bool TinDangExists(int id)
        {
            return _unitOfWork.BlogRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
