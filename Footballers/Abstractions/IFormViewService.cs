using Footballers.Models;
using Footballers.ViewModels;

namespace Footballers.Abstractions
{
    public interface IFormViewService
    {
        FormViewModel GetFormViewModel();
        FormViewModel GetFormViewModel(int id);
    }
}