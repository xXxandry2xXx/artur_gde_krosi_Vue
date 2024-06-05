import type { MutationTree } from 'vuex';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const mutations: MutationTree<AuthorizationState> = {

    setAuthorizationPopupMode(state, mode: string) {
        state.loginPopupMode = mode;
    }, 

    setLogin(state, loginInput) {
        state.loginUserData.login = loginInput.target.value;
    },

    setLoginCorrectness(state, isCorrect: boolean) {
        state.isCorrectLogIn.logLogin.status = isCorrect;
    },

    setLoginMessage(state, message: string) {
        state.isCorrectLogIn.logLogin.message = message;
    },

    setPassword(state, passwordInput) {
        state.loginUserData.password = passwordInput.target.value;
    },

    setPasswordCorrectness(state, isCorrect: boolean) {
        state.isCorrectLogIn.logPassword.status = isCorrect;
    },

    setPasswordMessage(state, message: string) {
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

    setRegUsernameCorrectness(state, isCorrect: boolean) {
        state.isCorrectRegistration.regUsername.status = isCorrect;
    },

    setRegUsernameMessage(state, message: string) {
        state.isCorrectRegistration.regUsername.message = message;
    },

    setRegEmailCorrectness(state, isCorrect: boolean) {
        state.isCorrectRegistration.regEmail.status = isCorrect;
    },

    setRegEmailMessage(state, message: string) {
        state.isCorrectRegistration.regEmail.message = message;
    },

    setRegNameCorrectness(state, status) {
        state.isCorrectRegistration.regName.status = status;
    },

    setRegNameMessage(state, message) {
        state.isCorrectRegistration.regName.message = message;
    },

    setRegPasswordCorrectness(state, isCorrect: boolean) {
        state.isCorrectRegistration.regPassword.status = isCorrect;
    },

    setRegPasswordMessage(state, message: string) {
        state.isCorrectRegistration.regPassword.message = message;
    },

    setRegPasswordConfirmationCorrectness(state, isCorrect: boolean) {
        state.isCorrectRegistration.regPasswordConfirmation.status = isCorrect;
    },

    setRegPasswordConfirmationMessage(state, message: string) {
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

    setLoginStatus(state, status: boolean) {
        state.succesfulyAuthorized.status = status;
    },

    setLoginStatusMessage(state, message: string) {
        state.succesfulyAuthorized.message = message;
    },

    setRegistrationStatus(state, status: boolean) {
        state.succesfulyRegistered.status = status;
    },

    setRegistrationStatusMessage(state, message: string) {
        state.succesfulyRegistered.message = message;
    },
}