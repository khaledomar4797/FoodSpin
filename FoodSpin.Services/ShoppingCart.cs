using FoodSpin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodSpin.Services
{
    public partial class ShoppingCart
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(Product product)
        {
            // Get the matching cart and product instances
            var cartProduct = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == product.ProductId);

            if (cartProduct == null)
            {
                // Create a new cart product if no cart product exists
                cartProduct = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartProduct);
            }
            else
            {
                // If the product does exist in the cart, 
                // then add one to the quantity
                cartProduct.Count++;
            }
            // Save changes
            storeDB.SaveChanges();

            return cartProduct.Count;
        }

        public int RemoveFromCart(int id)
        {


            // Get the cart

            var cartProduct = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.ProductId == id);


            int productCount = 0;

            if (cartProduct != null)
            {
                if (cartProduct.Count > 1)
                {
                    cartProduct.Count--;
                    productCount = cartProduct.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartProduct);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return productCount;
        }

        public void EmptyCart()
        {
            var cartProducts = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartProduct in cartProducts)
            {
                storeDB.Carts.Remove(cartProduct);
            }
            // Save changes
            storeDB.SaveChanges();
        }

        public List<Cart> GetCartProducts()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }

        public int GetCount()
        {
            // Get the count of each product in the cart and sum them up
            int? count = (from cartProducts in storeDB.Carts
                          where cartProducts.CartId == ShoppingCartId
                          select (int?)cartProducts.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal()
        {
            // Multiply product price by count of that product to get 
            // the current price for each of those products in the cart
            // sum all product price totals to get the cart total
            decimal? total = (from cartProducts in storeDB.Carts
                              where cartProducts.CartId == ShoppingCartId
                              select (int?)cartProducts.Count *
                              cartProducts.Product.ProductPrice).Sum();

            return total ?? decimal.Zero;
        }

        public Order CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            order.OrderDetails = new List<OrderDetail>();

            var cartProducts = GetCartProducts();
            // Iterate over the products in the cart, 
            // adding the order details for each
            foreach (var product in cartProducts)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = product.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = product.Product.ProductPrice,
                    Quantity = product.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (product.Count * product.Product.ProductPrice);
                order.OrderDetails.Add(orderDetail);
                storeDB.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order;
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart product in shoppingCart)
            {
                product.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }
}
