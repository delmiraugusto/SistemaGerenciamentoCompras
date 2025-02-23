import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/core/services/UserService';
import { Location } from '@angular/common';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  user: string | null = "";
  accessLevel: string | null = "";
  constructor(private userService: UserService, private router: Router, private location: Location) { }

  ngOnInit(): void {
    this.user = this.userService.getCurrentUser();
    this.accessLevel = this.userService.getCurrentAccessLevel();
  }

  Logout() {
    this.userService.Logout();
    this.router.navigate(['']);
  }


  goBack() {
    this.location.back();
  }


  AdminButton = () => this.router.navigate(['/adminbutton']);
  Button = () => this.router.navigate(['/button']);
  DropdownbuttonFoo = () => this.router.navigate(['dropdownbutton/foo']);
  DropdownbuttonBar = () => this.router.navigate(['dropdownbutton/bar']);

}
