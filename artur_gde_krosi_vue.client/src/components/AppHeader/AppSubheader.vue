<template>
    <nav class="app-subheader">
        <div class="authorized-user-panel" @click="isUserPanelVisible = !isUserPanelVisible" v-if="isUserAuthorized()">
            {{ $store.state.authorizedUser.userName }}
            <div class="user-dropdown" v-show="isUserPanelVisible">
                <button class="user-logout-button" @click="userLogout()">Выйти</button>
            </div>
        </div>

        <div class="autorization-buttons" v-else>
            <button class="autorization-button" @click="openAuthorizationPopup('log-in')">Вход</button>
            <span>или</span>
            <button class="autorization-button" @click="openAuthorizationPopup('registration')">Регистрация</button>
        </div>

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
    import { mapMutations, mapGetters, mapActions } from 'vuex';
    import AppSearchPanel from '@/components/AppHeader/AppSearchPanel.vue';

    export default defineComponent({
        components: { AppSearchPanel },

        data() {
            return {
                isUserPanelVisible: false,
                isSearchPanelVisible: false,
                currentCartCapacity: 0,
            }
        },

        methods: {
            ...mapActions(['logout']),
            ...mapMutations(['setSearchPanelVisibility', 'openAuthorizationPopup']),
            ...mapGetters(['isUserAuthorized']),

            showSearchPanel(this: any) {
                this.setSearchPanelVisibility(true);
            },

            hideSearchPanel(this: any) {
                setTimeout(() => this.setSearchPanelVisibility(false), 500)
            },

            userLogout(this: any) {
                this.logout();
                location.reload();
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