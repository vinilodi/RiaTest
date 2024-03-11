using System.Collections.ObjectModel;

namespace RiaTest.Application.DTOs
{
    public class ResultDTO
    {
        public int? Count { get; set; }
        public bool Success { get; set; }
        public Collection<string> Errors { get; set; }

        public ResultDTO()
        {
            Success = true;
            Errors = new Collection<string>();
        }
    }
}
