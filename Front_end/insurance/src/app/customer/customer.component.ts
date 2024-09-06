import { NgFor } from '@angular/common';
import { Component, numberAttribute } from '@angular/core';
import { Policy } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-customer',
  standalone: true,
  imports: [NgFor, RouterOutlet],
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.css'
})
export class CustomerComponent {
  policies!:Policy[];
  error:string='';

  constructor(private client:HttpCommunicator , private router:Router)
  {
    var response = client.customerHome();
    response.subscribe({
      error:e => {this.error = "Something is Wrong..."},
      next:n => {
        for(let i= 0 ; i < <number>n.body?.length ; i++)
        {
          this.policies=[...n.body!];
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
        for(let i= 0 ; i < <number>n.body?.length ; i++)
        {
          this.policies=[...n.body!];
        }
      }
    });
  }

  calculateMonthlyAmount(insuranceAmount: number, policyValidity: number): string {
    return (insuranceAmount / (policyValidity * 12)).toFixed(2);
}

buyPolicy(inp:number):void{
  //let x:string=inp.policyID.toString();
  //policyID:String = inp.policyID.toString(); // Convert to string
  this.router.navigate(['buyPolicy', inp]);
}

}
