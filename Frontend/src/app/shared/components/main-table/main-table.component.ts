import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Product } from 'src/app/core/models/Product';
import { ProductService } from 'src/app/core/services/ProductService';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { ProductComponent } from 'src/app/shared/dialogs/product/product.component';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';

@Component({
  selector: 'app-main-table',
  templateUrl: './main-table.component.html',
  styleUrls: ['./main-table.component.css'],
})

export class MainTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'price', 'Edit', 'Delete'];
  productList: MatTableDataSource<Product> = new MatTableDataSource<Product>;
  pageSizes: number[] = [25, 50, 100];
  length: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(private productService: ProductService, private dialog: MatDialog, private snackBar: SnackBar) { }
  ngOnInit(): void {
    this.refresh();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.productList.filter = filterValue.trim().toLowerCase();

    if (this.productList.paginator) {
      this.productList.paginator.firstPage();
    }
  }

  refresh() {
    this.productService.GetList().subscribe({
      next: data => {
        this.productList = new MatTableDataSource(data);
        this.productList.paginator = this.paginator;
        this.productList.sort = this.sort;
        this.length = this.productList._filterData.length;
        if (length > 100)
          this.pageSizes = [...this.pageSizes, length]
        console.log(this.productList);
      },
      error: error => {
        console.log(error);
      },
      complete: () => { }
    });
  }

  insert = () => this.openDialog("Insert Product");

  edit = (obj: Product) => this.openDialog("Edit Product", obj);

  remove = (id: number) => {
    this.productService.RemoveById(id).subscribe({
      next: () => {
        this.snackBar.open("Produto removido com sucesso!", false);

        this.refresh();
      },
      error: (err) => {
        this.snackBar.open("Erro ao remover o produto", true);
      }
    });
  };

  openDialog(title: string, object?: Product) {
    this.dialog.open(ProductComponent, {
      disableClose: true,
      data: {
        title: title,
        object: object
      }
    });
  }
}
