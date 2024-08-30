import axios from 'axios';
import apiUrl from '@/helper'
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserAccountState } from '@/store/modules/account/types';

export const actions: ActionTree<UserAccountState, RootState> = {

    async saveUserDataChanges() {
        const username = this.getters.getAuthorizedUser.userName;
        const data = this.getters.gatherUserFormData;

        try {
            const token = this.getters.getAuthorizedUserToken.slice(1, -1);
            const response = await axios.put(
                apiUrl + '/identity/SetingsUser/UserSettings',
                data,
                {
                    headers: {
                        'accept': '*/*',
                        'Authorization': 'Bearer ' + token,
                    }
                }
            );

            return response;
        } catch (error) {
            console.log(error);
        }
    },

    updateLocalUserData(this: any) {
        let userData = this.getters.getAuthorizedUser;
        userData.name = this.getters.getEditableUserData.name;
        userData.surname = this.getters.getEditableUserData.surname;
        userData.patronymic = this.getters.getEditableUserData.patronymic;
        userData.sendingMail = this.getters.getEditableUserData.sendingMail;

        localStorage.setItem('userData', JSON.stringify(userData));
    },

    initEditableUserData(this: any) {
        let userData = this.getters.getAuthorizedUser;
        if (userData.name !== null) this.commit('setNewUserName', userData.name);
        if (userData.surname !== null) this.commit('setNewUserSurname', userData.surname);
        if (userData.patronymic !== null) this.commit('setNewUserPatronymic', userData.patronymic);
        if (userData.sendingMail !== null) this.commit('setUserNewsletterStatus', userData.sendingMail)
    },

    confirmChanges(this: any) {
        let editableData = this.getters.getEditableUserData;
        if (editableData.name.length > 0) {
            this.dispatch('saveUserDataChanges').then((response: any, error: any) => {
                if (response && response.status === 200) {
                    this.dispatch('updateLocalUserData');
                    this.commit('setSuccesStatus', true);
                    this.commit('setSuccesMessage', 'Изменения успешно сохранены');
                } else {
                    this.commit('setSuccesStatus', false);
                    this.commit('setSuccesMessage', 'Произошла ошибка при изменении данных профиля');
                }
            });
        } else {
            this.commit('setSuccesStatus', false);
            this.commit('setSuccesMessage', 'Имя должно быть заполнено')
        }
        
    },
}