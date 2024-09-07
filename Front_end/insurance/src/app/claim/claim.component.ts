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
  sortDirection!: boolean;
  sortColumn!: string;

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


  sortData(column: string) {
    // Implement your sorting logic here
    // For example, you can sort the `myclaims` array based on the column
    this.myclaims.sort((a, b) => {
      if (a < b) {
        return -1;
      } else if (a> b) {
        return 1;
      } else {
        return 0;
      }
    });
  }
  
  testClick() {
    alert ("asd");
  }
  
  
}
