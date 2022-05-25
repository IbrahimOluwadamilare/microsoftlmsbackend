using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Models;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Interfaces.v1;


namespace microsoft_lms_backend.Controllers.v1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpPost]
        
        //an endpoint tht creates a new template when called and post the info to the database
        public async Task<ActionResult<GenericResponse<Templates>>> CreateTemplate([FromBody] TemplateInput templateInput)

        {
            // checks if model state is valid before procedding to create new template
            if (ModelState.IsValid) {
                try
                {
                    //creates an instance of the template model
                    var template = new Templates
                    {
                        Title = templateInput.Title,
                        Description = templateInput.Description,
                        TemplateURL = templateInput.TemplateURL,
                        DateCreated = DateTime.Now,
                        DateUpdated = DateTime.Now,
                    };

                    var newTemplate = await _templateService.CreateTemplateAsync(template);

                    if (newTemplate.Success == true)
                        { 
                        return new GenericResponse<Templates>
                        {
                            Data = newTemplate.Data,
                            Message = "Template created successfully",
                            Success = true
                        };
                    } else
                    {
                        return new GenericResponse<Templates>
                        {
                            Data = null,
                            Message = "Template not created successfully",
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
            //returns this if the state of the model is invalid
            else
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }
        }

        [HttpDelete]
        //an end point that deletes a template from the database 
        public async Task<ActionResult<GenericResponse<Templates>>> DeleteTemplate(int Id)
        {
            try
            {
                var deleteTemplate = await _templateService.DeleteTemplateAsync(Id);

                if (deleteTemplate.Success == true)
                {
                    return new GenericResponse<Templates>
                    {
                        Data = deleteTemplate.Data,
                        Message = "Template deleted successfully",
                        Success = true
                    };

                } else
                {
                    return new GenericResponse<Templates>
                    {
                        Data = null,
                        Message = "Unable to delete template",
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
        
        [HttpPost]
        //this end point allows the user to update the template by changing some information in it
        public async Task<ActionResult<GenericResponse<Templates>>> EditTemplate(int Id, TemplateInput templateInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // gets a template from the database by the ID
                    var templateEdit = await _templateService.GetTemplateByIdAsync(Id);
                    //updates template if it successfully gets the template b y ID
                    if (templateEdit.Success == true)
                   {
                        templateEdit.Data.Title = templateInput.Title;
                        templateEdit.Data.Description = templateInput.Description;
                        templateEdit.Data.TemplateURL = templateInput.TemplateURL;
                        templateEdit.Data.DateCreated = templateInput.DateCreated;
                        templateEdit.Data.DateUpdated = templateInput.DateUpdated;

                        var newTemplate = await _templateService.EditTemplateAsync(templateEdit.Data);
                    
                        return new GenericResponse<Templates>
                        {
                            Data = newTemplate.Data,
                            Message = "Template updated successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<Templates>
                        {
                            Data = null,
                            Message = "Template not updated",
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
            else
            {
                return new GenericResponse<Templates>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }

        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<Templates>>>> GetAllTemplates()
        {
            try
            {
                var allTemplates = await _templateService.GetAllTemplatesAsync();

                if (allTemplates.Success == true)
                {
                    return new GenericResponse<IEnumerable<Templates>>
                    {
                        Data = allTemplates.Data,
                        Message = "Templates listed",
                        Success = true
                    };
                } else
                {
                    return new GenericResponse<IEnumerable<Templates>>
                    {
                        Data = null,
                        Message = "Templates not found",
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
           
        [HttpGet]

        public async Task<ActionResult<GenericResponse<Templates>>> GetTemplateById (int Id)
        {
            try
            {
                var template = await _templateService.GetTemplateByIdAsync(Id);

                if (template.Success == true)
                {
                    return new GenericResponse<Templates>
                    {
                        Data = template.Data,
                        Message = "template found",
                        Success = true

                    };
                }
                else
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




