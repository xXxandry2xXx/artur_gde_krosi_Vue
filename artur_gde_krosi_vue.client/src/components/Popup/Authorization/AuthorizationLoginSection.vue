<template>
    <div class="authorization-section">
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
                    <DefaultInput placeholder="Пароль" :type="passwordInputType" @input="setPassword" />
                    <div class="toggle-password-button" @click="togglePassword">
                        <font-awesome-icon :icon="['fas', 'eye']" v-if="showPassword === false" />
                        <font-awesome-icon :icon="['fas', 'eye-slash']" v-else />
                    </div>
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectLogIn.logPassword.message }}</p>
            </div>
        </div>
        <p class="authorization-common-message" v-if="$store.state.authorization.succesfulyAuthorized.message !== ''">
            {{ $store.state.authorization.succesfulyAuthorized.message }}
        </p>
        <div class="authoriization-parameters">
            <CheckboxItem :checked="$store.state.authorization.loginUserData.rememberUser" @change="setRememberUser()">Запомнить меня</CheckboxItem>
            <span class="authorization-forget-password-button">Забыли пароль?</span>
        </div>
        <BorderedButton class="authorization-popup-button" @click="logInUser">Войти</BorderedButton>

        <p class="authorization-alternative">Ещё нет аккаунта? <span @click="setAuthorizationPopupMode('registration')">Зарегистрируйтесь!</span></p>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapActions, mapGetters } from 'vuex';

    export default defineComponent({

        data() {
            return {
                showPassword: false,
                passwordInputType: 'password'
            }
        },

        methods: {
            ...mapMutations(['setAuthorizationPopupMode', 'setLogin', 'setPassword', 'setRememberUser']),
            ...mapActions(['logInToAccount', 'validateLogin', 'validateLogInPassword']),
            ...mapGetters(['logInCorrectnessStatus', 'logInStatus']),

            logInUser(this: any) {
                this.validateLogin();
                this.validateLogInPassword();

                if (this.logInCorrectnessStatus()) {
                    this.logInToAccount().then((response: any) => {
                        if (this.logInStatus()) this.storeAndConfirmAuthorizedUserData(response.data.result, response.data.token.result);
                    });
                }
            },

            storeAndConfirmAuthorizedUserData(userData: any, token: string) {
                localStorage.setItem('userData', JSON.stringify(userData));
                localStorage.setItem('token', JSON.stringify(token));
                location.reload();
            },

            togglePassword(this: any) {
                if (this.showPassword === false) {
                    this.passwordInputType = 'text';
                    this.showPassword = true;
                } else {
                    this.passwordInputType = 'password';
                    this.showPassword = false;
                }
            }
        }
    })
</script>