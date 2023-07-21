function validation(){
    let textEmail = document.getElementById('textEmail').value;
    let textPass = document.getElementById('textPass').value;
    let textPassReturn = document.getElementById('textPassReturn').value;
    document.addEventListener("EmailIsExist", EmailIsExist);

    var regCorrectEmail = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

    if(textPass.length == 0 || textEmail.length == 0){

        sendError("Нельзя зарегистрировать аккаунт с пустыми данными!");
        return;
    }

    if(regCorrectEmail.test(textEmail) == false){
        sendError("Введите корректный Email!");
        return;
    }

    if(textPass.length < 6 || textPass.length > 18){
        sendError("Пароль не должен быть меньше 6 или больше 18 символов!");
        return;
    }

    if(textPass != textPassReturn){
        sendError("Введённые пароли не совпадают!");
        return;
    }

    mp.trigger("CEF:CLIENT:ON_BUTTON_REG_CLICK", textEmail, textPass);
}

EmailIsExist = () =>{
    sendError("Такой Email уже существует");
    return;
}

function sendError(text){
    let boxAlert = document.getElementById('boxAlert');
    boxAlert.style.visibility = 'visible'

    boxAlert.innerHTML = text;

    setTimeout(() => boxAlert.style.visibility = 'hidden', 4000);
}

document.querySelector("#buttonAuth").onclick = function(){
    validation()
}