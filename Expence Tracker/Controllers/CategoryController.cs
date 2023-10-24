using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User_Login.Areas.Identity.Data;
using User_Login.Data;
using User_Login.Models;
//using Microsoft.AspNetCore.Identity;

namespace User_Login.Controllers
{
    public class CategoryController : Controller
    {

        private readonly AuthDbContext _context;
        private readonly SignInManager<Users> _signInManager;

        public CategoryController(AuthDbContext context, SignInManager<Users> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            if(!_signInManager.IsSignedIn(User))
            {
                //return RedirectToAction(nameof(DashBoardController.Index));
                //return RedirectToPage("/");
            }
            var userId = Request.Cookies["UserId"];
            Console.WriteLine(userId);
            var userEmail = Request.Cookies["UserEmail"];
            userEmail = System.Net.WebUtility.UrlDecode(userEmail);
            Console.WriteLine(userEmail);

            if (string.IsNullOrEmpty(userId))
            {
                return View();
                return View("Error"); // Replace with appropriate error handling
            }

            var id = userId.ToString();
            Console.WriteLine($"Id {id}");

            /*var userCatagories = _context.Categories.Where(t => t.User_Id == id);
            Console.WriteLine("User catagories");
            Console.WriteLine(userCatagories);*/

            var applicationDbContext = _context.Categories
                .Where(t => t.User_Id == id) // Filter by user ID
                .ToListAsync();

            Console.WriteLine($"ApplicationDbContext: {applicationDbContext}");
            //var applicationDbContext = _context.Transactions.Include(t => t.Category);
            //return View(await applicationDbContext.ToListAsync());

            return View(await applicationDbContext);

            //return _context.Categories != null ? View(await _context.Categories.ToListAsync()) : Problem("Entity set 'ApplicationDbContext.Categories'  is null.");

            //return View(await _context.Categories.ToListAsync());
        }


        // GET: Category/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Category());
            else
                return View(_context.Categories.Find(id));

        }

        // POST: Category/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("In valid");
                var userEmail = Request.Cookies["UserEmail"];
                userEmail = System.Net.WebUtility.UrlDecode(userEmail);
                Console.WriteLine(userEmail);
                var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);

                if (user != null)
                {
                    Console.WriteLine(user.Id);
                    category.User_Id = user.Id;

                    if (category.CategoryId == 0)
                        _context.Add(category);
                    else
                        _context.Update(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                
            }
            else {
                Console.WriteLine("In valid");
                var userEmail = Request.Cookies["UserEmail"];
                userEmail = System.Net.WebUtility.UrlDecode(userEmail);
                Console.WriteLine(userEmail);
                var user = _context.Users.SingleOrDefault(u => u.Email == userEmail);

                if (user != null)
                {
                    Console.WriteLine(user.Id);
                    category.User_Id = user.Id;

                    if (category.CategoryId == 0)
                        _context.Add(category);
                    else
                        _context.Update(category);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                Console.WriteLine("In else");
                return View(category); 
            }
            return View(category);
        }


        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
