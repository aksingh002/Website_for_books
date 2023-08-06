using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotNet_webApplication.Models;
using dotNet_webApplication.Repository;
using dotNet_webApplication.Repository.IRepository;

namespace dotNet_webApplication.Areas.customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger ,IUnitOfWork unitOfWork )
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public IActionResult Index()
    {   
        IEnumerable<Product> productlist =_unitOfWork.Product.Getall(IncludeProperties:"Category");
        return View(productlist);
    }

    public IActionResult Details(int id)
    {   
        Product product =_unitOfWork.Product.Get(u=>u.Id==id,IncludeProperties:"Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    //
}
