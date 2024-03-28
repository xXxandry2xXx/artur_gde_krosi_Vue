<template>
    <header>
        <div class="appheader-top-bar">
            <div class="app-phone-and-adress">
                <span><font-awesome-icon :icon="['fas', 'phone']" /> +7 (912) 599-77-79</span>
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
            <div>
                <router-link :to="{ name: 'productsPage', params: { page: 1 } }">
                    <button class="nav-button">ВСЕ КРОССОВКИ</button>
                </router-link>
                <button class="nav-button">КОНТАКТЫ</button>
            </div>
        </nav>
    </header>
    <div class="app-subheader">
        <nav class="autorization-buttons">
            <button class="autorization-button">Вход</button>
            <span>или</span>
            <button class="autorization-button">Регистрация</button>
        </nav>
        <div class="app-subheader-buttons">
            <button class="app-subheader-button"><font-awesome-icon :icon="['fas', 'magnifying-glass']" /></button>
            <div class="cart-button">
                <span class="cart-capacity">{{ cartCapacity }}</span>
                <button class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></button>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue'

    export default defineComponent({
        data() {
            return {
                cartCapacity: 0,
            }
        }
    })
</script>