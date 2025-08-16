function cargarCarrito() {
    const carrito = JSON.parse(localStorage.getItem('carrito')) || [];
    const contenedor = document.getElementById('order-items');

    if (!contenedor) return;

    contenedor.innerHTML = '';

    let subtotal = 0;

    carrito.forEach(item => {
        const itemElement = document.createElement('div');
        itemElement.className = 'order-item';
        itemElement.innerHTML = `
            <div class="item-image">
                <img src="${item.imagen}" alt="${item.nombre}">
                <span class="item-quantity">${item.cantidad}</span>
            </div>
            <div class="item-details">
                <h4>${item.nombre}</h4>
                <div class="d-flex flex-row gap-3">
                    <span class="item-price" style="color: slategray">S/ ${item.precio.toFixed(2)}</span>
                    <span class="item-price">S/ ${(item.precio * item.cantidad).toFixed(2)}</span>
                </div>
            </div>
        `;
        contenedor.appendChild(itemElement);
        subtotal += item.precio * item.cantidad;
    });

    // Mostrar totales
    const costoEnvio = 10;
    const igv = subtotal * 0.18;
    const total = subtotal + costoEnvio;

    document.getElementById('subtotal').textContent = `S/ ${subtotal.toFixed(2)}`;
    document.getElementById('costoEnvio').textContent = `S/ ${costoEnvio.toFixed(2)}`;
    document.getElementById('total').innerHTML = `S/ ${total.toFixed(2)}`;
    document.getElementById('igv').textContent = igv.toFixed(2);
}

function setupDeliveryOptions() {
    const opciones = document.querySelectorAll('.delivery-option');
    const sucursalesSection = document.getElementById('sucursales-section');

    opciones.forEach(opcion => {
        opcion.addEventListener('click', () => {
            opciones.forEach(o => o.classList.remove('active'));
            opcion.classList.add('active');

            const tipo = opcion.dataset.type;
            sucursalesSection.style.display = tipo === 'retiro' ? 'block' : 'none';
        });
    });
}

function setupMetodoPago() {
    const radios = document.querySelectorAll('input[name="metodoPagoId"]');

    radios.forEach(radio => {
        radio.addEventListener('change', () => {
            document.querySelectorAll('.payment-method').forEach(m => {
                m.classList.remove('active');
                const detalles = m.querySelector('.payment-details');
                if (detalles) detalles.style.display = 'none';
            });

            const seleccionado = radio.closest('.payment-method');
            seleccionado.classList.add('active');
            const detallesSeleccionado = seleccionado.querySelector('.payment-details');
            if (detallesSeleccionado) detallesSeleccionado.style.display = 'block';
        });
    });
}

async function completarPedido() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

    // 1. Obtener datos del cliente
    const cliente = {
        nombre: document.getElementById("nombre").value,
        apellidos: document.getElementById("apellidos").value,
        correo: document.getElementById("correo").value,
        direccion: document.getElementById("direccion").value,
        descripcionCasa: document.getElementById("descripcionCasa").value,
        telefono: document.getElementById("telefono").value,
        dni: document.getElementById("dni").value,
        distrito: document.getElementById("distrito").value,
        departamentoId: parseInt(document.getElementById("departamento").value) || 1
    };

    // 2. Obtener datos de la orden
    const orden = {
        sucursalId: parseInt(document.querySelector("input[name='sucursalSeleccionada']:checked")?.value || 0),
        tipo: document.querySelector(".delivery-option.active")?.dataset.type || "delivery",
        metodoPagoId: parseInt(document.querySelector("input[name='metodoPagoId']:checked")?.value || 1),
        estado: "pendiente",
        ruc: document.getElementById("ruc").value
    };

    // 3. Dirección de entrega
    const direccion = {
        direccionLinea1: cliente.direccion,
        direccionLinea2: cliente.descripcionCasa,
        ciudad: "Lima",
        distrito: cliente.distrito
    };

    // 4. Obtener el carrito del localStorage y mapearlo
    const cartItems = JSON.parse(localStorage.getItem("carrito") || "[]");

    const detalles = cartItems.map(item => {
        const subtotal = item.precio * item.cantidad;
        return {
            productoId: item.productoId,
            descripcion: item.nombre,
            cantidad: item.cantidad,
            descuento: 0,
            subtotal: subtotal,
            total: subtotal
        };
    });
    
    console.log({ carritoBuy: detalles})
    
    // 5. Armar el JSON para el backend
    const data = {
        cliente,
        ordenDetalle: {
            orden,
            direccion,
            detalles:detalles
        }
    };

    // 6. Enviar por fetch
    try {
        const response = await fetch("/Checkout/OrdenarPedido", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                ...(token && { "RequestVerificationToken": token })
            },
            body: JSON.stringify(data)
        });
        
        const responseJson = await response.json();
        
        console.log({result: responseJson ?? 'nada'})
        if(!responseJson.success){
            const errorDiv = document.getElementById("error-pedido");
            if (errorDiv) {
                errorDiv.style.display = "block";
                errorDiv.textContent = responseJson.message || "Ocurrió un error al procesar el pedido.";
            }
        }else{

            localStorage.removeItem("carrito");
            window.location.href = "/"
        }

    } catch (err) {
        console.error(err);
    }
}



function obtenerSucursalSeleccionada() {
    const input = document.querySelector('input[name="sucursalSeleccionada"]:checked');
    return input ? input.value : null;
}

function aplicarDescuento() {
    const codigo = document.getElementById('codigoDescuento').value.trim();
    if (!codigo) {
        alert('Ingresa un código de descuento válido');
        return;
    }

    fetch(`/Descuento/Validar?codigo=${codigo}`)
        .then(res => res.json())
        .then(data => {
            if (!data.valido) {
                alert('Código inválido o expirado');
                return;
            }

            const descuento = data.descuento;
            const subtotalElement = document.getElementById('subtotal');
            const subtotal = parseFloat(subtotalElement.textContent.replace('S/ ', ''));
            const nuevoSubtotal = subtotal - (subtotal * descuento / 100);
            const igv = nuevoSubtotal * 0.18;
            const total = nuevoSubtotal + 10;

            subtotalElement.textContent = `S/ ${nuevoSubtotal.toFixed(2)}`;
            document.getElementById('total').innerHTML = `S/ ${total.toFixed(2)}`;
            document.getElementById('igv').textContent = igv.toFixed(2);

            alert(`Se aplicó un descuento del ${descuento}%`);
        })
        .catch(err => {
            console.error(err);
            alert('Error al validar el código');
        });
}

function setupSucursalSeleccion() {
    const radios = document.querySelectorAll('input[name="sucursalSeleccionada"]');
    
    radios.forEach(radio => {
        radio.addEventListener('change', () => {
            document.querySelectorAll('.sucursal-option').forEach(option => {
                option.classList.remove('selected');
            });

            const selectedOption = radio.closest('.sucursal-option');
            if (selectedOption) {
                selectedOption.classList.add('selected');
            }
        });
    });
}


function iniciarSesion() {
    window.location.href = '/';
}

document.addEventListener('DOMContentLoaded', () => {
    cargarCarrito();
    setupDeliveryOptions();
    setupMetodoPago();
    setupSucursalSeleccion();
});
