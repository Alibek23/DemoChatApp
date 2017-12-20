var socket,
    $txt = document.getElementById('message'),
    $user = document.getElementById('user'),
    $messages = document.getElementById('messages');

if (typeof (WebSocket) !== 'undefined') {
    socket = new WebSocket("ws://localhost/ChatApp/ChatHandler.ashx");
} else {
    socket = new MozWebSocket("ws://localhost/ChatApp/ChatHandler.ashx");
}

socket.onmessage = function (msg) {
    var $el = document.createElement('p');
    $el.innerHTML = msg.data;
    var text = msg.data.replace(/\u0000/g, "");
    //$messages.appendChild($el);
    $messages.value += text;
    var room = document.getElementById('roomName').innerHTML;
    LogMessage(text, room);
};

socket.onclose = function (event) {
    alert('Can not connect. Please refresh the page');
};

document.getElementById('send').onclick = function () {
    socket.send(new Date().toTimeString() + $user.innerHTML + $txt.value + '\r\n');
    $txt.value = '';
};

function LogMessage(data, room) {
    $.ajax({
        url: "@Url.Action("WriteLog", "Home")",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ message: data, room: room }),
        success: function (response) {
            //response ? alert("It worked!") : alert("It didn't work.");
        }
    });
}

function ReadLog(room) {
    $.ajax({
        url: "@Url.Action("ReadLog", "Home")",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ room: room }),
        success: function (response) {
            if (response)
                $messages.value = response.data;
        }
    });
}