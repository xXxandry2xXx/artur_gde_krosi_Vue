<template>
    <li>
        <p>
            <span>{{ characteristicName }}:</span><span>{{ characteristicValue }}</span>
        </p>
        <div class="characteristic-interaction-buttons" v-if="isManager">
            <BorderedButton class="characteristic-interaction-button" @click="openCharActionPopup('add-char-value', characteristicData.characteristicProductId)">
                <font-awesome-icon :icon="['fas', 'plus']" />
            </BorderedButton>
            <BorderedButton class="characteristic-interaction-button" @click="openCharActionPopup('edit-char', characteristicData.characteristicProductId)">
                <font-awesome-icon :icon="['fas', 'pen-to-square']" />
            </BorderedButton>
            <BorderedButton class="characteristic-interaction-button" @click="openCharActionPopup('sure-to-remove-char', characteristicData.characteristicProductId)">
                <font-awesome-icon :icon="['fas', 'trash-can']" />
            </BorderedButton>
        </div>
    </li>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { mapMutations } from 'vuex';

    export default defineComponent({

        props: ['characteristicData', 'isManager'],

        methods: {
            ...mapMutations(['setPopupVisibility', 'setPopupMode', 'setCurrentCharacteristicId', 'setNewCharacteristicName']),

            openCharActionPopup(this: any, popupMode: string, charId: string) {
                this.setPopupVisibility(true);
                this.setPopupMode(popupMode);
                this.setCurrentCharacteristicId(charId);
                this.setNewCharacteristicName(this.characteristicName);
            },
        },

        computed: {
            characteristicName(this: any) {
                return this.characteristicData.name
            },

            characteristicValue(this: any) {
                const characteristicValues = this.characteristicData.characteristicProductValues;
                const handledValues = characteristicValues.reduce((valuesArray: any, currentValue: any) => {
                    return valuesArray.concat(currentValue.value);
                }, [])

                return handledValues.join(', ')
            }
        }
    })
</script>