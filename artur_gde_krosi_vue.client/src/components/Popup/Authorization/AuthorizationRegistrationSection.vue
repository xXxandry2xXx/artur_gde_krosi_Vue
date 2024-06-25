<template>
    <div class="authorization-section">
        <h3>Регистрация</h3>
        <div class="authorization-section-fields">
            <div class="authorization-popup-field-wrapper">
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectRegistration.regEmail.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'at']" /></span>
                    <DefaultInput placeholder="E-mail" @input="handleEmail" />
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectRegistration.regEmail.message }}</p>
                <span class="required-field-indicator">*</span>
            </div>
            <div class="authorization-popup-field-wrapper">
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectRegistration.regUsername.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                    <DefaultInput placeholder="Логин" @input="handleUserName" />
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectRegistration.regUsername.message }}</p>
                <span class="required-field-indicator">*</span>
            </div>
            <div class="authorization-personal-data">
                <div class="authorization-popup-field-wrapper">
                    <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectRegistration.regName.status === false }">
                        <span><font-awesome-icon :icon="['fas', 'user']" /></span>
                        <DefaultInput placeholder="Имя" @input="handleName" />
                        <div class="required-field-indicator">*</div>
                    </div>
                    <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectRegistration.regName.message }}</p>
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
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectRegistration.regPassword.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                    <DefaultInput placeholder="Пароль" :type="passwordInputType" @input="handlePassword" />
                    <div class="toggle-password-button" @click="togglePassword">
                        <font-awesome-icon :icon="['fas', 'eye']" v-if="showPassword === false" />
                        <font-awesome-icon :icon="['fas', 'eye-slash']" v-else />
                    </div>
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectRegistration.regPassword.message }}</p>
                <span class="required-field-indicator">*</span>
            </div>
            <div class="authorization-popup-field-wrapper">
                <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': $store.state.authorization.isCorrectRegistration.regPasswordConfirmation.status === false }">
                    <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                    <DefaultInput placeholder="Подтверждение пароля" :type="passwordInputType" @input="handlePasswordConfirmation" />
                </div>
                <p class="authorization-popup-field-message">{{ $store.state.authorization.isCorrectRegistration.regPasswordConfirmation.message }}</p>
                <span class="required-field-indicator">*</span>
            </div>
            <p class="authorization-common-message" v-if="$store.state.authorization.succesfulyRegistered.message !== ''">
                {{ $store.state.authorization.succesfulyRegistered.message }}
            </p>
        </div>
        <CheckboxItem class="authorization-newsletter-checkbox" :checked="$store.state.authorization.registrationUserData.emailNewsletter" @change="setEmailNewsletter()">
            Отправлять мне уведомления об акциях и скидках
        </CheckboxItem>
        <BorderedButton class="authorization-popup-button" @click="registerUser">Зарегистрироваться</BorderedButton>
        <p class="authorization-alternative">Уже есть аккаунт? <span @click="setAuthorizationPopupMode('log-in')">Войдите!</span></p>
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
            ...mapMutations(
                [
                    'setAuthorizationPopupMode',
                    'setRegEmail',
                    'setRegUsername',
                    'setRegName',
                    'setRegSurname',
                    'setRegPatronymic',
                    'setRegPassword',
                    'setRegPasswordConfirmation',
                    'setEmailNewsletter',
                    'resetRegistrationFields'
                ]),

            ...mapActions(
                [
                    'registerNewUser',
                    'validateUserName',
                    'validateEmail',
                    'validateName',
                    'validatePassword',
                    'validatePasswordMatching',
                    'sendConfirmationEmail'
                ]),

            ...mapGetters(['registrationCorrectnessStatus', 'registrationStatus', 'getRegistrationUserData']),

            async registerUser() {
                this.validateFields();

                if (this.registrationCorrectnessStatus()) {
                    await this.registerNewUser();
                    if (this.registrationStatus()) {
                        this.sendConfirmationEmail();
                        this.resetRegistrationFields();
                    }
                }
            },

            validateFields() {
                this.validateUserName();
                this.validateEmail();
                this.validateName(this.getRegistrationUserData().name);
                this.validatePassword();
                this.validatePasswordMatching();
            },

            handleEmail(event: Event) {
                this.setRegEmail(event);
                this.validateEmail();
            },

            handleName(event: Event) {
                this.setRegName(event);
                this.validateName(this.getRegistrationUserData().name);
            },

            handleUserName(event: Event) {
                this.setRegUsername(event);
                this.validateUserName();
            },

            handlePassword(event: Event) {
                this.setRegPassword(event);
                this.validatePassword();
                this.validatePasswordMatching();
            },

            handlePasswordConfirmation(event: Event) {
                this.setRegPasswordConfirmation(event);
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
        }
    })
</script>