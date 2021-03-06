using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helpers;
using microsoft_lms_backend.InputModels.v1;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models;

namespace microsoft_lms_backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class SupportTicketController : Controller
    {
        private readonly ISupportTicket _supportTicketsService;
        public SupportTicketController(ISupportTicket supportTicketService)
        {
            _supportTicketsService = supportTicketService;
        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<SupportTickets>>> CreateSupportTicket([FromBody] SupportTicketsInput supportTicketsInput)

        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supportTicket = new SupportTickets
                    {
                        Department = supportTicketsInput.Department,
                        Priority = supportTicketsInput.Priority,
                        Subject = supportTicketsInput.Subject,
                        Description = supportTicketsInput.Description,
                        Message = supportTicketsInput.Message,
                        Status = supportTicketsInput.Status,
                        CaseOwner = supportTicketsInput.CaseOwner,
                        Attachment = supportTicketsInput.Attachment,
                        Response = supportTicketsInput.Response,
                        //DateCreated = DateTime.Now,
                        //DateUpdated = DateTime.Now,
                    };
                    
                    var newSupportTicket = await _supportTicketsService.CreateSupportTicketAsync(supportTicket);

                    if (newSupportTicket.Success == true)
                    {
                        return new GenericResponse<SupportTickets>
                        {
                            Data = newSupportTicket.Data,
                            Message = "Support ticket created successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<SupportTickets>
                        {
                            Data = null,
                            Message = "Support Ticket not created successfully",
                            Success = false
                        };
                    }


                }
                catch (Exception e)
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }

            }
            else
            {
                return new GenericResponse<SupportTickets>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }
        }
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<SupportTickets>>> DeleteSupportTicket(int Id)
        {
            try
            {
                var deleteSupportTicket = await _supportTicketsService.DeleteSupportTicketAsync(Id);

                if (deleteSupportTicket.Success == true)
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = deleteSupportTicket.Data,
                        Message = "SupportTicket deleted successfully",
                        Success = true
                    };

                }
                else
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = null,
                        Message = "Unable to delete support ticket",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<SupportTickets>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }

        }

        [HttpPost]
        public async Task<ActionResult<GenericResponse<SupportTickets>>> EditSupportTicket(int Id, SupportTicketsInput supportTicketsInput)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var supportTicketEdit = await _supportTicketsService.GetSupportTicketByIdAsync(Id);

                    if (supportTicketEdit.Success == true)
                    {
                        supportTicketEdit.Data.Department = supportTicketsInput.Department;
                        supportTicketEdit.Data.Priority = supportTicketsInput.Priority;
                        supportTicketEdit.Data.Subject = supportTicketsInput.Subject;
                        supportTicketEdit.Data.Description = supportTicketsInput.Description;
                        supportTicketEdit.Data.Message = supportTicketsInput.Message;
                        supportTicketEdit.Data.Status = supportTicketsInput.Status;
                        supportTicketEdit.Data.CaseOwner = supportTicketsInput.CaseOwner;
                        supportTicketEdit.Data.Attachment = supportTicketsInput.Attachment;
                        supportTicketEdit.Data.Response = supportTicketsInput.Response;

                        var newSupportTicket = await _supportTicketsService.EditSupportAsync(supportTicketEdit.Data);

                        return new GenericResponse<SupportTickets>
                        {
                            Data = newSupportTicket.Data,
                            Message = "Support ticket updated successfully",
                            Success = true
                        };
                    }
                    else
                    {
                        return new GenericResponse<SupportTickets>
                        {
                            Data = null,
                            Message = "Support ticket not updated",
                            Success = false
                        };
                    }

                }
                catch (Exception e)
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = null,
                        Message = e.Message,
                        Success = false
                    };
                }
            }
            else
            {
                return new GenericResponse<SupportTickets>
                {
                    Data = null,
                    Message = "Invalid operation",
                    Success = false

                };
            }
        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<SupportTickets>>>> GetAllSupportTickets()
        {
            try
            {
                var allTickets = await _supportTicketsService.GetAllSupportTicketsAsync();

                if (allTickets.Success == true)
                {
                    return new GenericResponse<IEnumerable<SupportTickets>>
                    {
                        Data = allTickets.Data,
                        Message = "Ticketss listed",
                        Success = true
                    };
                }
                else
                {
                    return new GenericResponse<IEnumerable<SupportTickets>>
                    {
                        Data = null,
                        Message = "Tickets not found",
                        Success = false
                    };
                }
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<SupportTickets>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
        [HttpGet]
        public async Task<ActionResult<GenericResponse<SupportTickets>>> GetSupportTicketById(int Id)
        {
            try
            {
                var ticket = await _supportTicketsService.GetSupportTicketByIdAsync(Id);

                if (ticket.Success == true)
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = ticket.Data,
                        Message = "ticket found",
                        Success = true

                    };
                }
                else
                {
                    return new GenericResponse<SupportTickets>
                    {
                        Data = null,
                        Message = "ticket not found",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                return new GenericResponse<SupportTickets>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }
    }
}
