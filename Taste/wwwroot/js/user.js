
var table;
$(document).ready(function () {
     table = $('#DT_load').DataTable({
        ajax: {
            url: "/api/user",
            method: "GET",
            dataType: 'json'
        },
        columns: [
            {
                data: 'fullName',
                width:'25%'
            },
            {
                data: 'email',
                width: '25%'
            },
            {
                data: 'phoneNumber',
                width: '25%'
            },
            {
                data: { id: 'id', lockoutEnd:'lockoutEnd'},
                render: (data) => {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today)         //=====> user is locked
                    {
                        return `<div class='text-center'>
                                    <a class="btn btn-danger mx-2 js-lockUnlock" data-user-id=${data.id}>
                                        <i class="fa-solid fa-lock-open"></i> Unlock
                                    </a>
                                </div>`
                    } else {                    //=====> user is locked
                        
                        return `<div class='text-center'>
                                    <a class="btn btn-success mx-2 js-lockUnlock" data-user-id=${data.id}>
                                        <i class="fa-solid fa-lock"></i> Lock
                                    </a>
                                </div>`
                    };
                },
                width: '25%'
            }
        ],
        language: {
            emptyTable : 'No data was found!'
        },
        width:'100%'
    });
});
$('#DT_load').on('click', '.js-lockUnlock', function () {
    var btn = $(this);
    $.ajax({
        method: "POST",
        url: "/api/user",
        contentType:"application/json",
        data: JSON.stringify(btn.attr("data-user-id")),
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                table.ajax.reload();
            } else {
                toastr.error(data.message);
            }
        }
    });
});