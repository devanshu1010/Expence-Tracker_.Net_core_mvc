
/*
namespace User_Login.Controllers
{
    public class TransactionController : Controller
    {
        // GET: TransactionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TransactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
*/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using User_Login.Areas.Identity.Data;
using User_Login.Data;
using User_Login.Models;

namespace User_Login.Controllers
{
    public class TransactionController : Controller
    {
        private readonly AuthDbContext _context;
        //private static readonly HttpContext httpContext;
        //string cookie_data = httpContext.Request.Cookies["MyCookie"];
        // AuthDbContext db = new AuthDbContext();
        //db.Users.Where(c => c..Equals(city).First();
        /*
        private readonly UserManager<Users> _userManager;

        public TransactionController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> YourMethodName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            string userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            // For ASP.NET Core <= 3.1
            Users applicationUser = await _userManager.GetUserAsync(User);
            // string userEmail = applicationUser?.Email; // will give the user's Email

            // For ASP.NET Core >= 5.0
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
        }
*/


        public TransactionController(AuthDbContext context)
        {
            _context = context;
        }
        /*
                List<Transactions> SelectedTransactions = _context.Users
                       .Include(x => x.Email)
                       .ToListAsync();
          */


        // Use userId as needed
        // return Ok(userId);

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var userId = Request.Cookies["UserId"];
            Console.WriteLine(userId);
            var userEmail = Request.Cookies["UserEmail"];
            userEmail = System.Net.WebUtility.UrlDecode(userEmail);
            Console.WriteLine(userEmail);

            if (string.IsNullOrEmpty(userId))
            {
                return View();
                //return View("Error"); // Replace with appropriate error handling
            }

            var id = userId.ToString();
            Console.WriteLine($"Id {id}");

            var userTransactions = _context.Transactions.Where(t => t.User_Id == id);
            Console.WriteLine("User transaction");            
            Console.WriteLine(userTransactions);

            var applicationDbContext = _context.Transactions
                .Where(t => t.User_Id == id) // Filter by user ID
                .Include(t => t.Category)
                .ToListAsync();

            Console.WriteLine($"ApplicationDbContext: {applicationDbContext}");
            //var applicationDbContext = _context.Transactions.Include(t => t.Category);
            //return View(await applicationDbContext.ToListAsync());

            return View(await applicationDbContext);
        }

        // GET: Transaction/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
                return View(new Transactions());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transactions/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //User_Id,
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionsId,CategoryId,Amount,Note,Date")] Transactions Transactions)
        {


            if (ModelState.IsValid )
            {
                var userEmail = Request.Cookies["UserEmail"];
                userEmail = System.Net.WebUtility.UrlDecode(userEmail);
                Console.WriteLine(userEmail);
                var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);

                if (user != null)
                {
                    Console.WriteLine(user.Id);
                    Transactions.User_Id = user.Id;

                    //return Ok(userId);
                    if (Transactions.TransactionId == 0)
                        _context.Add(Transactions);
                    else
                        _context.Update(Transactions);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    //return View(category);
                }
                
            }
            else
            {
                var userEmail = Request.Cookies["UserEmail"];
                userEmail = System.Net.WebUtility.UrlDecode(userEmail);
                Console.WriteLine(userEmail);
                var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);

                if (user != null)
                {
                    Console.WriteLine(user.Id);
                    Transactions.User_Id = user.Id;

                    //return Ok(userId);
                    if (Transactions.TransactionId == 0)
                        _context.Add(Transactions);
                    else
                        _context.Update(Transactions);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        // Log or display error messages as needed
                        userEmail = Request.Cookies["UserId"];
                        Console.WriteLine(error.ErrorMessage);
                        Console.WriteLine(userEmail);
                    }
                }
                //PopulateCategories();
                return View(Transactions);
            }
            PopulateCategories();
            return View(Transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactionss'  is null.");
            }
            var Transactions = await _context.Transactions.FindAsync(id);
            if (Transactions != null)
            {
                _context.Transactions.Remove(Transactions);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [NonAction]
        public void PopulateCategories()
        {
            var userId = Request.Cookies["UserId"];
            Console.WriteLine(userId);
            //var userEmail = Request.Cookies["UserEmail"];
            //userEmail = System.Net.WebUtility.UrlDecode(userEmail);
            //Console.WriteLine(userEmail);

            if (string.IsNullOrEmpty(userId))
            {

                View("Error"); // Replace with appropriate error handling
            }

            var CategoryCollection = _context.Categories.Where(t => t.User_Id == userId).ToList();
            Category DefaultCategory = new Category() { CategoryId = 0, Title = "Choose a Category" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}