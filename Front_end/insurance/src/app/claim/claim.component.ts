import { Component } from '@angular/core';
import { claims, GroupClaimDetail, moredetail } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { response } from 'express';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-claim',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './claim.component.html',
  styleUrl: './claim.component.css'
})
export class ClaimComponent 
{
  myclaims!:GroupClaimDetail[];
  message:string='';
 // moreDetailed?:moredetail[];
  

  constructor(private client:HttpCommunicator , private route:Router)
  {
    var response= this.client.claims();
    response.subscribe({
      error: e => {
        this.message = e.message;},
      next: n => 
      {
        this.myclaims = [...n.body!];
      }
    });
  }

  showClaimForm(inp:GroupClaimDetail) 
  {
    inp.show = !inp.show;
  }


  submitClaim(inp:GroupClaimDetail) 
  {
    if(inp.remainingAmount < inp.newClaim){  alert("Claim Should not Exceed Remaining Amount!");}
    else{
      let temp:number = inp.remainingAmount - inp.newClaim;
      var ersponse = this.client.newClaim(inp.purchaseId, inp.newClaim , temp);
      ersponse.subscribe({
        next:n => {
          this.message = <string>n.body;  this.showAlert(this.message);
          this.route.navigate(['claim'])
          
        },
        error:e => {
          this.message = e.message;  this.showAlert(this.message);
        }
      });

      
    }
  }


  showMoreDetails(inp:GroupClaimDetail)
  {
    inp.showMore = !inp.showMore;
    var ersponse = this.client.claimDetails(inp.purchaseId);
      ersponse.subscribe({
        next:n => {
          inp.moredetailed = [...n.body!];
          this.route.navigate(['claim']);
        },
        error:e => {
          this.message = e.message;  this.showAlert(this.message);
        }
      });
  }

  showAlert(message?: string) {
    Swal.fire({
        title: 'Alert',
        text: message,
        icon: 'info',
        confirmButtonText: 'OK'
    });
    this.route.navigate(['customer'])
}

}
