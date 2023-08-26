//Система домов

window.current = {
    selected_tenant_item: -1,
    max_tenant_item: 0,
    ul: null
};

function InitTenants() { return mp.events.add; }

document.querySelector("#house_icon").onclick = function() {
    HideAllItems();
    ShowHouseFuncs();
}

document.querySelector("#show_all_button").onclick = function() {
    HideAllItems();
    ShowMainMenu();
}

document.querySelector("#tenants_button").onclick = function() {
    HideAllItems();
    ShowTenantsContent();
    current.ul = document.getElementById("tenant_container");
    current.selected_tenant_item = -1;

    RemoveTenantsFromList();
    
    mp.trigger("CEF:CLIENT::ON_INIT_TENANTS_LIST");
}

document.querySelector("#button_tenant_delete").onclick = function() {
    if(current.selected_tenant_item != -1) {
        document.getElementById(current.selected_tenant_item).remove();
        mp.trigger("CEF:CLIENT::ON_CLICK_BUTTON_DELETE_TENANT", String(current.selected_tenant_item));
    }
}

document.querySelector('#tenant_container').onclick = function(e) {
    if (e.target.classList.value === "item") {
        setDefaultColorForTenantItems();
        current.selected_tenant_item = e.target.id;
        e.target.style.backgroundColor = "#808080";
        console.log(String(current.selected_tenant_item));
    }
}

function setDefaultColorForTenantItems() {
    var colorDivs = document.getElementsByClassName("item");
    for (var i = 0; i < colorDivs.length; i++) {
        colorDivs[i].style.backgroundColor = "#21382d";
    }
}

function HideAllItems() {
    let house = document.getElementById('house_icon');
    let house_content = document.getElementById('house_content');
    let tenants_content = document.getElementById('tenants-content');
    let tenants_button = document.getElementById('tenants_button');

    tenants_button.style.visibility = 'hidden';
    house.style.visibility = 'hidden';
    house_content.style.visibility = 'hidden';
    tenants_content.style.visibility = 'hidden';
}

function ShowHouseFuncs() {
    let house_content = document.getElementById('house_content');
    let tenants_button = document.getElementById('tenants_button');
    house_content.style.visibility = 'visible';
    tenants_button.style.visibility = 'visible';
}

function ShowMainMenu() {
    HideAllItems();
    let house = document.getElementById('house_icon');
    house.style.visibility = 'visible';
}

function ShowTenantsContent() {
    let house_content = document.getElementById('house_content');
    let tenants_content = document.getElementById('tenants-content');
    let tenants_button = document.getElementById('tenants_button');

    house_content.style.visibility = 'visible';
    tenants_content.style.visibility = 'visible';
    tenants_button.style.visibility = 'hidden';
}

function RemoveTenantsFromList() {
    while( current.ul.firstChild ){
        current.ul.removeChild( current.ul.firstChild );
    }
}

InitTenants()('CLIENT:CEF::INIT_TENANT_LIST', (_info_str) =>  {
    let li = document.createElement("div");
    li.className = 'item';

    current.selected_tenant_item++;
    li.id = current.selected_tenant_item;

    li.appendChild(document.createTextNode(_info_str));
    current.ul.appendChild(li);
})
