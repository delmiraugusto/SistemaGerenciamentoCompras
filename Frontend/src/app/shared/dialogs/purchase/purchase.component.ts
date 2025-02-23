import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Purchase, PurchaseInsert, PurchaseUpdate } from 'src/app/core/models/Purchase';
import { PurchaseService } from 'src/app/core/services/PurchaseService';
import { UserService } from 'src/app/core/services/UserService';
import { ProductService } from 'src/app/core/services/ProductService';
import { User } from 'src/app/core/models/User';
import { Product } from 'src/app/core/models/Product';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';

@Component({
    selector: 'app-purchase',
    templateUrl: './purchase.component.html',
    styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {
    title: string = "";
    purchase: any = new PurchaseInsert();
    users: User[] = [];
    products: Product[] = [];

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private purchaseService: PurchaseService,
        private userService: UserService,
        private productService: ProductService,
        private snackBar: SnackBar
    ) {
        this.title = this.data.title;
        this.purchase = this.data.object ?? new PurchaseInsert();

        this.purchase.userId = this.data.object?.userId ?? this.getUserIdFromToken();

        this.purchase.items = this.data.object?.items ?? this.data.object?.products ?? [];
    }

    getUserIdFromToken(): number | null {
        const token = sessionStorage.getItem('token');

        if (!token) {
            console.warn('Nenhum token encontrado no sessionStorage');
            return null;
        }

        try {
            const payloadBase64 = token.split('.')[1];
            if (!payloadBase64) {
                console.error('Formato de token inválido');
                return null;
            }

            const decodedPayload = JSON.parse(atob(payloadBase64));
            return decodedPayload.nameid || decodedPayload.sub ? Number(decodedPayload.nameid || decodedPayload.sub) : null;
        } catch (error) {
            console.error('Erro ao decodificar token:', error);
            return null;
        }
    }

    ngOnInit(): void {
        if (this.purchase.userId) {
            this.users = [];
        } else {
            this.loadUsers();
        }
        this.loadProducts();
    }

    loadUsers() {
        this.userService.GetList().subscribe((users: User[]) => {
            this.users = users;
        });
    }

    loadProducts() {
        this.productService.GetList().subscribe((products: Product[]) => {
            this.products = products;
        });
    }

    getTotal(item: any): number {
        return item.price * item.quantity;
    }

    getTotalPurchase(): number {
        if (!this.purchase.items || this.purchase.items.length === 0) {
            return 0;
        }

        return this.purchase.items.reduce((total: number, item: any) => total + (item.price * item.quantity), 0);
    }





    addItem() {
        if (!this.purchase.items) {
            this.purchase.items = [];
        }

        this.purchase.items.push({ productID: -1, quantity: 1, price: -1 });
    }

    updateItemPrice(index: number) {
        const selectedProduct = this.products.find(product => product.id === this.purchase.items[index].productID);
        this.purchase.items[index].price = selectedProduct ? selectedProduct.price : 0;
    }


    removeItem(index: number) {
        this.purchase.items.splice(index, 1);
    }

    Insert() {
        if (this.title.toLowerCase().includes("insert")) {
            this.purchaseService.Insert(this.purchase).subscribe({
                next: () => {
                    this.snackBar.open("Purchase insert sucessfully!", false);
                },
                error: () => {
                    this.snackBar.open("Error when entering purchase", true);
                }
            });
        }

    }
}
