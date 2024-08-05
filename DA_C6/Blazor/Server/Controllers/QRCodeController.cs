using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Blazor.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRCodeController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult GenerateQRCode([FromQuery] string data)
        {
            try
            {
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var bitmap = qrCode.GetGraphic(20))
                        {
                            using (var stream = new MemoryStream())
                            {
                                bitmap.Save(stream, ImageFormat.Png);
                                var qrCodeBase64 = Convert.ToBase64String(stream.ToArray());
                                return Ok($"data:image/png;base64,{qrCodeBase64}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"QR Code Generation Error: {ex.Message}");
            }
        }
    }
}
