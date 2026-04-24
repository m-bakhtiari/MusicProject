var swiper = new Swiper(".swiper",
    {
        effect: "coverflow",
        centeredSlides: true,
        slidesPerView: "auto",
        grabCursor: true,
        spaceBetween: 40,
        coverflowEffect: {
            rotate: 25,
            stretch: 0,
            depth: 50,
            modifier: 1,
            slideShadows: false,
            theme: 'dark'
        },
        autoplay: {
            delay: 3000,
            disableOnInteraction: false
        },
        loop: true,
        navigation: {
            nextEl: ".swiper-button-next",
            prevEl: ".swiper-button-prev"
        }
    });
function MentorInfo(id, name) {
    window.location.href = '/Info?id=' + id + '&name=' + name;
}
function openTab(evt, tabName) {
    var i, tabcontent, tablinks;

    // پنهان کردن تمام محتواهای تب
    tabcontent = document.getElementsByClassName("tab-content");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // حذف کلاس 'active' از تمام دکمه‌های تب
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // نمایش محتوای تب فعلی
    document.getElementById(tabName).style.display = "block";

    // اضافه کردن کلاس 'active' به دکمه‌ای که کلیک شده است
    evt.currentTarget.className += " active";
}

// به طور پیش‌فرض، تب اول را هنگام بارگذاری صفحه فعال می‌کند (اختیاری)
document.addEventListener("DOMContentLoaded", function () {
    document.querySelector(".tablinks").click();
});
function confirmSubmit(type) {
    if (type == 2) {
        Swal.fire({
            title: 'دریافت مدرک',
            text: ' پس از آماده‌سازی، مراحل چاپ و صدور مدرک از طریق پیامک به شما اطلاع داده خواهد شد ',
            icon: 'success',
            showCancelButton: false,
            confirmButtonText: 'متوجه شدم'
        });
    } else {
        var firstName = document.getElementById("firstName").value;
        var lastName = document.getElementById("lastName").value;
        var instrument = document.getElementById("instrument").value;
        var nationalCode = document.getElementById("nationalCode").value;
        var mobile = document.getElementById("mobile").value;
        var address = document.getElementById("address").value;
        var postalCode = document.getElementById("postalCode").value;
        if (firstName.length < 2)
            document.getElementById("error-text").innerText = "نام را وارد نمایید";
        else if (lastName.length < 2)
            document.getElementById("error-text").innerText = "نام خانوادگی را وارد نمایید";
        else if (instrument.length < 2)
            document.getElementById("error-text").innerText = "نام ساز را وارد نمایید";
        else if (nationalCode.length < 2)
            document.getElementById("error-text").innerText = "کد ملی را وارد نمایید";
        else if (mobile.length != 11)
            document.getElementById("error-text").innerText = "شماره موبایل معتبر نمی باشد";
        else if (address.length < 2)
            document.getElementById("error-text").innerText = "آدرس را وارد نمایید";
        else if (postalCode.length != 10)
            document.getElementById("error-text").innerText = "کد پستی معتبر نمی باشد";
        else if (containsPersianChars(firstName))
            document.getElementById("error-text").innerText = ' نام باید فقط با حروف انگلیسی وارد شود';
        else if (containsPersianChars(lastName))
            document.getElementById("error-text").innerText = ' نام خانوادگی باید فقط با حروف انگلیسی وارد شود';

        else if (type == 1) {
            Swal.fire({
                title: 'اطلاعات شما ثبت شد',
                text: 'اطلاعات شما با موفقیت ثبت شد ، پس از آماده‌سازی، مراحل چاپ و صدور مدرک از طریق پیامک به شما اطلاع داده خواهد شد ',
                icon: 'success',
                showCancelButton: false,
                confirmButtonText: 'متوجه شدم',
            }).then(() => {
                document.getElementById("requestCertificate").submit();
            });
        }
    }

}

function containsPersianChars(text) {
    const persianRegex = /[\u0600-\u06FF\u200C\u200B]/;
    return persianRegex.test(text);
}

function filterNote(pageId, instrumentId) {
    if (pageId != null)
        $("#pageId").val(pageId);
    if (instrumentId != null)
        $("#instrumentId").val(instrumentId);
    $("#formFilter").submit();
}

let deferredPrompt;
const installBtn = document.getElementById('installBtn');

installBtn.style.display = 'none';

window.addEventListener('beforeinstallprompt', (e) => {
    e.preventDefault();
    deferredPrompt = e;
    installBtn.style.display = 'block'; 
});

installBtn.addEventListener('click', async () => {
    if (!deferredPrompt) return;

    deferredPrompt.prompt(); 
    const { outcome } = await deferredPrompt.userChoice;

    if (outcome === 'accepted') {
        console.log('کاربر اپ را نصب کرد ✅');
    } else {
        console.log('کاربر نصب را رد کرد ❌');
    }

    deferredPrompt = null;
    installBtn.style.display = 'none';
});
