import type { ProductsCatalogState } from '@/store/modules/productsCatalog/types';
export interface RootState {
    showPreloader: boolean,
    showSearchPanel: boolean,
    showLogInPopup: boolean,
    loginPopupMode: string
};