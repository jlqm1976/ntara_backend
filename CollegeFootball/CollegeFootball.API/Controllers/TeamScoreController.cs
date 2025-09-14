using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CollegeFootball.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamScoreController : ControllerBase
    {
        private readonly ITeamScoreService tsService;

        public TeamScoreController(ITeamScoreService _tsservice)
        {
            tsService = _tsservice;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UploadCsv(IFormFile file)
        {
            // Validate the file.
            if (file == null || file.Length == 0)
            {
                return BadRequest("File not provided or is empty.");
            }

            // Validate the file extension to ensure it's a CSV.
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(fileExtension) || fileExtension != ".csv")
            {
                return BadRequest("Invalid file type. Only CSV files are allowed.");
            }

            // Define the path where the file will be saved.
            var tempFilePath = Path.GetTempFileName();
            var finalFilePath = Path.ChangeExtension(tempFilePath, ".csv");

            // Save the file to the server's temporary location.
            try
            {
                using (var stream = new FileStream(finalFilePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                tsService.ImportRecordsFromCsv(finalFilePath);

                // Return a success response.
                return Ok("Success");
            }
            catch (Domain.Exceptions.NoRecordsInCsvException)
            {
                return BadRequest("The uploaded CSV file contains no records.");
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging.
                return StatusCode(500, $"An error occurred during the file upload: {ex.Message}");
            }
            finally
            {
                // Optionally, clean up the temporary file after processing.
                if (System.IO.File.Exists(finalFilePath))
                {
                    System.IO.File.Delete(finalFilePath);
                }
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamScore>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var result = tsService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred with the request: {ex.Message}");
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamScore>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Search([FromQuery]string searchValue, [FromQuery]IEnumerable<string> columns)
        {
            try
            {
                if (string.IsNullOrEmpty(searchValue))
                {
                    return BadRequest("Search value cannot be null. To get all records, use the GetAll endpoint.");
                }
                else
                {
                    var result = tsService.Search(searchValue, columns);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred with the resquest: {ex.Message}");
            }
        }
    }
}
