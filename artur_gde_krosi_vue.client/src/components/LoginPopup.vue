<template>
    <div class="log-in-popup">
        <div class="log-in-popup-background" @click="setLogInPopupVisibility(false)"></div>
        <div class="log-in-popup-content">
            <div class="log-in-section" v-if="$store.state.loginPopupMode === 'log-in'">
                <h3>Вход</h3>
                <DefaultInput class="login-popup-field" placeholder="Логин" />
                <DefaultInput class="login-popup-field" placeholder="Пароль" />
                <BorderedButton class="log-in-button">Вход</BorderedButton>

                <div class="another-way-to-log-in">
                    <span>ИЛИ</span>
                    <button class="another-way-to-log-in-button" @click="openLoginPopup('registration')">Регистрация</button>
                </div>
            </div>
            <div class="log-in-section" v-else="$store.state.loginPopupMode === 'registration'">
                <h3>Регистрация</h3>
                <DefaultInput class="login-popup-field" placeholder="Логин" />
                <DefaultInput class="login-popup-field" placeholder="Пароль" />
                <DefaultInput class="login-popup-field" placeholder="Подтверждение пароля" />
                <BorderedButton class="log-in-button">Регистрация</BorderedButton>

                <div class="another-way-to-log-in">
                    <span>ИЛИ</span>
                    <button class="another-way-to-log-in-button" @click="openLoginPopup('log-in')">Войти</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex'

    export default defineComponent({
        methods: {
            ...mapMutations(['setLogInPopupVisibility', 'openLoginPopup'])
        }
    })

</script> 