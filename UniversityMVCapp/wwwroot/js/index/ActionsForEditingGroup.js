

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