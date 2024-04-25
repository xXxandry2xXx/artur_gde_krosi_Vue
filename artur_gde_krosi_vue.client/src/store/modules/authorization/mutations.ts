import type { MutationTree } from 'vuex';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const mutations: MutationTree<AuthorizationState> = {
    setAuthorizationPopupVisibility(state, value: boolean) {
        state.showLogInPopup = value;
        state.succesfulyRegistered.status = false;
    },

    openAuthorizationPopup(state, mode: string) {
        state.showLogInPopup = true;
        state.loginPopupMode = mode;
    },

    setLogin(state, loginInput) {
        state.loginUserData.login = loginInput.target.value;
    },

    setLoginCorrectness(state, isCorrect) {
        state.isCorrectLogIn.logLogin.status = isCorrect;
    },

    setLoginMessage(state, message) {
        state.isCorrectLogIn.logLogin.message = message;
    },

    setPassword(state, passwordInput) {
        state.loginUserData.password = passwordInput.target.value;
    },

    setPasswordCorrectness(state, isCorrect) {
        state.isCorrectLogIn.logPassword.status = isCorrect;
    },

    setPasswordMessage(state, message) {
        state.isCorrectLogIn.logPassword.message = message;
    },

    setRegEmail(state, emailInput) {
        state.registrationUserData.email = emailInput.target.value;
    },

    setRememberUser(state) {
        state.loginUserData.rememberUser = !state.loginUserData.rememberUser;
    },

    setRegUsername(state, usernameInput) {
        state.registrationUserData.username = usernameInput.target.value;;
    },

    setRegName(state, nameInput) {
        state.registrationUserData.name = nameInput.target.value;;
    },

    setRegSurname(state, surnameInput) {
        state.registrationUserData.surname = surnameInput.target.value;;
    },

    setRegPatronymic(state, patronymicInput) {
        state.registrationUserData.patronymic = patronymicInput.target.value;;
    },

    setRegPassword(state, passwordInput) {
        state.registrationUserData.password = passwordInput.target.value;;
    },

    setRegPasswordConfirmation(state, passwordConfirmationInput) {
        state.registrationUserData.passwordConfirmation = passwordConfirmationInput.target.value;
    },

    setEmailNewsletter(state) {
        state.registrationUserData.emailNewsletter = !state.registrationUserData.emailNewsletter;
    },

    setRegUsernameCorrectness(state, isCorrect) {
        state.isCorrectRegistration.regUsername.status = isCorrect;
    },

    setRegUsernameMessage(state, message) {
        state.isCorrectRegistration.regUsername.message = message;
    },

    setRegEmailCorrectness(state, isCorrect) {
        state.isCorrectRegistration.regEmail.status = isCorrect;
    },

    setRegEmailMessage(state, message) {
        state.isCorrectRegistration.regEmail.message = message;
    },

    setRegPasswordCorrectness(state, isCorrect) {
        state.isCorrectRegistration.regPassword.status = isCorrect;
    },

    setRegPasswordMessage(state, message) {
        state.isCorrectRegistration.regPassword.message = message;
    },

    setRegPasswordConfirmationCorrectness(state, isCorrect) {
        state.isCorrectRegistration.regPasswordConfirmation.status = isCorrect;
    },

    setRegPasswordConfirmationMessage(state, message) {
        state.isCorrectRegistration.regPasswordConfirmation.message = message;
    },

    resetRegistrationFields(state) {
        state.registrationUserData = {
            username: '',
            email: '',
            name: '',
            surname: '',
            patronymic: '',
            password: '',
            passwordConfirmation: '',
            emailNewsletter: false
        }
    },

    setLoginStatus(state, status) {
        state.succesfulyAuthorized.status = status;
    },

    setLoginStatusMessage(state, message) {
        state.succesfulyAuthorized.message = message;
    },

    setRegistrationStatus(state, status) {
        state.succesfulyRegistered.status = status;
    },

    setRegistrationStatusMessage(state, message) {
        state.succesfulyRegistered.message = message;
    },
}