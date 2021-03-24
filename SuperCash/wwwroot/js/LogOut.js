$(document).ready(() => {

    $('#logOut').click((e) => {
        e.preventDefault();

        Swal.fire({
            title: 'Are you sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, do it!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: '/Login/Salir',
                    method: 'POST',
                    success: (res) => {
                        const { status } = res;

                        if (status == 200) {
                            const { url } = res;
                            document.location.href = url;
                        }
                    },
                    error: (err) => {
                        console.error(err.responseText);
                    }
                });

            }
        })

     


    });

});