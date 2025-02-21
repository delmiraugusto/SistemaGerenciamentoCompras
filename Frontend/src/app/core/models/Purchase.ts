export class Purchase {
    id: number = -1;
    userID: number = -1;
    productID: number = -1;
    productName?: string;
    userName?: string;
    orderDate: Date = new Date();
    total: number = 0;
    products: { name: string; quantity: number }[] = [];
}

export class PurchaseInsert {
    userID: number = -1;
    items: { productID: number; quantity: number }[] = [];
}


export class PurchaseUpdate {
    id: number = -1;
    userID: number = -1;
    productID: number = -1;
    orderDate: Date = new Date();
    total: number = 0;
}