using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IPageService : IDisposable
    {
        int Add(PageViewModel pageVm);

        void Update(PageViewModel pageVm);

        void Delete(int id);

        List<PageViewModel> GetAll();

        PageViewModel GetAllPaggingByAlias(string alias);

        PageViewModel GetById(int id);

        void SaveChanges();
    }
}
