
$(document).ready(function () {
    $('.pageSelect').select2({
        allowClear: true,
        width: '100%',
    });
});

function tableToExcel() {
    var table2excel = new Table2Excel();
    table2excel.export(document.querySelectorAll("table.table"));
}