import { Component } from '@angular/core';
import { filterOption, salesAnalysis } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { filter } from 'rxjs';

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

  filters:filterOption = new filterOption();


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

  applyFilters()
  {
    if((this.filters.customerName == "" || undefined) && this.filters.endDate == undefined && this.filters.policyId == undefined && this.filters.policyName == undefined && this.filters.policyType == undefined && this.filters.startDate == undefined && ( this.filters.userName == undefined || ''))
    {this.filterPolicies();}
    else{
    var response = this.client.filterData(this.filters);
    response.subscribe({
      error:e => {this.error = e.message},
      next: n => {this.filtereddata = [...n.body!]}
    });}

  }
}



