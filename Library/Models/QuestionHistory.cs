using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class QuestionHistory
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? MainId { get; set; }

    public DateTime? UpdateDt { get; set; }

    public virtual Account? Account { get; set; }

    public virtual MainQuestion? Main { get; set; }
}
