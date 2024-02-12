import type ProductInterface from "./productInterface"

export default interface ProductsDataInterface {
    priseMax: number,
    priseMin: number,
    productcount: number,
    products: ProductInterface[]
}