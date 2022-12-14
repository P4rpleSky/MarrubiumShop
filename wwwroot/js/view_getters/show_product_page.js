import { row, get_product_section } from "../view_getters/product_viewer.js"
import { add_user_product, delete_user_product, get_user_products } from "../status_changer/status_changer.js"

await show_product_page();
const quantity = document.getElementById("quantity");
document.getElementById("plus").addEventListener("click", e => {
    /*e.preventDefault();*/
    quantity.innerText = Number(quantity.innerText) + 1;
});

document.getElementById("minus").addEventListener("click", e => {
    /*e.preventDefault();*/
    quantity.innerText = Number(quantity.innerText) - 1;
});

async function show_product_page() {

    const queryString = window.location.pathname.split('/')[2];
    const id = Number(queryString);

    const response = await fetch(`/catalog/${id}/product.json`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const products = await response.json();
        const product_section = document.createElement("div");
        product_section.className += "container";

        const main = document.querySelector("div.main-block");
        
        const image_url = `../img/products/${products[0].ImageName}.png`;

        let rusType = [];
        products[0].Type.forEach((e) => {
            switch (e) {
                case "Lotion":
                    rusType.push("крем");
                    break;
                case "Serum":
                    rusType.push("сыворотка");
                    break;
                case "Cleaning":
                    rusType.push("очищающее средство");
                    break;
                case "Mask":
                    rusType.push("маска");
                    break;
            }
        });

        let rusFunction = [];
        products[0].Function.forEach((e) => {
            switch (e) {
                case "Rejuvenating":
                    rusFunction.push("омолаживющая");
                    break;
                case "Restoring":
                    rusFunction.push("восстанавливающая");
                    break;
            }
        });

        let rusSkinType = [];
        products[0].SkinType.forEach((e) => {
            switch (e) {
                case "Soft":
                    rusSkinType.push("нежная");
                    break;
                case "Sensitive":
                    rusSkinType.push("чувствительная");
                    break;
                case "Rough":
                    rusSkinType.push("грубая");
                    break;
            }
        });

        product_section.innerHTML = `
        <div class="row">
            <div class="col-md-5">
                <img class="photo" src="${image_url}">
            </div>
            <div class="col-md-7">
                <h2 id="name">${products[0].ProductName}</h2>
                <h3 id="price">${products[0].ProductPrice} р</h3>
                <h3>Описание</h3>
                <div class="s1">Тип продукта: ${rusType.join(", ")}</div>
                <div class="s1">По функции: ${rusFunction.join(", ")}</div>
                <div class="s1">Тип кожи: ${rusSkinType.join(", ")}</div>
                <div class="buttons-row">
                    <div class="amount-multibutton">
                        <a id="minus"></a>
                        <div id="quantity">1</div>
                        <a id="plus"></a>
                    </div>
                    <a id="big-set-like"></a>
                </div>
                <a class="page-button">Добавить в корзину</a>
            </div>
        </div>
        <h3 id="recommended">Рекомендации</h3>`;

        let product_sections = [];
        const user_favourites = await get_user_products("favourite");
        if (user_favourites.Error === undefined) {
            for (let i = 1; i < products.length; i++) {

                let isInFavourite = false;
                if (user_favourites.Error === undefined) {
                    user_favourites.every(p => {
                        if (p.ProductId === products[i].ProductId) {
                            isInFavourite = true;
                            return false;
                        }
                        return true;
                    });
                };
                product_sections.push(get_product_section(products[i], isInFavourite));
            }
        }
        product_section.append(row(product_sections));
        main.append(product_section);

        let isInFavourite = false;
        if (user_favourites.Error === undefined) {
            user_favourites.every(p => {
                if (p.ProductId === products[0].ProductId) {
                    isInFavourite = true;
                    return false;
                }
                return true;
            });
        };
        const like_button = document.getElementById("big-set-like");
        like_button.style.backgroundImage = isInFavourite ? 'url("../img/big_favourite_click.png")' : "";
        like_button.addEventListener("click", e => {
            e.preventDefault();
            if (like_button.style.backgroundImage == 'url("../img/big_favourite_click.png")') {
                Promise.resolve(delete_user_product("favourite", products[0].ProductId)).then();
                like_button.style.backgroundImage = "";
            }
            else {
                Promise.resolve(add_user_product("favourite", products[0].ProductId)).then();
                like_button.style.backgroundImage = 'url("../img/big_favourite_click.png")';
            }
        })
        product_sections = [];
    }    
}
