using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projektowaniaOprogramowania.Models;
using projektowaniaOprogramowania.Services;
using projektowaniaOprogramowania.ViewModels;
using projektowaniaOprogramowania.ViewModels.CollegeStructures;
using projektowaniaOprogramowania.ViewModels.Users;

namespace projektowaniaOprogramowania.Controllers
{
    public class PodanieController : Controller
    {
        private readonly MyDbContext _context;
        private readonly PodanieService _podanieService;

        public PodanieController(MyDbContext context, PodanieService podanieService)
        {
            _context = context;
            _podanieService = podanieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Podanie/Details/5
        public IActionResult Details(long? id)
        {
            KandydatModel candidate = _context.Kandydaci.SingleOrDefault(candidate => candidate.Id == HttpContext.Session.GetLong("UserId"));
            if (candidate == null)
                return RedirectToAction("Error", "Home");

            var podanie = _podanieService.GetPodanieKandydata(candidate);

            KierunekNaPodaniuModel kierunki = _context.KierunkiNaPodaniu.SingleOrDefault(kierunki => kierunki.FkIdPodanieKandydata == podanie.Id);

            List<KierunekModel> kierunkiNaPodaniu = _context.Kierunki.Where(kierunek => kierunek.Id == kierunki.FkIdKierunek).ToList();

            var model = new KandydatZPodaniemViewModel
            {
                podanieKandydataViewModel = podanie,
                kandydatViewModel = candidate,
                kierunkiNaPodaniu = kierunkiNaPodaniu
            };


            return View(model);
        }
    }
}
