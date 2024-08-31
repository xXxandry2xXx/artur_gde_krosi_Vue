<template>
    <div class="authorized-user-panel" v-if="isUserAuthorized()"
         @mouseenter="showUserPanel"
         @mouseleave="hideUserPanel"
         @click="toggleUserPanelMobile"
         ref="userPanelButton">
        <span class="user-icon">
            <font-awesome-icon :icon="['fas', 'user']" />
        </span>
        <p>
            {{ $store.state.authorizedUser.userName }}
        </p>

        <transition name="fade">
            <div class="user-dropdown" @mouseenter="showUserPanel" @mouseleave="hideUserPanel" v-show="isUserPanelVisible" ref="userPanel">
                <div class="user-account-button" @click="moveToTheLink('/account')">
                    <font-awesome-icon :icon="['fas', 'user']" />
                    <span class="user-account-button-text">Личные данные</span>
                </div>
                <div class="user-account-button" @click="openInfoPopup">
                    <font-awesome-icon :icon="['fas', 'box']" />
                    <span class="user-account-button-text">Заказы</span>
                </div>
                <div class="user-account-button logout-button">
                    <span><font-awesome-icon :icon="['fas', 'right-from-bracket']" /></span>
                    <span class="user-account-button-text" @click="logout()">Выход</span>
                </div>
            </div>
        </transition>

    </div>

    <div class="autorization-buttons" v-else>
        <div class="autorization-buttons-pc">
            <button class="autorization-button" @click="openAuthorizationPopup('authorization', 'log-in')">Вход</button>
            <span>или</span>
            <button class="autorization-button" @click="openAuthorizationPopup('authorization', 'registration')">Регистрация</button>
        </div>
        <div class="autorization-button-mobile" @click="openAuthorizationPopup('authorization', 'log-in')">
            <font-awesome-icon :icon="['fas', 'right-to-bracket']" />
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import type { NavigationGuardNext, RouteLocationNormalized } from 'vue-router';
    import { mapMutations, mapGetters, mapActions } from 'vuex';

    export default defineComponent({
        data() {
            return {
                isUserPanelVisible: false,
                userPanelTimeout: null
            }
        },

        methods: {
            ...mapActions(['logout']),
            ...mapMutations(['setPopupVisibility', 'setPopupMode', 'setAuthorizationPopupMode']),
            ...mapGetters(['isUserAuthorized', 'isMobile', 'isTablet']),

            openAuthorizationPopup(popupMode: string, authorizationMode: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(popupMode);
                this.setAuthorizationPopupMode(authorizationMode);
            },

            moveToTheLink(this: any, link: string) {
                this.isUserPanelVisible = false;
                this.$router.push(link);
            },

            toggleUserPanelMobile(this: any) {
                if (this.isMobile() || this.isTablet()) this.isUserPanelVisible = !this.isUserPanelVisible;
            },

            showUserPanel(this: any, event: Event) {
                if (!this.isMobile() && !this.isTablet()) {
                    let target = event.target;
                    if (target === this.$refs.userPanel || target === this.$refs.userPanelButton) this.clearUserPanelTimeout(this.userPanelTimeout);
                    this.isUserPanelVisible = true;
                }
            },

            hideUserPanel() {
                if (!this.isMobile() && !this.isTablet()) this.setUserPanelTimeout();
            },

            setUserPanelTimeout(this: any) {
                this.userPanelTimeout = setTimeout(() => this.isUserPanelVisible = false, 500);
            },

            clearUserPanelTimeout(this: any) {
                this.userPanelTimeout = clearTimeout(this.userPanelTimeout);
            },

            openInfoPopup(this: any) {
                this.setPopupVisibility(true);
                this.setPopupMode('why-disabled');
            }
        },

        mounted() {
            this.$router.beforeEach((to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
                this.clearUserPanelTimeout();
                this.isUserPanelVisible = false;
                next();
            })
        }
    })
</script>