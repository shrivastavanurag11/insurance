import { Component } from '@angular/core';
import { claims } from '../models';
import { HttpCommunicator } from '../HttpCommunication';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-claim',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './claim.component.html',
  styleUrl: './claim.component.css'
})
export class ClaimComponent 
{
  myclaims!:claims[];
  message:string='';

  constructor(private client:HttpCommunicator)
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

  editClaim(inp:claims): void {
    // Logic for editing the claim
    // This can be redirecting to an edit form or enabling fields for editing
  }

  
}
