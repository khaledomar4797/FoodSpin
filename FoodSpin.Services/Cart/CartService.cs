using FoodSpin.Data;
using FoodSpin.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodSpin.Services
{
    public partial class CartService
    {
        ApplicationDbContext ctx = new ApplicationDbContext();
        string CartId { get; set; }
        public const string CartSessionKey = "CartId";

        public static CartService GetCart(HttpContextBase context)
        {
            var cart = new CartService();
            cart.CartId = cart.GetCartId(context);
            return cart;
        }

        public static CartService GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public int AddToCart(ProductDetail product)
        {
            var cartProduct = ctx.Carts.SingleOrDefault(
                c => c.CartId == CartId
                && c.ProductId == product.ProductId);

            if (cartProduct == null)
            {
                cartProduct = new Cart
                {
                    ProductId = product.ProductId,
                    CartId = CartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                ctx.Carts.Add(cartProduct);
            }
            else
            {
                cartProduct.Count++;
            }

            ctx.SaveChanges();

            return cartProduct.Count;
        }

        public int RemoveFromCart(int id)
        {
            var cartProduct = ctx.Carts.FirstOrDefault(
                cart => cart.CartId == CartId
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
                    ctx.Carts.Remove(cartProduct);
                }

                ctx.SaveChanges();
            }
            return productCount;
        }

        public void EmptyCart()
        {
            var cartProducts = ctx.Carts.Where(
                cart => cart.CartId == CartId);

            foreach (var cartProduct in cartProducts)
            {
                ctx.Carts.Remove(cartProduct);
            }

            ctx.SaveChanges();
        }

        public List<Cart> GetCartProducts()
        {
            return ctx.Carts.Where(
                cart => cart.CartId == CartId).ToList();
        }

        public int GetCartTotalProducts()
        {
            int? count = (from cartProducts in ctx.Carts
                          where cartProducts.CartId == CartId
                          select (int?)cartProducts.Count).Sum();

            return count ?? 0;
        }

        public decimal GetCartTotalPrice()
        {
            decimal? total = (from cartProducts in ctx.Carts
                              where cartProducts.CartId == CartId
                              select (int?)cartProducts.Count *
                              cartProducts.Product.ProductPrice).Sum();

            return total ?? decimal.Zero;
        }

        public Order CreateOrder(Order order)
        {
            decimal orderTotal = 0;
            order.OrderDetails = new List<OrderDetail>();

            var cartProducts = GetCartProducts();

            foreach (var product in cartProducts)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = product.ProductId,
                    OrderId = order.OrderId,
                    UnitPrice = product.Product.ProductPrice,
                    Quantity = product.Count
                };

                orderTotal += (product.Count * product.Product.ProductPrice);
                order.OrderDetails.Add(orderDetail);
                ctx.OrderDetails.Add(orderDetail);

            }

            order.Total = orderTotal;
            ctx.Orders.Add(order);

            ctx.SaveChanges();

            EmptyCart();

            return order;
        }

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
                    Guid tempCartId = Guid.NewGuid();

                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        
        public void MigrateCart(string userName)
        {
            var cart = ctx.Carts.Where(
                c => c.CartId == CartId);

            foreach (var product in cart)
            {
                product.CartId = userName;
            }
            ctx.SaveChanges();
        }
    }
}
