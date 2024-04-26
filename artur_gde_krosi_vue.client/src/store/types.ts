import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
export interface RootState {
    authorizedUser: any | null,
    showPreloader: boolean,
    showSearchPanel: boolean,
    showPopup: boolean,
    popupMode: string
};