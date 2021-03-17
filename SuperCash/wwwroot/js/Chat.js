﻿

$(document).ready(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    let ID = $('#ID').html(); 

    connection.on("ActualizarBalance", function (res) { 
        const { id, balance } = res;
        if (ID == id) {
            console.log(balance);
            $('#Balance').html(`${balance} TRX`);
        }
    });

    connection.on("Pagos", function (res) {        
        PagosRecientes();      
    });

    connection.start().then(function () {
        //console.log(connection);
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
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Depositado correctamente!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            $('#modalDepositos').modal('hide');
                        }
                       // console.log(res);
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

        Swal.fire({
            title: '¿Estas seguro?',
            text: `Realmente quieres comprar las licencias`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, hazlo!'
        }).then((result) => {
            if (result.isConfirmed) {                

                $.ajax({
                    url: '/Transacciones/ComprarLicencia',
                    method: 'POST',
                    success: (res) => {
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Licencia comprada!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            setTimeout(() => { document.location.reload() }, 1500)
                        }
                        if (status == 400) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'No cuentas con balance suficiente!'
                            })
                        }
                        
                    },
                    error: (err) => {
                        console.error(err);
                    }
                });

            }
        });

    }); 

    $("#UpgradeDirect").click(function (e) {
        e.preventDefault();

        Swal.fire({
            title: '¿Estas seguro?',
            text: `Realmente quieres subir de nivel`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, hazlo!'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({
                    url: '/Transacciones/SubirNivelDirecto',
                    method: 'POST',
                    success: (res) => {
                        console.log(res);
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Nivel actualizado!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            //setTimeout(() => { document.location.reload() }, 1500)
                        }
                        if (status == 400) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'No cuentas con balance suficiente!'
                            })
                        }

                    },
                    error: (err) => {
                        console.error(err);
                    }
                });

            }
        });

    });

    const PagosRecientes = () => {
        $.ajax({
            url: '/Transacciones/PagosRecientes',
            method: 'POST',
            success: (res) => {
                let pagos = '<tr>';
                console.log(res);
                $.each(res, function (key, value) {
                    const { clientID, trx, pay, fecha } = value;
                 
                        console.log(value);
                        pagos += `
                        <td>${trx}</td>
                        <td>${pay}</td>
                        <td>${clientID}</td>
                        <td>${fecha}</td>
                        `;

                        pagos += '</tr>';                  

                });
                if (pagos.length > 4) {
                    $('#PayList').html(pagos);
                }

            },
            error: (err) => {
                console.error(err);
            }
        });
    }
   

});



