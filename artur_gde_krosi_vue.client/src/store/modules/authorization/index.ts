import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';
import { mutations } from '@/store/modules/authorization/mutations';
import { getters } from '@/store/modules/authorization/getters';
import { actions } from '@/store/modules/authorization/actions';

const state: AuthorizationState = {
    showLogInPopup: false,
    loginPopupMode: '',

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
            status: null,
            message: ''
        },
        logPassword: {
            status: null,
            message: ''
        },
    },

    isCorrectRegistration: {
        regUsername: {
            status: null,
            message: ''
        },
        regEmail: {
            status: null,
            message: ''
        },
        regName: {
            status: null,
            message: ''
        },
        regPassword: {
            status: null,
            message: ''
        },
        regPasswordConfirmation: {
            status: null,
            message: ''
        },
    },

    succesfulyAuthorized: {
        status: null,
        message: ''
    },

    succesfulyRegistered: {
        status: null,
        message: ''
    }
}

const authorization: Module<AuthorizationState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default authorization;