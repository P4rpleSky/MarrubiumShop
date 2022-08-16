//<div class="product-section">
    //    <div class="visual">
    //        <a id="p1_1" href="#" class="product-preview"></a>
    //        <a class="set-like favourite"></a>
    //    </div>
    //    <a class="s2" href="#">Крем с ретинолом</a>
    //    <p class="s3">1000 р</p>
//</div>

getProducts()

function get_product_section(product) {
    
    const product_section = document.createElement("div");
    product_section.className += "product-section";

    const visual = document.createElement("div");
    visual.className += "visual";
    product_section.appendChild(visual);

    const product_preview = document.createElement("a");
    const image_url = `../img/products/${product.ImageName}.png`;
    const product_page_url = `https://localhost:7199/catalog/${product.ProductId}`;
    product_preview.className += `product-preview`;
    product_preview.setAttribute("style", `background-image:url("${image_url}")`);
    product_preview.setAttribute("href", product_page_url);
    visual.appendChild(product_preview);

    const like_button = document.createElement("a");
    like_button.className += "set-like";
    like_button.className += " favourite"
    like_button.addEventListener("click", e => {
        e.preventDefault();
        if (like_button.style.backgroundImage == 'url("../img/favourite_click.png")')
            like_button.style.backgroundImage = "";
        else
            like_button.style.backgroundImage = 'url("../img/favourite_click.png")';
    })
    visual.appendChild(like_button);

    const name = document.createElement("a");
    name.className += "s2";
    name.setAttribute("href", product_page_url);
    name.append(product.ProductName);
    product_section.append(name);

    const price = document.createElement("p");
    price.className += "s3";
    price.append(`${product.ProductPrice.toString()} р`);
    product_section.append(price);

    return product_section;
}

function row(product_sections) {

    const row = document.createElement("div");
    row.className += "row";
    row.className += " products"

    for (let i = 0; i < 4; i++) {
        if (i >= product_sections.length)
            break;
        const col = document.createElement("div");
        col.className += "col-md-3";
        col.appendChild(product_sections[i])
        row.appendChild(col)
    } 

    return row;
}

function more_button_row() {

    const row = document.createElement("div");
    row.className += "row";

    const show_more = document.createElement("div");
    show_more.setAttribute("id", "show-more");
    show_more.className += "offset-md-4";
    show_more.className += "col-md-4"
    row.appendChild(show_more);

    const button = document.createElement("a");
    button.className += "page-button";
    button.append("Показать ещё");
    show_more.appendChild(button);

    return row;
}

async function getProducts() {
    const response = await fetch("/catalog.json", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const products = await response.json();
        const rows = document.querySelector("div.main-block div.container");
        let product_sections = [];
        for (let i = 0; i < 8; i++) {
            product_sections.push(get_product_section(products[i]))
            if (product_sections.length % 4 === 0) {
                rows.append(row(product_sections))
                product_sections = []
            }
        }
        /*if (products > 8)*/
        rows.append(more_button_row())
    }
}
