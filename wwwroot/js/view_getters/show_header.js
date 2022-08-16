//<div class="product-section">
//        <div class="visual">
//            <a id="p1_1" href="#" class="product-preview"></a>
//            <a class="set-like favourite"></a>
//        </div>
//        <a class="s2" href="#">Крем с ретинолом</a>
//        <p class="s3">1000 р</p>
//</div>

show_header()

function show_header() {

    const header = document.createElement("div");
    header.className += "container";

    header.innerHTML = `
    <div class="row">
        <div class="col-md-2">
            <a class="logo" href="https://localhost:7199/"></a>
        </div>
        <div class="col-md-8">
            <ui class="main_menu">
                <li><a class="s1" href="https://localhost:7199/about">О нас</a></li>
                <li>
                    <a class="s1" href="https://localhost:7199/catalog">Каталог</a>
                    <ul>
                        <li><a class="s2" href="https://localhost:7199/catalog">Крема</a></li>
                        <li><a class="s2" href="https://localhost:7199/catalog">Сыворотки</a></li>
                        <li><a class="s2" href="https://localhost:7199/catalog">Очищающие средства</a></li>
                        <li><a class="s2" href="https://localhost:7199/catalog">Маски</a></li>
                    </ul>
                </li>
                <li><a class="s1" href="https://localhost:7199/delivery">Доставка</a></li>
                <li><a class="s1" href="https://localhost:7199/contacts">Контакты</a></li>
            </ui>
        </div>
        <div class="col-md-2">
            <div class="buttons_menu">
                <a class="profile" href="https://localhost:7199/my/profile"></a>
                <a class="favourite" href="https://localhost:7199/my/favourite"></a>
                <a class="cart" href="https://localhost:7199/my/cart"></a>
                <a class="search" href="#"></a>
            </div>
        </div>
    </div>`;

    const header_place = document.querySelector("header.header");
    
    header_place.append(header);
}
