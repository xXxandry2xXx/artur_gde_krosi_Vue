import type { GetterTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductCharacteristicsState } from '@/store/modules/productCharacteristics/types';
import router from '@/router/router';

export const getters: GetterTree<ProductCharacteristicsState, RootState> = {
    getCurrentProductId: (state) => {
        return state.currentProductId;
    },

    getCharacteristicById: (state) => (id: string) => {
        return state.characteristicsList.find((char: any) => char.characteristicProductId === id);
    },

    getCurrentCharacteristicId: (state) => {
        return state.currentCharacteristicId;
    },

    getCurrentCharacteristicName: (state) => {
        return state.currentCharacteristicName;
    },

    getCurrentProductCharacteristic: (state) => {
        return state.characteristicsList;
    },

    getCurrentCharacteristicValues: (state) => {
        return state.currentCharacteristicValues;
    }
}