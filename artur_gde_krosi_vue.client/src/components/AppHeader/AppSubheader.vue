<template>
    <nav class="app-subheader">
        <div class="autorization-buttons">
            <button class="autorization-button">Вход</button>
            <span>или</span>
            <button class="autorization-button">Регистрация</button>
        </div>

        <transition name="fade">
            <AppSearchPanel :isVisible="isSearchPanelVisible" />
        </transition>

        <div class="app-subheader-buttons">
            <button class="app-subheader-button" @click="toggleSearchPanel"><font-awesome-icon :icon="['fas', 'magnifying-glass']" /></button>
            <div class="cart-button">
                <span class="cart-capacity">{{ currentCartCapacity }}</span>
                <button class="app-subheader-button"><font-awesome-icon :icon="['fas', 'cart-shopping']" /></button>
            </div>
        </div>
    </nav>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import AppSearchPanel from '@/components/AppHeader/AppSearchPanel.vue';

    export default defineComponent({
        components: { AppSearchPanel },

        data() {
            return {
                isSearchPanelVisible: false,
                currentCartCapacity: 0,
            }
        },

        methods: {
            toggleSearchPanel(this: any) {
                this.isSearchPanelVisible = !this.isSearchPanelVisible
            }
        }
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