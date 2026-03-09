(() => {
    const Categoria = {
        tabla: null,

        init() {
            this.inicializarTabla();
        },

        inicializarTabla() {
            this.tabla = $('#tblCategoria').DataTable({
                ajax: {
                    url: '/Categoria/ObtenerCategorias',
                    type: 'GET',
                    dataSrc: 'data'
                },
                columns: [
                    { data: 'id' },
                    { data: 'nombre' },
                    { data: 'descripcion' },
                    {
                        data: null,
                        title: 'Acciones',
                        orderable: false,
                        render: function (data, type, row) {
                            return `
                                <a class="btn btn-sm btn-info" href="/Categoria/Details/${row.id}">Details</a>
                                <a class="btn btn-sm btn-primary" href="/Categoria/Edit/${row.id}">Editar</a>
                                <a class="btn btn-sm btn-danger" href="/Categoria/Delete/${row.id}">Eliminar</a>
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
        Categoria.init();
    });
})();