using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.ViewModels;

namespace TeduCoreApp.Application.Interfaces
{
    public interface IPageImageService : IDisposable
    {
        void Add(PageImageViewModel pageImage);

        PageImageViewModel GetById(int id);

        List<PageImageViewModel> GetAllByPageId(int pageId);

        void Delete(int id);

        void SaveChanges();
    }
}
