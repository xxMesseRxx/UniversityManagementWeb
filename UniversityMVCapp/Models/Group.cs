using System;
using System.Collections.Generic;

namespace UniversityMVCapp.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
