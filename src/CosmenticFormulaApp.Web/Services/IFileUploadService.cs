using CosmenticFormulaApp.Web.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace CosmenticFormulaApp.Web.Services
{
    public interface IFileUploadService
    {
        Task<List<UploadResult>> ProcessFilesAsync(List<IBrowserFile> files);
    }
}
