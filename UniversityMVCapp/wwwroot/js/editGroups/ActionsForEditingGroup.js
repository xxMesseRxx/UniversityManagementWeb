var editLinks = document.getElementsByClassName("editLink");
var removeLink = document.getElementsByClassName("removeLink");

for (let i = 0; i < editLinks.length; i++) {
    editLinks[i].addEventListener("click", async e => await addGroupToForm(e));
    removeLink[i].addEventListener("click", async e => await removeGroup(e.target.getAttribute("data-groupId")));
};

document.getElementById("resetBtn").addEventListener("click", () => {
    document.getElementById("saveBtn").innerText = "Добавить";
});

async function addGroupToForm(e) {
    e.preventDefault();
    const tableRow = e.target.parentNode.parentNode;
    const form = document.forms.editGroup;

    form.groupId.value = tableRow.children[0].innerText;
    form.name.value = tableRow.children[1].innerText;
    form.courseId.value = tableRow.children[2].getAttribute("data-courseId");

    document.getElementById("saveBtn").innerText = "Сохранить";
}

async function removeGroup(groupId) {
    const response = await fetch(`/groups/editGroups?groupId=${groupId}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const group = await response.json();
        document.querySelector(`tr[data-groupId='${group.groupId}']`).remove();
    }
    else {
        const error = await response.json();
        alert(error.message);
    }
}