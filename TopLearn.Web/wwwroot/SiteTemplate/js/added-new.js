

document.querySelectorAll("academy").forEach(slide => {
    slide.addEventListener("click", () => {
        var title = slide.getAttribute("data-title");
        var phone = slide.getAttribute("data-phone");
        var address = slide.getAttribute("data-address");
        var instrument = slide.getAttribute("data-instrument");
        var logo = slide.getAttribute("data-logo");
        var day = slide.getAttribute("data-day");
        var site = slide.getAttribute("data-site");

        Swal.fire({
            title: `${title}`,
            imageUrl: `academy/${logo}`,
            html: `<p>روزهای تدریس :<span style="color:darkslategrey"> ${day}</span></p>
                         <p>تلفن :<span style="color: darkslategrey"> ${phone}</span</p>
                         <p>سازها :<span style="color: darkslategrey"> ${instrument}</span></p>
                        <p>آدرس :<span style="color: darkslategrey"> ${address}</span></p>`,
            confirmButtonText: "وبسایت آموزشگاه",
            showCancelButton: false,
            showCloseButton: true,
            imageWidth: 400,
            imageHeight: 200,
            imageAlt: "Custom image",
            position: "top",
            customClass: {
                popup: 'swal2-dark'
            }
        }).then(res => {
            if (res.isConfirmed && site) {
                window.open(site, "_blank");
            }
        });
    });
});

/*Academy*/


/*Layout*/
document.addEventListener("DOMContentLoaded", () => {
    var isDesktop = () => window.innerWidth > 767.9;

    var gap = 15;

    if (isDesktop()) gap = 0.0285 * window.innerWidth;

    var sliders = [];

    ["#horizontal-ticker-rtl", "#horizontal-ticker-ltr"].forEach(
        (query, index) => {
            sliders.push(
                new Swiper(query, {
                    loop: true,
                    slidesPerView: "auto",
                    spaceBetween: gap,
                    speed: 8000,
                    allowTouchMove: false,
                    autoplay: {
                        delay: 0,
                        reverseDirection: index,
                        disableOnInteraction: false
                    }
                })
            );
        }
    );

    window.addEventListener("resize", () => {
        isDesktop() ? (gap = 0.0285 * window.innerWidth) : (gap = 15);

        sliders.forEach((slider) => {
            slider.params.spaceBetween = gap;
            slider.update();
        });
    });
});


document.querySelectorAll('.parket .info').forEach((elem) => {
    elem.querySelector('.dot').addEventListener('click',
        () => {
            elem.querySelector('.dot').classList.add('active');
            elem.querySelector('.box').classList.add('active');
        });
    elem.querySelector('.close').addEventListener('click',
        () => {
            elem.querySelector('.dot').classList.remove('active');
            elem.querySelector('.box').classList.remove('active');
        });
});



var swiper = new Swiper(".sliderMe .mySwiper", {
    effect: "cards",
    grabCursor: true,
});


Fancybox.bind("[data-fancybox]", {
    Toolbar: {
        display: [
            { id: "counter", position: "center" },
            "fullscreen",
            "close",
        ],
    },
    Image: {
        zoom: true,
    },
});

document.addEventListener("DOMContentLoaded", function () {
    const toggle = document.querySelector(".profile-toggle");
    if (!toggle) return;

    const menu = toggle.nextElementSibling;
    var dropdown01 = document.getElementById("dropdown-menu01");
    var dropdown02 = document.getElementById("dropdown-menu02");
    var dropdown03 = document.getElementById("dropdown-menu03");
    var header = document.querySelector('.mainHeader');
    toggle.addEventListener("click", function (e) {
        e.preventDefault();
        e.stopPropagation();

        const isOpen = menu.style.display === "block";
        menu.style.display = isOpen ? "none" : "block";
        dropdown01.style.display = isOpen ? "none" : "block";
        dropdown02.style.display = isOpen ? "none" : "block";
        dropdown03.style.display = isOpen ? "none" : "block";
        if (window.innerWidth <= 820) {
            header.style.height = isOpen ? "74px" : "222px";
        }



    });

    // فقط دسکتاپ
    if (window.innerWidth > 820) {
        document.addEventListener("click", function () {
            menu.style.display = "none";
        });
    }
});
/*Layout*/

/*Profile*/
function register() {
    var message = document.getElementById("message");
    var postalCode = document.getElementById("postalCode").value;
    if (postalCode.length > 0 && postalCode.length != 10) {
        message.textContent = "کد پستی باید 10 رقم باشد";
        return;
    }
    document.getElementById("registerForm").submit();
}
/*Profile*/

/*Register*/

function register() {
    var rePassword = document.getElementById("rePassword").value;
    var password = document.getElementById("password").value;
    var mobile = document.getElementById("mobile").value;
    var username = document.getElementById("username").value;
    var message = document.getElementById("message");
    if (username.length == 0) {
        message.textContent = "نام کاربری را وارد نمایید";
        return;
    }
    if (password.length == 0) {
        message.textContent = "رمز عبور را وارد نمایید";
        return;
    }
    if (mobile.length == 0) {
        message.textContent = "شماره همراه را وارد نمایید";
        return;
    }
    if (rePassword != password) {
        message.textContent = "رمز عبور و تکرار آن یکسان نمی باشد";
        return;
    }
    document.getElementById("registerForm").submit();
}
/*Register*/