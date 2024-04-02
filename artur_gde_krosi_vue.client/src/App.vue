<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <LoginPopup v-show="$store.state.showLogInPopup" />
    </transition>

    <AppHeader />

    <main class="main-content">
        <router-view>

        </router-view>
    </main>

    <AppFooter />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppHeader from '@/components/AppHeader/AppHeader.vue';
    import AppFooter from '@/components/AppFooter.vue';
    import LoginPopup from '@/components/LoginPopup.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter, LoginPopup },
    })
</script>