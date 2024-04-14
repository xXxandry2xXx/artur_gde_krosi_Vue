<template>
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
            ...mapMutations(['openAuthorizationPopup']),
            ...mapGetters(['isUserAuthorized']),

            userLogout(this: any) {
                this.logout();
                location.reload();
            }
        },
    })
</script>