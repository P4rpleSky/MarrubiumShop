//<div class="container">
//    <div class="row">
//        <div class="col-md-5">
//            <img class="photo" src="img/1_4.png">
//        </div>
//        <div class="col-md-6">
//            <h2 id="name">Сыворотка с полынью</h2>
//            <h3 id="price">1600 р</h3>
//            <h3>Описание</h3>
//            <div class="s1">Тип продукта: сыворотка</div>
//            <div class="s1">По функции: увлажняющее</div>
//            <div class="s1">Тип кожи: сухая</div>
//            <div class="amount-multibutton">
//                <a id="minus"></a>
//                <a id="amount">1</a>
//                <a id="plus"></a>
//            </div>
//            <a class="page-button">Добавить в корзину</a>
//        </div>
//    </div>
//</div>

show_product_page()

async function show_product_page() {

    const queryString = window.location.pathname.split('/')[2];
    const id = Number(queryString);

    const response = await fetch(`/catalog/${id}/product.json`, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const product = await response.json();
        const product_section = document.createElement("div");
        product_section.className += "container";

        const main = document.querySelector("div.main-block");
        
        const image_url = `../img/products/${product.ImageName}.png`;

        product_section.innerHTML = `
        <div class="row">
            <div class="col-md-5">
                <img class="photo" src="${image_url}">
            </div>
            <div class="col-md-6">
                <h2 id="name">${product.ProductName}</h2>
                <h3 id="price">${product.ProductPrice} р</h3>
                <h3>Описание</h3>
                <div class="s1">Тип продукта: ${product.Type.join(", ")}</div>
                <div class="s1">По функции: ${product.Function.join(", ")}</div>
                <div class="s1">Тип кожи: ${product.SkinType.join(", ")}</div>
                <div class="amount-multibutton">
                    <a id="minus"></a>
                    <a id="amount">1</a>
                    <a id="plus"></a>
                </div>
                <a class="page-button">Добавить в корзину</a>
            </div>
        </div>`;
        main.append(product_section);
    }    
}
