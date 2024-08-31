<template>
    <aside class="user-account-side-panel">
        <h2>{{ $store.state.authorizedUser.userName }}</h2>
        <div class="user-account-buttons">
            <div class="user-account-button" @click="$router.push('/account')">
                <font-awesome-icon :icon="['fas', 'user']" />
                <span class="user-account-button-text">Личные данные</span>
            </div>
            <div class="user-account-button">
                <font-awesome-icon :icon="['fas', 'box']" />
                <span class="user-account-button-text" @click="openPopup">Заказы</span>
            </div>
            <div class="user-account-button logout-button" @click="logout()">
                <span><font-awesome-icon :icon="['fas', 'right-from-bracket']" /></span>
                <span class="user-account-button-text">Выход</span>
            </div>
        </div>
    </aside>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapActions, mapMutations } from 'vuex';


    export default defineComponent({
        methods: {
            ...mapActions(['logout']),
            ...mapMutations(['setPopupVisibility', 'setPopupMode']),

            openPopup(this: any) {
                this.setPopupVisibility(true);
                this.setPopupMode('why-disabled');
            }
        },
    })
</script>