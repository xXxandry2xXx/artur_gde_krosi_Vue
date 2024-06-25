export interface CartItemInterface {
    shoppingCartId: string;
    quantity: number;
    availability: boolean;
    variantId: string;
    productId: string;
};

export interface CartInterface {
    itemsInCart: Array<CartItemInterface>;
    totalPrice: number;
};

export interface UserCartState {
    serverCart: CartInterface;
    localCart: CartInterface;
    chosenVariantId: string;
    chosenProductId: string;
};