using Footballers.Models;
using Footballers.ViewModels;

namespace Footballers.Abstractions
{
    public interface IFormViewRepository
    {
        FormViewModel Get();
        FormViewModel Get(int id);
    }
}