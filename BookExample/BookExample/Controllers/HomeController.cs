namespace BookExample.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using System.Web.Mvc;

    using BookExample.Models;
    using BookExample.Repository.Abstract;
    using BookExample.ViewModel;
    using BookExample.ViewModel.Mappers;

    public class HomeController : Controller
    {
        private readonly IBookRepository bookRepository;

        private readonly IAuthorRepository authorRepository;

        public HomeController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<BookViewModel> viewModel = this.bookRepository.Get().ToList().Select(x => x.ToViewModel());
            this.ViewBag.AuthorsCount = this.authorRepository.Get().Count();

            return this.View(viewModel);
        }

        public ActionResult Details(int id)
        {
            BookViewModel viewModel = this.bookRepository.Get(id).ToViewModel();

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new BookViewModel();
            viewModel.AvailableReleseYears = this.GetAvailableYears(null);
            viewModel.Authors = this.authorRepository.Get().Select(AuthorMapper.ToExpressionViewModel);

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = viewModel.ToModel();
                var authors = this.authorRepository.Get();
                book.Authors = new Collection<Author>();
                foreach (var selectedAuthor in viewModel.SelectedAuthors)
                {
                    book.Authors.Add(authors.First(x => x.Id == selectedAuthor));
                }

                this.bookRepository.Add(book);

                return this.RedirectToAction("Index");
            }

            viewModel.AvailableReleseYears = this.GetAvailableYears(null);
            viewModel.Authors = this.authorRepository.Get().Select(AuthorMapper.ToExpressionViewModel);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModel = this.bookRepository.Get(id).ToViewModel();
            viewModel.AvailableReleseYears = this.GetAvailableYears(viewModel.ReleseYear);
            viewModel.Authors = this.authorRepository.Get().Select(AuthorMapper.ToExpressionViewModel).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = viewModel.ToModel();
                var authors = this.authorRepository.Get();
                book.Authors = new Collection<Author>();
                foreach (var selectedAuthor in viewModel.SelectedAuthors)
                {
                    book.Authors.Add(authors.First(x => x.Id == selectedAuthor));
                }

                this.bookRepository.Update(book);

                return this.RedirectToAction("Index");
            }

            viewModel.AvailableReleseYears = this.GetAvailableYears(null);
            viewModel.Authors = this.authorRepository.Get().Select(AuthorMapper.ToExpressionViewModel);

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            BookViewModel viewModel = this.bookRepository.Get(id).ToViewModel();

            return this.View(viewModel);
        }

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            this.bookRepository.Delete(id);

            return this.RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetAvailableYears(int? year)
        {
            for (int i = DateTime.Now.Year; i > 1000; i--)
            {
                yield return new SelectListItem { Text = i.ToString(), Value = i.ToString(), Selected = i == year };
            }
        }
    }
}