using System;

namespace Notes.Domain
{
    public class Note
    {
        // Nts
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}