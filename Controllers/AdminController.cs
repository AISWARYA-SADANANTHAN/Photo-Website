using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotographyWebsite.Models;
using System.Drawing.Printing;
using PhotographyWebsite.Models;
using PhotographyWebsite.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Text;




namespace PhotographyWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext appDbContext)
        {
            _context= appDbContext;
        }
        public IActionResult Index()
        {
          
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
               
                    // Here you would typically validate the user credentials
                    // For demonstration, let's assume a simple check
                    if (model.Email == "nahas@gmail.com" && model.Password == "nahas123")
                    {
                        // Redirect to admin dashboard or home page
                        return RedirectToAction("Admin_view");
                    }
                
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    ViewBag.Message = "Invalid login credentials";
                    TempData["Message"] = "Invalid Login Credentials";

                }
                
              return RedirectToAction("Index");
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return PartialView("_RegistrationForm", new RegistrationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle registration logic here (e.g., save to database)
                // Redirect or return a success message
            }

            // If we got this far, something failed; redisplay the form
            return PartialView("_RegistrationForm", model);
        }

        //--------------------------------------------------------//

        // GET: Students/Create
        public IActionResult RegistrationForm() // Removed the underscore for naming convention
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrationForm(Employee model)
        {
            if (ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.Name = model.Name;
                emp.Email= model.Email; 
                emp.Password=model.Password;
                   try
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync(); 
            ModelState.Clear();

           return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Optionally log the exception
            ModelState.AddModelError("", "An error occurred while saving the data.");
                  
                    return View(model);
        }

                
            }
            return View(); 


        }

        //------------------Admin View------------------------------------//


        // GET: Students
        public async Task<IActionResult> Admin_view()
        {
            return View(await _context.Employees.ToListAsync());
        }
        //-----------------------------------------------------------------//

        [HttpGet]
        public IActionResult Cust_Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cust_Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Employees
                    .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("Name", user.Email),
                    new Claim(ClaimTypes.Role, "User")
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Sign in the user
                    //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError("", "Invalid login attempt.");
                    ViewBag.Message = "Invalid login credentials";
                    TempData["Message"] = "Invalid Login Credentials";
                }

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

         public IActionResult Website_View()
        {
            return View("Website_View");
        }



        public IActionResult Order_photo()
        {
            return View("Order_photo");
        }


        //---------------------Order table -----------------------------//


        // GET: Students/Create
       
        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ordereplace(orderplaced model)
        {
            if (ModelState.IsValid)
            {
                Order ord = new Order();
                ord.Id = model.Id;
                ord.Customer_name = model.Customer_name;
                ord.Email_id = model.Email_id;
                ord.type_photo = model.type_photo;
                ord.book_date_st = model.book_date_st;
                ord.date_end = model.date_end;
                try
                {
                    _context.Orders.Add(ord);
                    await _context.SaveChangesAsync();
                    ModelState.Clear();
                    ViewBag.Message = "Order placed Successfully";
                    return RedirectToAction("Website_View");
                }
                catch (Exception ex)
                {
                    // Optionally log the exception
                    ModelState.AddModelError("", "An error occurred while saving the data.");

                    return View(model);
                }


            }
            return View("Index");

        }

    }
}
