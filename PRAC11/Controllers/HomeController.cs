using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using PRAC11.Models;
using System.Diagnostics;

namespace PRAC11.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> subirArchivo(IFormFile archivo)
        {
            Stream archivoASubir = archivo.OpenReadStream();

            string email = "cargar@emagill.com";
            string clave = "del1alnueve";
            string ruta = "aspstoragefile.appspost.com";
            string api_key = "AIzaSyAhSfON0lsHDGE";

            var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
            var autenticarFireBase = await auth.SignInWithEmailAndPasswordAsync(email, clave);

            var cancellation = new CancellationTokenSource();
            var tokenUser = autenticarFireBase.FirebaseToken;

            var tareaCargarArchivo = new FirebaseStorage(ruta,
                                                       new FirebaseStorageOptions
                                                       {
                                                           AuthTokenAsyncFactory = () => Task.FromResult(tokenUser),
                                                           ThrowOnCancel = true
                                                       }).Child("Archivo")
                                                       .Child(archivo.FileName)
                                                       .PutAsync(archivoASubir, cancellation.Token);
            var urlArchivoCargado = await tareaCargarArchivo;


            return RedirectToAction("VerImagen");
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
}
