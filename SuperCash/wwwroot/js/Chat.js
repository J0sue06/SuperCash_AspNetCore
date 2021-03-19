

$(document).ready(() => {
    let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
    let ID = $('#ID').html(); 

    connection.on("Update", function (res) {
        setTimeout(() => {
            UpdateInfo();
        },2000)
        
    });

    //connection.on("ActualizarBalance", function (res) { 
    //    const { id, balance } = res;
    //    if (ID == id) {
    //        console.log(balance);
    //        $('#Balance').html(`${balance} TRX`);
    //    }
    //});

    //connection.on("Pagos", function (res) {        
    //    PagosRecientes();      
    //});

    connection.start().then(function () {
        //console.log(connection);
    }).catch(function (err) {
        return console.error(err.toString());
    });

    $("#deposit").click(function (e) {
        e.preventDefault();

        const _monto = $('#MontoDeposito').val();

        if (_monto <= 0 ) {
            console.log("Validacion");
            return;
        }

        const _valor = parseFloat(_monto);

        const feePay = 0.005;

        const sub = _valor * feePay;
        const fee = _valor + sub;
        const realFee = RedondearValor(fee);
        console.log(realFee);

        Swal.fire({
            title: 'Confirmacion de Deposito',
            html: `<h2>Monto ${_monto} BTC </h2>
                   <h2>Procesador de pago</h2> (0.5%)
                   <hr/>
                   <h3>Total</h3>${realFee} BTC
                    `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Si, hazlo!'            
        }).then((result) => {
            if (result.isConfirmed) {

                const obj = {
                    monto: _valor
                };

                $.ajax({
                    url: '/Transacciones/Deposito',
                    method: 'POST',
                    data: { model: obj, Moneda: "TRX" },
                    success: (res) => {
                        const { status, redirectUrl } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Depositado correctamente!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                            $('#modalDepositos').modal('hide');
                            setTimeout(() => {
                                document.location.href = redirectUrl;
                            },1500)
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
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Nivel actualizado!',
                                showConfirmButton: false,
                                timer: 1500
                            });                            
                        }
                        if (status == 400) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'No cuentas con balance suficiente!'
                            });
                        }
                    },
                    error: (err) => {
                        console.error(err);
                    }
                });

            }
        });

    });

    $("#UpgradeTeam").click(function (e) {
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
                    url: '/Transacciones/SubirNivelEquipos',
                    method: 'POST',
                    success: (res) => {
                        const { status } = res;
                        if (status == 200) {
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Nivel actualizado!',
                                showConfirmButton: false,
                                timer: 1500
                            });
                        }
                        if (status == 400) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'No cuentas con balance suficiente!'
                            });
                        }
                    },
                    error: (err) => {
                        console.error(err);
                    }
                });

            }
        });

    });

    const UpdateInfo = () => {
        $.ajax({
            url: '/Home/UpdateInfo',
            method: 'GET',
            success: (res) => {
                const { infoUser, balance, payments, directos } = res;
                console.log(res);
                const { costoDirecto, costoEquipo, nivelDirecto, nivelEquipo, rango } = infoUser;
                $('#Balance').html(`${balance} TRX`);
                $('#Rank').html(rango);
                $('#DirectLevel').html(`LEVEL ${nivelDirecto}`);
                $('#PayDirect').html(`${costoDirecto} TRX`);
                $('#TeamLevel').html(`LEVEL ${nivelEquipo}`);
                $('#PayTeam').html(`${costoEquipo} TRX`);
                $('#affiliateDirect').html(directos);
               
                let pagos = '<tr>';    
                $.each(payments, function (key, value) {
                    const { montoTrx, tipoPago, idUsuario, fecha } = value;                        
                        pagos += `
                        <th>${montoTrx} TRX</th>
                        <td>${tipoPago}</td>
                        <td class="text-right">${idUsuario}</td>
                        <td class="text-right">${fecha}</td>`;
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


    const RedondearValor = (x, posiciones = 8) => {
        const s = x.toString()
        const l = s.length
        const decimalLength = s.indexOf('.') + 1
        const numStr = s.substr(0, decimalLength + posiciones)
        return Number(numStr)
    }
   

});



