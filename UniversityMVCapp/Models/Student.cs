using System;
using System.Collections.Generic;

namespace UniversityMVCapp;

public partial class Student
{
    public int StudentId { get; set; }

    public int GroupId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual Group Group { get; set; } = null!;
}
