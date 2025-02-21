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
    purchase: PurchaseInsert = new PurchaseInsert();
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
    }

    ngOnInit(): void {
        this.loadUsers();
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

    addItem() {
        this.purchase.items.push({ productID: -1, quantity: 1 });
    }

    removeItem(index: number) {
        this.purchase.items.splice(index, 1);
    }

    Insert() {
        if (this.title.toLowerCase().includes("insert")) {
            this.purchaseService.Insert(this.purchase).subscribe({
                next: () => {
                    this.snackBar.open("Compra inserida com sucesso!", false);
                },
                error: () => {
                    this.snackBar.open("Erro ao inserir a compra", true);
                }
            });
        }
    }
}
