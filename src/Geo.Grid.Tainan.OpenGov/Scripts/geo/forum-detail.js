$(function () {
    $('body').addClass('detail');
    ShowPartial("/Forum/DiscussPartial/" + $('#ForumId').val(), "dvDiscuss");

    $('#btnLogin').click(function () {
        var url = '/Account/Login?ReturnUrl=' + location.pathname + location.search;
        location.href = url;
    });

    $('#btnPost').click(function () {
        var message = $('#Message');
        if (!message.val()) {
            alert('請先輸入討論內容！');
            return;
        }
        $('#imgLoading').show();
        DoAjaxPost('/Forum/AddMessage/' + $('#ForumId').val(), { message: message.val() }, function (result) {
            if (result.Success) {
                //$.notify("相關連結已移除!", "success");
                message.val('');
                ShowPartial("/Forum/DiscussPartial/" + $('#ForumId').val(), "dvDiscuss");
            } else {
                $.notify(result.Message, "error");
            }
            $('#imgLoading').hide();
        });
    });


    $(".actbox").fancybox({
        'modal': true
    });
});

function ActivityDetail(id) {
    var url = "/Activity/Detail/" + id;
    DoAjaxPost(url, {}, function (result) {
        $('#ActivityTitle').html(result.Subject);

        $('#actDate').html(result.DateString);
        $('#actTime').html(result.TimeString);
        $('#actLocation').html(result.Location);
        $('#actDescription').html(result.Description);
        var aps = '<ul>';
        $(result.Attachments).each(function (i, d) {
            aps += '<li style="margin:10px;"><a href="/resource/Document/' + d.Id + '">' + d.Name + '</a></li>';
        });
        aps += '</ul>';
        $('#dvAttachment').html(aps);
    });
}