function isNotMax(e){
    e = e || window.event;
    var target = e.target || e.srcElement;
    var code=e.keyCode?e.keyCode:(e.which?e.which:e.charCode)

    switch (code){
        case 13:
        case 8:
        case 9:
        case 46:
        case 37:
        case 38:
        case 39:
        case 40:
            return true;
    }
    return target.value.length <= target.getAttribute('maxlength');
}