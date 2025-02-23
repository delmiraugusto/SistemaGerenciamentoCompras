import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SnackBar } from 'src/app/shared/components/snack-bar/snack-bar.component';
import { SpinnerService } from 'src/app/shared/components/spinner/spinner.service';
import { UserLogin } from 'src/app/core/models/User';
import { UserService } from 'src/app/core/services/UserService';
import { jwtDecode } from 'jwt-decode';


@Component({
  selector: 'login-page',
  templateUrl: './login-page.html',
  styleUrls: ['./login-page.css']
})
export class LoginPage {
  user: UserLogin = new UserLogin();

  constructor(
    private userService: UserService,
    private router: Router,
    private snackBar: SnackBar,
    private spiner: SpinnerService) { }

  ngOnInit(): void { }

  CheckBlank() {
    if (!this.user.email) {
      this.snackBar.open("User Cannot Be Blank", true);
      return true;
    }

    if (!this.user.password) {
      this.snackBar.open("Password Cannot Be Blank", true);
      return true;
    }
    return false;
  }

  Login() {
    if (!this.CheckBlank()) {
      this.spiner.show();
      this.userService.Login(this.user).subscribe({
        next: () => {
          this.snackBar.open("Successful login!", false);

          const token = sessionStorage.getItem('token');
          if (token) {
            const decodedToken: any = jwtDecode(token);
            const roleID = decodedToken?.roleID;

            if (roleID === '1') {
              this.router.navigate(['/menuAdmin']);
            } else if (roleID === '2') {
              this.router.navigate(['/listPurchaseByUser']);
            }
          } else {
            this.router.navigate(['']);
          }
        },
        error: error => {
          this.spiner.hide();
          this.snackBar.open("Invalid email and/or password", true);
        },
        complete: () => this.spiner.hide()
      });
    }
  }
}