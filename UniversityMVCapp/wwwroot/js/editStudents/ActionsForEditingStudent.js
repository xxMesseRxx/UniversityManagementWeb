var editLinks = document.getElementsByClassName("editLink");
var removeLink = document.getElementsByClassName("removeLink");

for (let i = 0; i < editLinks.length; i++) {
    editLinks[i].addEventListener("click", async e => await addStudentToForm(e));
    removeLink[i].addEventListener("click", async e => await removeStudent(e.target.getAttribute("data-studentId")));
};

document.getElementById("resetBtn").addEventListener("click", () => {
    document.getElementById("saveBtn").innerText = "Добавить";
});

async function addStudentToForm(e) {
    e.preventDefault();
    const tableRow = e.target.parentNode.parentNode;
    const form = document.forms.editStudent;

    form.studentId.value = tableRow.children[0].innerText;
    form.firstName.value = tableRow.children[1].innerText;
    form.lastName.value = tableRow.children[2].innerText;
    form.groupId.value = tableRow.children[3].getAttribute("data-groupId");

    document.getElementById("saveBtn").innerText = "Сохранить";
}

async function removeStudent(studentId) {
    const response = await fetch(`/students/editStudents?studentId=${studentId}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const student = await response.json();
        document.querySelector(`tr[data-studentId='${student.studentId}']`).remove();
    }
    else {
        const error = await response.json();
        alert(error.message);
    }
}