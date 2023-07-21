function validation(){

    let textEmail = document.getElementById('textEmail').value;
    let textPass = document.getElementById('textPass').value;
    document.addEventListener("DataIsInvalid", DataIsInvalid);

    if(textPass.length == 0 || textEmail.length == 0){

        sendError("Заполните данные!")
        return;
    }

    mp.trigger("CEF:CLIENT::ON_AUTH_BUTTON_CLICK", textEmail, textPass);
}

DataIsInvalid = () =>
{
    sendError("Были введены неверные данные!");
    return;
}

function sendError(text){
    let boxAlert = document.getElementById('boxAlert');
    boxAlert.style.visibility = 'visible'

    boxAlert.innerHTML = text;

    setTimeout(() => boxAlert.style.visibility = 'hidden', 4000);
}

document.querySelector("#buttonAuth").onclick = function()
{
    validation()
}

