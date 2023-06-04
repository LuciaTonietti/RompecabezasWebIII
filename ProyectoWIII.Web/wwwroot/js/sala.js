var cantidadPiezas = 9;
var connection = new signalR.HubConnectionBuilder().withUrl("/puzzleHub").build();

connection.on("RecibirGanador", function () {
    console.log("Hay un ganador.");
});

connection.start().then(function () {
    console.log("Conectado.");
})

const imagenes = crearArrayImagenes(cantidadPiezas);

const puzzle = document.getElementById('puzzle');
const piezas = document.getElementById('piezas');
const mensaje = document.getElementById('mensaje');
switch (cantidadPiezas) {
    case 9:
        puzzle.classList.add('puzzle-9-piezas');
        piezas.classList.add('piezas-9');
        break;
    case 25:
        puzzle.classList.add('puzzle-25-piezas');
        piezas.classList.add('piezas-25');
}

let terminado = imagenes.length;

while (imagenes.length) {
    const index = Math.floor(Math.random() * imagenes.length);
    const div = document.createElement('div');
    switch (cantidadPiezas) {
        case 9:
            div.className = 'pieza-9';
            break;
        case 25:
            div.className = 'pieza-25';
    }    
    div.id = imagenes[index];
    div.draggable = true;
    div.style.backgroundImage = `url("/imagenes/${cantidadPiezas}-piezas/${imagenes[index]}.jpg")`;
    piezas.appendChild(div);
    imagenes.splice(index, 1);
}

for (let i = 0; i < terminado; i++) {
    const div = document.createElement('div');
    switch (cantidadPiezas) {
        case 9:
            div.className = 'placeholder-puzzle-9-piezas';
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
    console.log("id: ", id);
    const numero = id.split('-')[1];
    console.log("e.target.dataset.id", e.target.dataset.id);
    if (e.target.dataset.id === numero) {
        e.target.appendChild(document.getElementById(id));

        terminado--;

        if (terminado === 0) {
            document.body.classList.add('ganaste');
            connection.invoke("EnviarGanador").catch(function (err) {
                return console.error(err.toString());
            });
        }
    }
});

function crearArrayImagenes(numElementos) {
    const imagenes = [];
    for (let i = 0; i < numElementos; i++) {
        const imagen = `paisaje-${i}`;
        imagenes.push(imagen);
    }
    return imagenes;
}