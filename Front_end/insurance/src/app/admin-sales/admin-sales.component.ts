import { Component } from '@angular/core';
import { salesAnalysis } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin-sales',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-sales.component.html',
  styleUrl: './admin-sales.component.css'
})
export class AdminSalesComponent 
{
  sales:salesAnalysis[] = [];
  error:string = '';

  filtereddata: salesAnalysis[] = [];
  searchTerm: string = '';

  filterPolicyID: string = '';
  filterPolicyName: string = '';
  filterUserName: string = '';
  filterAmount: string = '';
  filterPolicyType: string = '';
  filterDate:string = '';

  constructor(private client:HttpCommunicator)
  {
    var res = this.client.salesData();
    res.subscribe({
      next:n => {this.sales = [...n.body!];this.filtereddata = this.sales;},
      error:e => {this.error = e.message;}
    });

    
  }

  ngOnInit(){
    
  }

  filterPolicies(): void {
    this.filtereddata = this.sales.filter(policy =>
      policy.policyName?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      policy.userName?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      policy.policyType?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
      policy.amount?.toString().includes(this.searchTerm.toLowerCase()) ||
      policy.soldOn?.toString().includes(this.searchTerm.toLowerCase())
      
    );
  }

  // filterPolicies(): void {
  //   const searchTermLower = this.searchTerm.toLowerCase();
  //   this.filtereddata = this.sales.filter(policy => {
  //     const matchesPolicyName = policy.policyName?.toLowerCase().includes(searchTermLower);
  //     const matchesUserName = policy.userName?.toLowerCase().includes(searchTermLower);
  //     const matchesPolicyType = policy.policyType?.toLowerCase().includes(searchTermLower);
  //     const matchesDate = policy.soldOn.toLocaleDateString().includes(searchTermLower);
  //     const matchesAmount = policy.amount?.toString().includes(searchTermLower);
  
  //     return matchesPolicyName || matchesUserName || matchesPolicyType || matchesDate || matchesAmount;
  //   });
}

  



