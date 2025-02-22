import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { User } from 'src/app/core/models/User'; // Importe a classe User
import { UserService } from 'src/app/core/services/UserService';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { UserComponent } from '../../dialogs/user/user-component';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';
import { HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css'],
})
export class UserTableComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'email', 'roleID', 'Edit', 'Delete'];
  userList: MatTableDataSource<User> = new MatTableDataSource<User>();
  pageSizes: number[] = [25, 50, 100];
  length: number = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private userService: UserService, private dialog: MatDialog, private snackBar: SnackBar) { }

  ngOnInit(): void {
    this.refresh();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.userList.filter = filterValue.trim().toLowerCase();

    if (this.userList.paginator) {
      this.userList.paginator.firstPage();
    }
  }

  refresh() {
    this.userService.GetList().subscribe({
      next: data => {
        this.userList = new MatTableDataSource(data);
        this.userList.paginator = this.paginator;
        this.userList.sort = this.sort;
        this.length = this.userList._filterData.length;
        if (this.length > 100) {
          this.pageSizes = [...this.pageSizes, this.length];
        }
      },
      error: error => {
        console.log(error);
      }
    });
  }

  insert = () => this.openDialog('Insert User');

  edit = (obj: User) => this.openDialog('Edit User', obj);

  remove = (id: number) => {
    this.userService.RemoveById(id).subscribe({
      next: () => {
        this.snackBar.open("User remove with sucess!", false);
        this.refresh();
      },
      error: (err) => {
        if (err.status == HttpStatusCode.Conflict) {
          return this.snackBar.open("It was not possible to remove the user because there are registered purchases", true)
        }
        return this.snackBar.open("Error to remove user", true);
      }
    });
  };

  openDialog(title: string, object?: User) {
    this.dialog.open(UserComponent, {
      disableClose: true,
      data: {
        title: title,
        object: object
      }
    });
  }
}
