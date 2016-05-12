var lockSearchOpen = false;
var animationFinished = false;
$("#searchIcon").hover(
    function(e) {
        if (!lockSearchOpen) {
            $("#searchBar").css({
                position: "fixed",
                top: $(this).position().top,
                left: -$("#searchBar").outerWidth(),
                visibility: "visible"
            });
            $("#searchNav").val("");
            $("#searchBar").animate({
                left: 0
            }, {
                duration: 300,
                complete: function() {
                    animationFinished = true;
                    $("#searchNav").focus();
                }
            });
        }
    },
    function(e) {
        if (!lockSearchOpen && animationFinished) {
            closeSearch();
        }
    });
$("#searchNav").focus(function (event) {
    lockSearchOpen = true;
});
$("#searchNav").focusout(function (event) {
    lockSearchOpen = false;
    closeSearch();
});
function closeSearch() {
    $("#searchBar").animate({
        left: -$("#searchBar").outerWidth()
    }, 150);
    animationFinished = false;
}
var autoComplete = $("#searchNav").autocomplete({
    source: function(request, response) {
        $.ajax({
            accepts: "application/json",
            dataType: "json",
            url: "/Api/Serie/" + request.term,
            success: function(result) {
                response(result);
            }
    });
},
minLength: 3,
    focus: function(event, ui) {
        $("#searchNav").val(ui.item.Title);
        return false;
    },
select: function(event, ui) {
    window.location.href = "/Series/Details/" + ui.item.Id;
    return true;
},
open: function(event, ui) {
    lockSearchOpen = true;
},
close: function(event, ui) {
    if (lockSearchOpen) {
        setTimeout(closeSearch(), 100);
    }
    lockSearchOpen = false;
}
});
autoComplete.autocomplete("instance")._renderItem = function(ul, item) {
    return $("<li>")
        .append("<table><tr>" +
            "<td style='padding: 0px 5px;'><img src='" + item.ThumbUrl + "' style='width: 64px;'></td>" +
            "<td>" +
            item.Title +
            "<p style='font-size: 12px; margin: 0;'>" + item.ShortDescripton + "</p>" +
            "</td>" +
            "</tr></table>")
        .appendTo(ul);
};