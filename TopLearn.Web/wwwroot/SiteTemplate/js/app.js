// Open/Close Menu
document.querySelector("header .openMenu").addEventListener("click",
    () => {
        document.querySelector("header .mainMenu").classList.add("opened");
        document.querySelector("header .mainMenu").classList.add("bgAnimated");
        var time = window.innerWidth < 767 ? 1000 : 2000;
        setTimeout(() => {
            document.querySelector("header .mainMenu").classList.add("animated");
        },
            time);
    });
document.querySelector("header .closeMenu").addEventListener("click",
    () => {
        document.querySelector("header .mainMenu").classList.remove("animated");
        var time = window.innerWidth < 767 ? 1500 : 2500;
        setTimeout(() => {
            document.querySelector("header .mainMenu").classList.remove("bgAnimated");
        },
            500);
        setTimeout(() => {
            document.querySelector("header .mainMenu").classList.remove("opened");
        },
            time);
    });
document.querySelectorAll("header .mainHeader .menu li").forEach((item) => {
    item.addEventListener("mouseenter",
        () => {
            document.querySelectorAll("header .mainHeader .menu li").forEach((li) => {
                li.classList.add("hover");
            })
            item.classList.remove("hover");
        })
    item.addEventListener("mouseleave",
        () => {
            document.querySelectorAll("header .mainHeader .menu li").forEach((li) => {
                li.classList.remove("hover");
            });
        });
});
// Enable Scroll
if (document.querySelector("[data-scroll-container]") != undefined) {
    var locoScroll = new LocomotiveScroll({
        el: document.querySelector("[data-scroll-container]"),
        smooth: true
    });
    locoScroll.on("scroll", (args) => {
        if (args.delta != undefined) {
            var scrTop = args.delta.y;
            if (scrTop > 80) {
                document.querySelector("header").classList.add("fixed");
            } else {
                document.querySelector("header").classList.remove("fixed");
            }
        }
    })
}
document.addEventListener("scroll",
    () => {
        var scrTop = document.scrollingElement.scrollTop;
        if (scrTop > 80) {
            document.querySelector("header").classList.add("fixed");
        } else {
            document.querySelector("header").classList.remove("fixed");
        }
    });
// Mouse
var hasCircle = document.querySelectorAll(".hasCircle");
var mouseCircle = document.querySelector(".mouseCircle");
if (hasCircle.length > 0) {
    hasCircle.forEach((el) => {
        el.addEventListener("mouseenter",
            () => {
                mouseCircle.classList.add("active");
            });
        el.addEventListener("mousemove",
            (event) => {
                var x = event.screenX;
                var y = event.screenY;
                mouseCircle.style.left = "${x}px";
                mouseCircle.style.top = "${y}px";
            });
        el.addEventListener("mouseleave",
            () => {
                mouseCircle.classList.remove("active");
            });
    });
}