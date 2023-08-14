window.current = {
    carname: 'kuruma',
    cost: '10000'
  };

RefreshInfo()('CLIENT:CEF::DRAW_UI_AUTOROOM', (_carname, _cost) =>  {
    let carname = document.getElementById('carname');
    let cost = document.getElementById('cost');

    cost.textContent = _cost + "$"
    carname.textContent = _carname

    current.carname = _carname;
    current.cost = _cost;
})
function RefreshInfo() {
    return mp.events.add;
}

ShowConfirmation()('CLIENT:CEF::SHOW_CONFIRMATION', () =>  {
    let conrimationMenu = document.getElementById('confirmation');
    let contentMenu = document.getElementById('content');
    let paramsMenu = document.getElementById('params');

    let carname = document.getElementById('text_carname');
    let cost = document.getElementById('text_cost');

    conrimationMenu.style.visibility = 'visible';
    contentMenu.style.visibility = 'hidden';
    paramsMenu.style.visibility = 'hidden';

    carname.textContent = current.carname;
    cost.textContent = current.cost;
})
function ShowConfirmation() {
    return mp.events.add;
}

HideConfirmation()('CLIENT:CEF::HIDE_CONFIRMATION', () =>  {
    let conrimationMenu = document.getElementById('confirmation');
    let contentMenu = document.getElementById('content');
    let paramsMenu = document.getElementById('params');

    conrimationMenu.style.visibility = 'hidden';
    contentMenu.style.visibility = 'visible';
    paramsMenu.style.visibility = 'visible';
})
function HideConfirmation() 
{
    return mp.events.add;
}

document.querySelector("#btn_confirm").onclick = function()
{
    mp.trigger("CEF:CLIENT::ON_CONFIRM_BUTTON_CLICK");
}

document.querySelector("#btn_cancel").onclick = function()
{
    mp.trigger("CEF:CLIENT::ON_CANCEL_BUTTON_CLICK");
}

