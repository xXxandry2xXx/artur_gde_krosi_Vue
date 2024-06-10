<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-editing-form">
            <h3>Редактирование характеристики</h3>
            <div class="characteristic-popup-list">
                <div class="characteristic-popup-listed-char">
                    <h5>Название характеристики</h5>
                    <DefaultInput :value="currentChar.name" @input="handleNameEditing" />
                </div>
                <CharacteristicEditingField v-for="(charVal, idx) in currentChar.characteristicProductValues" :charValue="charVal" :valNumber="idx" />
            </div>
        </div>
        <p class="characteristic-popup-error-message"
           :class="{'user-account-editing-message-incorrect': isNameFilled === false || areValuesFilled === false }"
           v-show="!isNameFilled || !areValuesFilled">{{ errorMessage }}</p>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="saveChanges">Сохранить</BorderedButton>
            <BorderedButton @click="closePopup">Отмена</BorderedButton>
        </div>
    </div>

</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import CharacteristicEditingField from '@/components/Popup/Characteristics/CharacteristicEditingField.vue';

    export default defineComponent({

        data() {
            return {
                isNameFilled: null,
                errorMessage: ''
            }
        },

        components: { CharacteristicEditingField },

        methods: {
            ...mapActions(['saveCharacteristicChanges']),
            ...mapMutations(['setPopupVisibility', 'clearCurrentCharacteristicValues', 'setNewCharacteristicName']),
            ...mapGetters(['getCurrentProductCharacteristic', 'getCurrentCharacteristicId', 'getCurrentCharacteristicValues', 'getCurrentCharacteristicName']),


            handleNameEditing(this: any, event: Event) {
                let element = event.target as HTMLInputElement;
                this.setNewCharacteristicName(element.value);
            },

            validateNameFillness(this: any) {
                let charName = this.getCurrentCharacteristicName();
                if (charName.length > 0) {
                    this.isNameFilled = true;
                } else {
                    this.isNameFilled = false;
                }

                if (this.isNameFilled && this.areValuesFilled) {
                    this.errorMessage = '';
                } else {
                    this.errorMessage = 'Заполните все поля характеристики';
                }
            },

            saveChanges(this: any) {
                this.validateNameFillness();
                if (this.isNameFilled && this.areValuesFilled) {
                    this.saveCharacteristicChanges();
                    this.closePopup();
                }
            },

            closePopup(this: any) {
                this.setPopupVisibility(false);
            }
        },

        computed: {
            currentChar(this: any) {
                let charId = this.getCurrentCharacteristicId();
                let currentCharList = this.getCurrentProductCharacteristic();
                return currentCharList.find((char: any) => char.characteristicProductId === charId);
            },

            areValuesFilled(this: any) {
                let currentCharValues = this.getCurrentCharacteristicValues();
                let isFilled = true;
                if (currentCharValues.length > 0) {
                    currentCharValues.forEach((charValue: any) => {
                        if (charValue.value.length > 0) {
                            isFilled = true;
                        } else {
                            isFilled = false;
                        }
                    });
                }

                return isFilled;
            },
        },

        mounted() {
            this.clearCurrentCharacteristicValues([]);
        }
    })
</script>