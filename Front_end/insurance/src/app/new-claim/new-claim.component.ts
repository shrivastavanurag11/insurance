import { Component } from '@angular/core';
import { HttpCommunicator } from '../HttpCommunication';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-new-claim',
  standalone: true,
  imports: [],
  templateUrl: './new-claim.component.html',
  styleUrl: './new-claim.component.css'
})
export class NewClaimComponent 
{
  purchaseId!:number;
  remainingAmount!:number;
  message:string='';

  
  constructor(private act:ActivatedRoute , private client:HttpCommunicator , private route:Router){ }

    ngOnInit()
    {this.act.params.subscribe(x => {this.purchaseId = x['id'],
                                      this.remainingAmount = x['remainingAmount']
    });}
  
  submitClaim()
  {


  }
}
