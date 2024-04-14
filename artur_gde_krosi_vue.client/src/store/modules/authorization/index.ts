import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';
import { mutations } from '@/store/modules/authorization/mutations';
import { getters } from '@/store/modules/authorization/getters';
import { actions } from '@/store/modules/authorization/actions';

const state: AuthorizationState = {
    showLogInPopup: false,
    loginPopupMode: '',

    serverUserMessage: null,

    loginUserData: {
        login: '',
        password: '',
        rememberUser: false
    },

    registrationUserData: {
        username: '',
        email: '',
        name: '',
        surname: '',
        patronymic: '',
        password: '',
        passwordConfirmation: '',
        emailNewsletter: false
    },

    isCorrectLogIn: {
        logLogin: {
            status: true,
            message: ''
        },
        logPassword: {
            status: true,
            message: ''
        },
    },

    isCorrectRegistration: {
        regUsername: {
            status: true,
            message: ''
        },
        regEmail: {
            status: true,
            message: ''
        },
        regPassword: {
            status: true,
            message: ''
        },
        regPasswordConfirmation: {
            status: true,
            message: ''
        },
    },

    succesfulyAuthorized: false,
    userDoesNotExist: true,
}

const authorization: Module<AuthorizationState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default authorization;