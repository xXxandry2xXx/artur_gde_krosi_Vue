
export interface CartItemInterface {
    shopping—artId: string,
    quantity: number,
    availability: boolean,
    variantId: string,
    productId: string
}

export interface UserCartState {
    itemsInCart: Array<CartItemInterface>,
    currentCartPrices: any,
    chosenVariantId: string
}