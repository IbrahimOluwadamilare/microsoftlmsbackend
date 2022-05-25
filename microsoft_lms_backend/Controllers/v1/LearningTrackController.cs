using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microsoft_lms_backend.Helper;
using microsoft_lms_backend.InputModels.v1.ContentManagementInputModel;
using microsoft_lms_backend.Interfaces.v1;
using microsoft_lms_backend.Models.v1.ContentManagementModel;

namespace microsoft_lms_backend.Controllers.v1
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class LearningTrackController : ControllerBase
    {

        //Dependency injection of learning track service
        private readonly ILearningTrack _learningTrackService;

        //learning trackController constructor
        public LearningTrackController(ILearningTrack learningTrackService)
        {
            _learningTrackService = learningTrackService;
        }

        //Creating New learning track post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<LearningTrack>>> CreateNewLearningTrack([FromBody] LearningTrackInput input)
        {
            try
            {
                //checking model state validity
                if (ModelState.IsValid)
                {
                    var learningTrack = new LearningTrack
                    {
                        TrackName = input.TrackName,
                        TrackDescription = input.TrackDescription,
                        TrackBanner = input.TrackBanner,
                        Id = input.CourseCategoryId

                    };
                    //Creating learning track using the service
                    var newLearningTrack = await _learningTrackService.CreateNewLearningTrackAsync(learningTrack);

                    //checking for operation failure
                    if (newLearningTrack.Success == false)
                    {
                        return StatusCode((int)HttpStatusCode.BadRequest, newLearningTrack);
                    }
                    else
                    {
                        //when operation is successful
                        return new GenericResponse<LearningTrack>
                        {
                            Data = newLearningTrack.Data,
                            Message = newLearningTrack.Message,
                            Success = true
                        };
                    }
                }
                else
                {
                    //when model state is invalid
                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = false

                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = true
                };
            }

        }

        //All LearningTrack Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<LearningTrack>>>> GetAllLearningTracks()
        {
            try
            {
                //Getting all learning track using the service
                var learningTrack = await _learningTrackService.GetAllLearningTrackAsync();

                //checks for operation failure
                if (learningTrack.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, learningTrack);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<LearningTrack>>
                    {
                        Data = learningTrack.Data,
                        Message = learningTrack.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<LearningTrack>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };

            }
        }

        //Single learning track get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<LearningTrack>>> GetLearningTrackById(int Id)
        {
            try
            {
                //Getting a single learning track by Id using the service
                var learningTrack = await _learningTrackService.GetLearningTrackbyIdAsync(Id);

                //checks for operation failure
                if (learningTrack.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, learningTrack);
                }
                else
                {
                    //when get operation is successful
                    return new GenericResponse<LearningTrack>
                    {
                        Data = learningTrack.Data,
                        Message = "Fechted",
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //Update post request
        [HttpPost]
        public async Task<ActionResult<GenericResponse<LearningTrack>>> UpdateLearningTrack(int Id, LearningTrackInput Input)
        {
            try
            {
                //checking model state validity
                if (!ModelState.IsValid)
                {
                    return new GenericResponse<LearningTrack>
                    {
                        Data = null,
                        Message = "Invalid operation",
                        Success = true
                    };
                }
                else
                {
                    //Getting learning track using the Id
                    var LearningTrackFromDatabase = await _learningTrackService.GetLearningTrackbyIdAsync(Id);

                    //checks for operation failure
                    if (!LearningTrackFromDatabase.Success)
                    {
                        return new GenericResponse<LearningTrack>
                        {
                            Data = null,
                            Message = "learning track to be updated does not exist",
                            Success = false
                        };
                    }
                    else
                    {
                        //changing the learning track to the new instance
                        LearningTrackFromDatabase.Data.TrackName = Input.TrackName;
                        LearningTrackFromDatabase.Data.TrackDescription = Input.TrackDescription;
                        LearningTrackFromDatabase.Data.TrackBanner = Input.TrackBanner;
                        LearningTrackFromDatabase.Data.Id = Input.CourseCategoryId;

                        //Updating the learning track using the service
                        var newLearningTrack = await _learningTrackService.UpdateLearningTrackAsync(Id, LearningTrackFromDatabase.Data);

                        //checks for operation failure
                        if (!newLearningTrack.Success)
                        {
                            return StatusCode((int)HttpStatusCode.BadRequest, LearningTrackFromDatabase);
                        }
                        else
                        {
                            //when update operation is successfull
                            return new GenericResponse<LearningTrack>
                            {
                                Data = LearningTrackFromDatabase.Data,
                                Message = newLearningTrack.Message,
                                Success = true
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //learning track Delete request
        [HttpDelete]
        public async Task<ActionResult<GenericResponse<LearningTrack>>> RemoveItemFromLearningTrack(int Id)
        {
            try
            {
                //deleting a learning track using the service
                var isDeleted = await _learningTrackService.RemoveFromLearningTrackAsync(Id);

                //checks for operation failure
                if (!isDeleted.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, isDeleted);
                }
                else
                {
                    //when delete operation is successfull
                    return new GenericResponse<LearningTrack>
                    {
                        Data = isDeleted.Data,
                        Message = isDeleted.Message,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<LearningTrack>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        //All Learning Track by course category Get request
        [HttpGet]
        public async Task<ActionResult<GenericResponse<IEnumerable<LearningTrack>>>> GetAllLearningTrackByCourseCategory(int CourseCategoryId)
        {
            try
            {
                //Getting all learning track by course category using the service
                var learningTrack = await _learningTrackService.GetAllLearningTrackByCourseCategoryAsync(CourseCategoryId);

                //checks for operation failure
                if (learningTrack.Success == false)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, learningTrack);
                }
                else
                {
                    //when operation is successful
                    return new GenericResponse<IEnumerable<LearningTrack>>
                    {
                        Data = learningTrack.Data,
                        Message = learningTrack.Message,
                        Success = true
                    };
                }

            }
            catch (Exception e)
            {
                //returning any catched error
                return new GenericResponse<IEnumerable<LearningTrack>>
                {
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

    }
}