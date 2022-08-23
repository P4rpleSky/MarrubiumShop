import { show_products } from "../view_getters/show_products.js"

document.querySelectorAll("select").forEach(s => {
    s.addEventListener("change", e => {
        e.preventDefault();
        document.getElementById("product-block").innerHTML = '';
        show_products();
    })
})