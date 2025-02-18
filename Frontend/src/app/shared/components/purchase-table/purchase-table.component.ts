import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Purchase } from 'src/app/core/models/Purchase';
import { PurchaseService } from 'src/app/core/services/PurchaseService';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { PurchaseComponent } from 'src/app/shared/dialogs/purchase/purchase.component';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';

@Component({
  selector: 'app-purchase-table',
  templateUrl: './purchase-table.component.html',
  styleUrls: ['./purchase-table.component.css'],
})
export class PurchaseTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'userID', 'name', 'orderDate', 'total', 'Edit', 'Delete'];
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
    this.purchaseService.GetList().subscribe({
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
        console.log(error);
      }
    });
  }

  insert = () => this.openDialog('Insert Purchase');

  edit = (obj: Purchase) => this.openDialog('Edit Purchase', obj);

  remove = (id: number) => {
    this.purchaseService.RemoveById(id).subscribe({
      next: () => {
        this.snackBar.open("Produto removido com sucesso!", false);

        this.refresh();
      },
      error: (err) => {
        this.snackBar.open("Erro ao remover o produto", true);
      }
    });
  };

  openDialog(title: string, object?: Purchase) {
    this.dialog.open(PurchaseComponent, {
      disableClose: true,
      data: {
        title: title,
        object: object
      }
    });
  }
}
