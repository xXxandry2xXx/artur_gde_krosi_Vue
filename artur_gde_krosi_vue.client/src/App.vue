<template>
    <AppHeader />

    <main class="main-content">
        <router-view>

        </router-view>
    </main>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppHeader from '@/components/AppHeader.vue';

    export default defineComponent({
        components: { AppHeader },
    })
</script>