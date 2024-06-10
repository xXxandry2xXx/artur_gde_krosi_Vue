<template> 
    <div class="characteristic-popup">
        <div class="characteristic-popup-form">
            <h3>Новая характеристика</h3>
            <div class="characteristic-popup-field">
                <span>Название</span>
                <DefaultInput placeholder="Название характеристики" v-model="newCharacteristic.name"
                              :class="{'characteristic-popup-incorrect': isNameFilled === false }"/>
            </div>
            <div class="characteristic-popup-field">
                <span>Значение</span>
                <DefaultInput placeholder="Значение характеристики" v-model="newCharacteristic.value" 
                              :class="{'characteristic-popup-incorrect': isValueFilled === false }"/>
            </div>
            <p class="characteristic-popup-error-message"
               :class="{'user-account-editing-message-incorrect': isValueFilled === false || isNameFilled === false }"
               v-show="!isValueFilled || !isNameFilled">{{ errorMessage }}</p>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="createCharacteristic">Сохранить</BorderedButton>
            <BorderedButton @click="closePopup">Отмена</BorderedButton>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapGetters, mapActions, mapMutations } from 'vuex';

    export default defineComponent({
        data() {
            return {
                newCharacteristic: {
                    name: '',
                    value: ''
                },

                isNameFilled: null,
                isValueFilled: null,
                errorMessage: ''
            }
        },

        methods: {
            ...mapMutations(['setPopupVisibility']),
            ...mapGetters(['getCurrentProductId']),
            ...mapActions(['createNewChar']),

            validateValues(this: any) {
                if (this.newCharacteristic.name.length && this.newCharacteristic.value.length > 0) {
                    this.isNameFilled = true;
                    this.isValueFilled = true;
                    return;
                }

                if (this.newCharacteristic.name.length === 0) {
                    this.isNameFilled = false;
                    this.errorMessage = 'Введите название характеристики';
                } else if (this.newCharacteristic.value.length === 0 ) {
                    this.isValueFilled = false;
                    this.errorMessage = 'Введите значение характеристики';
                } else {
                    this.isNameFilled = false;
                    this.isValueFilled = false;
                    this.errorMessage = 'Заполните обязательное поле';
                }
            },

            createCharacteristic(this: any) {
                this.validateValues();
                if (this.isNameFilled && this.isValueFilled) {
                    this.createNewChar(this.newCharacteristic);
                    this.closePopup();
                }
            },

            closePopup(this: any) {
                this.setPopupVisibility(false)
            }
        },
    })
</script>