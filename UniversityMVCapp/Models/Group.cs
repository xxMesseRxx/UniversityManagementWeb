using System;
using System.Collections.Generic;

namespace UniversityMVCapp.Models;

public class Group
{
    public int GroupId { get; set; }
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
    public Course Course { get; set; } = null!;
    public List<Student> Students { get; set; } = new List<Student>();
}
