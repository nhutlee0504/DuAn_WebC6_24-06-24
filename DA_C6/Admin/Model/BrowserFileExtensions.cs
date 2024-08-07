using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public static class BrowserFileExtensions
{
    public static async Task<IFormFile> ToFormFileAsync(this IBrowserFile browserFile)
    {
        var stream = new MemoryStream();
        await browserFile.OpenReadStream().CopyToAsync(stream);
        stream.Position = 0;

        return new FormFile(stream, 0, stream.Length, browserFile.Name, browserFile.Name);
    }
}
