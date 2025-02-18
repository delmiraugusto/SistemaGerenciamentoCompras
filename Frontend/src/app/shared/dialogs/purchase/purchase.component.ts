import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Purchase, PurchaseInsert, PurchaseUpdate } from 'src/app/core/models/Purchase';
import { PurchaseService } from 'src/app/core/services/PurchaseService';

@Component({
    selector: 'app-purchase',
    templateUrl: './purchase.component.html',
    styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent implements OnInit {
    title: string = "";
    purchase: Purchase = new Purchase();

    constructor(@Inject(MAT_DIALOG_DATA) public data: any, private purchaseService: PurchaseService) {
        this.title = this.data.title;
        this.purchase = this.data.object ?? new Purchase();
    }

    ngOnInit(): void { }

    Insert = () => {
        if (this.title.toLowerCase().includes("insert")) {
            const purchaseInsert: PurchaseInsert = new PurchaseInsert();
            purchaseInsert.userID = this.purchase.userID;
            purchaseInsert.orderDate = this.purchase.orderDate;
            purchaseInsert.total = this.purchase.total;
            this.purchaseService.Insert(purchaseInsert).subscribe();
        } else {
            const itemUpdate: PurchaseUpdate = new PurchaseUpdate();
            itemUpdate.id = this.purchase.id;
            itemUpdate.userID = this.purchase.userID;
            itemUpdate.orderDate = this.purchase.orderDate;
            itemUpdate.total = this.purchase.total;
            this.purchaseService.UpdateById(itemUpdate.id, itemUpdate).subscribe();
        }
    }
}
