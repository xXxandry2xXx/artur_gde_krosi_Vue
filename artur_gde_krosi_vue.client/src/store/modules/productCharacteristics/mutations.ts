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
    }
}