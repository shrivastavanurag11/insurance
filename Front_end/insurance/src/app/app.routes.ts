import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RegistrationComponent } from './registration/registration.component';

export const routes: Routes = [
    {path:"login", component:LoginComponent},
    {path:'',redirectTo:'/login',pathMatch:'full'},
    {path:"login/registration" , component:RegistrationComponent}
];

