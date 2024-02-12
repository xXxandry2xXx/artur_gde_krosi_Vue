import type VariantsInterface from '@/types/variantsInterface';
import type ProductImagesInterface from '@/types/productImagesInterface';

export default interface ProductInterface {
    productId: string,
    name: string,
    prise: number,
    variants: VariantsInterface[],
    modelKrosovock_Name: string,
    brend_Name: string,
    images: ProductImagesInterface
}