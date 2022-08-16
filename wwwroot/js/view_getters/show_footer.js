//<div class="product-section">
//        <div class="visual">
//            <a id="p1_1" href="#" class="product-preview"></a>
//            <a class="set-like favourite"></a>
//        </div>
//        <a class="s2" href="#">Крем с ретинолом</a>
//        <p class="s3">1000 р</p>
//</div>

show_footer()

function show_footer() {

    const footer = document.createElement("div");
    footer.className += "container";

    footer.innerHTML = `
    <div class="row">
        <div class="col-md-3">
            <img src="img/logo.png" alt="Marrubium" class="footer-center">
        </div>
        <div class="offset-md-1 col-md-2">
            <div class="footer-menu-1 flex-cont">
                <a href="https://localhost:7199/about">О нас</a>
                <a href="https://localhost:7199/catalog">Каталог</a>
                <a href="https://localhost:7199/contacts">Контакты</a>
            </div>
        </div>
        <div class="col-md-3">
            <div class="footer-menu-2 flex-cont">
                <a href="https://localhost:7199/delivery">Доставка</a>
                <a href="mailto:aliev.af@edu.spbstu.ru">Напишите нам</a>
            </div>
        </div>
        <div class="col-md-3 ">
            <div class="tel-number footer-center">
                <p class="tel">+7(999)999-99-99</p>
            </div>
        </div>
    </div>`;

    const footer_place = document.querySelector("div.footer");
    
    footer_place.append(footer);
}
