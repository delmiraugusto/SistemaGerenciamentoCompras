import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Purchase } from 'src/app/core/models/Purchase';
import { PurchaseService } from 'src/app/core/services/PurchaseService';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { PurchaseComponent } from 'src/app/shared/dialogs/purchase/purchase.component';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';
import { PurchaseDetailsComponent } from '../purchase-details/purchase-details.component';

@Component({
  selector: 'app-purchaseByUser-table',
  templateUrl: './purchase-byUser-table.html',
  styleUrls: ['./purchase-byUser-table.css'],
})
export class PurchaseTableByUserComponent implements OnInit {
  displayedColumns: string[] = ['id', 'orderDate', 'total', 'details'];
  purchaseList: MatTableDataSource<Purchase> = new MatTableDataSource<Purchase>();
  pageSizes: number[] = [25, 50, 100];
  length: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private purchaseService: PurchaseService, private dialog: MatDialog, private snackBar: SnackBar) { }

  ngOnInit(): void {
    this.refresh();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.purchaseList.filter = filterValue.trim().toLowerCase();

    if (this.purchaseList.paginator) {
      this.purchaseList.paginator.firstPage();
    }
  }

  refresh() {
    const userId = Number(this.getUserIdFromToken());

    if (!userId) {
      console.error('User ID not found in token');
      return;
    }

    this.purchaseService.GetByUserId(userId).subscribe({
      next: data => {
        this.purchaseList = new MatTableDataSource(data);
        this.purchaseList.paginator = this.paginator;
        this.purchaseList.sort = this.sort;
        this.length = this.purchaseList._filterData.length;
        if (this.length > 100) {
          this.pageSizes = [...this.pageSizes, this.length];
        }
      },
      error: error => {
        console.error(error);
      }
    });
  }

  getUserIdFromToken(): number | null {
    const token = sessionStorage.getItem('token');

    if (!token) {
      console.warn('No tokens found in sessionStorage');
      return null;
    }

    try {
      const payloadBase64 = token.split('.')[1];

      if (!payloadBase64) {
        console.error('Invalid token format');
        return null;
      }

      const decodedPayload = JSON.parse(atob(payloadBase64));

      const userId = decodedPayload.nameid || decodedPayload.sub;

      return userId ? Number(userId) : null;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  insert = () => {
    const userId = this.getUserIdFromToken();

    if (!userId) {
      console.error("User ID not found in token");
      return;
    }

    this.openDialog('Insert Purchase', { userID: userId });
  };


  edit = (obj: Purchase) => this.openDialog('Edit Purchase', obj);

  remove = (id: number) => {
    this.purchaseService.RemoveById(id).subscribe({
      next: () => {
        this.snackBar.open("Produto removido com sucesso!", false);
        this.refresh();
      },
      error: () => {
        this.snackBar.open("Erro ao remover o produto", true);
      }
    });
  };

  openPurchaseDetails(purchase: Purchase) {
    this.dialog.open(PurchaseDetailsComponent, {
      width: '500px',
      data: { products: purchase.products, purchaseID: purchase.id }
    });
  }

  openDialog(title: string, object?: Partial<Purchase>) {
    this.dialog.open(PurchaseComponent, {
      disableClose: true,
      data: {
        title: title,
        object: object || {}
      }
    });
  }

}
