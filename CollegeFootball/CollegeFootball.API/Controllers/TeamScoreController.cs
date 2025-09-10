using Microsoft.AspNetCore.Mvc;

namespace CollegeFootball.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TeamScoreController : ControllerBase
    {
        public TeamScoreController()
        {

        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadCsv(IFormFile file)
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
                await using (var stream = new FileStream(finalFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Return a success response.
                return Ok("Success");
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
    }
}
