<template>
    <div class="user-account-editing">
        <div class="user-account-editing-headings">
            <h1>Личные данные</h1>
            <p v-show="$store.state.account.succesMessage != ''"
               class="user-account-editing-message"
               :class="{'user-account-editing-message-incorrect': $store.state.account.isSucces === false }">
                {{ $store.state.account.succesMessage }}
            </p>
        </div>
        <div class="user-account-editing-all-fields">
            <div class="user-account-fields">
                <div class="user-account-field">
                    <span>Имя:</span>
                    <DefaultInput :value="getEditableUserData().name" @input="enterNewName" placeholder="Введите своё имя" />
                </div>
                <div class="user-account-field">
                    <span>Фамилия:</span>
                    <DefaultInput :value="getEditableUserData().surname" @input="enterNewSurname" placeholder="Введите свою фамилию" />
                </div>
                <div class="user-account-field">
                    <span>Отчество:</span>
                    <DefaultInput :value="getEditableUserData().patronymic" @input="enterPatronymic" placeholder="Введите своё отчество" />
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
                <button class="user-account-change-button" @click="openChangeDataPopup('change-password')">Сменить пароль</button>
            </div>
        </div>
        <BorderedButton @click="confirmChanges">Сохранить</BorderedButton>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations, mapGetters, mapActions } from 'vuex';


    export default defineComponent({

        methods: {
            ...mapGetters(['getAuthorizedUser', 'getEditableUserData']),
            ...mapMutations([
                'setNewUserName',
                'setNewUserSurname',
                'setNewUserPatronymic',
                'setUserNewsletterStatus',
                'setPopupVisibility',
                'setPopupMode'
            ]),
            ...mapActions([
                'saveUserDataChanges',
                'updateLocalUserData',
                'initEditableUserData',
                'confirmChanges',
                'sendMessageForPasswordChanging'
            ]),

            enterNewName(event: Event) {
                let input = event.target as HTMLInputElement;
                this.setNewUserName(input.value);
            },

            enterNewSurname(event: Event) {
                let input = event.target as HTMLInputElement;
                this.setNewUserSurname(input.value);
            },

            enterPatronymic(event: Event) {
                let input = event.target as HTMLInputElement;
                this.setNewUserPatronymic(input.value);
            },

            setNewsletterSatus(event: Event) {
                let input = event.target as HTMLInputElement;
            },

            openChangeDataPopup(mode: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(mode);
            },
        },

        mounted() {
            this.initEditableUserData();
        }
    })
</script>