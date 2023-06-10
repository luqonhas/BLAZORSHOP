using BlazorShop.Domain.Commands.Categories;
using BlazorShop.Domain.Interfaces;
using BlazorShop.Shared.Commands;
using BlazorShop.Shared.Handlers.Contracts;
using Flunt.Notifications;

namespace BlazorShop.Domain.Handlers.Categories
{
    public class DeleteCategoryHandle : Notifiable<Notification>, IHandlerCommand<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryHandle(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICommandResult Handler(DeleteCategoryCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the category you want to delete", command.Notifications);
            }

            var searchedCategory = _categoryRepository.SearchById(command.Id);

            if (searchedCategory == null)
            {
                return new GenericCommandResult(false, "Category not found", command.Notifications);
            }

            _categoryRepository.Delete(searchedCategory.Id);

            return new GenericCommandResult(false, "Category deleted successfully!", "");
        }
    }
}
