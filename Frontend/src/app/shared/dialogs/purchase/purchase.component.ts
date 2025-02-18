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
    purchase: Purchase = new Purchase();
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
        this.purchase = this.data.object ?? new Purchase();
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

    Insert = () => {
        if (this.title.toLowerCase().includes("insert")) {
            const purchaseInsert: PurchaseInsert = new PurchaseInsert();
            purchaseInsert.userID = this.purchase.userID;
            purchaseInsert.productId = this.purchase.productID;
            purchaseInsert.orderDate = this.purchase.orderDate;
            purchaseInsert.total = this.purchase.total;

            this.purchaseService.Insert(purchaseInsert).subscribe({
                next: response => {
                    this.snackBar.open("Compra inserida com sucesso!", false);
                },
                error: err => {
                    this.snackBar.open("Erro ao inserir a compra", true);
                }
            });
        } else {
            const itemUpdate: PurchaseUpdate = new PurchaseUpdate();
            itemUpdate.id = this.purchase.id;
            itemUpdate.userID = this.purchase.userID;
            itemUpdate.productID = this.purchase.productID;
            itemUpdate.orderDate = this.purchase.orderDate;
            itemUpdate.total = this.purchase.total;

            this.purchaseService.UpdateById(itemUpdate.id, itemUpdate).subscribe({
                next: response => {
                    this.snackBar.open("Produto atualizado com sucesso!", false);
                },
                error: err => {
                    this.snackBar.open("Erro ao atualizar a compra", true);
                }
            });
        }
    };
}
