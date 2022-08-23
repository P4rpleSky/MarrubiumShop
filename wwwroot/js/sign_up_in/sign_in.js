let form = document.getElementsByClassName("sign-in")[0];
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

form.addEventListener("submit", e => {
    e.preventDefault();
    login();
}, true);

async function login() {
    const response = await fetch("/login_user_post", {
        body: JSON.stringify({
            FirstName: "unknown",
            LastName: "unknown",
            PhoneNumber: document.getElementById("tel-or-email").value,
            CustomerPassword: document.getElementById("user-password").value,
            CustomerEmail: document.getElementById("tel-or-email").value
        }),
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" }
    });

    if (response.ok === true) {

        const result = await response.json();
        if (!!result.error) {
            if (result.error === "exception") {
                document.getElementsByClassName("sign-in")[0].remove();
                document.getElementsByClassName("main-block")[0].innerHTML += `
                    <h2>При входе на сайт произошла ошибка, попробуйте снова</h2>
                    <a href="https://localhost:7199/login" class="page-button">Войти на сайт</a>`
                return;
            }
            document.getElementById(result.error).style.border = '1px solid red';
            document.getElementById(result.error + "-w").style.display = "block";
            return;
        }
        window.location.replace("https://localhost:7199/");
    }
}



