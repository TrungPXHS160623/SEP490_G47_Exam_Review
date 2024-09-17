using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class MainQuestion
{
    public int MainId { get; set; }

    public string? MainContent { get; set; }

    public string? SubjectId { get; set; }

    public string? Images { get; set; }

    public int? QuestionType { get; set; }

    public virtual ICollection<QuestionHistory> QuestionHistories { get; set; } = new List<QuestionHistory>();

    public virtual ICollection<SubQuestion> SubQuestions { get; set; } = new List<SubQuestion>();

    public virtual QuestionSubject? Subject { get; set; }
}
