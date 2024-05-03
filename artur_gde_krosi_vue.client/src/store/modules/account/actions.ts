import axios from 'axios';
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { UserAccountState } from '@/store/modules/account/types';

export const actions: ActionTree<UserAccountState, RootState> = {

    async saveUserDataChanges() {
        const username = this.getters.getAuthorizedUser.userName;
        const data = this.getters.gatherUserFormData;

        try {
            const response = await axios.put(
                'http://localhost:5263/api/identity/SetingsUser/UserSettings',
                data,
                {
                    params: {
                        'userName': username.toString()
                    },
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
        this.commit('setNewUserName', userData.name);
        this.commit('setNewUserSurname', userData.surname);
        this.commit('setNewUserPatronymic', userData.patronymic);
        this.commit('setUserNewsletterStatus', userData.sendingMail)
    },

    confirmChanges(this: any) {
        this.dispatch('saveUserDataChanges').then((response: any, error: any) => {
            if (response.status === 200) {
                this.dispatch('updateLocalUserData');
                this.commit('setSuccesMessage', 'Изменения успешно сохранены');
            } else {
                this.commit('setSuccesMessage', 'Произошла ошибка при изменении данных профиля');
                console.log(error);
            }
        });
    },
}