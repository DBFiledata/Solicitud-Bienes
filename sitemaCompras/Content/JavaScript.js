

(function (global, undefined) {
    var demo = {};

    function openRadWin(url) {
        radopen(url, 'RadWindow1');
    }

    global.openRadWin = openRadWin;

})(window);