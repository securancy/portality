$(document).ready(function () {
    // On document load, create the editor window 
    // and add it to the body
    $("<div />")
        .attr("id", "EditWindow")
        .attr("title", "Portality™")
        .appendTo($("body"))
        .append("<textarea id='PortalityContentBox' />")
        .dialog({
            autoOpen: false,
            minHeight: 350,
            minWidth: 500,
            width: 500,
            height: 350,
            modal: true,
            buttons: {
                "Close": function () {
                    $(this).dialog("close");
                },
                "Save": function () {
                    var url_send = '/portalityeditor/content/save';
                    $.post(url_send, { contentId: $("#PortalityContentBox").attr("passthroughId"), value: $("#txtcontent").val() }, function (data) {
                        // todo: (optionally) reload page on succesful save *or* reload content for that <div> block
                    })
                }
            }
        });

    // Append the showDialog function to each div with class "editable"
    $(".editable").dblclick(function () {
        var guid = $(this).attr("id");
        showDialog(guid);
    })
});



function showDialog(id) {
    var url_get = '/portalityeditor/content/load';
    var data = $.post(url_get, { contentId: id }, function (data) {

        // set the contents of the dialog
        $("#PortalityContentBox").attr("passthroughId", id).val(data);

        $("#EditWindow")
        .dialog('open');    // open the dialog
        return false;

    });


}