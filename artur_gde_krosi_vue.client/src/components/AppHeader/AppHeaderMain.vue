<template>
    <header>
        <div class="appheader-top-bar">
            <div class="app-phone-and-adress">
                <span><font-awesome-icon :icon="['fas', 'phone']" /> +7 (999) 999-99-99</span>
                <div class="app-adress">
                    <span><font-awesome-icon :icon="['fas', 'clock']" /> 10:00-22:00</span>
                    <span>г. Пермь, ул. Революции, 22</span>
                </div>
            </div>
        </div>
        <nav class="appheader-navbar">
            <div class="logo" @click="$router.push('/')">
                <span class="logo-top">АРТУР, ГДЕ МОИ </span>
                <span class="logo-bottom">КРОССОВКИ?</span>
            </div>
            <div class="appheader-nav-buttons">
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                    <button class="nav-button">В КАТАЛОГ</button>
                </router-link>
                <!--<button class="nav-button">КОНТАКТЫ</button>-->
            </div>
        </nav>
    </header>
</template>