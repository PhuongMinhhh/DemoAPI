using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase

    {
        private static List<Customer> customers = new List<Customer>
            {
               /*new Customer {
                   Id = 2,
                   Username= "Erico",
                   FirstName = " Akala",
                   LastName = " Suko",
                   Email = "djoo2@gmail.com",
                   Place = "Tokyo"

               }*/
            };
        private readonly DataContext _context;
        public CustomerController(DataContext context) {
            _context = context; 
           
        }
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
            {
            
            return Ok( await _context.Customers.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get( int id)
        { 
           var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            
                return BadRequest("Customer not found.");
            

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer) 
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer request)
        {

            var dbCustomer = await _context.Customers.FindAsync(request.Id);
            if (dbCustomer == null)
                return BadRequest("Customer not found");

            dbCustomer.Username = request.Username;
            dbCustomer.FirstName = request.FirstName;
            dbCustomer.LastName = request.LastName;
            dbCustomer.Email = request.Email;
            dbCustomer.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> DeleteCustomer( int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return BadRequest("Customer not found");

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return Ok(await _context.Customers.ToListAsync());
        }
    }
}
