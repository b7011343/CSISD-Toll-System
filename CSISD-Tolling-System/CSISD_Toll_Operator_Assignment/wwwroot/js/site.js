$(document).ready(function () {
    $('#data-table').DataTable();
});

const adjustFontSizes = (multiplier = 1) => {
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
