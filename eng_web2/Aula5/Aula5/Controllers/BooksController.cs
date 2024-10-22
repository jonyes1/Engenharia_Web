using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aula5.Data;
using Aula5.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace Aula5.Controllers
{
    public class BooksController : Controller
    {
        private readonly Aula5Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(Aula5Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.Book.ToListAsync());
        }
        public IActionResult Download(string? id)
        {

            string pathFile = Path.Combine(_webHostEnvironment.WebRootPath, "Documents", id);
            byte[] filBytes = System.IO.File.ReadAllBytes(pathFile);

            string? mimeType;
            
            if (new FileExtensionContentTypeProvider().TryGetContentType(id, out mimeType)==false)
            {
                mimeType = "application/force-download";

            }
            return File(filBytes, mimeType);


        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CoverPhoto,Document")] BookViewModel book)
        {
            var PhotoExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var DocumentExtensions = new[] { ".pdf", ".doc", ".docx", ".epb" };

            var extension = Path.GetExtension(book.CoverPhoto.FileName).ToLower();


            if (!PhotoExtensions.Contains(extension))
            {
                ModelState.AddModelError("CoverPhoto", "Please, submit a valid image (jpg, jpeg, png, gif, bmp)");
            }
            extension = Path.GetExtension(book.Document.FileName).ToLower();

            if (!DocumentExtensions.Contains(extension))
            {
                ModelState.AddModelError("Document", "Please, submit a valid document (pdf, doc, docx, epb)");
            }



            if (ModelState.IsValid)
            {

                Book newBook = new Book();

                newBook.Title = book.Title;
                newBook.Document=Path.GetFileName(book.Document.FileName);
                newBook.CoverPhoto=Path.GetFileName(book.CoverPhoto.FileName);


                //save the files in the file system of the server
                //save the cover image

                string coverFileName = Path.GetFileName(book.CoverPhoto.FileName);
                string coverFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "NovaPasta", coverFileName);

                using (var stream = new FileStream(coverFullPath, FileMode.Create))
                {
                      book.CoverPhoto.CopyTo(stream);

                }

                //save the document

                string docFileName = Path.GetFileName(book.Document.FileName);
                string docFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "NovaPasta1", docFileName);

                using (var stream = new FileStream(coverFullPath, FileMode.Create))
                {
                    book.Document.CopyTo(stream);

                }



                _context.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CoverPhoto,Document")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
