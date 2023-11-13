using FevalAWSNetCoreS3.Services;
using Microsoft.AspNetCore.Mvc;

namespace FevalAWSNetCoreS3.Controllers
{
    public class AwsFilesController : Controller
    {
        //EL CONTROLADOR RECIBIRA EL SERVICIO DE AWS
        //PARA TRABAJAR CON EL
        private ServiceAWSS3 service;

        public AwsFilesController(ServiceAWSS3 service) 
        {
            this.service = service;        
        }

        public async Task<IActionResult> Index()
        {
            List<string> ficheros =
                await this.service.GetFilesAsync();
            return View(ficheros);
        }

        //TENDREMOS UNA PAGINA PARA PODER SUBIR LOS FICHEROS.
        public IActionResult UploadFile()
        {
            return View();
        }

        //AL UTILIZAR FORMULARIOS EN LA PAGINA, DEBEMOS CAPTURAR
        //LOS DATOS DENTRO DE UN METODO POST
        [HttpPost]
        public async Task<IActionResult> UploadFile
            (IFormFile file)
        {
            //LEEMOS EL STREAM DEL FICHERO Y LO SUBIMOS 
            //UTILIZANDO EL SERVICIO
            using (Stream stream = file.OpenReadStream())
            {
                await
                    this.service.UploadFileAsync(file.FileName, stream);
            }
            ViewData["MENSAJE"] = "Fichero subido correctamente a AWS S3";
            return View();
        }

        public async Task<IActionResult> DeleteFile(string filename)
        {
            await this.service.DeleteFileAsync(filename);
            return RedirectToAction("Index");
        }
    }
}
