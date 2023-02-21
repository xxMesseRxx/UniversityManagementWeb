using System;
using System.Collections.Generic;

namespace UniversityMVCapp;

public partial class Course
{
    public int CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Group> Groups { get; } = new List<Group>();
}
