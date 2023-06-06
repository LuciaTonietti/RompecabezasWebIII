var connection = new signalR.HubConnectionBuilder().withUrl("/puzzleHub").build();

connection.on("RecibirGanador", function (ganador) {
    var div = document.getElementById('miPopup');
    var p = document.createElement('p');
    p.textContent = `El ganador es ${ganador}`;
    div.appendChild(p);
    div.style.display = 'block';
    console.log(`El ganador es ${ganador}`);
});

connection.on("ActualizarListaJugadores", function (jugadoresConectados) {
    verificarParticipantesConectados(jugadoresConectados);
    actualizarTablaJugadores(jugadoresConectados);    
});

connection.start().then(function () {
    connection.invoke("AgregarJugador", nombreUsuario, nroSala).catch(function (err) {
        return console.error(err.toString());
    });
})

const imagenes = crearArrayImagenes(cantPiezas);

const puzzle = document.getElementById('puzzle');
const piezas = document.getElementById('piezas');
const mensaje = document.getElementById('mensaje');
switch (cantPiezas) {
    case 9:
        puzzle.classList.add('puzzle-9-piezas');
        piezas.classList.add('piezas-9');
        break;
    case 16:
        puzzle.classList.add('puzzle-16-piezas');
        piezas.classList.add('piezas-16');
        break;
    case 25:
        puzzle.classList.add('puzzle-25-piezas');
        piezas.classList.add('piezas-25');
}

let terminado = imagenes.length;

while (imagenes.length) {
    const index = Math.floor(Math.random() * imagenes.length);
    const div = document.createElement('div');
    switch (cantPiezas) {
        case 9:
            div.className = 'pieza-9';
            break;
        case 16:
            div.className = 'pieza-16';
            break;
        case 25:
            div.className = 'pieza-25';
    }    
    div.id = imagenes[index];
    div.draggable = true;
    div.style.backgroundImage = `url("/imagenes/${cantPiezas}-piezas/${imagenes[index]}.jpg")`;
    piezas.appendChild(div);
    imagenes.splice(index, 1);
}

for (let i = 0; i < terminado; i++) {
    const div = document.createElement('div');
    switch (cantPiezas) {
        case 9:
            div.className = 'placeholder-puzzle-9-piezas';
            break;
        case 16:
            div.className = 'placeholder-puzzle-16-piezas';
            break;
        case 25:
            div.className = 'placeholder-puzzle-25-piezas';
    }
    div.dataset.id = i;
    puzzle.appendChild(div);
}


piezas.addEventListener('dragstart', e => {
    e.dataTransfer.setData('id', e.target.id);
});

puzzle.addEventListener('dragover', e => {
    e.preventDefault();
    e.target.classList.add('hover');
});

puzzle.addEventListener('dragleave', e => {
    e.target.classList.remove('hover');
});

puzzle.addEventListener('drop', e => {
    e.target.classList.remove('hover');

    const id = e.dataTransfer.getData('id');
    const numero = id.split('-')[1];
    if (e.target.dataset.id === numero) {
        e.target.appendChild(document.getElementById(id));

        connection.invoke("SumarPuntaje").catch(function (err) {
            return console.error(err.toString());
        });

        terminado--;
        if (terminado === 0) {
            document.body.classList.add('ganaste');
            connection.invoke("EnviarGanador", nombreUsuario).catch(function (err) {
                return console.error(err.toString());
            });
        }
    } else {
        connection.invoke("RestarPuntaje").catch(function (err) {
            return console.error(err.toString());
        });
    }
});

function crearArrayImagenes(numElementos) {
    const imagenes = [];
    for (let i = 0; i < numElementos; i++) {
        const imagen = `camino-${i}`;
        imagenes.push(imagen);
    }
    return imagenes;
}

function cerrarPopup() {
    var popup = document.getElementById("miPopup");
    popup.style.display = "none";
}

function verificarParticipantesConectados(jugadoresConectados, div) {
    var div = document.getElementById('cartelBienvenida');
    if (jugadoresConectados.length >= 2) {
        var valorDisplay = window.getComputedStyle(div).getPropertyValue("display");
        if (valorDisplay != 'none') {
            div.style.display = 'none';
        }
    }
}

function actualizarTablaJugadores(jugadoresConectados) {
    var tablaPuntaje = document.getElementById('tablaPuntaje');
    tablaPuntaje.innerHTML = "";
    var thead = document.createElement('thead');
    var tr = document.createElement('tr');
    var th = document.createElement('th');
    th.textContent = "Jugador";
    var th1 = document.createElement('th');
    th1.textContent = "Puntaje";
    tr.appendChild(th);
    tr.appendChild(th1);
    thead.appendChild(tr);
    tablaPuntaje.appendChild(thead);
    for (let i = 0; i < jugadoresConectados.length; i++) {
        var tr = document.createElement('tr');
        var td = document.createElement('td');
        var td2 = document.createElement('td');
        td.textContent = jugadoresConectados[i]['nickName'];
        td2.textContent = jugadoresConectados[i]['score'];
        tr.appendChild(td);
        tr.appendChild(td2);
        tablaPuntaje.appendChild(tr);
    }

    
}