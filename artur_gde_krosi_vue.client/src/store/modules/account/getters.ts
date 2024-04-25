import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserAccountState } from '@/store/modules/account/types';

export const getters: GetterTree<UserAccountState, RootState> = {
    gatherUserFormData: (state) => {
        const data = new FormData();
        data.append('name', state.editableUserData.name.toString());
        data.append('surname', state.editableUserData.surname.toString());
        data.append('patronymic', state.editableUserData.patronymic.toString());
        data.append('sendingMail', state.editableUserData.sendingMail.toString());

        return data;
    },

    getEditableUserData: (state) => {
        return state.editableUserData;
    }
}