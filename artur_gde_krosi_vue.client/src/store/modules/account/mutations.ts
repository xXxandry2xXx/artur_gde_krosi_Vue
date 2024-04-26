import type { MutationTree } from 'vuex';
import type { UserAccountState } from '@/store/modules/account/types';

export const mutations: MutationTree<UserAccountState> = {
    setNewUserName(state, value: string) {
        state.editableUserData.name = value;
    },

    setNewUserSurname(state, value: string) {
        state.editableUserData.surname = value;
    },

    setNewUserPatronymic(state, value: string) {
        state.editableUserData.patronymic = value;
    },

    setUserNewsletterStatus(state, value: boolean) {
        state.editableUserData.sendingMail = value.toString();
    },

    setSuccesMessage(state, message) {
        state.succesMessage = message;
    }
}