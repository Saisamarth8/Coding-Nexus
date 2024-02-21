
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using TutorialWebApp.Models.ViewModels;

namespace TutorialWebApp.Controllers
{
    public class OrderController : Controller
    {

        [BindProperty]
        public OrderEntity _OrderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult InitiateOrder()
        {
            string apiKey = "rzp_test_6t2D52aaxnZZXu";
            string apiSecret = "iVx9qHY8k7HKAGmdK4TZ8yg4";
           
            Random _random = new Random();
            string TransactionId = _random.Next(0, 10000).ToString();

            Dictionary<string, object> input = new Dictionary<string, object>();

            input.Add("amount", Convert.ToDecimal(_OrderDetails.TotalAmount)*100); 
            input.Add("currency","INR");
            input.Add("receipt", TransactionId);

            RazorpayClient client = new RazorpayClient(apiKey,apiSecret);
            Razorpay.Api.Order order = client.Order.Create(input);

            ViewBag.orderid = order["id"].ToString();

            return View("Payment", _OrderDetails);
        }
        
        public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            Dictionary<string, string> attributes = new Dictionary<string,string>();
            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);

            Utils.verifyPaymentSignature(attributes);

            OrderEntity orderdetails = new OrderEntity();
            orderdetails.TransactionId = razorpay_payment_id;
            orderdetails.OrderId = razorpay_order_id;

            return View("PaymentSuccess",orderdetails);
        }
    }
}