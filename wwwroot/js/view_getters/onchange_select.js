import { show_products } from "../view_getters/show_products.js"

document.querySelectorAll("select").forEach(s => {
    s.addEventListener("change", e => {
        //Array.from(document.getElementsByClassName("row products")).forEach(r => { r.remove() })
        //Array.prototype.forEach.call(document.getElementsByClassName("row more"), r => r.remove());
        //Array.prototype.forEach.call(document.querySelector("h3"), r => r.remove());
        document.getElementById("product-block").innerHTML = '';
        show_products();
    })
})