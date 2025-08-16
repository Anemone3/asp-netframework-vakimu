function obtenerCarrito() {
    const carrito = localStorage.getItem("carrito");
    return carrito ? JSON.parse(carrito) : [];
}

function guardarCarrito(carrito) {
    localStorage.setItem("carrito", JSON.stringify(carrito));
    actualizarContadorCarrito();
}

function agregarAlCarrito(id, nombre, precio, imagen, cantidad = 1) {
    let carrito = obtenerCarrito();

    const index = carrito.findIndex(p => p.productoId === id);
    if (index >= 0) {
        carrito[index].cantidad += cantidad;
    } else {
        carrito.push({
            productoId: id,
            nombre: nombre,
            precio: precio,
            cantidad: cantidad,
            imagen: imagen
        });
    }

    guardarCarrito(carrito);
    alert(`${nombre} x${cantidad} agregado(s) al carrito 🛒`);
}

function cargarCarrito() {
    const carrito = obtenerCarrito();
    const container = document.getElementById('cart-items');
    if (!container) return;

    container.innerHTML = '';

    if (carrito.length === 0) {
        container.innerHTML = '<div class="empty-cart"><p>Tu carrito está vacío</p></div>';
        const subtotalElem = document.getElementById('subtotal');
        if (subtotalElem) subtotalElem.textContent = 'S/ 0.00';
        return;
    }

    carrito.forEach(item => {
        const itemElement = document.createElement('div');
        itemElement.className = 'cart-item';

        const precio = typeof item.precio === "number" ? item.precio.toFixed(2) : '0.00';

        itemElement.innerHTML = `
            <img src="${item.imagen}" alt="${item.nombre}" class="item-image">
            <div class="item-details">
                <div class="item-name">${item.nombre}</div>
                <div class="quantity-controls py-3">
                    <button class="quantity-btn" onclick="cambiarCantidad(${item.productoId}, -1)">−</button>
                    <span class="quantity-display">${item.cantidad}</span>
                    <button class="quantity-btn" onclick="cambiarCantidad(${item.productoId}, 1)">+</button>
                </div>
                <button class="remove-btn" onclick="eliminarItem(${item.productoId})">Eliminar</button>
            </div>
             <div class="d-flex flex-row gap-3 ">
                        <span class="item-price" style="color: slategray">S/ ${item.precio.toFixed(2)}</span>
                        <span class="item-price">S/ ${(item.precio * item.cantidad).toFixed((2))}</span>
             </div>
        `;
        container.appendChild(itemElement);
    });

    actualizarSubtotal();
}

function cambiarCantidad(id, delta) {
    const carrito = obtenerCarrito();
    const index = carrito.findIndex(p => p.productoId === id);
    if (index !== -1) {
        carrito[index].cantidad += delta;
        if (carrito[index].cantidad <= 0) {
            carrito.splice(index, 1);
        }
        guardarCarrito(carrito);
        cargarCarrito();
    }
}

function eliminarItem(id) {
    let carrito = obtenerCarrito();
    carrito = carrito.filter(p => p.productoId !== id);
    guardarCarrito(carrito);
    cargarCarrito();
}

function actualizarSubtotal() {
    const carrito = obtenerCarrito();
    const subtotal = carrito.reduce((sum, item) => sum + (item.precio * item.cantidad || 0), 0);
    const subtotalElement = document.getElementById('subtotal');
    if (subtotalElement) {
        subtotalElement.textContent = `S/ ${subtotal.toFixed(2)}`;
    }
}

function actualizarContadorCarrito() {
    const carrito = obtenerCarrito();
    const totalItems = carrito.reduce((sum, item) => sum + item.cantidad, 0);
    const contador = document.getElementById("cart-count");
    if (contador) {
        contador.textContent = totalItems;
    }
}

// Controlar cantidad desde la vista
function incrementarCantidad(id, stockMaximo) {
    const span = document.getElementById(`cantidad-${id}`);
    let cantidad = parseInt(span.textContent);

    if (cantidad < stockMaximo) {
        span.textContent = cantidad + 1;
    }
}

function decrementarCantidad(id) {
    const span = document.getElementById(`cantidad-${id}`);
    let cantidad = parseInt(span.textContent);

    if (cantidad > 1) {
        span.textContent = cantidad - 1;
    }
}

// Agregar producto desde vista con cantidad elegida
function agregarDesdeVista(id, nombre, precio, imagen) {
    const cantidad = parseInt(document.getElementById(`cantidad-${id}`).textContent);
    let carrito = obtenerCarrito();

    const index = carrito.findIndex(p => p.productoId === id);
    if (index >= 0) {
        carrito[index].cantidad += cantidad;
    } else {
        carrito.push({
            productoId: id,
            nombre: nombre,
            precio: precio,
            cantidad: cantidad,
            imagen: imagen
        });
    }

    guardarCarrito(carrito);
    alert(`${cantidad} unidad(es) de "${nombre}" agregadas al carrito 🛒`);
    actualizarContadorCarrito();
}

document.addEventListener("DOMContentLoaded", () => {
    actualizarContadorCarrito();
    if (document.getElementById("cart-items")) {
        cargarCarrito();
    }
});