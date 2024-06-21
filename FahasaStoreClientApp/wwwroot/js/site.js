

const HandlerCRUD = (e, event) => {
    event.preventDefault();
    let href = $(e).attr("href");
    let id = $(e).attr("IdValue");
    if (id) {
        href = `${href}/${id}`;
    }
    $.ajax({
        url: href,
        type: 'GET',
        success: function (data) {
            $('#modal-for-crud').html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = `Error fetching data: ${jqXHR.status} ${jqXHR.statusText}`;
            if (jqXHR.responseText) {
                errorMessage += `\nResponse: ${jqXHR.responseText}`;
            }
            alert(errorMessage);
        }
    });
}
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

const RenderPartialView = (url, elementId) => {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#' + elementId).html(data);
        },
        error: function () {
            console.log("Error: user login");
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

const HandlerCategoryFilter = () => {
    var navLinkBtns = $('.ul-Tabs .nav-link');
    navLinkBtns.each(function () {
        var categoryId = $(this).attr("CategoryId");
        var subcategoryId = $(this).attr("SubcategoryId");
        if ($(this).hasClass('active')) {
            // Call your AJAX function here
            $.ajax({
                url: `/Home/HandlerFilter`,
                method: 'GET',
                data: {
                    CategoryId: categoryId,
                    SubcategoryId: subcategoryId
                },
                success: function (response) {
                    $(`#tab-content_${categoryId}`).html(response);
                },
                error: function (error) {
                    console.error('AJAX call failed:', error);
                }
            });
        }
        $(this).on('click', function () {
            $.ajax({
                url: `/Home/HandlerFilter`,
                method: 'GET',
                data: {
                    CategoryId: categoryId,
                    SubcategoryId: subcategoryId
                },
                success: function (response) {
                    $(`#tab-content_${categoryId}`).html(response);
                },
                error: function (error) {
                    console.error('AJAX call failed:', error);
                }
            });
        });
    });
}
function initSlickSliders() {
    $('.ranking-slider-for').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        dots: false,
        fade: true,
        autoplay: true,
        autoplaySpeed: 2000,
        vertical: false,
        asNavFor: '.ranking-slider-nav'
    });
    $('.ranking-slider-nav').slick({
        slidesToShow: 5,
        slidesToScroll: 1,
        asNavFor: '.ranking-slider-for',
        dots: false,
        arrows: false,
        focusOnSelect: true,
        vertical: true
    });
}

const HandlerBestSelling = () => {
    var navLinkBtns = $('#rankingTab .nav-link');
    initSlickSliders();
    navLinkBtns.each(function () {
        var categoryId = $(this).attr("CategoryId");
        if ($(this).hasClass('active')) {
            // Call your AJAX function here
            $.ajax({
                url: `/Home/GetTopSellingBooksByCategory`,
                method: 'GET',
                data: {
                    categoryId: categoryId
                },
                success: function (response) {
                    $(`#tab-content_rankingTab`).html(response);
                    initSlickSliders();
                },
                error: function (error) {
                    console.error('AJAX call failed:', error);
                }
            });
        }
        $(this).on('click', function () {
            $.ajax({
                url: `/Home/GetTopSellingBooksByCategory`,
                method: 'GET',
                data: {
                    CategoryId: categoryId
                },
                success: function (response) {
                    $('.ranking-slider-for').slick('unslick');
                    $('.ranking-slider-nav').slick('unslick');

                    $(`#tab-content_rankingTab`).html(response);
                    initSlickSliders();
                },
                error: function (error) {
                    console.error('AJAX call failed:', error);
                }
            });
        });
    });
}
function handleSelectedChange(e, event) {
    var value = $(e).val();
    if (value == "0") {
        $(e).removeAttr('name');
    }
    document.getElementById("filter-form").submit();
}
function AddCartItem(e, event) {
    event.preventDefault();
    var quantity = $("#order-quantity").val();
    var bookId = $(e).attr("BookId");
    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        url: $(e).attr('action'),
        type: 'POST',
        headers: {
            // Đặt header X-CSRF-TOKEN với giá trị của mã xác thực CSRF
            'X-CSRF-TOKEN': token
        },
        data: {
            Quantity: quantity,
            BookId: bookId 
        },
        success: function (response) {
            toastr.success("Thêm vào giỏ hàng thành công");
            window.location.reload();
        },
        error: function (xhr, status, error) {
            toastr.error("Xảy ra lỗi");
        }
    });
}

function UpdateTotalMoney() {
    var selectedIds = $(".cartItem-checkbox:checked").map(function () {
        return $(this).val();
    }).get();

    if (selectedIds.length === 0) {
        $("#total-money").text("0 đ");
        return;
    }

    $.ajax({
        url: '/user/GetIntoMoney',
        type: 'GET',
        data: { cartItemIds: selectedIds },
        traditional: true, // Để truyền mảng đơn giản hơn
        success: function (response) {
            $("#total-money").text(response.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + " đ");
        },
        error: function (xhr, status, error) {
            alert("Đã xảy ra lỗi: " + error);
        }
    });
}

function IntoMoney() {
    $(document).on('change', '.cartItem-checkbox', function () {
        UpdateTotalMoney();
    });
}
function UpdateCartItem() {
    $(document).on('change', '.quantity-book', function () {
        var quantity = $(this).val();
        var cartItemId = $(this).attr('cartItemId');
        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: '/User/UpdateCartItem',
            type: 'POST',
            data: { id: cartItemId, quantity: quantity },
            headers: {
                'X-CSRF-TOKEN': token
            },
            success: function (response) {
                UpdateTotalMoney();
            },
            error: function (xhr, status, error) {
                alert("Đã xảy ra lỗi: " + error);
            }
        });
    });
}

// /Home/Filter
function submitForm() {
    const $form = $('#filterForm');
    const formData = $form.serialize();

    $.ajax({
        url: $form.attr('action'),
        type: $form.attr('method'),
        data: formData,
        success: function (html) {
            $('#filterResults').html(html);
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    });
}

function resetFilter(filterName) {
    $('input[name="' + filterName + '"]').prop('checked', false);
    submitForm();
}

function resetAllFilters() {
    $('input[type="radio"]').prop('checked', false);
    submitForm();
}




SearchResultHandler();
CategoryHandler();
activeTooltips();
HandlerCategoryFilter();
IntoMoney();
HandlerBestSelling();
UpdateCartItem();