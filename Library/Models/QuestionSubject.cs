using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class QuestionSubject
{
    public string SubjectId { get; set; } = null!;

    public string? SubjectName { get; set; }

    public string? Time { get; set; }

    public virtual ICollection<MainQuestion> MainQuestions { get; set; } = new List<MainQuestion>();
}
