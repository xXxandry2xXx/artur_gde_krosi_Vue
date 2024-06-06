
export interface ProductCharacteristicsState {
    currentProductId: string,
    characteristicsList: Array<any>,
    currentCharacteristicId: string,
    currentCharacteristicName: string
    currentCharacteristicValues: Array<EditableCharacteristicValue>
}

export interface EditableCharacteristicValue {
    id: string,
    value: string
}
