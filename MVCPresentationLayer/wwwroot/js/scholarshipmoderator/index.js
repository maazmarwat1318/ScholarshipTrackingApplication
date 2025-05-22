let deleteConfirmationDialog = new bootstrap.Modal(document.getElementById('deleteConfirmationModal'), {
    keyboard: false 
});

let deleteForm = $("#delete-form");

console.log("Please refresh");

$(".card-delete-button").on("click", function () {
    $("#userDeleteButton").off('click');
    deleteConfirmationDialog.show();
    let userId = $(this).siblings(".user-id").text();
    $("#userDeleteButton").on("click", function () {
        deleteConfirmationDialog.hide()
        $('#deleteForm').find("input").val(userId)
        $('#deleteForm').find("button").get(0).click();
    })
})