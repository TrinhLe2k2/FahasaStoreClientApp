

SearchResultHandler()
CategoryHandler()
activeTooltips()

function SearchResultHandler() {
    $(document).click(function (event) {
        if (!$(".search-box").is(event.target) && $(".search-box").has(event.target).length === 0 || $(".search-box .shadowBox").is(event.target)) {
            $(".search-box .result-search").hide();
            $(".search-box .shadowBox").hide();
        } else {
            $(".search-box .result-search").show();
            $(".search-box .shadowBox").show();
            KeyframeExpand($(".search-box .result-search .container").height());
        }
    });
}
function CategoryHandler() {
    var categoryBox = $(".category-box");
    var categoryMenu = $(".category-box .category-menu");
    var shadowBox = $(".category-box .shadowBox");

    categoryBox.click(function (event) {

        if (!categoryMenu.is(event.target) && categoryMenu.has(event.target).length === 0) {
            categoryMenu.toggle();
            shadowBox.toggle();
            $(".category-box .category-btn").toggleClass('active');
            KeyframeExpand($(".category-box .category-menu .container").height());
        }
    });

    $(document).click(function (event) {
        if (!categoryBox.is(event.target) && categoryBox.has(event.target).length === 0 && !shadowBox.is(event.target)) {
            categoryMenu.hide();
            shadowBox.hide();
            $(".category-box .category-btn").removeClass('active');
        }
    });
}

function KeyframeExpand(height) {
    var styleSheet = document.getElementById('dynamic-keyframes').sheet;
    var keyframes = `
        @keyframes expand {
            from {
                height: 0;
                opacity: 0.8;
            }
            to {
                height: ${height}px;
                opacity: 1;
            }
        }
    `;
    styleSheet.insertRule(keyframes, styleSheet.cssRules.length);
}

function activeTooltips() {
    const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]')
    const tooltipList = [...tooltipTriggerList].map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl))
}

















//function SearchResultHandler() {
//    $(document).click(function (event) {
//        if (!$("#search-box").is(event.target) && $("#search-box").has(event.target).length === 0) {
//            $("#result-search").hide();
//            $("#shadow").hide();
//        } else {
//            $("#result-search").show();
//            $("#shadow").show();
//        }
//    });
//}

//function CategoryHandler() {
//    $("#category-box a[aria-expanded]").on('click', function () {
//        if ($(this).attr('aria-expanded') === 'true') {
//            $("#shadow").show();
//        } else {
//            $("#shadow").hide();
//        }
//    });
//}

//function ShadowHandler() {
    
//    $("#shadow").click(function (event) {
//        $(this).hide();
//    });
//}