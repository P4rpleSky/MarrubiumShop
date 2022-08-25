import { show_products } from "../view_getters/product_viewer.js"

document.querySelectorAll("select").forEach(s => {
    s.addEventListener("change", e => {
        e.preventDefault();
        document.getElementById("product-block").innerHTML = '';
        show_products();
    })
})