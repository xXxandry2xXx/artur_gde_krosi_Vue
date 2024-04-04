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
        logLogin: true,
        logPassword: true,
        regUsername: true,
        regEmail: true,
        regSurname: true,
        regPatronymic: true,
        regPassword: true,
        regPasswordConfirmation: true
    }
}

const authorization: Module<AuthorizationState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default authorization;