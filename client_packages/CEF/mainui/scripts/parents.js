drawUI()('CLIENT:CEF::DRAW_UI_DATA', (_address, _money) =>  {
    let address = document.getElementById('addresstext');
    let money = document.getElementById('shownmoney');

    if(_money == null || _address == null) {
        return;
    }

    money.textContent = _money + " $"
    address.textContent = _address
})
function drawUI() {
    return mp.events.add;
}

