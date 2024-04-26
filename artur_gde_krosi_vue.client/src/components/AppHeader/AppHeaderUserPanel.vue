<template>
    <div class="authorized-user-panel" @click="isUserPanelVisible = !isUserPanelVisible" v-if="isUserAuthorized()">
        <p>{{ $store.state.authorizedUser.userName }}</p>
        <div class="user-dropdown" v-show="isUserPanelVisible">
            <div class="user-account-button" @click="$router.push('/account')">
                <font-awesome-icon :icon="['fas', 'user']" />
                <span class="user-account-button-text">Личные данные</span>
            </div>
            <div class="user-account-button">
                <font-awesome-icon :icon="['fas', 'box']" />
                <span class="user-account-button-text">Заказы</span>
            </div>
            <div class="user-account-button logout-button">
                <span><font-awesome-icon :icon="['fas', 'right-from-bracket']" /></span>
                <span class="user-account-button-text" @click="logout()">Выход</span>
            </div>
        </div>
    </div>

    <div class="autorization-buttons" v-else>
        <button class="autorization-button" @click="openAuthorizationPopup('authorization', 'log-in')">Вход</button>
        <span>или</span>
        <button class="autorization-button" @click="openAuthorizationPopup('authorization', 'registration')">Регистрация</button>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters, mapActions } from 'vuex';

    export default defineComponent({

        data() {
            return {
                isUserPanelVisible: false,
            }
        },

        methods: {
            ...mapActions(['logout']),
            ...mapMutations(['setPopupVisibility', 'setPopupMode', 'setAuthorizationPopupMode']),
            ...mapGetters(['isUserAuthorized']),

            openAuthorizationPopup(popupMode: string, authorizationMode: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(popupMode);
                this.setAuthorizationPopupMode(authorizationMode);
            },
        },
    })
</script>