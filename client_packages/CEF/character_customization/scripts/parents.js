window.current = {
    State: 0,
    MAX_SKIN: 13,
    Sex: 0
  };

document.querySelector("#buttonPrev").onclick = function() {
    current.Sex = 0;
    current.State = 0;
    console.log(String(current.Sex));
    mp.trigger("CEF:CLIENT::ON_CHANGE_SEX", current.Sex);
}

document.querySelector("#buttonNext").onclick = function() {
    current.Sex = 1;
    current.State = 0;
    console.log(String(current.Sex));
    mp.trigger("CEF:CLIENT::ON_CHANGE_SEX", current.Sex);
}

document.querySelector("#buttonChangePrev").onclick = function() {
    if(current.State > 0)
    {
        current.State--;
        console.log(String(current.State));
        mp.trigger("CEF:CLIENT::ON_CHANGE_SKIN", current.State);
    }
}

document.querySelector("#buttonChangeNext").onclick = function() {
    if(current.State < current.MAX_SKIN)
    {
        current.State++;
        console.log(String(current.State));
        mp.trigger("CEF:CLIENT::ON_CHANGE_SKIN", current.State);
    }
}

document.querySelector("#getnextbutton").onclick = function() {
    mp.trigger("CEF:CLIENT::ON_GETTING_NEXT");
}