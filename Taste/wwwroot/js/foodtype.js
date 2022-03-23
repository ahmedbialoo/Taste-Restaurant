
var table;
$(document).ready(function () {
     table = $('#DT_load').DataTable({
        ajax: {
            url: "/api/foodtype",
            method: "GET",
            dataType: 'json'
        },
        columns: [
            {
                data: 'name',
                width:'40%'
            },
            {
                data: 'id',
                render: (data) => {
                    return `<div class='text-center'>
                                <a href="/Admin/FoodType/Upsert?id=${data}" class="btn btn-success mx-2">
                                    <i class="far fa-edit"></i> Edit
                                </a>
                                <a class="btn btn-warning js-delete mx-2" data-foodtype-id=${data}>
                                    <i class="far fa-trash-alt"></i> Delete
                                </a>
                            </div>`;
                },
                width: '30%'
            }
        ],
        language: {
            emptyTable : 'No data was found!'
        },
        width:'100%'
    });
});
$('#DT_load').on('click', '.js-delete', function () {
    var btn = $(this);  
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                method: "DELETE",
                url: "/api/foodtype/" + btn.attr("data-foodtype-id"),
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        table.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });

});
