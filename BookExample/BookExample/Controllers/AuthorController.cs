namespace BookExample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using BookExample.Repository.Abstract;
    using BookExample.ViewModel;
    using BookExample.ViewModel.Mappers;

    public class AuthorController : Controller
    {
        private readonly IAuthorRepository authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IQueryable<AuthorViewModel> viewModel = this.authorRepository.Get().Select(AuthorMapper.ToExpressionViewModel);

            return this.View(viewModel);
        }

        public ActionResult Details(int id)
        {
            AuthorViewModel viewModel = this.authorRepository.Get(id).ToViewModel();

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new AuthorViewModel { AvailableBirthYears = this.GetAvailableYears(null), AvailableDeathYears = this.GetAvailableYears(null)};
            
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!viewModel.DeathYear.HasValue || viewModel.DeathYear.Value > viewModel.BirthYear)
                {
                    this.authorRepository.Add(viewModel.ToModel());

                    return this.RedirectToAction("Index");
                }

                ModelState.AddModelError("DeathYear", "Death year must be greater than birth year.");
            }

            viewModel.AvailableBirthYears = this.GetAvailableYears(viewModel.BirthYear);
            viewModel.AvailableDeathYears = this.GetAvailableYears(viewModel.DeathYear);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = this.authorRepository.Get(id).ToViewModel();
            viewModel.AvailableBirthYears = this.GetAvailableYears(viewModel.BirthYear);
            viewModel.AvailableDeathYears = this.GetAvailableYears(viewModel.DeathYear);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AuthorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (!viewModel.DeathYear.HasValue || viewModel.DeathYear.Value > viewModel.BirthYear)
                {
                    this.authorRepository.Update(viewModel.ToModel());

                    return this.RedirectToAction("Index");
                }

                ModelState.AddModelError("DeathYear", "Death year must be greater than birth year.");
            }

            viewModel.AvailableBirthYears = this.GetAvailableYears(viewModel.BirthYear);
            viewModel.AvailableDeathYears = this.GetAvailableYears(viewModel.DeathYear);

            return this.View(viewModel);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            AuthorViewModel viewModel = this.authorRepository.Get(id).ToViewModel();

            return this.View(viewModel);
        }



        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            this.authorRepository.Delete(id);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult GetAuthorList(int number, int? id)
        {
            this.ViewBag.Number = number;
            IEnumerable<SelectListItem> viewModel = this.authorRepository
                .Get()
                .ToList()
                .Select(x => new SelectListItem{ Text = x.FullName, Value = x.Id.ToString(), Selected = x.Id == id});

            return this.PartialView("GetAuthorListPartial", viewModel);
        }

        private IEnumerable<SelectListItem> GetAvailableYears(int? selectedYear)
        {
            for (int i = DateTime.Now.Year; i > 0; i--)
            {
                yield return new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == selectedYear };
            }
        }
    }
}