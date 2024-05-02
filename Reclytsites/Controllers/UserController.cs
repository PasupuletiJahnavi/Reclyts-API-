using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reclytsites.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Reclytsites;
using Reclytsites.Migrations;

namespace Reclytsites.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<MainController> _logger;

        public MainController(AppDbContext context )
        {
            _context = context;
           
        }
      

        

        [HttpPost("user/register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return BadRequest(ModelState);
            }

            string hashedPassword = HashPassword(model.Password);

            string username = GenerateUniqueUsername(model.Email);

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = username,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

           
            return Ok(new { Message = "User registration successful." });
        }

        [HttpPost("user/login")]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null || !VerifyPassword(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return BadRequest(ModelState);
            }

         
            return Ok(new { Message = "User login successful." });
        }

        [HttpGet("user")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

      

        [HttpGet("tutorial")]
        public ActionResult<IEnumerable<Tutorial>> GetTutorials()
        {
            var tutorials = _context.Tutorials.ToList();
            return Ok(tutorials);
        }

        [HttpGet("tutorial/{description}")]
        public ActionResult<Tutorial> GetTutorial(string description)
        {
            var tutorial = _context.Tutorials.FirstOrDefault(t => t.Description == description);
            if (tutorial == null)
            {
                return NotFound();
            }
            return Ok(tutorial);
        }

        [HttpPost("tutorial")]
        public IActionResult CreateTutorial(Tutorial model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            model.Id = 0; 

            _context.Tutorials.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateDescription([FromBody] string description)
        {
            var tutorial = await _context.Tutorials.FirstOrDefaultAsync();
            if (tutorial != null)
            {
                tutorial.Description = description;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    
    [HttpDelete("tutorial/{description}")]
        public IActionResult DeleteTutorial(string description)
        {
            var tutorial = _context.Tutorials.FirstOrDefault(t => t.Description == description);
            if (tutorial == null)
            {
                return NotFound();
            }

            _context.Tutorials.Remove(tutorial);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpGet("task")]
        public ActionResult<IEnumerable<TaskModel>> GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpGet("task/{id}")]
        public ActionResult<TaskModel> GetTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            return Ok(task);
        }

        [HttpPost("task")]
        public IActionResult CreateTask(TaskModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            

            _context.Tasks.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }


        [HttpPut("task/{id}")]
        public IActionResult UpdateTask(int id, TaskModel model)
        {
            var existingTask = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }

          

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("task/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("document")]
        public ActionResult<IEnumerable<Document>> GetDocuments()
        {
            return _context.Documents.ToList();
        }

        [HttpGet("GetDocument/{id}")]
        public ActionResult<Document> GetDocument(int id)
        {
            var document = _context.Documents.Find(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        [HttpPost("saveDocument")]
        public ActionResult<Document> PostDocument(Document document)
        {
            _context.Documents.Add(document);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }

        [HttpPut("updatDocs/{id}")]
        public IActionResult PutDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Document> DeleteDocument(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            _context.SaveChanges();

            return document;
        }

        [HttpGet("Download/{id}")]
        public IActionResult DownloadUserDocument(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            var filePath = "FilePath/" + document.FileName; 

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", document.FileName);
        }

        [HttpGet("View/{id}")]
        public IActionResult ViewDocument(int id)
        {
            var document = _context.Documents.Find(id);
            if (document == null)
            {
                return NotFound();
            }

            var filePath = "FilePath/" + document.FileName;

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf"); 
        }
        [HttpGet("updateinfo")]
        public ActionResult<IEnumerable<Reclytsites.Models.UpdateInfo>> GetUpdateInfos()
        {
            var updateInfos = _context.UpdateInfos.ToList();
            return Ok(updateInfos);
        }

        [HttpGet("updateinfo/{updateNumber}")]
        public ActionResult<Reclytsites.Models.UpdateInfo> GetUpdateInfo(int updateNumber)
        {
            var updateInfo = _context.UpdateInfos.FirstOrDefault(u => u.UpdateNumber == updateNumber);
            if (updateInfo == null)
            {
                return NotFound();
            }
            return Ok(updateInfo);
        }

        [HttpPost("updateinfo")]
        public IActionResult CreateUpdateInfo([FromBody] Reclytsites.Models.UpdateInfo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.UpdateInfos.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }

        [HttpPut("updateinfo/{updateNumber}")]
        public IActionResult UpdateUpdateInfo(int updateNumber, [FromBody] Reclytsites.Models.UpdateInfo model)
        {
            var existingUpdateInfo = _context.UpdateInfos.FirstOrDefault(u => u.UpdateNumber == updateNumber);
            if (existingUpdateInfo == null)
            {
                return NotFound();
            }

            existingUpdateInfo.Timestamp = model.Timestamp;
            existingUpdateInfo.SerialNumber = model.SerialNumber;
            existingUpdateInfo.ContactName = model.ContactName;
            existingUpdateInfo.Designation = model.Designation;
            existingUpdateInfo.PhoneNumber = model.PhoneNumber;
            existingUpdateInfo.Email = model.Email;
            existingUpdateInfo.ClientName = model.ClientName;
            existingUpdateInfo.Address = model.Address;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("updateinfo/{updateNumber}")]
        public IActionResult DeleteUpdateInfo(int updateNumber)
        {
            var updateInfo = _context.UpdateInfos.FirstOrDefault(u => u.UpdateNumber == updateNumber);
            if (updateInfo == null)
            {
                return NotFound();
            }

            _context.UpdateInfos.Remove(updateInfo);
            _context.SaveChanges();

            return NoContent();
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<Document>> GetDocuments()
        //{
        //    return _context.Documents.ToList();
        //}

        //// GET: api/Documents/5
        //[HttpGet("{id}")]
        //public ActionResult<Document> GetDocument(int id)
        //{
        //    var document = _context.Documents.Find(id);

        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    return document;
        //}

        //// POST: api/Documents
        //[HttpPost]
        //public ActionResult<Document> PostDocument(Document document)
        //{
        //    _context.Documents.Add(document);
        //    _context.SaveChanges();

        //    return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        //}

        //// PUT: api/Documents/5
        //[HttpPut("{id}")]
        //public IActionResult PutDocument(int id, Document document)
        //{
        //    if (id != document.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(document).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DocumentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: api/Documents/5
        //[HttpDelete("{id}")]
        //public ActionResult<Document> DeleteDocument(int id)
        //{
        //    var document = _context.Documents.Find(id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Documents.Remove(document);
        //    _context.SaveChanges();

        //    return document;
        //}

        //// GET: api/Documents/Download/5
        //[HttpGet("Download/{id}")]
        //public IActionResult DownloadDocument(int id)
        //{
        //    var document = _context.Documents.Find(id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    // Replace 'FilePath' with the path where your documents are stored
        //    var filePath = "FilePath/" + document.FileName; // Adjust as per your file storage structure

        //    // Check if file exists
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound();
        //    }

        //    // Return the file
        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "application/octet-stream", document.FileName);
        //}

        //// GET: api/Documents/View/5
        //[HttpGet("View/{id}")]
        //public IActionResult ViewDocument(int id)
        //{
        //    var document = _context.Documents.Find(id);
        //    if (document == null)
        //    {
        //        return NotFound();
        //    }

        //    // Replace 'FilePath' with the path where your documents are stored
        //    var filePath = "FilePath/" + document.FileName; // Adjust as per your file storage structure

        //    // Check if file exists
        //    if (!System.IO.File.Exists(filePath))
        //    {
        //        return NotFound();
        //    }

        //    // Return the file for viewing
        //    var fileBytes = System.IO.File.ReadAllBytes(filePath);
        //    return File(fileBytes, "application/pdf"); // Adjust the content type as per your document type
        //}

        private bool DocumentExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
        [HttpGet("client")]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            var clients = _context.Clients.ToList();
            return Ok(clients);
        }

        [HttpGet("client/{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Sno == id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost("client")]
        public IActionResult CreateClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Clients.Add(client);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetClient), new { id = client.Sno }, client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the client.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut("client/{id}")]
        public IActionResult UpdateClient(int id, Client client)
        {
            if (id != client.Sno)
            {
                return BadRequest("Client ID mismatch.");
            }

            var existingClient = _context.Clients.FirstOrDefault(c => c.Sno == id);
            if (existingClient == null)
            {
                return NotFound("Client not found.");
            }

            try
            {
                existingClient.ClientName = client.ClientName;
                existingClient.Address = client.Address;
                existingClient.Issue = client.Issue;
                existingClient.DateOfJoining = client.DateOfJoining;
                existingClient.AssignedTo = client.AssignedTo;
                existingClient.Status = client.Status;

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the client.");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("client/{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Sno == id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(c => c.Sno == id);
        }
        [HttpGet("comments")]
        public ActionResult<IEnumerable<Comment>> GetComments()
        {
            var comments = _context.Comments.ToList();
            return Ok(comments);
        }

        [HttpGet("comment/{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost("comment")]
        public IActionResult CreateComment(Comment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Comments.Add(model);
            _context.SaveChanges();

            return Ok(model);
        }

        [HttpPut("comment/{id}")]
        public IActionResult UpdateComment(int id, Comment model)
        {
            var existingComment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (existingComment == null)
            {
                return NotFound();
            }

            existingComment.Text = model.Text;
            existingComment.Instructions = model.Instructions;
            existingComment.Author = model.Author;
            existingComment.ProfileImage = model.ProfileImage;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("comment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return NoContent();
        }


        [HttpGet("project")]
        public ActionResult<IEnumerable<ProjectModel>> GetProjects()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        [HttpGet("project/{id}")]
        public ActionResult<ProjectModel> GetProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost("project")]
        public IActionResult CreateProject(ProjectModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        [HttpPut("project/{id}")]
        public IActionResult UpdateProject(int id, ProjectModel project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            var existingProject = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (existingProject == null)
            {
                return NotFound();
            }

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.Status = project.Status;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.AssignedTo = project.AssignedTo;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("project/{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return NoContent();
        }



        private string GenerateUniqueUsername(string email)
        {
            
            return email;
        }

        private string HashPassword(string password)
        {
            return password;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
           
            return password == hashedPassword;
        }
    }
}
