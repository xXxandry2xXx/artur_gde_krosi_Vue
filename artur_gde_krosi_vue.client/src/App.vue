<template>
    <Preloader v-show="$store.state.showPreloader" />
    <AppHeader />

    <main class="main-content">
        <router-view>

        </router-view>
    </main>

    <AppFooter />
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppHeader from '@/components/AppHeader.vue';
    import AppFooter from '@/components/AppFooter.vue';

    export default defineComponent({
        components: { AppHeader, AppFooter }
    })
</script>