<template>
    <div class="authorization-section" v-show="$store.state.authorization.loginPopupMode === 'log-in'">
        <h3>Вход</h3>
        <div class="authorization-section-fields">
            <div class="authorization-popup-field-wrapper">
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectLogIn.logLogin.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                    <DefaultInput placeholder="Логин или E-mail" @input="setLogin" />
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectLogIn.logLogin.message }}</p>
            </div>
            <div class="authorization-popup-field-wrapper">
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectLogIn.logPassword.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                    <DefaultInput placeholder="Пароль" type="password" @input="setPassword" />
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectLogIn.logPassword.message }}</p>
            </div>
        </div>
        <div class="authoriization-parameters">
            <CheckboxItem :checked="$store.state.authorization.loginUserData.rememberUser" @change="setRememberUser()">Запомнить меня</CheckboxItem>
            <span class="authorization-forget-password-button">Забыли пароль?</span>
        </div>
        <BorderedButton class="authorization-popup-button" @click="logInUser">Войти</BorderedButton>

        <p class="authorization-alternative">Ещё нет аккаунта? <span @click="openAuthorizationPopup('registration')">Зарегистрируйтесь!</span></p>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions, mapGetters } from 'vuex';

    export default defineComponent({
        methods: {
            ...mapMutations(['setAuthorizationPopupVisibility',
                'openAuthorizationPopup',
                'setLogin',
                'setPassword',
                'setRememberUser'
            ]),  
            ...mapActions(['logInToAccount', 'validateLogin', 'validateLogInPassword']),
            ...mapGetters(['logInStatus']),

            logInUser(this: any) {
                this.validateLogin();
                this.validateLogInPassword();
                if (this.logInStatus()) {
                    this.logInToAccount().then((response: any) => this.storeAuthorizedUserData(response.data.user, response.data.token.result));
                    location.reload();
                }
            },

            storeAuthorizedUserData(userData: any, token: string) {
                localStorage.setItem('userData', JSON.stringify(userData));
                localStorage.setItem('token', JSON.stringify(token));
            }
        }
    })
</script>