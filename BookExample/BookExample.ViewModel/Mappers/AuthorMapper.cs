namespace BookExample.ViewModel.Mappers
{
    using System;
    using System.Linq.Expressions;

    using BookExample.Models;

    using Omu.ValueInjecter;

    public static class AuthorMapper
    {
        public static Expression<Func<Author, AuthorViewModel>> ToExpressionViewModel =
            model =>
            new AuthorViewModel
                {
                    Id = model.Id,
                    FullName = model.FullName,
                    BirthYear = model.BirthYear,
                    DeathYear = model.DeathYear
                };

        public static AuthorViewModel ToViewModel(this Author model)
        {
            var viewModel = new AuthorViewModel();
            viewModel.InjectFrom(model);

            return viewModel;
        }

        public static Author ToModel(this AuthorViewModel viewModel)
        {
            var model = new Author();
            model.InjectFrom(viewModel);

            return model;
        }
    }
}
