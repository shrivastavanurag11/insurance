import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { RegistrationComponent } from './registration/registration.component';
import { AdminComponent } from './admin/admin.component';
import { CustomerComponent } from './customer/customer.component';
import { adminGuard } from './models';
import { BeneficaryFormComponent } from './beneficary-form/beneficary-form.component';
import { ClaimComponent } from './claim/claim.component';
import { MyPoliciesComponent } from './my-policies/my-policies.component';
import { UpdateUserComponent } from './update-user/update-user.component';
import { AdminUserComponent } from './admin-user/admin-user.component';
import { AdminSalesComponent } from './admin-sales/admin-sales.component';
import { NewPolicyComponent } from './new-policy/new-policy.component';
import { BarchartComponent } from './barchart/barchart.component';

export const routes: Routes = [
    {path:'login', component:LoginComponent},
    {path:'',redirectTo:'/login',pathMatch:'full'},
    {path:'login/registration' , component:RegistrationComponent},
    {path:'admin',component:AdminComponent,canActivate:[adminGuard]},
    {path:'customer', component:CustomerComponent},
    {path:'buyPolicy/:id', component:BeneficaryFormComponent},
    {path:'claim', component:ClaimComponent},
    {path:'myPolicies',component:MyPoliciesComponent},
    {path:'update-user', component:UpdateUserComponent},
    {path:'admin_user', component:AdminUserComponent},
    {path:'admin_sales',component:AdminSalesComponent},
    {path:'newPolicy', component:NewPolicyComponent},
    {path:'bar', component:BarchartComponent}
];


