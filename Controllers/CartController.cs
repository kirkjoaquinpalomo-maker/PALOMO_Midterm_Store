using Microsoft.AspNetCore.Mvc;
using PALOMO_Midterm_Store.Data;
using PALOMO_Midterm_Store.Models;

namespace PALOMO_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var cartItems = _db.CartItems.ToList();

            return View(cartItems);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _db.CartItems.Add(cartItem);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var cartItem = _db.CartItems.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            cartItem.Quantity = quantity;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cartItem = _db.CartItems.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            _db.CartItems.Remove(cartItem);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}