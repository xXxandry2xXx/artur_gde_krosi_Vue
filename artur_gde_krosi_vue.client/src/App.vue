<template>
    <Preloader v-show="$store.state.showPreloader" />

    <transition name="fade">
        <AuthorizationPopup v-show="$store.state.showLogInPopup" />
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
    import AuthorizationPopup from '@/components/AuthorizationPopup/AuthorizationPopup.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter, AuthorizationPopup },
    })
</script>