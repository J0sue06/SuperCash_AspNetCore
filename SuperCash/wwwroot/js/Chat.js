

$(document).ready(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    

    connection.on("Deposito", function (res) {
        $('#totalGanancias').html(`${res} TX`);
        //console.log(res);
    });

    connection.start().then(function () {
        console.log("Socket Conectado");
    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#deposit").click(function (e) {
        e.preventDefault();

        const _monto = $('#MontoDeposito').val();

        if (_monto == '') {
            return;
        }

        Swal.fire({
            title: '¿Estas seguro?',
            text: `Realmente quieres hacer un deposito por ${_monto} BTC`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, hazlo!'            
        }).then((result) => {
            if (result.isConfirmed) {

                const obj = {
                    monto: _monto
                };

                $.ajax({
                    url: '/Transacciones/Deposito',
                    method: 'POST',
                    data: { model: obj },
                    success: (res) => {
                        console.log(res);
                    },
                    error: (err) => {
                        console.error(err);
                    }
                });

            }
        });

    });

    $("#buyLincense").click(function (e) {
        e.preventDefault();
        Valor++;

        //$.ajax({
        //    url: '/Transacciones/Deposito',
        //    method: 'POST',
        //    data: { monto: '' },
        //    success: (res) => {
        //        console.log(res);
        //    },
        //    error: (err) => {
        //        console.error(err);
        //    }
        //});


    });



});



