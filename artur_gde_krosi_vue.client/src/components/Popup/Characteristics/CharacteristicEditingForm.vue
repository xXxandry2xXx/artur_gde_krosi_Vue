<template>
    <div class="characteristic-popup">
        <div class="characteristic-popup-editing-form">
            <h3>Редактирование характеристики</h3>
            <div class="characteristic-popup-list">
                <div class="characteristic-popup-listed-char">
                    <h5>Название характеристики</h5>
                    <DefaultInput :value="currentChar.name" />
                </div>
                <CharacteristicEditingField v-for="(charVal, idx) in currentChar.characteristicProductValues" :charValue="charVal" :valNumber="idx"/>
            </div>
        </div>
        <div class="characteristic-popup-buttons">
            <BorderedButton @click="console.log(this.getCurrentCharacteristicValues())">Сохранить</BorderedButton>
            <BorderedButton @click="closePopup">Отмена</BorderedButton>
        </div>
    </div>
    
</template>

<script lang="ts">
    import { defineComponent } from "vue";
    import { mapGetters, mapActions, mapMutations } from 'vuex';
    import CharacteristicEditingField from '@/components/Popup/Characteristics/CharacteristicEditingField.vue';

    export default defineComponent({
        components: { CharacteristicEditingField },

        methods: {
            ...mapMutations(['setPopupVisibility', 'clearCurrentCharacteristicValues']),
            ...mapGetters(['getCurrentProductCharacteristic', 'getCurrentCharacteristicId', 'getCurrentCharacteristicValues']),

            closePopup(this: any) {
                this.setPopupVisibility(false)
            }
        },

        computed: {
            currentChar(this: any) {
                let charId = this.getCurrentCharacteristicId();
                let currentCharList = this.getCurrentProductCharacteristic();
                return currentCharList.find((char: any) => char.characteristicProductId === charId);
            }
        },

        mounted() {
            this.clearCurrentCharacteristicValues([])
        }
    })
</script>