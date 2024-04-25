<template>
    <div class="user-account-editing">
        <h1>Личные данные</h1>
        <div class="user-account-editing-all-fields">
            <div class="user-account-fields">
                <div class="user-account-field">
                    <span>Имя:</span>
                    <DefaultInput :value="editableUserData.name" v-model="editableUserData.name" placeholder="Введите своё имя" />
                </div>
                <div class="user-account-field">
                    <span>Фамилия:</span>
                    <DefaultInput :value="editableUserData.surname" v-model="editableUserData.surname" placeholder="Введите свою фамилию" />
                </div>
                <div class="user-account-field">
                    <span>Отчество:</span>
                    <DefaultInput :value="editableUserData.patronymic" v-model="editableUserData.patronymic" placeholder="Введите своё отчество" />
                </div>
                <div class="user-account-field">
                    <span>E-mail:</span>
                    <DefaultInput class="not-clickable" :value="$store.state.authorizedUser.email" placeholder="Введите свой E-mail адрес" disabled />
                </div>
                <div class="user-account-field">
                    <span>Телефон:</span>
                    <DefaultInput class="not-clickable"
                                  v-if="$store.state.authorizedUser.phoneNumber !== null"
                                  :value="$store.state.authorizedUser.phoneNumber"
                                  placeholder="Введите свой номер телефона"
                                  disabled />
                    <DefaultInput class="not-clickable" v-else placeholder="Не указан" disabled />
                </div>
            </div>
            <div class="user-account-change-data-fields">
                <div class="user-account-field">
                    <span>Новый пароль</span>
                    <DefaultInput placeholder="Введите новый пароль" />
                </div>
                <div class="user-account-field">
                    <span>Подтвердите новый пароль</span>
                    <DefaultInput placeholder="Подтвердите новый пароль" />
                </div>
            </div>
            <div class="user-account-change-data-fields">
                <div class="user-account-field">
                    <span>Новый E-mail</span>
                    <DefaultInput placeholder="Введите новый E-mail" />
                </div>
            </div>
        </div>
        <BorderedButton @click="confirmChanges">Сохранить</BorderedButton>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapGetters } from 'vuex';
    import axios from 'axios';


    export default defineComponent({
        data() {
            return {
                editableUserData: {
                    name: '',
                    surname: '',
                    patronymic: '',
                    sendingMail: ''
                }
            }
        },

        methods: {
            ...mapGetters(['getAuthorizedUser']),

            confirmChanges(this: any) {
                this.saveUserDataChanges().then((response: any, error: any) => {
                    if (response.status === 200) {
                        this.updateLocalUserData()
                        location.reload();
                    } else {
                        console.log(error);
                    }
                });

            },

            async saveUserDataChanges(this: any) {
                const username = this.getAuthorizedUser().userName;
                const data = this.gatherUserFormData;

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
                let userData = this.getAuthorizedUser();
                userData.name = this.editableUserData.name;
                userData.surname = this.editableUserData.surname;
                userData.patronymic = this.editableUserData.patronymic;
                userData.sendingMail = this.editableUserData.sendingMail;

                localStorage.setItem('userData', JSON.stringify(userData));
            },

            initEditableUserData(this: any) {
                let userData = this.getAuthorizedUser();
                this.editableUserData.name = userData.name;
                this.editableUserData.surname = userData.surname;
                this.editableUserData.patronymic = userData.patronymic;
                this.editableUserData.sendingMail = userData.sendingMail;
            }
        },

        computed: {
            gatherUserFormData(this: any) {
                const data = new FormData();
                data.append('name', this.editableUserData.name.toString());
                data.append('surname', this.editableUserData.surname.toString());
                data.append('patronymic', this.editableUserData.patronymic.toString());
                data.append('sendingMail', this.editableUserData.sendingMail.toString());

                return data;
            }
        },

        mounted() {
            this.initEditableUserData();
        }
    })
</script>