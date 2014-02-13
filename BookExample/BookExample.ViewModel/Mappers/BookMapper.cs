namespace BookExample.ViewModel.Mappers
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using BookExample.Models;

    using Omu.ValueInjecter;

    public static class BookMapper
    {
        public static BookViewModel ToViewModel(this Book model)
        {
            var viewModel = new BookViewModel();
            viewModel.InjectFrom(model);
            viewModel.Authors = model.Authors.Select(x => x.ToViewModel());

            return viewModel;
        }

        public static Book ToModel(this BookViewModel viewModel)
        {
            var model = new Book();
            model.InjectFrom(viewModel);
            
            return model;
        }
    }
}
