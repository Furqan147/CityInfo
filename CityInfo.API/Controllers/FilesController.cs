using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO.Abstractions;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        #region Private Fields

        private readonly IHttpContextAccessor _httpContext;
        private readonly IFile _file;
        private readonly IPath _path;
        private readonly FileExtensionContentTypeProvider _fileExtensionProvidor;
        private readonly ILogger<FilesController> _logger;

        #endregion

        #region Constructor

        public FilesController(FileExtensionContentTypeProvider fileExtensionProvidor, IHttpContextAccessor httpContext, ILogger<FilesController> logger, IFile file, IPath path)
        {
            _fileExtensionProvidor = fileExtensionProvidor ?? throw new ArgumentNullException(nameof(fileExtensionProvidor));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _file = file ?? throw new ArgumentNullException(nameof(file));
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        #endregion

        #region Public Action Methods

        /// <summary>
        /// Get file by file id.
        /// </summary>
        /// <param name="fileId">File id</param>
        /// <returns>File bytes along with content type and file name</returns>
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            try
            {
                _logger.LogDebug($"Fetching file bytes for [FileId] : [{fileId}], for request : [{_httpContext.HttpContext?.TraceIdentifier}]");

                var filePath = "SampleFileForAPI.txt";

                if (!_file.Exists(filePath))
                {
                    return NotFound();
                }

                if (_fileExtensionProvidor.TryGetContentType(filePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var fileBytes = _file.ReadAllBytes(filePath);

                return File(fileBytes, contentType, _path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                _logger.LogError($"An exception occurred while fetching file with [FileId] : [{fileId}], for [Request] : [{_httpContext.HttpContext?.TraceIdentifier}], [Message] : [{ex.Message}]");
                throw;
            }
        }

        #endregion

    }
}
