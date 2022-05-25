using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class ModuleService : IModule
    {
        public readonly ApplicationDbContext _dbcontext;

        public ModuleService()
        {

        }
        public ModuleService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

//ModuleList
        //Service implementation for creating new course module
        public async Task<GenericResponse<CourseModule>> CreateNewModuleAsync(CourseModule Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<CourseModule>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if Course exist
                    var course = await _dbcontext.Courses.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    if (course == null)
                    {
                        return new GenericResponse<CourseModule>
                        {
                            Data = Input,
                            Message = "Course Id does not exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var courseModule = new CourseModule
                        {
                            ModuleTitle = Input.ModuleTitle,
                            Courses = course
                        };
                        //If input and course is not null, add to database
                        await _dbcontext.CourseModule.AddAsync(courseModule);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<CourseModule>
                        {
                            Data = courseModule,
                            Message = "Course Module added successfully",
                            Success = true
                        };
                    }
                   
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all course module
        public async Task<GenericResponse<IEnumerable<CourseModule>>> GetAllModuleAsync()
        {
            try
            {
                //Getting all module from database
                var courseModule = await _dbcontext.CourseModule.ToListAsync();
                if (courseModule.Count == 0)
                {
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = null,
                        Message = "Course module is empty",
                        Success = true
                    };
                }
                //returning all module gotten from database
                return new GenericResponse<IEnumerable<CourseModule>>
                {
                    Data = courseModule,
                    Message = $"successfully gets {courseModule.Count} module(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<CourseModule>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single course module 
        public async Task<GenericResponse<CourseModule>> GetModulebyIdAsync(int Id)
        {
            try
            {
                //find the Course module by id from the database
                var courseModule = await _dbcontext.CourseModule.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (courseModule == null)
                {
                    return new GenericResponse<CourseModule>
                    {
                        Data = null,
                        Message = "Course Module does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the course Module
                    return new GenericResponse<CourseModule>
                    {
                        Data = courseModule,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a course Module
        public async Task<GenericResponse<CourseModule>> UpdateModuleAsync(int Id, CourseModule Input)
        {
            try
            {
                //find the course Module by it id from the database
                var courseModule = await _dbcontext.CourseModule.FirstOrDefaultAsync(x => x.Id == Id);

                //find the course by provided id from the database
                var course = await _dbcontext.Courses.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If course Module not found
                if (courseModule == null)
                {
                    return new GenericResponse<CourseModule>
                    {
                        Data = null,
                        Message = "Course module to be updated does not exist",
                        Success = false
                    };
                }
                //If course not found
                else if (course == null)
                {
                    return new GenericResponse<CourseModule>
                    {
                        Data = null,
                        Message = "Course Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var courseModuleUpdate = new CourseModule
                    {
                        ModuleTitle = Input.ModuleTitle,
                        Courses = course
                    };
                    //if found, update with the new changes and save changes
                    var result = _dbcontext.CourseModule.Update(courseModuleUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<CourseModule>
                    {
                        Data = courseModuleUpdate,
                        Message = "Course Module Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a course Module
        public async Task<GenericResponse<CourseModule>> RemoveFromModuleAsync(int Id)
        {
            try
            {
                //find the Course Module by id from the database
                var courseModuleToBeRemoved = await _dbcontext.CourseModule.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (courseModuleToBeRemoved != null)
                {
                    _dbcontext.Remove(courseModuleToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<CourseModule>
                    {
                        Data = null,
                        Message = "Course module has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<CourseModule>
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
                return new GenericResponse<CourseModule>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all module by course
        public async Task<GenericResponse<IEnumerable<CourseModule>>> GetAllModuleByCourseAsync(int CourseId)
        {
            try
            {
                //find the modules by course Id from the database
                var courseModule = await _dbcontext.CourseModule.Where(w => w.Courses.Id == CourseId).ToListAsync();
                if (courseModule.Count == 0)
                {
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = null,
                        Message = "Course module is empty",
                        Success = true
                    };
                }
                //If not found
                if (courseModule == null)
                {
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = null,
                        Message = "There is no module for this course",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<CourseModule>>
                    {
                        Data = courseModule,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<CourseModule>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }


        //Module List Item
        //Service implementation for creating new module list item
        public async Task<GenericResponse<ModuleListItem>> CreateNewModuleListItemAsync(ModuleListItem Input)
        {
            try
            {
                //checks for null input
                if (Input == null)
                {
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = Input,
                        Message = "Input is null",
                        Success = false
                    };
                }
                else
                {
                    //check if Course Module exist
                    var courseModule = await _dbcontext.CourseModule.FirstOrDefaultAsync(w => w.Id == Input.Id);
                    if (courseModule == null)
                    {
                        return new GenericResponse<ModuleListItem>
                        {
                            Data = Input,
                            Message = "Course Module Id does not exit",
                            Success = false
                        };
                    }
                    else
                    {
                        var moduleListItem = new ModuleListItem
                        {
                            Title = Input.Title,
                            Detail = Input.Detail,
                            CourseVideo = Input.CourseVideo,
                            CourseModule = courseModule
                        };
                        //If input is not null, add it to database
                        await _dbcontext.ModuleListItem.AddAsync(moduleListItem);
                        _dbcontext.SaveChanges();

                        return new GenericResponse<ModuleListItem>
                        {
                            Data = moduleListItem,
                            Message = "Module List Item added successfully",
                            Success = true
                        };
                    }
                    
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all module list item
        public async Task<GenericResponse<IEnumerable<ModuleListItem>>> GetAllModuleListItemAsync()
        {
            try
            {
                //Getting all module list item from database
                var moduleListItems = await _dbcontext.ModuleListItem.ToListAsync();
                if (moduleListItems.Count == 0)
                {
                    return new GenericResponse<IEnumerable<ModuleListItem>>
                    {
                        Data = null,
                        Message = "module item is empty",
                        Success = false
                    };
                }
                //returning all module list item gotten from database
                return new GenericResponse<IEnumerable<ModuleListItem>>
                {
                    Data = moduleListItems,
                    Message = $"successfully gets {moduleListItems.Count} module list item(s)",
                    Success = true
                };
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<ModuleListItem>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                }; ;
            }
        }

        //Service implementation for getting a single module list item
        public async Task<GenericResponse<ModuleListItem>> GetModuleListItembyIdAsync(int Id)
        {
            try
            {
                //find the Module List item by id from the database
                var moduleListItem = await _dbcontext.ModuleListItem.FirstOrDefaultAsync(x => x.Id == Id);

                //If not found
                if (moduleListItem == null)
                {
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = null,
                        Message = "Module List item does not exist",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the Module List item 
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = moduleListItem,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for Updating a module list item
        public async Task<GenericResponse<ModuleListItem>> UpdateModuleListItemAsync(int Id, ModuleListItem Input)
        {
            try
            {
                //find the Module List item by it id from the database
                var moduleListItem = await _dbcontext.ModuleListItem.FirstOrDefaultAsync(x => x.Id == Id);
                //find the Module List item by it id from the database
                var courseModule = await _dbcontext.CourseModule.FirstOrDefaultAsync(x => x.Id == Input.Id);

                //If not found
                if (moduleListItem == null)
                {
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = null,
                        Message = "Module List item to be updated does not exist",
                        Success = false
                    };
                }
                else if (courseModule == null)
                {
                    return new GenericResponse<ModuleListItem>
                    {
                        Data = null,
                        Message = "Course Module Id does not exit",
                        Success = false
                    };
                }
                else
                {
                    var moduleListItemUpdate = new ModuleListItem
                    {
                        Title = Input.Title,
                        Detail = Input.Detail,
                        CourseVideo = Input.CourseVideo,
                        CourseModule = courseModule
                    };
                    //if found, update with the new changes from input and save changes
                    var result = _dbcontext.ModuleListItem.Update(moduleListItemUpdate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ModuleListItem>
                    {
                        Data = moduleListItemUpdate,
                        Message = "Module List item Updated Successfully",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for deleting a course Module list item
        public async Task<GenericResponse<ModuleListItem>> RemoveFromModuleListItemAsync(int Id)
        {
            try
            {
                //find the Module List item by id from the database
                var modulListItemToBeRemoved = await _dbcontext.ModuleListItem.FirstOrDefaultAsync(x => x.Id == Id);

                //If found, remove it from database and save changes
                if (modulListItemToBeRemoved != null)
                {
                    _dbcontext.Remove(modulListItemToBeRemoved);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<ModuleListItem>
                    {
                        Data = null,
                        Message = "Module List item has been deleted sucessfully",
                        Success = true
                    };
                }
                else
                {
                    //If not found
                    return new GenericResponse<ModuleListItem>
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
                return new GenericResponse<ModuleListItem>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Service implementation for getting all module list by course module
        public async Task<GenericResponse<IEnumerable<ModuleListItem>>> GetAllModuleListItemByModuleAsync(int ModuleId)
        {
            try
            {
                //find the module list by course module Id from the database
                var moduleList = await _dbcontext.ModuleListItem.Where(w => w.CourseModule.Id == ModuleId).ToListAsync();

                //If not found
                if (moduleList == null)
                {
                    return new GenericResponse<IEnumerable<ModuleListItem>>
                    {
                        Data = null,
                        Message = "There is no list for this module",
                        Success = false
                    };
                }
                else
                {
                    //If found, return the article
                    return new GenericResponse<IEnumerable<ModuleListItem>>
                    {
                        Data = moduleList,
                        Message = null,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //return any catched error
                return new GenericResponse<IEnumerable<ModuleListItem>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
