await show_products();
export { row, get_product_section, show_products };

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
    row.className += " more";

    const show_more = document.createElement("div");
    show_more.setAttribute("id", "show-more");
    show_more.className += "offset-md-4";
    show_more.className += "col-md-4"
    row.appendChild(show_more);

    const button = document.createElement("a");
    button.className += "page-button";
    button.append("Показать ещё");
    button.addEventListener("click", e => {
        e.preventDefault();
        show_products()
    });
    show_more.appendChild(button);

    return row;
}

async function show_products() {

    let select = document.getElementById('type-of-product');
    let productType = select.options[select.selectedIndex].value;

    select = document.getElementById('function');
    let func = select.options[select.selectedIndex].value;

    select = document.getElementById('skin-type');
    let skinType = select.options[select.selectedIndex].value;

    select = document.getElementById('sorting');
    let order = select.options[select.selectedIndex].value;

    const response = await fetch("/catalog.json", {
        body: JSON.stringify({
            Type: productType,
            Function: func,
            SkinType: skinType,
            Order: order
        }),
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });
    if (response.ok === true) {
        const rows = document.getElementById("product-block");
        const products = await response.json();
        if (products.length === 0) {
            const response_text = document.createElement("h3");
            response_text.append("Товаров по данному запросу не найдено, попробуйте изменить фильтры");
            rows.append(response_text);
            return;
        }
        let product_sections = [];
        if (document.getElementsByClassName('products').length === 0) {
            for (let i = 0; i < Math.min(products.length, 8); i++) {
                product_sections.push(get_product_section(products[i]))
                if (product_sections.length % 4 === 0) {
                    rows.append(row(product_sections));
                    product_sections = [];
                }
            }
            if (products.length > 8)
                rows.append(more_button_row());
        }
        else {
            let buttons = document.getElementsByClassName('page-button');
            buttons[0].parentNode.parentNode.removeChild(buttons[0].parentNode);
            for (let i = 8; i < products.length; i++) {
                product_sections.push(get_product_section(products[i]));
                if (product_sections.length % 4 === 0) {
                    rows.append(row(product_sections));
                    product_sections = [];
                }
            }
        }
        if (product_sections.length != 0) {
            rows.append(row(product_sections));
            product_sections = [];
        } 
    }
}