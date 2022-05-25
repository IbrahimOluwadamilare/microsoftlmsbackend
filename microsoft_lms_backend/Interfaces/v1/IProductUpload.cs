using System;
using System.Collections.Generic;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.InputModels.v1.ProductUploadInputModel;
using System.Threading.Tasks;
using microsoft_lms_backend.Models.v1.ProductUpload;

namespace microsoft_lms_backend.Interfaces.v1
{
    public interface IProductUpload
    {
        //Product Input
        Task<GenericResponse<Product>> CreateNewProductAsync(Product Input);
        Task<GenericResponse<IEnumerable<Product>>> GetAllProductAsync();
        Task<GenericResponse<Product>> GetProductbyIdAsync(int Id);
        Task<GenericResponse<Product>> UpdateProductAsync(int Id, Product Input);
        Task<GenericResponse<Product>> RemoveFromProductAsync(int Id);
        Task<GenericResponse<IEnumerable<Product>>> GetAllProductByProductTypeAsync(int ProductTypeId);


        //Product Type Input
        Task<GenericResponse<ProductType>> CreateNewProductTypeAsync(ProductType Input);
        Task<GenericResponse<IEnumerable<ProductType>>> GetAllProductTypeAsync();
        Task<GenericResponse<ProductType>> GetProductTypebyIdAsync(int Id);
        Task<GenericResponse<ProductType>> UpdateProductTypeAsync(int Id, ProductType Input);
        Task<GenericResponse<ProductType>> RemoveFromProductTypeAsync(int Id);

    }
}
