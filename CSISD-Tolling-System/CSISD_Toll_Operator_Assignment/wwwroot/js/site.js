$(document).ready(function () {
    $('#data-table').DataTable();
});
//this function is used to adjust the size of the text on the screen - this is to help people with poor eye sight
const adjustFontSizes = (multiplier) => {
    console.log(`Adjusting magnification to ${multiplier*100}%`)
    const el = document.querySelector('body');
    let n, a = [], walk = document.createTreeWalker(el, NodeFilter.SHOW_TEXT, null, false);
    while (n = walk.nextNode()) a.push(n);
    for (const x of a) {
        const htmlEl = x.parentElement;
        const style = window.getComputedStyle(htmlEl, null).getPropertyValue('font-size');
        const fontSize = parseFloat(style);
        htmlEl.style.fontSize = (fontSize * multiplier) + 'px';
    }
};
//this function highlights which of the li elements in the ul element is currently active - this helps users know which page they are on
$(document).ready(function () {
    $('li.active').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('li').addClass('active');
    $('.nav-link').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('.nav-link').addClass('active');
});
//this is an on click function which disables the button with the id "delContract" and remove the class "hidden" from the p element with the id "delTxt"
$("#delContract").click(function () {
    $("#delContract").prop("disabled", true);
    $("#delTxt").removeClass("hidden");
});
//this is an on click function which disables the button with the id "newContract" and remove the class "hidden" from the p element with the id "newTxt"
$("#newContract").click(function () {
    $("#newContract").prop("disabled", true);
    $("#newTxt").removeClass("hidden");
});