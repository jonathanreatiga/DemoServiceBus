using ApiFinanciera.Context;
using ApiFinanciera.Dto;
using ApiFinanciera.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ApiFinanciera.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GananciaController : ControllerBase
    {
        private readonly ApiContext _context;
        readonly IConfiguration _configuration;

        public GananciaController(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet("GetAll")]
        public async Task<object> Index()
        {
            DbSet<Producto> productos = _context.Producto;
            DbSet<Ganancia> ganancias = _context.Ganancia;


            var query =
                from producto in productos
                join ganancia in ganancias
                on producto.IdProducto equals ganancia.IdProducto
                select new GananaciaDto
                {
                    Nombre = producto.Nombre,
                    Unidades = ganancia.Unidades,
                    Monto = ganancia.Monto
                };

            List<GananaciaDto> gananaciaDto = query.ToList();
            return gananaciaDto;
        }
    }
}
