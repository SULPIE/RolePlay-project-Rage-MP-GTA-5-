window.current = {
    current_answer: -1,
    current_question: -1,
    max_qustions: -1,

    isUserPressedF: false,

    answers: 3,

    header_text: document.getElementById('header_text'),
    qustion_text: document.getElementById('question'),
    answer_first: document.getElementById('first_answer'),
    answer_second: document.getElementById('second_answer'),
    answer_third: document.getElementById('third_answer'),

    confirmation_box: document.getElementById('confirmation'),
    desc_box: document.getElementById('description'),
    content_box: document.getElementById('content')
};

document.querySelector("#button_next").onclick = function() {
    if(current_answer != -1){
        mp.trigger("CEF:CLIENT::ON_NEXTBUTTON_CLICK");
    }
}

function LoadNextQuestionFromClient() { return mp.events.add; }
LoadNextQuestionFromClient()('CLIENT:CEF::LOAD_QUESTION', (_max_question, _question, _answer1, _answer2, _answer3, _current_question) =>  {
    current.current_answer = -1;
    current.current_question++;
    current.header_text = "Вопрос " + _current_question + "/" + _max_question;
    current.qustion_text = _question;
    current.answer_first = _answer1;
    current.answer_second = _answer2;
    current.answer_third = _answer3;
})

for ( let i = 0; i < current.answers; i++ ) {
    document.getElementById(String(i)).onclick = function() {
        if(i == current.current_answer) { return; }

        SetColorDefaultToAnswers()

        var item = document.getElementById(String(this.id));
        item.style.backgroundColor = "#876900";
        current.current_answer = i;
        console.log(String(current.current_answer));
    }
}

function SetColorDefaultToAnswers() {
    for(let idx = 0; idx < current.answers; idx++) {
        var item = document.getElementById(String(idx));
        item.style.backgroundColor = "#006400";
    }
}

addEventListener("keydown", moveRect);
function moveRect(e){
    if(e.key == "f" && !current.isUserPressedF) {
        current.content_box.style.visibility = 'hidden';
        current.content_box.style.visibility = 'hidden';
        current.confirmation_box.style.visibility = 'visible';
        current.isUserPressedF = true;
    }
}

document.querySelector("#btn_cancel").onclick = function() {
    mp.trigger("CEF:CLIENT::ON_PREVBUTTON_CLICK");
}

document.querySelector("#btn_cancel").onclick = function() {
    current.content_box.style.visibility = 'visible';
    current.content_box.style.visibility = 'visible';
    current.confirmation_box.style.visibility = 'hidden';
    current.isUserPressedF = false;
}