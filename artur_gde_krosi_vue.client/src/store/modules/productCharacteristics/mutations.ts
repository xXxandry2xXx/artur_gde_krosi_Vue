import type { MutationTree } from 'vuex';
import type { ProductCharacteristicsState } from '@/store/modules/productCharacteristics/types';

export const mutations: MutationTree<ProductCharacteristicsState> = {
    setCurrentProductId(state, id) {
        state.currentProductId = id;
    },

    setCurrentCharacteristicId(state, id) {
        state.currentCharacteristicId = id;
    },

    setProductCharacteristics(state, chars) {
        state.characteristicsList = chars;
    },

    setNewCharacteristicName(state, name) {
        state.currentCharacteristicName = name;
    },

    addCurrentCharacteristicValue(state, newValue) {
        const valuesArray = state.currentCharacteristicValues;
        const existingValue = valuesArray.find((value) => value.id === newValue.id);
        if (!existingValue) {
            valuesArray.push(newValue);
        } else {
            valuesArray.splice(valuesArray.indexOf(existingValue), 1)
            valuesArray.push(newValue);
        }
    },

    clearCurrentCharacteristicValues(state, clearValue) {
        state.currentCharacteristicValues = clearValue;
    }
}