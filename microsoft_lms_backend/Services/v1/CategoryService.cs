using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class CategoryService : ICategory
    {
        public readonly ApplicationDbContext _dbcontext;

        public CategoryService()
        {

        }
        public CategoryService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //Service implementation for creating new category
        public async Task<GenericResponse<Category>> CreateNewCategoryAsync(Category Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<Category>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    var category = await _dbcontext.Category.FirstOrDefaultAsync(w => w.Name == Input.Name);
                    if (category != null)
                    {
                        return new GenericResponse<Category>
                        {
                            Data = Input,
                            Message = $"Category with the name {Input.Name} already exit",
                            Success = false
                        };
                    }
                    //If input is not null, add it to database
                    await _dbcontext.Category.AddAsync(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Category>
                    {
                        Data = Input,
                        Message = "Category added successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all category
        public async Task<GenericResponse<IEnumerable<Category>>> GetAllCategoryAsync()
        {
            try
            {
                //Getting all category from database
                var category = await _dbcontext.Category.ToListAsync();
                if (category.Count == 0)
                {
                    return new GenericResponse<IEnumerable<Category>>
                    {
                        Data = null,
                        Message = "Category Category is empty",
                        Success = false
                    };
                }
                //returning all category gotten from database
                return new GenericResponse<IEnumerable<Category>>
                {
                    Data = category,
                    Message = $"successfully gets {category.Count} category",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<Category>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single category
        public async Task<GenericResponse<Category>> GetCategorybyIdAsync(int Id)
        {
            try
            {
                //find the category by id from the database
                var category = await _dbcontext.Category.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (category == null)
                {
                    return new GenericResponse<Category>
                    {
                        Data = null,
                        Message = "category does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the category
                    return new GenericResponse<Category>
                    {
                        Data = category,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a category
        public async Task<GenericResponse<Category>> UpdateCategoryAsync(Category Input)
        {
            try
            {
                //find the category by it id from the database
                var category = await _dbcontext.Category.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If not found
                if (category == null)
                {
                    return new GenericResponse<Category>
                    {
                        Data = null,
                        Message = "category to be updated does not exist",
                        Success = false
                    };
                }
                else
                {
                    //if found, update with the new changes from input and save changes
                    var result = _dbcontext.Category.Update(Input);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Category>
                    {
                        Data = null,
                        Message = "category Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a category
        public async Task<GenericResponse<Category>> RemoveFromCategoryAsync(int Id)
        {
            try
            {
                //find the category by id from the database
                var categoryToBeRemoved = await _dbcontext.Category.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (categoryToBeRemoved != null)
                {
                    _dbcontext.Remove(categoryToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Category>
                    {
                        Data = null,
                        Message = "category has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<Category>
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
                return new GenericResponse<Category>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
