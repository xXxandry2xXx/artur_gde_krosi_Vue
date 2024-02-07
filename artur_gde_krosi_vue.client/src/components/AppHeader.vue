<template>
    <header>
        <img class="logo" src="../assets/img/logo.jpg">
        <div>
            <button class="login-button">Вход</button>
            <button class="login-button">Регистрация</button>
        </div>
    </header>
</template>

<style>
    header {
        box-sizing: border-box;
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0 30px;
        margin-bottom: 40px;
        width: 100%;
        height: 70px;
        background: #FDDD00;
        box-shadow: 4px 4px 7px 1px rgb(0, 0, 0, 20%);
    }

    .logo {
        width: 200px;
        height: 70px;
        object-fit: cover;
    }

    .login-button {
        background: #000;
        color: #fff;
        border: none;
        padding: 10px 25px;
        font-size: 17px;
        font-weight: 600;
        border-radius: 7px;
        margin: 0px 10px;
    }
</style>