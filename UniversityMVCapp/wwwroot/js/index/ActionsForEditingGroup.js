var editLinks = document.getElementsByClassName("editLink");
var removeLink = document.getElementsByClassName("removeLink");

for (let i = 0; i < editLinks.length; i++) {
    editLinks[i].addEventListener("click", async e => await addGroupToForm(e));
};

async function addGroupToForm(e) {
    e.preventDefault();
    let tableRow = e.target.parentNode.parentNode;
    let form = document.forms.editGroup;

    form.groupId.value = tableRow.children[0].innerText;
    form.name.value = tableRow.children[1].innerText;
    form.courseId.value = tableRow.children[2].getAttribute("data-courseId");
}



//var editGroupForm = document.forms["editGroup"];

//document.getElementById("saveBtn").addEventListener("click", async () => await createGroup(editGroupForm.elements.name.value, editGroupForm.elements.courseId.value));

//async function createGroup(groupName, courseId) {
//    const response = await fetch("/home/editGroup", {
//        method: "POST",
//        headers: { "Accept": "application/json", "Content-Type": "application/json" },
//        body: JSON.stringify({
//            name: groupName,
//            courseId: parseInt(courseId, 10)
//        })
//    });
//    if (response.ok === true) {
//        const group = await response.json();
//    }
//}