using QRCoder;
using System.Diagnostics;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using QRCode1.Models;



namespace QRCode1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }




public ActionResult Index(ErrorViewModel obj)
    {
        string qrText = obj.name??"test";
    
    QRCodeGenerator qrGenerator = new QRCodeGenerator();

    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);

    
    QRCode qrCode1 = new QRCode(qrCodeData);

    Bitmap qrCodeImage = qrCode1.GetGraphic(20);

     byte[] byteImage = BitmapToBytes(qrCodeImage);

   
    string imageBase64 = Convert.ToBase64String(byteImage);
    string imageSrc = $"data:image/png;base64,{imageBase64}";

   ViewBag.QrCodeImageSrc = imageSrc;

    return View();
}
public byte[] BitmapToBytes(Bitmap img)
{
    using (MemoryStream stream = new MemoryStream())
    {
        img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        return stream.ToArray();
    }
}



public IActionResult Privacy()
    {
        return View();
    }

   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
