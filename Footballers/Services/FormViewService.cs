using Footballers.Abstractions;
using Footballers.Models;
using Footballers.ViewModels;

namespace Footballers.Services
{
    public class FormViewService : IFormViewService
    {
        private readonly IFormViewRepository _formViewRepository;

        public FormViewService(IFormViewRepository formViewRepository)
        {
            _formViewRepository = formViewRepository;
        }

        public FormViewModel GetFormViewModel()
        {
            return _formViewRepository.Get();
        }

        public FormViewModel GetFormViewModel(int id)
        {
            return _formViewRepository.Get(id);
        }
    }
}
