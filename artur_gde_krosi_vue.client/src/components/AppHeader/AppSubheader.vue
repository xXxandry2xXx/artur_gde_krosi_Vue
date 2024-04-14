<template>
    <nav class="app-subheader">
        <AppHeaderUserPanel />

        <transition name="fade">
            <AppSearchPanel @mouseenter="showSearchPanel" @mouseleave="hideSearchPanel" />
        </transition>

        <div class="app-subheader-buttons">
            <button class="app-subheader-button" @mouseenter="showSearchPanel"><font-awesome-icon :icon="['fas', 'magnifying-glass']" /></button>
            <button class="cart-button">
                <span class="cart-capacity">{{ currentCartCapacity }}</span>
                <span class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></span>
            </button>
        </div>
    </nav>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';
    import AppSearchPanel from '@/components/AppHeader/AppSearchPanel.vue';
    import AppHeaderUserPanel from '@/components/AppHeader/AppHeaderUserPanel.vue';

    export default defineComponent({
        components: { AppSearchPanel, AppHeaderUserPanel },

        data() {
            return {
                isSearchPanelVisible: false,
                currentCartCapacity: 0,
            }
        },

        methods: {
            ...mapMutations(['setSearchPanelVisibility']),

            showSearchPanel(this: any) {
                this.setSearchPanelVisibility(true);
            },

            hideSearchPanel(this: any) {
                setTimeout(() => this.setSearchPanelVisibility(false), 500)
            },
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