using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.ViewModels.Product;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Application.Implementation
{
    public class ProductService : IProductService
    {
        private IMapper _mapper;
        private IRepository<Product, int> _productRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<ProductCategory, int> _productCategoryRepository;

        public ProductService(IMapper mapper, IRepository<Product, int> productRepository, IUnitOfWork unitOfWork, IRepository<ProductCategory, int> productCategoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ProductViewModel> AddAsync(ProductViewModel productVm)
        {
            Product product = _mapper.Map<Product>(productVm);
            await _productRepository.AddAsync(product);
            _unitOfWork.Commit();

            return productVm;
        }

        public void Update(ProductViewModel productVm)
        {
            Product product = _mapper.Map<Product>(productVm);
            _productRepository.Update(product);
        }

        public void Delete(int id)
        {
            _productRepository.Remove(id);
        }

        public List<string> GetProductName(string productName)
        {
            var listProduct = _productRepository.FindAll(x => x.Name.Contains(productName));
            List<string> listNames = new List<string>();
            foreach (var item in listProduct)
            {
                listNames.Add(item.Name);
            }
            return listNames;
        }

        public List<ProductViewModel> GetAll()
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll().OrderByDescending(x => x.DateCreated).ToList());
        }

        public List<ProductViewModel> GetAll(int? categoryId, string hotPromotion, string keyword, int page, int pageSize, out int totalRow)
        {
            var listProduct = _productRepository.FindAll(c => c.ProductCategory);
            if (categoryId.HasValue)
            {
                listProduct = listProduct.Where(x => x.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                bool flagId = int.TryParse(keyword, out int id);
                if (flagId == true)
                {
                    listProduct = listProduct.Where(x => x.Id == id);
                }
                else
                {
                    listProduct = listProduct.Where(x => x.Name.Contains(keyword));
                }
            }
            totalRow = listProduct.Count();
            listProduct = listProduct.OrderByDescending(d => d.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(listProduct.ToList());
        }

        public List<ProductViewModel> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            var listProduct = _productRepository.FindAll().OrderByDescending(x => x.DateCreated);
            totalRow = listProduct.Count();
            return _mapper.Map<List<ProductViewModel>>(listProduct.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public ProductViewModel GetById(int id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.FindById(id, c => c.ProductCategory));
        }

        public List<ProductViewModel> GetAllByCategoryPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.CategoryId == categoryId, c => c.ProductCategory);
            switch (sort)
            {
                case "nameIncrease":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "nameDecrease":
                    query = query.OrderByDescending(x => x.Name);
                    break;

                case "priceIncrease":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "priceDecrease":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.DateModified);
                    break;
            }
            totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(query.ToList());
        }

        public List<ProductViewModel> GetAllByNamePaging(string Name, int page, int pageSize, out int totalRow)
        {
            var query = _productRepository.FindAll(x => x.Status == Data.Enums.Status.Active && x.Name.Contains(Name));
            totalRow = query.Count();
            query = query.OrderBy(x => x.Name).Skip((page - 1) * pageSize).Take(pageSize);
            return _mapper.Map<List<ProductViewModel>>(query.ToList());
        }

        public List<ProductViewModel> GetProductRelate(int categoryId, int number)
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll(x => x.CategoryId == categoryId && x.Status == Data.Enums.Status.Active)
                .OrderByDescending(x => x.DateModified).Take(number).ToList());
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void UpdateDb(Product productDb)
        {
            _productRepository.Update(productDb);
        }

        public Product GetProductDbById(int id)
        {
            return _productRepository.FindById(id);
        }

        public List<ProductViewModel> GetNewProduct(int number)
        {
            return _mapper.Map<List<ProductViewModel>>(_productRepository.FindAll(x => x.Status == Data.Enums.Status.Active)
                .OrderByDescending(x => x.DateCreated).Take(number).ToList());
        }
    }
}