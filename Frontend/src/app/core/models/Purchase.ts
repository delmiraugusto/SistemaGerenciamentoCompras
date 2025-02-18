export class Purchase {
    id: number = -1;
    userID: number = -1;
    productID: number = -1;
    productName?: string;
    userName?: string;
    orderDate: Date = new Date();
    total: number = 0;
}

export class PurchaseInsert {
    userID: number = -1;
    productId: number = -1;
    orderDate: Date = new Date();
    total: number = 0;
}

export class PurchaseUpdate {
    id: number = -1;
    userID: number = -1;
    productID: number = -1;
    orderDate: Date = new Date();
    total: number = 0;
}