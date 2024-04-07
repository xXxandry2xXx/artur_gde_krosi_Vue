import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { AuthorizationState } from '@/store/modules/authorization/types';

export const getters: GetterTree<AuthorizationState, RootState> = {
    getRegistrationFormData: (state) => {
        const form = new FormData();

        form.append('Username', state.registrationUserData.username);
        form.append('Email', state.registrationUserData.email);
        form.append('Password', state.registrationUserData.password);
        form.append('name', state.registrationUserData.name);
        form.append('surname', state.registrationUserData.surname);
        form.append('patronymic', state.registrationUserData.patronymic);
        form.append('sendingMail', 'false');

        return form;
    }
}