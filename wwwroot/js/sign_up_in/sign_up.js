let form = document.getElementsByClassName("sign-up")[0];
let checkbox = document.getElementById("confirm");
let button = document.getElementById("reg");
let warnings = document.getElementsByClassName("warning");

document.querySelectorAll("input").forEach(s => {
    s.addEventListener("input", e => {
        if (s.style.border === '1px solid red') {
            s.style.border = '0';
            s.style.borderBottom = '1px solid #FDB8D5';
            document.getElementById(s.id + "-w").style.display = "none";
        }
        const warning = document.getElementById(s.type + "-w");
        if (!!warning)
            warning.style.display = "none";
    })
});

checkbox.addEventListener("change", e => {
    button.disabled = checkbox.checked === true ? false : true;
});

form.addEventListener("submit", e => {

    e.preventDefault();
    const password1 = document.getElementById("password").value;
    const password2 = document.getElementById("password_copy").value;
    if (password1 != password2) {
        document.getElementById("password-w").style.display = "block";
        document.getElementById("password_copy").style.border = '1px solid red';
        return;
    }
    reg();
}, true);

async function reg() {
    const response = await fetch("/reg_user_post", {
        body: JSON.stringify({
            FirstName: document.getElementById("first_name").value,
            LastName: document.getElementById("last_name").value,
            PhoneNumber: document.getElementById("phone").value,
            CustomerPassword: document.getElementById("password").value,
            CustomerEmail: document.getElementById("email").value
        }),
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    if (response.ok === true) {

        const result = await response.json();
        if (!!result.error) {
            if (result.error === "user-password" || result.error === "tel-or-email") {
                document.getElementById(result.error).style.border = '1px solid red';
                document.getElementById(result.error + "-w").style.display = "block";
            }
            else {
                document.getElementsByClassName("sign-in")[0].remove();
                document.getElementsByClassName("main-block")[0].innerHTML += `
                    <h2>${result.error}</h2>
                    <a href="https://localhost:7199/login" class="page-button">Регистрация</a>`
            }
        }
        else {
            document.getElementsByClassName("sign-up")[0].remove();
            document.getElementsByClassName("main-block")[0].innerHTML += `
            <h2 charset='utf-8'>${result.Message}</h2>
            <a href="https://localhost:7199/login" class="page-button">Войти на сайт</a>`
        }
    }
}

