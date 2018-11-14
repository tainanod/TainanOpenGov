//單筆上傳
$('.blockFile input[type="file"]').change(function () {
    var $name = $(this).attr('xx');
    var data = new FormData();
    data.append('file', this.files[0]);
    data.append('controllerName', _controllerName);

    $.ajax({
        type: 'Post',
        url: _photoUrl,
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.Success) {
                $('#' + $name).val(result.ID);
                $('#block_src_' + $name).attr('src', _photoDetail + "?id=" + result.ID);
            }
        }
    });
});

//多圖上傳
$('.blockFileList input[type="file"]').change(function () {
    var $name = $(this).attr('xx');
    var data = new FormData();
    data.append('file', this.files[0]);
    data.append('controllerName', _controllerName);

    $.ajax({
        type: 'Post',
        url: _photoUrl,
        data: data,
        cache: false,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.Success) {
                var $id = result.ID,
                    $html = $('#blockImgHtmlDemo').html(),
                    $i = $('.blockImgUrl').length;

                $html = $html.replace(/{imgClass}/ig, "blockImgUrl");
                $html = $html.replace(/{imgDel}/ig, "blockImgDel");
                $html = $html.replace(/{imgId}/ig, "blockImgId");
                $html = $html.replace(/{blockImg}/ig, _photoDetail + "?id=" + $id);
                $html = $html.replace(/{imgNumber}/ig, $i);
                $html = $html.replace(/{blockImgName}/ig, $id);

                $('#blockImg').hide();
                $('#blockImageDemoList').append($html);
            } else {
                alert('新增失敗! 請重新上傳');
            }
        }
    });
});

//多圖刪除
$('.blockFileList').on("click", ".blockImgDel", function () {
    if (confirm('確定刪除？')) {
        var i = $('.blockFileList .blockImgDel').index($(this));
        $('.blockImgUrl').eq(i).hide();
        $('.blockImgId').eq(i).val('');
    }
});

//野生臺南-應用展示-相關連結-start
$('.blockUrl').on('click', '.pageLinkCreate', function () {
    var $name = $('#LinkName'),
        $link = $('#LinkUrl'),
        $i = $('.LinkOption>.blockLi').length,
        $html = $('#PageLink').html();

    $html = $html.replace(/{number}/ig, $i);
    $html = $html.replace(/{pageNameVal}/ig, $name.val());
    $html = $html.replace(/{pageLinkVal}/ig, $link.val());

    $('.LinkOption').append($html);
    $name.val('');
    $link.val('');
});

$('.blockUrl').on("click", ".blockRemove", function () {
    if (confirm('確定刪除？')) {
        var i = $('.blockUrl .blockRemove').index($(this));
        console.log(i);
                
        $('.LinkOption .blockLinkName').eq(i).val('');
        $('.LinkOption .blockLinkUrl').eq(i).val('');
        $('.LinkOption .blockLi').eq(i).hide();
    }
});

//野生臺南-應用展示-相關連結-end