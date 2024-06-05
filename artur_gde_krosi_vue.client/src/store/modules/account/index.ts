import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserAccountState } from '@/store/modules/account/types';
import { mutations } from '@/store/modules/account/mutations';
import { getters } from '@/store/modules/account/getters';
import { actions } from '@/store/modules/account/actions';

const state: UserAccountState = {
    editableUserData: {
        name: '',
        surname: '',
        patronymic: '',
        sendingMail: ''
    },

    isSucces: false,
    succesMessage: ''
}

const account: Module<UserAccountState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default account;