var courses = document.querySelectorAll(".Courses > li");

for (let i = 0; i < courses.length; i++) {
    courses[i].addEventListener("click", async function (e) {
        let ul = e.target.appendChild(document.createElement("ul"));

        let groups = await getGroupsOfCourse(e.target.id);
        groups.forEach(group => {
            let li = document.createElement("li");
            li.innerText = group.name;

            ul.appendChild(li);
        });
    });
}

async function getGroupsOfCourse(courseId) {
    const response = await fetch(`home/course/${courseId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const groups = await response.json();
        return groups;
    }
}