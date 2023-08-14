function ShowConfirmation() {
    return mp.events.add;
}

function RefreshInfo() {
    return mp.events.add;
}

function HideConfirmation() {
    return mp.events.add;
}


RefreshInfo()('CLIENT:CEF::DRAW_UI_HOUSE', (_class, _id, _cost, _owner, _status) =>  {
    let houseid = document.getElementById('houseid');
    let houseType = document.getElementById('house_type');
    let houseCost = document.getElementById('house_cost');
    let houseOwner = document.getElementById('house_owner');
    let houseState = document.getElementById('house_state');
    let confirmation_text = document.getElementById('confirm_text');

    houseid.textContent = _id;
    houseType.textContent = _class;
    houseCost.textContent = _cost;
    houseOwner.textContent = _owner;
    houseState.textContent = _status;

})

ShowConfirmation()('CLIENT:CEF::SHOW_CONFIRMATION', (_cost, _type) =>  {
    let params = document.getElementById('params');
    let confirmation = document.getElementById('confirmation');
    let confirmation_text = document.getElementById('confirm_text');
    let house_type = document.getElementById('text_house_type');
    let cost = document.getElementById('text_cost');

    params.style.visibility = 'hidden';
    confirmation_text.style.visibility = 'hidden';

    house_type.style.textContent = _type;
    cost.style.textContent = _cost;
})

HideConfirmation()('CLIENT:CEF::HIDE_CONFIRMATION', () =>  {
    let conrimationMenu = document.getElementById('confirmation');
    let contentMenu = document.getElementById('confirm_text');
    let paramsMenu = document.getElementById('params');

    conrimationMenu.style.visibility = 'hidden';
    contentMenu.style.visibility = 'visible';
    paramsMenu.style.visibility = 'visible';
})

document.querySelector("#btn_confirm").onclick = function()
{
    mp.trigger("CEF:CLIENT::ON_CONFIRM_BUTTON_CLICK");
}

document.querySelector("#btn_cancel").onclick = function()
{
    mp.trigger("CEF:CLIENT::ON_CANCEL_BUTTON_CLICK");
}

