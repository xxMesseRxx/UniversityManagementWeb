var courses = document.querySelectorAll(".course");

for (let i = 0; i < courses.length; i++) {
    courses[i].addEventListener("click", async e => await сhooseVisibilityAction(e));
};

async function сhooseVisibilityAction(e) {
    if (e.target.children.length === 0) {
        await setChildren(e.target);
    }
    else {
        e.target.querySelector("ul").classList.toggle("hidden");
    }

    if (e.target.classList.contains("group") || e.target.classList.contains("course")) {
        e.target.classList.toggle("opened");
    }
}

async function setChildren(element) {
    if (element.classList.contains("course")) {
        await setChildrenOfCourse(element);
    }
    else if (element.classList.contains("group")) {
        await setChildrenOfGroup(element);
    }
}

async function setChildrenOfCourse(course) {
    let ul = course.appendChild(document.createElement("ul"));

    let groups = await getGroupsOfCourse(course.getAttribute("data-courseId"));
    groups.forEach(group => {
        let li = document.createElement("li");
        li.innerText = group.name;
        li.classList.add("group");
        li.setAttribute("data-groupId", group.groupId);

        ul.appendChild(li);
    });
}

async function getGroupsOfCourse(courseId) {
    const response = await fetch(`/home/groups?courseId=${courseId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const groups = await response.json();
        return groups;
    }
}

async function setChildrenOfGroup(group) {
    let ul = group.appendChild(document.createElement("ul"));

    let students = await getStudentsOfGroup(group.getAttribute("data-groupId"));
    students.forEach(student => {
        let li = document.createElement("li");
        li.innerText = student.firstName + " " + student.lastName;

        ul.appendChild(li);
    });
}

async function getStudentsOfGroup(groupId) {
    const response = await fetch(`/home/students?groupId=${groupId}`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const students = await response.json();
        return students;
    }
}