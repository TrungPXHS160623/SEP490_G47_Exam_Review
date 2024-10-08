using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class CampusUserSubject
{
    public int Id { get; set; }

    public int? CampusId { get; set; }

    public int? SubjectId { get; set; }

    public int? UserId { get; set; }
	public bool IsLecturer { get; set; }
	public virtual Campus? Campus { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual User? User { get; set; }
}
