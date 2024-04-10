import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const getters: GetterTree<AuthorizationState, RootState> = {

    getLogInUserData: (state) => {
        return state.loginUserData;
    },

    getRegistrationUserData: (state) => {
        return state.registrationUserData;
    },

    getRegistrationFormData: (state) => {
        const form = new FormData();

        form.append('Username', state.registrationUserData.username.toString());
        form.append('Email', state.registrationUserData.email.toString());
        form.append('Password', state.registrationUserData.password.toString());
        form.append('name', state.registrationUserData.name.toString());
        form.append('surname', state.registrationUserData.surname.toString());
        form.append('patronymic', state.registrationUserData.patronymic.toString());
        form.append('sendingMail', state.registrationUserData.emailNewsletter.toString());

        return form;
    },

    logInStatus: (state) => {
        const logInDataCorrectness = state.isCorrectLogIn;
        const succes = Object.values(logInDataCorrectness).every(condition => condition.status === true);

        return succes;
    },

    registrationStatus: (state) => {
        const registrationDataCorrectnes = state.isCorrectRegistration;
        const succes = Object.values(registrationDataCorrectnes).every(condition => condition.status === true);

        return succes;
    },
}