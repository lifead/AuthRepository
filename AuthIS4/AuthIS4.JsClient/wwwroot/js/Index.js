document.getElementById("login").addEventListener("click", login);
document.getElementById("callApi").addEventListener("click", callApi);
document.getElementById("refresh").addEventListener("click", refresh);
document.getElementById("logout").addEventListener("click", logout);


const settings = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: "https://localhost:10001",
    client_id: "client_id_js",
    response_type: "code",
    scope: "openid profile OrdersApi",
    redirect_uri: "https://localhost:9001/callback.html",
    silent_redirect_uri: "https://localhost:9001/refresh.html",
    post_logout_redirect_uri: "https://localhost:9001/index.html"
}

const manager = new Oidc.UserManager(settings);

manager.events.addUserSignedOut(function () {
    print("Сессия завершена");
});

manager.getUser().then(function (user) {
    if (user) {
        print("Вход выполнен", user);
    }
    else {
        print("Пользователь не определен")
    }
});

function login() {
    manager.signinRedirect();
}

function refresh() {
    manager.signinSilent()
        .then(function (user)
        {
            print("Токен обновлен", user)
        })
        .catch(function (error)
        {
            print("Ошибка при обновлении токена", error);
        });
}


function callApi() {
    manager.getUser()
        .then(function (user) {
            if (user === null) {
                print("Ошибка авторизации, пользователь не найден");
            }

            const xhr = new XMLHttpRequest();
            xhr.open("GET", "https://localhost:7001/Home/GetSecret", true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    print(xhr.responseText, xhr.response);
                } else {
                    print("Ошибка обращения к внешнему ресурсу", xhr);
                }
            }

            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr.send();
        })
        .catch(function (error) {
            print(error);
        });
}


function logout() {
    manager.signoutRedirect();
}

function print(message, data) {
    if (message) {
        document.getElementById("message").innerHTML = message;
    }
    else {
        document.getElementById("message").innerHTML = "";
    }

    if (data && typeof data == "object") {
        document.getElementById("data").innerHTML = JSON.stringify(data, null, 2);
    }
    else {
        document.getElementById("data").innerHTML = "";
    }
}