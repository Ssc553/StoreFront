using Microsoft.AspNetCore.Mvc;
using StoreFront.DATA.EF.Models;
using Microsoft.AspNetCore.Identity;
using StoreFront.UI.MVC.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {

        private readonly ScottsStoreContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartController(ScottsStoreContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //Retrieve the cart context from session
            var sessionCart = HttpContext.Session.GetString("cart");

            //Create the shell for the C# version of the cart
            Dictionary<int, CartItemViewModel>? shoppingCart;

            // Check to see if the cart exists
            if (string.IsNullOrEmpty(sessionCart))
            {
                shoppingCart = new();
                ViewBag.Message = "There are no items in your cart.";
            }
            else
            {
                ViewBag.Message = null;
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }
            return View(shoppingCart);
        }

        public IActionResult AddToCart(int id)
        {
            #region Session Notes
            /*
             * Session is a tool available on the server-side that can store information for a user while they are actively using your site.
             * Typically the session lasts for 20 minutes (this can be adjusted in Program.cs).
             * Once the 20 minutes is up, the session variable is disposed.
             * 
             * Values that we can store in Session are limited to: string, int
             * - Because of this we have to get creative when trying to store complex objects (like CartItemViewModel).
             * To keep the info separated into properties we will convert the C# object to a JSON string.
             * */
            #endregion
            var sessionCart = HttpContext.Session.GetString("cart");

            //Empty shell for LOCAL shopping cart variable
            Dictionary<int, CartItemViewModel> shoppingCart;

            if (string.IsNullOrEmpty(sessionCart))
            {
                shoppingCart = new();
            }
            else
            {//dJSonify
                shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(sessionCart);
            }

            // add newly selected items to the cart
            Item? product = _context.Items.Find(id);

            //Initialize a cart item so we can add to the cart
            CartItemViewModel item = new(1, product);

            if (shoppingCart.ContainsKey(product.ItemId))
            {
                shoppingCart[product.ItemId].Qty++;
            }
            else
            {
                shoppingCart.Add(product.ItemId, item);
            }
            string jsonCart = JsonConvert.SerializeObject(shoppingCart);
            HttpContext.Session.SetString("cart", jsonCart);
            return RedirectToAction("Index");


        }

        public IActionResult RemoveFromCart(int id)

        //Dry : var shoppingCart = GetCart()
        //retrieve the cart from session
        {
            var jsonCart = HttpContext.Session.GetString("cart");
            var shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(jsonCart);

            shoppingCart.Remove(id);

            //Check if there are any other items in the cart, if not, remove the cart from session

            if (shoppingCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }
            return RedirectToAction(nameof(Index));


        }
        public IActionResult UpdateCart(int ItemId, int qty)
        {
            var jsonCart = HttpContext.Session.GetString("cart");
            var shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(jsonCart);

            //update the qty for our specific dictionary key.
            //if qty is 0,remove the item from the cart
            if (qty <= 0)
            {
                RemoveFromCart(ItemId);

            }
            else
            {
                shoppingCart[ItemId].Qty = qty;
                jsonCart = JsonConvert.SerializeObject(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }
            return RedirectToAction("Index");
        }




        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SubmitOrder()
        {
            #region Planning out Order Submission
            //BIG PICTURE PLAN
            //Create Order object -> then save to the DB
            //  - OrderDate
            //  - UserId
            //  - ShipToName, ShipCity, ShipState, ShipZip --> this info needs to be pulledfrom the UserDetails record.
            //  Alternatively, use a checkout screen and a Create Orders template.
            //Add the record to _context
            //Save DB changes

            //Create OrderProducts object for each item in the cart
            //  - ProductId -> available from cart
            //  - OrderId -> from Order object
            //  - Qty -> available from cart
            //  - ProductPrice -> available from cart
            //Add the record to _context
            //Save DB changes
            #endregion
            //retrieve the current user's ID
            var userId = (await _userManager.GetUserAsync(HttpContext.User))?.Id;

            //retrieve the UserDetails for that user
            var ud = _context.Users.Find(userId);
            if (ud == null)
            {
                var newUd = new User()
                {
                    UserId = userId,
                    FirstName = "Default",
                    LastName = "Name",
                };
                _context.Add(newUd);
                ud = newUd;
            }

            //Create the order object and assign values (either from user details or from yourcheckout form submission.)
            Order o = new()
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                ShipServer = ud?.Server ?? "Not Given",
                ShipAccountName = ud?.AccountName ?? "Not Given",
                ShipAccountCountry = ud?.Country ?? "Not Given",
                
            };

            _context.Add(o);

            //Retrieve the session cart
            var jsonCart = HttpContext.Session.GetString("cart");
            var shoppingCart = JsonConvert.DeserializeObject<Dictionary<int,CartItemViewModel>>(jsonCart);

            foreach (var item in shoppingCart.Values)
            {
                //create an OrderProduct object for each item in the cart
                OrderDetail op = new OrderDetail()
                {
                    OrderId = o.OrderId,
                    ItemId = item.Item.ItemId,
                    ItemPrice = item.Item.Price,
                    Quantity = (short)item.Qty
                };

                o.OrderDetails.Add(op);
            }
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Orders");
        }






        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!_context.Users.Any(ud => ud.UserId == userId))
            {
                var newUd = new User()
                {
                    UserId = userId,
                    AccountName = cvm.AccountName,
                    
                };
                _context.Add(newUd);
            }

            Order o = new()
            {
                OrderDate = DateTime.Now,
                UserId = userId,
                ShipServer = cvm.ServerName,
                ShipAccountName = cvm.AccountName,
                ShipAccountCountry = cvm.AccountCountry,
                
            };

            _context.Orders.Add(o);
            //Retrieve the session cart
            var jsonCart = HttpContext.Session.GetString("cart");
            var shoppingCart = JsonConvert.DeserializeObject<Dictionary<int, CartItemViewModel>>(jsonCart);

            foreach (var item in shoppingCart.Values)
            {
                //create an OrderProduct object for each item in the cart
                OrderDetail op = new OrderDetail()
                {
                    OrderId = o.OrderId,
                    ItemId = item.Item.ItemId,
                    ItemPrice = item.Item.Price,
                    Quantity = (short)item.Qty
                };

                o.OrderDetails.Add(op);
            }
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("cart");
            return RedirectToAction("Index", "Orders");
        }
    }
}
