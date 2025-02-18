import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/UserService';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {

    constructor(private userService: UserService, private router: Router, private snackBar: SnackBar) { }

    canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        const roleID = this.userService.getCurrentAccessLevel();

        if (!roleID) {
            this.snackBar.open("You must be logged in to access this page", true);
            this.router.navigate(['/']);
            return false;
        }

        const allowedRoles = next.data['roleID'] as Array<string>;

        if (allowedRoles.includes(roleID)) {
            return true;
        } else {
            this.snackBar.open("Unauthorized", true);
            this.router.navigate(['/']);
            return false;
        }
    }
}
