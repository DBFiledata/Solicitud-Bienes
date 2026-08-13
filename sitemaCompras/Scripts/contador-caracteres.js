// Contador de caracteres para los campos de texto con tope.
//
// No conoce ningun numero: el limite lo declara el propio campo con su atributo maxlength, y este
// script solo lo lee. Asi el tope vive en un unico lugar (el marcado, que el servidor revalida) y
// el contador nunca queda desfasado si cambia.
//
// Se aplica solo a los textarea con maxlength, se dibuja solo (no hace falta agregar marcado en
// cada vista) y se rearma despues de cada postback parcial del UpdatePanel, que reemplaza el DOM
// de la seccion entera -- incluido el propio contador.
(function () {
    'use strict';

    var CLASE_CONTADOR = 'js-contador-caracteres';
    var UMBRAL_AVISO = 0.9; // desde el 90% del tope se avisa, antes de llegar al limite

    // El navegador cuenta los saltos de linea como UN caracter para el maxlength, aunque despues
    // los envie como "\r\n". Se cuenta igual aca para que el numero mostrado coincida siempre con
    // lo que el campo deja escribir (y con lo que valida el servidor, que normaliza igual).
    function largo(textarea) {
        return textarea.value.replace(/\r\n/g, '\n').length;
    }

    function contadorDe(textarea) {
        var siguiente = textarea.nextElementSibling;
        if (siguiente && siguiente.classList.contains(CLASE_CONTADOR)) return siguiente;
        var el = document.createElement('small');
        el.className = 'form-text text-right ' + CLASE_CONTADOR;
        textarea.parentNode.insertBefore(el, textarea.nextSibling);
        return el;
    }

    function aplica(textarea) {
        // En las vistas de solo lectura el campo llega deshabilitado: ahi el contador es ruido.
        return textarea.hasAttribute('maxlength') && !textarea.disabled && !textarea.readOnly;
    }

    function refrescar(textarea) {
        var tope = parseInt(textarea.getAttribute('maxlength'), 10);
        if (!tope) return;

        var usados = largo(textarea);
        var enElLimite = usados >= tope;
        var cerca = usados >= tope * UMBRAL_AVISO;

        var el = contadorDe(textarea);
        // Este archivo esta guardado en UTF-8 y los acentos van literales: IIS sirve los .js
        // estaticos sin declarar charset, pero un script clasico hereda entonces la codificacion
        // del documento, y las dos plantillas maestras declaran <meta charset="utf-8">.
        el.textContent = usados + ' / ' + tope + ' caracteres'
            + (enElLimite ? ' — límite alcanzado' : '');

        el.classList.toggle('text-danger', enElLimite);
        el.classList.toggle('font-weight-bold', enElLimite);
        el.classList.toggle('text-warning', cerca && !enElLimite);
        el.classList.toggle('text-muted', !cerca);
        textarea.classList.toggle('border-danger', enElLimite);
    }

    function refrescarTodos() {
        var campos = document.querySelectorAll('textarea[maxlength]');
        for (var i = 0; i < campos.length; i++)
            if (aplica(campos[i])) refrescar(campos[i]);
    }

    // Un unico listener delegado en el documento: atarlo al textarea se perderia en cuanto el
    // UpdatePanel lo vuelva a renderizar.
    document.addEventListener('input', function (e) {
        var t = e.target;
        if (t && t.tagName === 'TEXTAREA' && aplica(t)) refrescar(t);
    });

    function inicializar() {
        refrescarTodos();
        // Sys lo define el ScriptManager, que se renderiza dentro del <form>: recien existe cuando
        // el documento termino de cargar, por eso la suscripcion va aca y no al cargar el script.
        if (window.Sys && Sys.WebForms && Sys.WebForms.PageRequestManager)
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(refrescarTodos);
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', inicializar);
    else inicializar();
})();
