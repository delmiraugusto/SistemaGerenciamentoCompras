import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User, UserInsert, UserUpdate } from 'src/app/core/models/User';
import { UserService } from 'src/app/core/services/UserService';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';

@Component({
    selector: 'app-user',
    templateUrl: './user.component.html',
    styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
    title: string = "";
    user: User = new User();
    roles: { id: number, name: string }[] = [
        { id: 1, name: 'Admin' },
        { id: 2, name: 'Client' },
    ];

    hide = true;

    constructor(
        @Inject(MAT_DIALOG_DATA) public data: any,
        private userService: UserService,
        private snackBar: SnackBar
    ) {
        this.title = this.data.title;
        this.user = this.data.object ?? new User();

        this.user.password = '';
    }



    ngOnInit(): void { }

    Insert = () => {
        if (this.title.toLowerCase().includes("insert")) {
            const userInsert: UserInsert = new UserInsert();
            userInsert.email = this.user.email;
            userInsert.name = this.user.name;
            userInsert.roleID = this.user.roleID;
            userInsert.password = this.user.password;

            this.userService.Insert(userInsert).subscribe({
                next: () => {
                    this.snackBar.open("User entered com sucesso!", false);
                },
                error: (err) => {
                    this.snackBar.open("Error when entering user", true);
                }
            });
        } else {
            const userUpdate: UserUpdate = new UserUpdate();
            userUpdate.email = this.user.email;
            userUpdate.name = this.user.name;
            userUpdate.password = this.user.password;
            userUpdate.roleID = this.user.roleID;

            console.log('Dados para atualização:', userUpdate);

            this.userService.UpdateById(this.user.id, userUpdate).subscribe({
                next: () => {
                    this.snackBar.open("Usuário atualizado com sucesso!", false);
                },
                error: (err) => {
                    this.snackBar.open("Erro ao atualizar o usuário", true);
                }
            });
        }
    }

}
