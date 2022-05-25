using System;
using System.Collections.Generic;
using microsoft_lms_backend.Interfaces.v1;
using System.Linq;
using System.Threading.Tasks;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Models.v1.ProductUpload;
using microsoft_lms_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace microsoft_lms_backend.Services.v1
{
    public class ProductUploadService : IProductUpload
    {
        public readonly ApplicationDbContext _dbcontext;

        public ProductUploadService()
        {

        }
        public ProductUploadService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<GenericResponse<Product>> CreateNewProductAsync(Product Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<Product>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if product type exit 
                    var productType = await _dbcontext.ProductType.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    if (productType == null)
                    {
                        return new GenericResponse<Product>
                        {
                            Data = Input,
                            Message = "Product type Id does not exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var product = new Product
                        {
                            ProductName = Input.ProductName,
                            ProductDescription = Input.ProductDescription,
                            Price = Input.Price,
                            DateCreated = DateTime.Now,
                            ProductType = productType,
                        };
                        //If input and product type is not null, add to database
                        await _dbcontext.Product.AddAsync(product);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<Product>
                        {
                            Data = product,
                            Message = "Product added successfully",
                            Success = true
                        };
                    }
                 
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Product>
                {
                    Data = Input,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        public async Task<GenericResponse<IEnumerable<Product>>> GetAllProductAsync()
        {
            try
            {
                //Getting all product from database
                var products = await _dbcontext.Product.ToListAsync();
                if (products.Count == 0)
                {
                    return new GenericResponse<IEnumerable<Product>>
                    {
                        Data = null,
                        Message = "Product is empty",
                        Success = true
                    };
                }
                //returning all product gotten from database
                return new GenericResponse<IEnumerable<Product>>
                {
                    Data = products,
                    Message = $"successfully gets {products.Count} product(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Product>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        public async Task<GenericResponse<Product>> GetProductbyIdAsync(int Id)
        {
            try
            {
                //find the product by id from the database
                var product = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (product == null)
                {
                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Product does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the product
                    return new GenericResponse<Product>
                    {
                        Data = product,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        public async Task<GenericResponse<Product>> UpdateProductAsync(int Id, Product Input)
        {
            try
            {
                //find the Product by it id from the database
                var product = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == Input.Id);
                var productType = await _dbcontext.ProductType.FirstOrDefaultAsync(w => w.Id == Input.Id);

                //If not found
                if (product == null)
                {
                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Product to be updated does not exist",
                        Success = false
                    };
                }//check if product type exit 
                else if (productType == null)
                {
                    return new GenericResponse<Product>
                    {
                        Data = Input,
                        Message = "Product type Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var productUpdate = new Product
                    {
                        ProductName = Input.ProductName,
                        ProductDescription = Input.ProductDescription,
                        Price = Input.Price,
                        DateCreated = DateTime.Now,
                        ProductType = productType,
                    };
                    //if both found, update with the new changes and save changes
                    var result = _dbcontext.Product.Update(productUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Product>
                    {
                        Data = productUpdate,
                        Message = "Product Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Product>> RemoveFromProductAsync(int Id)
        {
            try
            {
                //find the product by id from the database
                var productToBeRemoved = await _dbcontext.Product.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (productToBeRemoved != null)
                {
                    _dbcontext.Remove(productToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Product has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<Product>
                    {
                        Data = null,
                        Message = "Not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Product>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        public async Task<GenericResponse<IEnumerable<Product>>> GetAllProductByProductTypeAsync(int ProductTypeId)
        {
            try
            {
                //find the Product by product type Id from the database
                var products = await _dbcontext.Product.Where(w => w.ProductType.Id == ProductTypeId).ToListAsync();

                //If not found
                if (products == null)
                {
                    return new GenericResponse<IEnumerable<Product>>
                    {
                        Data = null,
                        Message = "There is no product for this product type Id",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<Product>>
                    {
                        Data = products,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Product>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //ProductType

        public async Task<GenericResponse<ProductType>> CreateNewProductTypeAsync(ProductType Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<ProductType>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //If input is not null, add it to database
                    await _dbcontext.ProductType.AddAsync(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ProductType>
                    {
                        Data = Input,
                        Message = "Product Type added successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ProductType>
                {
                    Data = Input,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<ProductType>>> GetAllProductTypeAsync()
        {
            try
            {
                //Getting all product from database
                var productType = await _dbcontext.ProductType.ToListAsync();
                if (productType.Count == 0)
                {
                    return new GenericResponse<IEnumerable<ProductType>>
                    {
                        Data = null,
                        Message = "Product type is empty",
                        Success = true
                    };
                }
                //returning all product gotten from database
                return new GenericResponse<IEnumerable<ProductType>>
                {
                    Data = productType,
                    Message = $"successfully gets {productType.Count} product types(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<ProductType>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        public async Task<GenericResponse<ProductType>> GetProductTypebyIdAsync(int Id)
        {
            try
            {
                //find the product by id from the database
                var productType = await _dbcontext.ProductType.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (productType == null)
                {
                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Product Type does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the product
                    return new GenericResponse<ProductType>
                    {
                        Data = productType,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<ProductType>> UpdateProductTypeAsync(int Id, ProductType Input)
        {
            try
            {
                //find the Product type by it id from the database
                var product = await _dbcontext.ProductType.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (product == null)
                {
                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Product type to be updated does not exist",
                        Success = false
                    };
                }
                else
                {
                    //if found, update with the new changes from input and save changes
                    var result = _dbcontext.ProductType.Update(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Product type Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<ProductType>> RemoveFromProductTypeAsync(int Id)
        {
            try
            {
                //find the product type by id from the database
                var productToBeRemoved = await _dbcontext.ProductType.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (productToBeRemoved != null)
                {
                    _dbcontext.Remove(productToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Product type has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<ProductType>
                    {
                        Data = null,
                        Message = "Not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ProductType>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
