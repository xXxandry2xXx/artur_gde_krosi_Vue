<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-form">
            <h3>Новое значение характеристики</h3>
            <div class="characteristic-popup-field">
                <span>Значение</span>
                <DefaultInput placeholder="Значение характеристики" v-model="charNewValue.value" :value="charNewValue.value"/>
            </div>
            <p class="characteristic-popup-error-message" :class="{'user-account-editing-message-incorrect': isFieldsFilled === false }" v-show="!isFieldsFilled">{{ errorMessage }}</p>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="addNewValueToChar">Сохранить</BorderedButton>
            <BorderedButton @click="closePopup">Отмена</BorderedButton>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapActions, mapGetters, mapMutations } from 'vuex';

    export default defineComponent({

        data() {
            return {
                charNewValue: {
                    targetCharId: '',
                    value: ''
                },

                isFieldsFilled: null,
                errorMessage: ''
            }
        },

        methods: {
            ...mapMutations(['setPopupVisibility']),
            ...mapActions(['addValueToChar']),
            ...mapGetters(['getCurrentCharacteristicId', 'validateFieldFullnes']),

            validateFields(this: any) {
                if (this.charNewValue.value.length > 0) {
                    this.isFieldsFilled = true;
                    this.errorMessage = '';
                } else {
                    this.isFieldsFilled = false;
                    this.errorMessage = 'Заполните обязательное поле';
                }
            },

            addNewValueToChar(this: any) {
               this.validateFields()
                if (this.isFieldsFilled) {
                    this.addValueToChar(this.charNewValue);
                    this.closePopup();
                }
            },

            closePopup(this: any) {
                this.setPopupVisibility(false)
            }
        },

        mounted() {
            this.charNewValue.targetCharId = this.getCurrentCharacteristicId();
        }
    })
</script>