


let chatBox = document.querySelector(".chat-box");
let toggleButton = document.querySelector(".chat-header button")
let chatContent = document.querySelector(".chat-content");
toggleButton.addEventListener('click', () => {
    if (chatContent.style.maxHeight) {
        chatContent.style.maxHeight = null;
        chatContent.classList.remove('active');
        toggleButton.innerText = "Show"
        $('#smgs').text("");
    } else {
        chatContent.style.maxHeight = 500 + "px";
        chatContent.classList.add('active');
        toggleButton.innerText = "Hide"
    }
})
// Outside click
window.addEventListener('click', function (e) {
    if (!chatBox.contains(e.target)) {
        chatContent.style.maxHeight = null;
        chatContent.classList.remove('active');
        toggleButton.innerText = "Show";

    }
});
(() => {
    chatContent.style.maxHeight = 500 + "px";
    chatContent.classList.add('active');
    toggleButton.innerText = "Hide"
})();