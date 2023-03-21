using System;
using System.Collections.Generic;

namespace UniversityMVCapp.Models;

public class Student
{
    public int StudentId { get; set; }
    public int GroupId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public Group Group { get; set; } = null!;
}
