import { NgFor } from '@angular/common';
import { Component, numberAttribute } from '@angular/core';
import { Policy } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { Router, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [NgFor, RouterOutlet, FormsModule],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
  policies!:Policy[];
  filtered!:Policy[];
  error:string='';
  searchQuery: string = '';

  constructor(private client:HttpCommunicator , private router:Router)
  {
    var response = client.customerHome();
    response.subscribe({
      error:e => {this.error = "Something is Wrong..."},
      next:n => {
        for(let i= 0 ; i < <number>n.body?.length ; i++)
        {
          this.filtered=[...n.body!];
          this.policies = this.filtered;
        }
      }
    });
  }
  
  nextPage():void
  {
    var response = this.client.nextCustomerPage();
    response.subscribe({
      error:e => {this.error = "Something is Wrong..."},
      next:n => {

          this.policies=[...n.body!];

      }
    });
  }

  filteredPolicies(): void {
    if (!this.searchQuery) {
    this.policies = this.filtered;
    }
     this.policies =  this.policies.filter(policy =>
      policy.policyName.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      policy.policyType.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
      policy.policyDescription.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  calculateMonthlyAmount(insuranceAmount: number, policyValidity: number): string {
    return (insuranceAmount / (policyValidity * 12)).toFixed(2);
}

buyPolicy(inp:number):void{
  //let x:string=inp.policyID.toString();
  //policyID:String = inp.policyID.toString(); // Convert to string
  this.router.navigate(['buyPolicy', inp]);
}


logout():void
{
  sessionStorage.removeItem('jwttoken');
  this.router.navigate(['login']);
}

prevPage(){}

}
