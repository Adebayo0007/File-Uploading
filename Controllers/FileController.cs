
using classwork.ApplicationDbContext;
using classwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace classwork.Controllers;

public class FileController : Controller
{
private readonly ApplicationContext _context;
public FileController(ApplicationContext context)
{
    _context = context;
}
    public async Task<IActionResult> Index()
    {
        var fileuploadViewModel = await LoadAllFiles();
        ViewBag.Message = TempData["Message"];
        return View(fileuploadViewModel);
    }

    private async Task<FileUploadViewModel> LoadAllFiles()
    {
        var viewModel = new FileUploadViewModel();
        viewModel.FilesOnDatabase = await _context.FileOnDatabaseModels.ToListAsync();
        viewModel.FilesOnFileSystem = await _context.FileOnFileSystemModels.ToListAsync();
        return viewModel;
    }

    [HttpPost]
public async Task<IActionResult> UploadToFileSystem(List<IFormFile> files, string description)
{
    foreach(var file in files)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
        bool basePathExists = System.IO.Directory.Exists(basePath);
        if (!basePathExists) Directory.CreateDirectory(basePath);
        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
        var filePath = Path.Combine(basePath, file.FileName);
        var extension = Path.GetExtension(file.FileName);
        if (!System.IO.File.Exists(filePath))
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var fileModel = new FileOnFileSystemModel
            {
                CreatedOn = DateTime.UtcNow,
                FileType = file.ContentType,
                Extension = extension,
                Name = fileName,
                Description = description,
                FilePath = filePath
            };
            _context.FileOnFileSystemModels.Add(fileModel);
            _context.SaveChanges();
        }
    }

    TempData["Message"] = "File successfully uploaded to File System.";
    return RedirectToAction("Index");
}

public async Task<IActionResult> DownloadFileFromFileSystem(int id)
{

    var file = await _context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();
    if (file == null) return null;
    var memory = new MemoryStream();
    using (var stream = new FileStream(file.FilePath, FileMode.Open))
    {
        await stream.CopyToAsync(memory);
    }
    memory.Position = 0;
    return File(memory, file.FileType, file.Name + file.Extension);
}

public async Task<IActionResult> DeleteFileFromFileSystem(int id)
{

    var file = await _context.FileOnFileSystemModels.Where(x => x.Id == id).FirstOrDefaultAsync();
    if (file == null) return null;
    if (System.IO.File.Exists(file.FilePath))
    {
        System.IO.File.Delete(file.FilePath);
    }
    _context.FileOnFileSystemModels.Remove(file);
    _context.SaveChanges();
    TempData["Message"] = $"Removed {file.Name + file.Extension} successfully from File System.";
    return RedirectToAction("Index");
}

}