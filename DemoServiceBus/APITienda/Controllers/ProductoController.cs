using ApiTienda.Context;
using ApiTienda.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ApiContext _context;

        public ProductoController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<object> Index()
        {
            return _context.Producto.ToList();
        }
    }
}
