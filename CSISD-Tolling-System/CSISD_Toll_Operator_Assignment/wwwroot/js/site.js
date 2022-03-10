$(document).ready(function () {
    $('#data-table').DataTable();
});

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

$(document).ready(function () {
    $('li.active').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('li').addClass('active');
    $('.nav-link').removeClass('active');
    $('a[href="' + location.pathname + '"]').closest('.nav-link').addClass('active');
});

$("#delContract").click(function () {
    $("#delContract").prop("disabled", true);
    $("#delTxt").removeClass("hidden");
});

$("#newContract").click(function () {
    $("#newContract").prop("disabled", true);
    $("#newTxt").removeClass("hidden");
});