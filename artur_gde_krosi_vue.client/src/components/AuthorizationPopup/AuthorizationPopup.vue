<template>
    <div class="authorization-popup">
        <div class="authorization-popup-background" @click="setLogInPopupVisibility(false)"></div>
        <div class="authorization-popup-content">
            <!--<BorderedButton class="authorization-popup-button">Вход</BorderedButton>
            <BorderedButton class="authorization-popup-button">Регистрация</BorderedButton>-->
            <div>
                <div class="authorization-section" v-show="$store.state.loginPopupMode === 'log-in'">
                    <h3>Вход</h3>
                    <div class="authorization-section-fields">
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                            <DefaultInput placeholder="Логин" @input="setLogin"/>
                        </div>
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                            <DefaultInput placeholder="Пароль" type="password" @input="setPassword"/>
                        </div>
                    </div>
                    <div class="authoriization-parameters">
                        <CheckboxItem>Запомнить меня</CheckboxItem>
                        <span class="authorization-forget-password-button">Забыли пароль?</span>
                    </div>
                    <BorderedButton class="authorization-popup-button">Войти</BorderedButton>
                </div>

                <div class="authorization-section" v-show="$store.state.loginPopupMode === 'registration'">
                    <h3>Регистрация</h3>
                    <div class="authorization-section-fields">
                        <div class="authorization-popup-field-wrapper">
                            <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrect.regEmail.status === false }">
                                <span><font-awesome-icon :icon="['fas', 'at']" /></span>
                                <DefaultInput placeholder="E-mail" @input="setRegEmail" />
                            </div>
                            <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrect.regEmail.message }}</p>
                        </div>
                        <div class="authorization-popup-field-wrapper">
                            <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrect.regUsername.status === false }">
                                <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                <DefaultInput placeholder="Логин" @input="setRegUsername" />
                            </div>
                            <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrect.regUsername.message }}</p>
                        </div>
                        <div class="authorization-personal-data">
                            <div class="authorization-popup-field-wrapper">
                                <div class="authorization-popup-field">
                                    <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                    <DefaultInput placeholder="Имя" @input="setRegName" />
                                </div>
                            </div>
                            <div class="authorization-popup-field-wrapper">
                                <div class="authorization-popup-field">
                                    <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                    <DefaultInput placeholder="Фамилия" @input="setRegSurname" />
                                </div>
                            </div>
                        </div>
                        <div class="authorization-popup-field-wrapper">
                            <div class="authorization-popup-field">
                                <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                <DefaultInput placeholder="Отчество" @input="setRegPatronymic" />
                            </div>
                        </div>
                        <div class="authorization-popup-field-wrapper">
                            <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrect.regPassword.status === false }">
                                <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                                <DefaultInput placeholder="Пароль" :type="passwordInputType" @input="setRegPassword" />
                                <div class="toggle-password-button" @click="togglePassword">
                                    <font-awesome-icon :icon="['fas', 'eye']" v-if="showPassword === false" />
                                    <font-awesome-icon :icon="['fas', 'eye-slash']" v-else />
                                </div>
                            </div>
                            <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrect.regPassword.message }}</p>
                        </div>
                        <div class="authorization-popup-field-wrapper">
                            <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrect.regPasswordConfirmation.status === false }">
                                <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                                <DefaultInput placeholder="Подтверждение пароля" :type="passwordInputType" @input="setRegPasswordConfirmation" />
                            </div>
                            <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrect.regPasswordConfirmation.message }}</p>
                        </div>
                    </div>
                    <BorderedButton class="authorization-popup-button" @click="registerUser">Зарегистрироваться</BorderedButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters, mapActions } from 'vuex';

    export default defineComponent({

        data() {
            return {
                showPassword: false,
                passwordInputType: 'password'
            }
        },

        methods: {
            ...mapMutations(['setLogInPopupVisibility', 
            'openLoginPopup',
            'setLogin',
            'setPassword',
            'setRegEmail',
            'setRegUsername',
            'setRegName',
            'setRegSurname',
            'setRegPatronymic',
            'setRegPassword',
            'setRegPasswordConfirmation'
            ]),
            ...mapActions([
                'registerNewUser',
                'validateUserName',
                'validateEmail',
                'validatePassword',
                'validatePasswordMatching'
            ]),

            registerUser() {
                //this.registerNewUser().then((response: any) => console.log(response.data));
                this.validateUserName();
                this.validateEmail();
                this.validatePassword();
                this.validatePasswordMatching();
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
        },
    })
</script> 