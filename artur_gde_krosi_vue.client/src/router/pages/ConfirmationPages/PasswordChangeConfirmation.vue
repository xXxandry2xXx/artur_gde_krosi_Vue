<template>
    <div class="confirmation-content" v-if="isAllowed">
        <div class="password-confirmation-content" v-if="!succesfulyChanged">
            <h1>Смена пароля</h1>
            <div class="password-confirmation-fields">
                <div class="authorization-popup-field-wrapper">
                    <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': passwordCorrectness.status === false }">
                        <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                        <DefaultInput name="changePassord" placeholder="Новый пароль" :type="passwordInputType" v-model="newPassword" @input="validatePassword" />
                        <div class="toggle-password-button" @click="togglePassword">
                            <font-awesome-icon :icon="['fas', 'eye']" v-if="showPassword === false" />
                            <font-awesome-icon :icon="['fas', 'eye-slash']" v-else />
                        </div>
                    </div>
                    <p class="authorization-popup-field-message">{{ passwordCorrectness.message }}</p>
                </div>
                <div class="authorization-popup-field-wrapper">
                    <div class="authorization-popup-field" :class="{'authorization-popup-field-incorrect': passwordMatching.status === false }">
                        <span><font-awesome-icon :icon="['fas', 'key']" /></span>
                        <DefaultInput name="changePassordConfirmation"
                                      placeholder="Подтвердите новый пароль"
                                      :type="passwordInputType"
                                      v-model="newPasswordConfirmation"
                                      @input="validatePasswordMatching" />
                    </div>
                    <p class="authorization-popup-field-message">{{ passwordMatching.message }}</p>
                </div>
            </div>
            <BorderedButton class="confirm-popup-changes-button" @click="tryToConfirmNewPassword()">Сохранить</BorderedButton>
        </div>
        <div class="password-changed-succes" v-else-if="succesfulyChanged">
            <AnimatedCheckmark />
            <h2>Ваш пароль успешно изменен</h2>
            <p>Отлично! Вы успешно изменили пароль</p>
            <a href="/">На главную</a>
        </div>
    </div>
    <div v-else>
        <Preloader />
        <div style="height: 400px;"></div>
    </div>
</template>
<script lang="ts">
    import apiUrl from '@/helper'
    import axios from 'axios';
    import { defineComponent } from 'vue';

    export default defineComponent({
        props: ['email', 'token'],

        data() {
            return {
                isAllowed: false,
                succesfulyChanged: false,
                showPassword: false,
                passwordInputType: 'password',
                newPassword: '',
                newPasswordConfirmation: '',

                passwordCorrectness: {
                    status: true,
                    message: ''
                },
                passwordMatching: {
                    status: true,
                    message: ''
                },
            }
        },

        methods: {
            async checkTokenCorrectness(this: any) {
                try {
                    const response = await axios.put(
                        apiUrl + '/identity/SetingsUser/VerifyPasswordResetTokenAsync',
                        '',
                        {
                            params: {
                                'email': this.email,
                                'token': this.token
                            },
                            headers: {
                                'accept': '*/*'
                            }
                        }
                    );
                    if(response.status === 200) this.isAllowed = true;
                } catch (error) {
                    this.$router.push('/');
                }
            },

            async confirmNewPassword(this: any) {
                try {
                    const response = await axios.put(
                        apiUrl + '/identity/SetingsUser/PasswordReset',
                        '',
                        {
                            params: {
                                'email': this.email,
                                'token': this.token,
                                'newPassword': this.newPassword
                            },
                            headers: {
                                'accept': '*/*'
                            }
                        }
                    );
                    if (response.status === 200) this.succesfulyChanged = true;
                } catch (error) {
                    console.log(error);
                }
            },

            validatePassword(this: any) {
                let password = this.newPassword;
                let passwordPattern = /^(?=.*[A-Z])(?=.*\d).+$/;
                let passwordNonAlphanumericPattern = /.*[!@#$%^&*()-+=\\[\]{};:'",<.>/?].*/;
                if (password === '') {
                    this.passwordCorrectness.status = false;
                    this.passwordCorrectness.message = 'Заполните обязательное поле';
                    return
                }
                if (password.length < 6) {
                    this.passwordCorrectness.status = false;
                    this.passwordCorrectness.message = 'Пароль должен быть не короче 6 символов';
                } else if (!passwordNonAlphanumericPattern.test(password)) {
                    this.passwordCorrectness.status = false;
                    this.passwordCorrectness.message = 'Пароль должен включать спецсимвол';
                } else if (!passwordPattern.test(password)) {
                    this.passwordCorrectness.status = false;
                    this.passwordCorrectness.message = 'Пароль должен включать хотя бы одну заглавную букву и хотя бы одну цифру';
                } else {
                    this.passwordCorrectness.status = true;
                    this.passwordCorrectness.message = '';
                }
            },

            validatePasswordMatching(this: any) {
                let password = this.newPassword;
                let passwordConfirmation = this.newPasswordConfirmation;
                if (passwordConfirmation === '') {
                    this.passwordMatching.status = false;
                    this.passwordMatching.message = 'Необходимо подтвердить пароль';
                    return
                }

                if (password === passwordConfirmation) {
                    this.passwordMatching.status = true;
                    this.passwordMatching.message = '';
                } else {
                    this.passwordMatching.status = false;
                    this.passwordMatching.message = 'Пароли не совпадают';
                }
            },

            tryToConfirmNewPassword(this: any) {
                this.validatePassword();
                this.validatePasswordMatching();

                if (this.passwordCorrectness.status && this.passwordMatching.status) this.confirmNewPassword()
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

        mounted() {
            this.checkTokenCorrectness();
        }
    })
</script>