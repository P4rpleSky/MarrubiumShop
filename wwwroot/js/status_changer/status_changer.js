async function add_user_product(category, prod_id) {
    const response = await fetch(`/userProducts`, {
        method: "POST",
        headers: { "Accept": "application/json", "ProductsCategory": category, "ProductId": prod_id }
    });
    if (response.ok === true) {
        const message = await response.json();
        if (!!message.Error)
            alert(message.Error);
    }
}

async function delete_user_product(category, prod_id) {
    const response = await fetch(`/userProducts`, {
        method: "DELETE",
        headers: { "Accept": "application/json", "ProductsCategory": category, "ProductId": prod_id }
    });
    if (response.ok === true) {
        const message = await response.json();
        if (!!message.Error)
            alert(message.Error);
    }
}

async function get_user_products(category) {
    const response = await fetch(`/userProducts`, {
        method: "GET",
        headers: { "Accept": "application/json", "ProductsCategory": category }
    });
    if (response.ok === true) {
        return await response.json();
    }
}

export { add_user_product, delete_user_product, get_user_products };



