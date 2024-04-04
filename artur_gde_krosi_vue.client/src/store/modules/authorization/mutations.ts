import type { MutationTree } from 'vuex';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const mutations: MutationTree<AuthorizationState> = {
    setLogin(state, loginInput) {
        state.loginUserData.login = loginInput.target.value;
    },

    setPassword(state, passwordInput) {
        state.loginUserData.password = passwordInput.target.value;
    },

    setRegistrationEmail(state, emailInput) {
        state.registrationUserData.email = emailInput.target.value;
    },

    setRegistrationUsername(state, usernameInput) {
        state.registrationUserData.username = usernameInput.target.value;;
    },

    setRegistrationName(state, nameInput) {
        state.registrationUserData.name = nameInput.target.value;;
    },

    setRegistrationSurname(state, surnameInput) {
        state.registrationUserData.surname = surnameInput.target.value;;
    },

    setRegistrationPatronymic(state, patronymicInput) {
        state.registrationUserData.patronymic = patronymicInput.target.value;;
    },

    setRegistrationPassword(state, passwordInput) {
        state.registrationUserData.password = passwordInput.target.value;;
    },

    setRegistrationPasswordConfirmation(state, passwordConfirmationInput) {
        state.registrationUserData.passwordConfirmation = passwordConfirmationInput.target.value;;
    }
}