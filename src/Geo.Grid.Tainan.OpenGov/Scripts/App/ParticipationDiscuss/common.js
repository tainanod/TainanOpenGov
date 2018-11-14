String.prototype.toDate =function () {
    var input = this.valueOf();
    var dt = new Date(input);
    if (dt == 'Invalid Date')
        return input + ' Error Format';

    return dt.toLocaleDateString('roc', { year: 'numeric', month: '2-digit', day: '2-digit' });
};



String.prototype.replaceNtoBR = function () {
    var input = this.valueOf();
    return input.replace('\\n', '<br>');
}

String.prototype.getEmptyString = function () {
    var input = this.valueOf();
    return input == undefined ? "" : input;
}