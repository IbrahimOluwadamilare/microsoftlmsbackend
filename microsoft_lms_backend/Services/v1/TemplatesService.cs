using Microsoft.EntityFrameworkCore;
using microsoft_lms_backend.Data;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.Interfaces.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace microsoft_lms_backend.Services.v1
{
    public class TemplatesService : ITemplateService
    {
        private readonly ApplicationDbContext _dbcontext;

        public TemplatesService()
        {
        }

        public TemplatesService(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<GenericResponse<Templates>> CreateTemplateAsync(Templates templates) 
        {
            try
            {
                if (templates == null)
                {
                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "template is null",
                        Success = false
                    };
                } else
                {
                    await _dbcontext.Templates.AddAsync(templates);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Templates>
                    {
                        Data = templates,
                        Message = "template created successfully",
                        Success = true
                    };
                }
                

            }
            catch (Exception e)
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Templates>> DeleteTemplateAsync(int Id)
        {
            try
            {
                var editTemplate = await _dbcontext.Templates.FirstOrDefaultAsync(t => t.Id == Id);
                if (editTemplate == null)
                {
                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "template not found",
                        Success = false
                    };
                } else

                {
                    _dbcontext.Remove(editTemplate);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "template successfully deleted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        public async Task<GenericResponse<Templates>> EditTemplateAsync(Templates templates)
        {
           try

            {
                var editTemplate = await _dbcontext.Templates.FirstOrDefaultAsync(t => t.Id == templates.Id);
                if (editTemplate != null)
                {
                    _dbcontext.Templates.Update(templates);
                    _dbcontext.SaveChanges();

                    return new GenericResponse<Templates>
                    {
                        Data = templates,
                        Message = "Template suceessfully updated",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "Template not found",
                        Success = false

                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Templates>>> GetAllTemplatesAsync()
        {
            try
            {
                var templates = await _dbcontext.Templates.ToListAsync();

                if (templates != null)
                {
                    return new GenericResponse<IEnumerable<Templates>>
                    {
                        Data = templates,
                        Message = "All templates listed",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<IEnumerable<Templates>>
                    {
                        Data = null,
                        Message = "templates not found", 
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<Templates>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Templates>> GetTemplateByIdAsync(int Id)
        {
            try
            {
                var template = await _dbcontext.Templates.SingleOrDefaultAsync(t => t.Id == Id);

                if (template != null)
                {
                    return new GenericResponse<Templates>
                    {
                        Data = template,
                        Message = "template found",
                        Success = true

                    };
                } else
                {
                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "template not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
