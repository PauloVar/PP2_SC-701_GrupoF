(() => {
    const Producto = {
        tabla: null,

        init() {
            this.inicializarTabla();
        },

        inicializarTabla() {
            this.tabla = $('#tblProducto').DataTable({
                ajax: {
                    url: '/Producto/ObtenerProductos',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    { data: 'precio' },
                    { data: 'stock' },
                    { data: 'categoriaNombre' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <a class="btn btn-sm btn-info" href="/Producto/Details/${row.id}">Details</a>
                                <a class="btn btn-sm btn-primary" href="/Producto/Edit/${row.id}">Editar</a>
                                <a class="btn btn-sm btn-danger" href="/Producto/Delete/${row.id}">Eliminar</a>
                            `;
                        }
                    }
                ],
                language: {
                    url: 'https://cdn.datatables.net/plug-ins/1.13.6/i18n/es-ES.json'
                }
            });
        }
    };

    document.addEventListener('DOMContentLoaded', () => {
        Producto.init();
    });
})();