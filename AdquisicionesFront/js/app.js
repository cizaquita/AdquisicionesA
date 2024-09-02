const apiUrl = "https://localhost:7062/adquisiciones";

document.addEventListener("DOMContentLoaded", () => {
    obtenerAdquisiciones();

    document.getElementById("btnNuevaAdquisicion").addEventListener("click", mostrarModal);
    document.getElementById("valorUnitario").addEventListener("change", calcularTotal);
    document.getElementById("cantidad").addEventListener("change", calcularTotal);
    document.getElementById("formAdquisicion").addEventListener("submit", guardarAdquisicion);
    document.querySelector(".close").addEventListener("click", cerrarModal);

    document.getElementById('campoBusqueda').addEventListener('keyup', function() {
        let input = document.getElementById('campoBusqueda').value.toLowerCase();
        let rows = document.querySelectorAll('#adquisicionesBody tr');

        rows.forEach(row => {
            let cells = row.getElementsByTagName('td');
            let match = false;

            for (let i = 0; i < cells.length; i++) {
                if (cells[i].textContent.toLowerCase().includes(input)) {
                    match = true;
                    break;
                }
            }

            row.style.display = match ? '' : 'none';
        });
    });
});
function obtenerAdquisiciones() {
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => {
            const tbody = document.getElementById("adquisicionesBody");
            tbody.innerHTML = "";
            data.forEach(adquisicion => {
                const row = document.createElement("tr");
                row.innerHTML = `
                    <td>${adquisicion.id}</td>
                    <td>${adquisicion.presupuesto}</td>
                    <td>${adquisicion.unidad}</td>
                    <td>${adquisicion.tipo}</td>
                    <td>${adquisicion.cantidad}</td>
                    <td>${adquisicion.valorUnitario}</td>
                    <td>${adquisicion.valorTotal}</td>
                    <td>${adquisicion.fecha}</td>
                    <td>${adquisicion.proveedor}</td>
                    <td>${adquisicion.documentacion}</td>
                    <td>
                        <button onclick="editarAdquisicion(${adquisicion.id})">Editar</button>
                        <button onclick="eliminarAdquisicion(${adquisicion.id})">Eliminar</button>
                        <button onclick="logAdquisicion(${adquisicion.id})">Log</button>
                    </td>
                `;
                tbody.appendChild(row);
            });
            if (!data.length){
                const row = document.createElement("tr");
                row.innerHTML = "<td colspan='11' class='text-align: center;'>No se encontraron registros.</td>";
                tbody.appendChild(row);
            }
        })
        .catch(error => console.error("Error al obtener las adquisiciones:", error));
}

function calcularTotal(){
    let cantidad = parseInt(document.getElementById("cantidad").value);
    let valorUnitario = parseFloat(document.getElementById("valorUnitario").value);
    
    if (!isNaN(cantidad) && cantidad > 0 && !isNaN(valorUnitario) && valorUnitario > 0) {
        
        let valorTotal = cantidad * valorUnitario;
        document.getElementById("valorTotal").value = valorTotal.toFixed(2);
    } else {
        document.getElementById("valorTotal").value = '';
    }
}
function mostrarModal() {
    document.getElementById("formAdquisicion").reset();
    document.getElementById("adquisicionId").value = "";
    document.getElementById("modalAdquisicion").style.display = "block";
    document.getElementById("modalTitle").innerText = "Nueva Adquisición";
}

function cerrarModal() {
    document.getElementById("modalAdquisicion").style.display = "none";
    document.getElementById("modalAdquisicionLog").style.display = "none";
}

function guardarAdquisicion(event) {
    event.preventDefault();

    const id = document.getElementById("adquisicionId").value;
    const adquisicion = {
        presupuesto: document.getElementById("presupuesto").value,
        unidad: document.getElementById("unidad").value,
        tipo: document.getElementById("tipo").value,
        cantidad: parseInt(document.getElementById("cantidad").value),
        valorUnitario: parseFloat(document.getElementById("valorUnitario").value),
        fecha: document.getElementById("fecha").value,
        proveedor: document.getElementById("proveedor").value,
        documentacion: document.getElementById("documentacion").value,
    };

    if (id) {
        actualizarAdquisicion(id, adquisicion);
    } else {
        crearAdquisicion(adquisicion);
    }

    cerrarModal();
}

async function crearAdquisicion(adquisicion) {
    try {
        const response = await fetch(apiUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(adquisicion)
        });

        if (!response.ok) {
            throw new Error('Error al crear la adquisición: ' + response.statusText);
        }

        const result = await response.json();
        console.log('Adquisición creada:', result);
        
        obtenerAdquisiciones();
    } catch (error) {
        console.error('Error al crear la adquisición:', error);
    }
}

function editarAdquisicion(id) {
    fetch(`${apiUrl}/${id}`)
        .then(response => response.json())
        .then(adquisicion => {
            document.getElementById("adquisicionId").value = adquisicion.id;
            document.getElementById("presupuesto").value = adquisicion.presupuesto;
            document.getElementById("unidad").value = adquisicion.unidad;
            document.getElementById("tipo").value = adquisicion.tipo;
            document.getElementById("cantidad").value = adquisicion.cantidad;
            document.getElementById("valorUnitario").value = adquisicion.valorUnitario;
            document.getElementById("valorTotal").value = adquisicion.valorTotal;
            const fecha = new Date(adquisicion.fecha);
            const fechaFormateada = fecha.toISOString().split('T')[0];
            document.getElementById('fecha').value = fechaFormateada;
            document.getElementById("proveedor").value = adquisicion.proveedor;
            document.getElementById("documentacion").value = adquisicion.documentacion;
            
            document.getElementById("modalTitle").innerText = "Editar Adquisición";
            document.getElementById("modalAdquisicion").style.display = "block";
        })
        .catch(error => console.error("Error al obtener la adquisición:", error));
}

function logAdquisicion(id) {
    fetch(`${apiUrl}Log/${id}`)
        .then(response => response.json())
        .then(data => {
            const content = document.getElementById("contenidoLog");
            content.innerHTML = "";
            data.forEach(adquisicionLog => {
                const ul = document.createElement("ul");
                ul.innerHTML = `
                    <li><b>${adquisicionLog.usuario}:</b> ${adquisicionLog.tipoCambio} ${adquisicionLog.detallesCambio} <br/>${adquisicionLog.fechaCambio}</li>
                `;
                content.appendChild(ul);
            });
            document.getElementById("modalAdquisicionLog").style.display = "block";
        })
        .catch(error => console.error("Error al obtener la adquisición:", error));
}

function actualizarAdquisicion(id, adquisicion) {
    fetch(`${apiUrl}/${id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(adquisicion)
    })
    .then(() => obtenerAdquisiciones())
    .catch(error => console.error("Error al actualizar la adquisición:", error));
}

function eliminarAdquisicion(id) {
    if (confirm("¿Está seguro de que desea eliminar esta adquisición?")) {
        fetch(`${apiUrl}/${id}`, {
            method: "DELETE"
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Error en la respuesta de la red');
            }
            return response.json();
        })
        .then(data => {
            alert("Adquisición eliminada exitosamente.");
            obtenerAdquisiciones();
        })
        .catch(error => {
            console.error("Error al eliminar la adquisición:", error);
            alert("No se pudo eliminar la adquisición. Intente nuevamente.");
        });
    } else {
        console.log("Eliminación cancelada.");
    }
}

