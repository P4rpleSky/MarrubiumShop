async function add_to_fav(prod_id) {
    const response = await fetch(`/${prod_id}/add_to_fav`, {
        method: "PUT",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const message = await response.json();
        if (!!message.Error)
            alert(message.Error);
    }
}

async function delete_from_fav(prod_id) {
    const response = await fetch(`/${prod_id}/delete_from_fav`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const message = await response.json();
        if (!!message.Error)
            alert(message.Error);
    }
}

export { add_to_fav, delete_from_fav };



