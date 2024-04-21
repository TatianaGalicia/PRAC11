using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCpractica9.Models;

namespace MVCpractica9.Controllers
{
    public class equiposController : Controller
    {
        private readonly equiposDbContext _equiposDbContext;

        public equiposController(equiposDbContext equiposDbContexto)
        {
            _equiposDbContext = equiposDbContexto;
        }

        public IActionResult Index()
        {

            //Aqui se obtienen los datos de marcas de la base para mostrarlo en el dropdown
            var listaDeMarcas = (from m in _equiposDbContext.marcas
                                 select m).ToList();
            ViewData["listadoDeMarcas"] = new SelectList(listaDeMarcas, "id_marca", "nombre_marca");

            //Aqui se obtienen los datos de Tipo de Equipo de la base para mostrarlo en el dropdown
            var listaDeTipoEquipo = (from m in _equiposDbContext.tipo_equipo
                                     select m).ToList();
            ViewData["listadoDetipo_equipo"] = new SelectList(listaDeTipoEquipo, "id_tipo_eq", "descripcion");

            //Aqui se obtienen los datos de Tipo de Equipo de la base para mostrarlo en el dropdown
            var listaDeEstadoEquipo = (from m in _equiposDbContext.estados_equipo
                                       select m).ToList();
            ViewData["listadoDeestado_equipo"] = new SelectList(listaDeEstadoEquipo, "id_estados_equipo", "estado");

            //Aqui se obtienen los datos de toda la tabla equipos
            var listadoDeEquipos = (from e in _equiposDbContext.equipos
                                    join m in _equiposDbContext.marcas on e.marca_id equals m.id_marca
                                    select new
                                    {
                                        id_equipo= e.id_equipos,
                                        nombre = e.nombre,
                                        descripcion = e.descripcion,
                                        marca_id = e.marca_id,
                                        marca_nombre = m.nombre_marca
                                    }).ToList();
            ViewData["listadoEquipo"] = listadoDeEquipos;

            return View();
        }

        public IActionResult CrearEquipos(equipos nuevoEquipo)
        {
            _equiposDbContext.Add(nuevoEquipo);
            _equiposDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var equipos = await _equiposDbContext.equipos.FindAsync(id);
            if (equipos != null)
            {
                _equiposDbContext.equipos.Remove(equipos);
            }

            await _equiposDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
