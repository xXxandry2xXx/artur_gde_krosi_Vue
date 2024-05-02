<template>
    <nav class="app-subheader">
        <AppHeaderUserPanel />

        <div class="app-subheader-search-and-cart">
            <AppSearchPanel />
            <button class="cart-button">
                <span class="cart-capacity">{{ currentCartCapacity }}</span>
                <span class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></span>
            </button>
        </div>
    </nav>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppSearchPanel from '@/components/AppHeader/AppSearchPanel.vue';
    import AppHeaderUserPanel from '@/components/AppHeader/AppHeaderUserPanel.vue';

    export default defineComponent({
        components: { AppSearchPanel, AppHeaderUserPanel },

        data() {
            return {
                currentCartCapacity: 0,
            }
        },
    })
</script>

<style>
    .fade-enter-to,
    .fade-leave-from {
        opacity: 1;
        transition: opacity 0.2s;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
        transition: opacity 0.2s;
    }
</style>