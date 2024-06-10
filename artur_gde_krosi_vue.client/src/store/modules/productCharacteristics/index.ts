import type { Module } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductCharacteristicsState } from '@/store/modules/productCharacteristics/types';
import { mutations } from '@/store/modules/productCharacteristics/mutations';
import { getters } from '@/store/modules/productCharacteristics/getters';
import { actions } from '@/store/modules/productCharacteristics/actions';

const state: ProductCharacteristicsState = {
    currentProductId: '',
    characteristicsList: [],
    currentCharacteristicId: '',
    currentCharacteristicName: '',
    currentCharacteristicValues: [],
}

const productCharacteristics: Module<ProductCharacteristicsState, RootState> = {
    state,
    mutations,
    getters,
    actions
}

export default productCharacteristics;