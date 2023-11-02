
const description = document.getElementById("desc");
const register = document.getElementById("review");
const loginForm = document.getElementById("desc-part");
const registerForm = document.getElementById("rewiev-part");

description.addEventListener("click", (e) => {
    e.preventDefault();

    // description.classList.add("text-black");
    // register.classList.remove("text-black");
    register.classList.add("text-gray")
    description.classList.remove("text-gray")
    loginForm.classList.add("block");
    loginForm.classList.remove("hidden");
    registerForm.classList.add("hidden");
    registerForm.classList.remove("block");

})
register.addEventListener("click", (e) => {
    e.preventDefault();


    register.classList.remove("text-gray");
    description.classList.add("text-gray")
    loginForm.classList.add("hidden");
    loginForm.classList.remove("block");
    registerForm.classList.add("block");
    registerForm.classList.remove("hidden");

})



// img zoom
const container = document.getElementById("container");
const img = document.getElementById("hover-img");
container.addEventListener("mousemove", onZoom);
container.addEventListener("mouseover", onZoom);
container.addEventListener("mouseleave", offZoom);
function onZoom(e) {
    const x = e.clientX - e.target.offsetLeft;
    const y = e.clientY - e.target.offsetTop;
    img.style.transformOrigin = `${x}px ${y}px`;
    img.style.transform = "scale(2.5)";
}
function offZoom(e) {
    img.style.transformOrigin = `center center`;
    img.style.transform = "scale(1)";
}

// changeable img
let thumbnails = document.getElementsByClassName('thumbnail')
let main_img = document.getElementById('hover-img')
let activeImages = document.getElementsByClassName('active')
//console.log(main_img.src);
for (var i = 0; i < thumbnails.length; i++) {

    thumbnails[i].addEventListener('click', function () {

        if (activeImages.length > 0) {
            activeImages[0].classList.remove('active')
        }


        this.classList.add('active')
        main_img.src = this.src
    })
}
