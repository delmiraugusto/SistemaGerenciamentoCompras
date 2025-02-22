import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-purchase-details',
    templateUrl: './purchase-details.component.html',
    styleUrls: ['./purchase-details.component.css']
})
export class PurchaseDetailsComponent {
    constructor(
        public dialogRef: MatDialogRef<PurchaseDetailsComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { products: { name: string, quantity: number, UnitValue: number, total: number }[], purchaseID: number }
    ) { }
}
