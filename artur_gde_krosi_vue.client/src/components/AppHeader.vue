<template>
    <header>
        <div class="appheader-top-bar">
            <div class="autorization-buttons">
                <button class="login-button">Вход</button>
                <button class="login-button">Регистрация</button>
            </div>
        </div>
        <nav class="appheader-navbar">
            <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                <button class="nav-button">ВСЕ КРОССОВКИ</button>
            </router-link>
            <div class="logo" @click="$router.push('/')">
                <span class="logo-top">АРТУР, ГДЕ МОИ </span>
                <span class="logo-bottom">КРОССОВКИ?</span>
            </div>
            <button class="nav-button">КОНТАКТЫ</button>
        </nav>
    </header>
</template>