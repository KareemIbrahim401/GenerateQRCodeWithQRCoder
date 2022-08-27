using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace GenerateQRCode.Controllers
{
    public class QRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Booking()
        {
            string information = "01099417291";


            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(information, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qRCodeData);

            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = qrCode.GetGraphic(20))
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    ViewBag.BookingId = information;
                }
            }
            return View();
        }
    }

}
