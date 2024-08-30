import axios from 'axios';
import apiUrl from '@/helper'
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';


export const actions: ActionTree<AuthorizationState, RootState> = {
    async registerNewUser() {
        const form = this.getters.getRegistrationFormData;
        try {
            const response = await axios.post(apiUrl + '/identity/Authorize/Register', form, { headers: { 'accept': '*/*', 'Content-Type': 'multipart/form-data' } });
            if (response.status === 200) {
                this.commit('setRegistrationStatus', true);
                this.commit('setRegistrationStatusMessage', '');
            }

            return response;
        } catch (error: any) {
            this.commit('setRegistrationStatusMessage', error.response.data.message);
        }
    },

    async sendConfirmationEmail() {
        let email = this.getters.getRegistrationUserData.email;
        try {
            const response = await axios.get(apiUrl + '/identity/SetingsUser/GenerateTokenOnRegEmail', {
                params: {
                    'email': email
                },
                headers: {
                    'accept': '*/*'
                }
            });
            return response;
        } catch (error) {
            console.log(error);
        }
    },

    async logInToAccount() {
        let loginUserData = this.getters.getLogInUserData;
        try {
            const response = await axios.get(apiUrl + '/identity/Authorize/Login', {
                params: {
                    'usernameOrEmail': loginUserData.login,
                    'password': loginUserData.password
                },
                headers: {
                    'accept': '*/*'
                }
            });

            if (response.status === 200) {
                this.commit('setLoginStatus', true);
            }
            return response;
        } catch (error: any) {
            this.commit('setLoginStatus', false);
            this.commit('setLoginStatusMessage', error.response.data.message);
        }
    },

    logout() {
        localStorage.removeItem('token');
        localStorage.removeItem('userData');
        location.reload();
    },

    validateName({ state }: { state: AuthorizationState }, validatableName) {
        if (validatableName.length > 0) {
            this.commit('setRegNameCorrectness', true);
            this.commit('setRegNameMessage', '');
        } else {
            this.commit('setRegNameCorrectness', false);
            this.commit('setRegNameMessage', 'Заполните обязательное поле');
        }
    },

    validateLogin({ state }: { state: AuthorizationState }) {
        let login = state.loginUserData.login;
        if (login === '') {
            this.commit('setLoginCorrectness', false);
            this.commit('setLoginMessage', 'Заполните обязательное поле');
            return
        }

        this.commit('setLoginCorrectness', true);
        this.commit('setLoginMessage', '');
    },

    validateLogInPassword({ state }: { state: AuthorizationState }) {
        let password = state.loginUserData.password;
        if (password === '') {
            this.commit('setPasswordCorrectness', false);
            this.commit('setPasswordMessage', 'Заполните обязательное поле');
            return
        }

        this.commit('setPasswordCorrectness', true);
        this.commit('setPasswordMessage', '');
    },

    validateUserName({ state }: { state: AuthorizationState }) {
        let username = state.registrationUserData.username;
        let usernamePattern = /^[a-zA-Z0-9]+$/;
        if (username === '') {
            this.commit('setRegUsernameCorrectness', false);
            this.commit('setRegUsernameMessage', 'Заполните обязательное поле');
            return
        }
        if (username.length <= 5 || username.length > 15) {
            this.commit('setRegUsernameCorrectness', false);
            this.commit('setRegUsernameMessage', 'Имя пользователя должно быть длиннее 5 и короче 15 символов');
        } else if (!usernamePattern.test(username)) {
            this.commit('setRegUsernameCorrectness', false);
            this.commit('setRegUsernameMessage', 'Имя пользователя должно состоять из цифр и латинских букв');
        } else {
            this.commit('setRegUsernameCorrectness', true);
            this.commit('setRegUsernameMessage', '');
        }
    },

    validateEmail({ state }: { state: AuthorizationState }) {
        let email = state.registrationUserData.email;
        let emailPattern = /^[^ ]+@[^ ]+\.[a-z]{2,3}$/;
        if (email === '') {
            this.commit('setRegEmailCorrectness', false);
            this.commit('setRegEmailMessage', 'Заполните обязательное поле');
            return
        }
        if (!emailPattern.test(email)) {
            this.commit('setRegEmailCorrectness', false);
            this.commit('setRegEmailMessage', 'Введите корректный E-mail');
        } else {
            this.commit('setRegEmailCorrectness', true);
            this.commit('setRegEmailMessage', '');
        }
    },

    validatePassword({ state }: { state: AuthorizationState }) {
        let password = state.registrationUserData.password;
        let passwordPattern = /^(?=.*[A-Z])(?=.*\d).+$/;
        let passwordNonAlphanumericPattern = /.*[!@#$%^&*()-+=\\[\]{};:'",<.>/?].*/;
        if (password === '') {
            this.commit('setRegPasswordCorrectness', false);
            this.commit('setRegPasswordMessage', 'Заполните обязательное поле');
            return
        }
        if (password.length < 6) {
            this.commit('setRegPasswordCorrectness', false);
            this.commit('setRegPasswordMessage', 'Пароль должен быть не короче 6 символов');
        } else if (!passwordNonAlphanumericPattern.test(password)) {
            this.commit('setRegPasswordCorrectness', false);
            this.commit('setRegPasswordMessage', 'Пароль должен включать спецсимвол');
        } else if (!passwordPattern.test(password)) {
            this.commit('setRegPasswordCorrectness', false);
            this.commit('setRegPasswordMessage', 'Пароль должен включать хотя бы одну заглавную букву и хотя бы одну цифру');
        } else {
            this.commit('setRegPasswordCorrectness', true);
            this.commit('setRegPasswordMessage', '');
        }
    },

    validatePasswordMatching({ state }: { state: AuthorizationState }) {
        let password = state.registrationUserData.password;
        let passwordConfirmation = state.registrationUserData.passwordConfirmation;
        if (passwordConfirmation === '') {
            this.commit('setRegPasswordConfirmationCorrectness', false);
            this.commit('setRegPasswordConfirmationMessage', 'Заполните обязательное поле');
            return
        }

        if (password === passwordConfirmation) {
            this.commit('setRegPasswordConfirmationCorrectness', true);
            this.commit('setRegPasswordConfirmationMessage', '');
        } else {
            this.commit('setRegPasswordConfirmationCorrectness', false);
            this.commit('setRegPasswordConfirmationMessage', 'Пароли не совпадают');
        }
    }
}

