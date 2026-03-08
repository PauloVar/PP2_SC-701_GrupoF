(() => {
    const Cliente = {
        tabla: null,

        init() {
            this.inicializarTabla();
            this.registrarEventos();
        },

        inicializarTabla() {
            this.tabla = $('#tblCliente').DataTable({
                ajax: {
                    url: '/Cliente/ObtenerClientes',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'apellidos' },
                    { data: 'identificacion' },
                    { data: 'email' },
                    { data: 'direccion' },
                    {
                        data: 'telefonos',
                        render: function (data) {
                            if (!data || data.length === 0) return '';
                            return data.map(t => `${t.numero} (${t.tipo})`).join(', ');
                        }
                    },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <button class="btn btn-sm btn-info btn-detalle" data-id="${row.id}">
                                    Detalle
                                </button>
                                <button class="btn btn-sm btn-primary btn-editar" data-id="${row.id}">
                                    Editar
                                </button>
                                <button class="btn btn-sm btn-danger btn-eliminar" data-id="${row.id}">
                                    Eliminar
                                </button>`;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        },

        registrarEventos() {
            $('#tblCliente').on('click', '.btn-editar', function () {
                const id = $(this).data('id');
                Cliente.cargarDatosClienteParaEditar(id);
            });

            $('#tblCliente').on('click', '.btn-detalle', function () {
                const id = $(this).data('id');
                Cliente.cargarDetalleCliente(id);
            });

            $('#tblCliente').on('click', '.btn-eliminar', function () {
                const id = $(this).data('id');
                Cliente.eliminarCliente(id);
            });

            $('#btnGuardarCliente').on('click', function () {
                Cliente.guardarCliente();
            });

            $('#btnEditarCliente').on('click', function () {
                Cliente.editarCliente();
            });

            $('#btnAgregarTelefono').on('click', function () {
                Cliente.agregarTelefono();
            });

            $('#tblTelefonos').on('click', '.btn-eliminar-tel', function () {
                const clienteId = $('#DetalleClienteId').val();
                const telId = $(this).data('id');
                Cliente.eliminarTelefono(clienteId, telId);
            });
        },

        guardarCliente() {
            let form = $('#formCrearCliente');

            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalCrearCliente').modal('hide');
                        form[0].reset();
                        Cliente.tabla.ajax.reload();

                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                }
            });
        },

        cargarDatosClienteParaEditar(id) {
            $.get(`/Cliente/ObtenerClientePorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    let data = result.data;

                    $('#ClienteId').val(data.id);
                    $('#Nombre').val(data.nombre);
                    $('#Apellidos').val(data.apellidos);
                    $('#Identificacion').val(data.identificacion);
                    $('#Email').val(data.email);
                    $('#Direccion').val(data.direccion);

                    $('#modalEditarCliente').modal('show');
                } else {
                    Swal.fire({ title: 'Error', text: result.mensaje, icon: 'warning' });
                }
            });
        },

        editarCliente() {
            let form = $('#formEditarCliente');

            if (!form.valid()) return;

            $.ajax({
                url: form.attr('action'),
                type: 'POST',
                data: form.serialize(),
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#modalEditarCliente').modal('hide');
                        form[0].reset();
                        Cliente.tabla.ajax.reload();

                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                }
            });
        },

        eliminarCliente(id) {
            Swal.fire({
                title: '¿Estás seguro?',
                text: "¡No podrás revertir esta operación!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Cliente/EliminarCliente',
                        type: 'POST',
                        data: { id: id },
                        success: function (response) {
                            if (response.esCorrecto) {
                                Cliente.tabla.ajax.reload();
                                Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                            } else {
                                Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                            }
                        }
                    });
                }
            });
        },

        cargarDetalleCliente(id) {
            $.get(`/Cliente/ObtenerClientePorId?id=${id}`, function (result) {
                if (result.esCorrecto) {
                    let c = result.data;

                    $('#DetalleClienteId').val(c.id);
                    $('#dNombre').text(c.nombre);
                    $('#dApellidos').text(c.apellidos);
                    $('#dIdentificacion').text(c.identificacion);
                    $('#dEmail').text(c.email);
                    $('#dDireccion').text(c.direccion);

                    Cliente.pintarTelefonos(c.telefonos);

                    $('#modalDetalleCliente').modal('show');
                } else {
                    Swal.fire({ title: 'Error', text: result.mensaje, icon: 'warning' });
                }
            });
        },

        pintarTelefonos(telefonos) {
            const tbody = $('#tblTelefonos tbody');
            tbody.empty();

            if (!telefonos || telefonos.length === 0) return;

            telefonos.forEach(t => {
                tbody.append(`
                    <tr>
                        <td>${t.id}</td>
                        <td>${t.numero}</td>
                        <td>${t.tipo}</td>
                        <td>
                            <button class="btn btn-sm btn-danger btn-eliminar-tel" data-id="${t.id}">
                                Eliminar
                            </button>
                        </td>
                    </tr>
                `);
            });
        },

        agregarTelefono() {
            const clienteId = $('#DetalleClienteId').val();
            if (!clienteId) return;

            const numero = $('#TelNumero').val();
            const tipo = $('#TelTipo').val();

            if (!numero || !tipo) return;

            $.ajax({
                url: '/Cliente/AgregarTelefono',
                type: 'POST',
                data: {
                    clienteId: clienteId,
                    telefono: { Numero: numero, Tipo: tipo }
                },
                success: function (response) {
                    if (response.esCorrecto) {
                        $('#TelNumero').val('');
                        $('#TelTipo').val('');

                        
                        Cliente.cargarDetalleCliente(clienteId);

                        
                        if (Cliente.tabla) {
                            Cliente.tabla.ajax.reload(null, false);
                        }

                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                },
                error: function (xhr) {
                    Swal.fire({ title: 'Error', text: 'Error en la solicitud (AgregarTelefono).', icon: 'error' });
                    console.error(xhr);
                }
            });
        },

        eliminarTelefono(clienteId, telefonoId) {
            $.ajax({
                url: '/Cliente/EliminarTelefono',
                type: 'POST',
                data: { clienteId: clienteId, telefonoId: telefonoId },
                success: function (response) {
                    if (response.esCorrecto) {
                        Cliente.cargarDetalleCliente(clienteId);

                        if (Cliente.tabla) {
                            Cliente.tabla.ajax.reload(null, false);
                        }

                        Swal.fire({ title: 'Éxito', text: response.mensaje, icon: 'success' });
                    } else {
                        Swal.fire({ title: 'Error', text: response.mensaje, icon: 'warning' });
                    }
                },
                error: function (xhr) {
                    Swal.fire({ title: 'Error', text: 'Error en la solicitud (EliminarTelefono).', icon: 'error' });
                    console.error(xhr);
                }
            });
        }
    };

    $(document).ready(() => Cliente.init());
})();
