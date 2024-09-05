import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RegistrationComponent } from './registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { CustomerComponent } from './customer/customer.component';
import { adminGuard } from './models';

export const routes: Routes = [
    {path:'login', component:LoginComponent},
    {path:'',redirectTo:'/login',pathMatch:'full'},
    {path:'login/registration' , component:RegistrationComponent},
    {path:'admin',component:AdminComponent,canActivate:[adminGuard]},
    {path:'customer', component:CustomerComponent}
];


