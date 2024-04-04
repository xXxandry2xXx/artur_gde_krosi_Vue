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
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'at']" /></span>
                            <DefaultInput placeholder="E-mail" @input="setRegistrationEmail"/>
                        </div>
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                            <DefaultInput placeholder="Логин" @input="setRegistrationUsername"/>
                        </div>
                        <div class="authorization-personal-data">
                            <div class="authorization-popup-field">
                                <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                <DefaultInput placeholder="Имя" @input="setRegistrationName"/>
                            </div>
                            <div class="authorization-popup-field">
                                <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                                <DefaultInput placeholder="Фамилия" @input="setRegistrationSurname"/>
                            </div>
                        </div>
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                            <DefaultInput placeholder="Отчество" @input="setRegistrationPatronymic"/>
                        </div>
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                            <DefaultInput placeholder="Пароль" type="password" @input="setRegistrationPassword"/>
                        </div>
                        <div class="authorization-popup-field">
                            <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                            <DefaultInput placeholder="Подтверждение пароля" type="password" @input="setRegistrationPasswordConfirmation"/>
                        </div>
                    </div>
                    <BorderedButton class="authorization-popup-button">Зарегистрироваться</BorderedButton>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters } from 'vuex';

    export default defineComponent({

        methods: {
            ...mapMutations(['setLogInPopupVisibility', 
            'openLoginPopup',
            'setLogin',
            'setPassword',
            'setRegistrationEmail',
            'setRegistrationUsername',
            'setRegistrationName',
            'setRegistrationSurname',
            'setRegistrationPatronymic',
            'setRegistrationPassword',
            'setRegistrationPasswordConfirmation'
        ]),
        },

        mounted() {

        }
    })
</script> 