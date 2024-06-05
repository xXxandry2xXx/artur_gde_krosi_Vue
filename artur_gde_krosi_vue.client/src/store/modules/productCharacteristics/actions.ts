import axios from 'axios';
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductCharacteristicsState } from '@/store/modules/productCharacteristics/types';

export const actions: ActionTree<ProductCharacteristicsState, RootState> = {
    async addChar({ state }: { state: ProductCharacteristicsState }, charName) {
        let productId = this.getters.getCurrentProductId;

        try {
            const response = await axios.post(
                'http://localhost:5263/api/CharacteristicProductFolder/CharacteristicProducts',
                '',
                {
                    params: {
                        'ProductId': productId,
                        'name': charName
                    },
                    headers: {
                        'accept': '*/*',
                        'content-type': 'application/x-www-form-urlencoded'
                    }
                }
            );
        } catch (error) {
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async addValueToChar({ state }: { state: ProductCharacteristicsState }, charValue) {
        try {
            const response = await axios.post(
                'http://localhost:5263/api/CharacteristicProductFolder/CharacteristicProductValue',
                '',
                {
                    params: {
                        'value': charValue.value,
                        'CharacteristicProductId': charValue.targetCharId
                    },

                    headers: {
                        'accept': '*/*',
                        'content-type': 'application/x-www-form-urlencoded'
                    }
                }
            );
        } catch (error) {
            ;
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async createNewChar({ state }: { state: ProductCharacteristicsState }, newChar) {

        if (newChar.name.length > 0 && newChar.value.length > 0) {
            this.dispatch('addChar', newChar.name);
            this.dispatch('addValueToChar', newChar.value);
        } else if (newChar.name.length === 0) {
            console.log('Заполните обязательное (название)');
        } else if (newChar.value.length === 0) {
            console.log('Заполните обязательное (значение)');
        }

        this.dispatch('getCurrentProductCharList');
    },

    async removeChar({ state }: { state: ProductCharacteristicsState }, charId) {
        try {
            const response = await axios.delete('http://localhost:5263/api/CharacteristicProductFolder/CharacteristicProducts', {
                params: {
                    'CharacteristicProductId': charId
                },
                headers: {
                    'accept': '*/*'
                }
            });
        } catch (error) {
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async getCurrentProductCharList({ state }: { state: ProductCharacteristicsState }) {
        try {
            const response = await axios.get('http://localhost:5263/api/Product/GetProduct', {
                params: {
                    'ProductId': this.getters.getCurrentProductId
                },
                headers: {
                    'accept': '*/*'
                }
            });
            this.commit('setProductCharacteristics', response.data.characteristicProducts);
        } catch (error) {
            console.log(error)
        }
    }
}