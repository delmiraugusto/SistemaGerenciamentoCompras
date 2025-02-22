import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPage } from './shared/pages/login-page/login-page';
import { ButtonPage } from './shared/pages/button-page/button-page';
import { FooPage } from './shared/pages/foo-page/foo-page';
import { BarPage } from './shared/pages/bar-page/bar-page';
import { AuthGuard } from './core/guard/auth-guard';
import { PurchasePage } from './shared/pages/purchase-page/purchase-page';
import { UserPage } from './shared/pages/user-page/user-page';

const routes: Routes = [
  { path: '', component: LoginPage },
  { path: 'listProduct', component: ButtonPage, canActivate: [AuthGuard], data: { roleID: ['1'] } },
  { path: 'listPurchase', component: PurchasePage, canActivate: [AuthGuard], data: { roleID: ['1', '2'] } },
  { path: 'listUser', component: UserPage, canActivate: [AuthGuard], data: { roleID: ['1', '2'] } },
  { path: 'dropdownbutton/foo', component: FooPage, canActivate: [AuthGuard], data: { roleID: ['1', '2'] } },
  { path: 'dropdownbutton/bar', component: BarPage, canActivate: [AuthGuard], data: { roles: ['user'] } },
  //{ path: 'homeAdmin', component: HomeAdminPage, canActivate: [AuthGuard], data: { roles: ['1'] } }, // Admin (role 1)
  //{ path: 'homeCliente', component: HomeClientePage, canActivate: [AuthGuard], data: { roles: ['2'] } }, // Cliente (role 2)
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
