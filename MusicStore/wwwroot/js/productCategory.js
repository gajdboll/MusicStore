 
$(document).ready(function () {
   loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').dataTable({
        ajax: {
            url: '/productCategory/getall',
            dataSrc: 'data.$values'
},
        columns: [
            { data: 'WebHeader.name', "width": "15%"},
            { data: 'Name', "width": "20%"},
            { data: 'Descriptions', "width":"25%"},
            { data: 'isActive', "width": "10%" } ,
            { data: 'Actions', "width": "20%" } 
        ]
    });
}