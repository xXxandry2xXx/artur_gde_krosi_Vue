import axios from 'axios';
import apiUrl from '@/helper'
import type { ActionTree } from 'vuex';
import type { RootState } from '@/store/types';
import type { ProductCharacteristicsState } from '@/store/modules/productCharacteristics/types';

export const actions: ActionTree<ProductCharacteristicsState, RootState> = {
    async addChar({ state }: { state: ProductCharacteristicsState }, charName) {
        let productId = this.getters.getCurrentProductId;

        try {
            const token = this.getters.getAuthorizedUserToken.slice(1, -1);
            const response = await axios.post(
                apiUrl + '/CharacteristicProductFolder/CharacteristicProducts',
                '',
                {
                    params: {
                        'ProductId': productId,
                        'name': charName
                    },
                    headers: {
                        'accept': '*/*',
                        'content-type': 'application/x-www-form-urlencoded',
                        'Authorization': 'Bearer ' + token,
                    }
                }
            );
            return response;
        } catch (error) {
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async addValueToChar({ state }: { state: ProductCharacteristicsState }, charValue) {
        try {
            const token = this.getters.getAuthorizedUserToken.slice(1, -1);
            const response = await axios.post(
                apiUrl + '/CharacteristicProductFolder/CharacteristicProductValue',
                '',
                {
                    params: {
                        'value': charValue.value,
                        'CharacteristicProductId': charValue.targetCharId
                    },

                    headers: {
                        'accept': '*/*',
                        'Authorization': 'Bearer ' + token,
                        'content-type': 'application/x-www-form-urlencoded'
                    }
                }
            );
        } catch (error) {
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async createNewChar({ state }: { state: ProductCharacteristicsState }, newChar) {

        if (newChar.name.length > 0 && newChar.value.length > 0) {
            this.dispatch('addChar', newChar.name).then(response => {
                if (response) {
                    let charValue = { value: newChar.value, targetCharId: response.data };
                    this.dispatch('addValueToChar', charValue);
                } else {
                    console.log('access denied')
                }
            });
        }

        this.dispatch('getCurrentProductCharList');
    },

    async removeChar({ state }: { state: ProductCharacteristicsState }, charId) {
        try {
            const token = this.getters.getAuthorizedUserToken.slice(1, -1);
            const response = await axios.delete(apiUrl + '/CharacteristicProductFolder/CharacteristicProducts', {
                params: {
                    'CharacteristicProductId': charId
                },
                headers: {
                    'accept': '*/*',
                    'Authorization': 'Bearer ' + token,
                }
            });
        } catch (error) {
            console.log(error);
        }

        this.dispatch('getCurrentProductCharList');
    },

    async getCurrentProductCharList({ state }: { state: ProductCharacteristicsState }) {
        try {
            const response = await axios.get(apiUrl + '/Product/GetProduct', {
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
    },

    async confirmChangedCharacteristicName({ state }: { state: ProductCharacteristicsState }) {
        const charName = state.currentCharacteristicName;
        const charId = state.currentCharacteristicId;

        try {
            const token = this.getters.getAuthorizedUserToken.slice(1, -1);
            const response = await axios.put(
                apiUrl + '/CharacteristicProductFolder/CharacteristicProducts',
                '',
                {
                    params: {
                        'CharacteristicProductId': charId,
                        'name': charName
                    },
                    headers: {
                        'accept': '*/*',
                        'Authorization': 'Bearer ' + token,
                    }
                }
            );
        } catch (error) {
            console.log(error);
        }
    },

    confirmChangedCharacteristicValues({ state }: { state: ProductCharacteristicsState }) {
        const valuesArray = state.currentCharacteristicValues;

        if (valuesArray.length > 0) {
            valuesArray.forEach(async (value) => {
                try {
                    const token = this.getters.getAuthorizedUserToken.slice(1, -1);
                    const response = await axios.put(
                        apiUrl + '/CharacteristicProductFolder/CharacteristicProductValue',
                        '',
                        {
                            params: {
                                'CharacteristicProductValueId': value.id.toString(),
                                'value': value.value.toString(),
                            },
                            headers: {
                                'accept': '*/*',
                                'Authorization': 'Bearer ' + token,
                            }
                        }
                    );
                    if (response.status === 200) this.dispatch('getCurrentProductCharList')
                } catch (error) {
                    console.log(error);
                }
            })
        }
    },

    saveCharacteristicChanges() {
        this.dispatch('confirmChangedCharacteristicName');
        this.dispatch('confirmChangedCharacteristicValues');
        this.dispatch('getCurrentProductCharList');
    }
}