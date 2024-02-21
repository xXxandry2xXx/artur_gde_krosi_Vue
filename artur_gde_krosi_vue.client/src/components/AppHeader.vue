<template>
    <header>
        <img class="logo" src="../assets/img/logo.jpg">
        <nav class="appheader-navbar">
            <button class="nav-button" @click="$router.push('/')">Главная</button>
            <button class="nav-button" @click="$router.push('/products')">Кроссовки</button>
        </nav>
        <div class="autorization-buttons">
            <button class="login-button">Вход</button>
            <button class="login-button">Регистрация</button>
        </div>
    </header>
</template>