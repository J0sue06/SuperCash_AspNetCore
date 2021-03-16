

$(document).ready(()=>{

    $('#Login').click((e) => {
        e.preventDefault();

        const obj = {
            ID: $('#ID').val(),
            Pass: $('#Pass').val()
        };

        $.ajax({
            url: '/Login/Acceso',
            method: 'POST',
            data: { model: obj },
            success: (res) => {
                const { status } = res;
                if (status == 200) {
                    document.location.href = "/Home";
                }
                console.log(res);
            },
            error: (err) => {                
                $.each(err.responseJSON, (i, res) => {
                    if (res[0] != null) {
                        const { errorMessage } = res[0];
                        console.log(errorMessage);
                    }                    
                });
            }
        });       

    });

});