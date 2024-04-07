import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';
import { mutations } from '@/store/modules/authorization/mutations';
import { getters } from '@/store/modules/authorization/getters';
import { actions } from '@/store/modules/authorization/actions';

const state: AuthorizationState = {
    loginUserData: {
        login: '',
        password: '',
    },

    registrationUserData: {
        username: '',
        email: '',
        name: '',
        surname: '',
        patronymic: '',
        password: '',
        passwordConfirmation: ''
    },

    isCorrect: {
        logLogin: {
            status: true,
            message: ''
        },
        logPassword: {
            status: true,
            message: ''
        },
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
        }
    }
}

const authorization: Module<AuthorizationState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default authorization;