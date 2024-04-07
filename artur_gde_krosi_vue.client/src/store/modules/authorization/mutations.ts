import type { MutationTree } from 'vuex';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const mutations: MutationTree<AuthorizationState> = {
    setLogin(state, loginInput) {
        state.loginUserData.login = loginInput.target.value;
    },

    setPassword(state, passwordInput) {
        state.loginUserData.password = passwordInput.target.value;
    },

    setRegEmail(state, emailInput) {
        state.registrationUserData.email = emailInput.target.value;
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

    setRegUsernameCorrectness(state, isCorrect) {
        state.isCorrect.regUsername.status = isCorrect;
    },

    setRegUsernameMessage(state, message) {
        state.isCorrect.regUsername.message = message;
    },

    setRegEmailCorrectness(state, isCorrect) {
        state.isCorrect.regEmail.status = isCorrect;
    },

    setRegEmailMessage(state, message) {
        state.isCorrect.regEmail.message = message;
    },

    setRegPasswordCorrectness(state, isCorrect) {
        state.isCorrect.regPassword.status = isCorrect;
    },

    setRegPasswordMessage(state, message) {
        state.isCorrect.regPassword.message = message;
    },

    setRegPasswordConfirmationCorrectness(state, isCorrect) {
        state.isCorrect.regPasswordConfirmation.status = isCorrect;
    },

    setRegPasswordConfirmationMessage(state, message) {
        state.isCorrect.regPasswordConfirmation.message = message;
    },
}